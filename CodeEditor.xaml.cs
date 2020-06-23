using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace CoderEditor
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class CodeEditor : UserControl, INotifyPropertyChanged
    {
        public CodeEditor()
        {
            InitializeComponent();
            //DataContext = this;
        }

        string[] separator = new string[] { "//Main" };

        public static DependencyProperty CodeTextProperty1;
        public static DependencyProperty CodeTextProperty2;
        public static DependencyProperty fontSizeProperty;


        static CodeEditor()
        {
            var metadata = new FrameworkPropertyMetadata(new PropertyChangedCallback(OnCodeTextChanged1));
            CodeTextProperty1 = DependencyProperty.Register("CodeText1", typeof(string), typeof(CodeEditor), metadata);
            metadata = new FrameworkPropertyMetadata(new PropertyChangedCallback(OnCodeTextChanged2));
            CodeTextProperty2 = DependencyProperty.Register("CodeText2", typeof(string), typeof(CodeEditor), metadata);
            metadata = new FrameworkPropertyMetadata(new PropertyChangedCallback(OnfontSizeChanged));
            fontSizeProperty = DependencyProperty.Register("fontSize", typeof(int), typeof(CodeEditor), metadata);
        }


        private static void OnfontSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CodeEditor userControl = (CodeEditor)d;
            int newVal = (int)e.NewValue;
            if (newVal == 0) return;
            if (newVal == (int)e.OldValue) return;
            userControl.textAreaGlobal.FontSize = newVal;
            userControl.textAreaMain.FontSize = newVal;
        }

        private static void OnCodeTextChanged1(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CodeEditor userControl = (CodeEditor)d;
            string newVal = (string)e.NewValue;
            if (newVal == (string)e.OldValue || newVal==null) return;
            userControl.Coder1.GiveMeTheText = newVal;
            if (HasChanges == false) HasChanges = true;
        }

        private static void OnCodeTextChanged2(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CodeEditor userControl = (CodeEditor)d;
            string newVal = (string)e.NewValue;
            if (newVal == (string)e.OldValue || newVal == null) return;
            userControl.Coder2.GiveMeTheText = newVal;
            if (HasChanges == false) HasChanges = true;
        }


        public string CodeText1
        {
            get { return (string)GetValue(CodeTextProperty1); }
            set
            {
                SetValue(CodeTextProperty1, value); OnPropertyChanged();
            }
        }
        public string CodeText2
        {
            get { return (string)GetValue(CodeTextProperty2); }
            set
            {
                SetValue(CodeTextProperty2, value); OnPropertyChanged();
            }
        }

        public int fontSize
        {
            get { return (int)GetValue(fontSizeProperty); }
            set
            {
                SetValue(fontSizeProperty, value); OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private static bool HasChanges;
        private void increaseFontClick(object sender, RoutedEventArgs e)
        {
            if (decreaseFontButton.IsEnabled == false) decreaseFontButton.IsEnabled = true;
            if (textAreaGlobal.FontSize < 36)
            {
                textAreaGlobal.FontSize += 2;
                textAreaMain.FontSize += 2;
            }
            else increaseFontButton.IsEnabled = false;
        }

        void decreaseFontClick(object sender, RoutedEventArgs e)
        {
            if (increaseFontButton.IsEnabled == false) increaseFontButton.IsEnabled = true;
            if (textAreaGlobal.FontSize > 8)
            {
                textAreaGlobal.FontSize -= 2;
                textAreaMain.FontSize -= 2;
            }
            else decreaseFontButton.IsEnabled = false;
        }

        bool open()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Text files (*.txt)|*.txt|All Files (*.*)|*.*";
            dlg.CheckFileExists = true;
            if (dlg.ShowDialog() ?? false)
            {
                var fileName = dlg.FileName;
                var sr = new StreamReader(fileName);
                var textForOpen = sr.ReadToEnd().Split(separator, StringSplitOptions.None);
                textAreaGlobal.Text = textForOpen[0].Trim();
                textAreaMain.Text = textForOpen.Length == 2 ? textForOpen[1].Trim() : "";
                return true;
            }
            return false;
        }

        bool save(string file = "")
        {
            if (file == "" || file == null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Text files (*.txt)|*.txt|All Files (*.*)|*.*";
                if (sfd.ShowDialog() ?? false)
                {
                    using (FileStream fs = File.Create(sfd.FileName))
                    {
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            var textForSaving = $"{textAreaGlobal.Text}\n{separator[0]}\n{textAreaMain.Text}";
                            sw.WriteLine(textForSaving);
                        }
                    }
                    return true;
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        void newFileClick(object sender, RoutedEventArgs e)
        {
            if (IsNeedSaveFile())
            {
                var result = MessageBox.Show("Do you want to save changes?", "New", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (save())
                    {
                        textAreaGlobal.Text=string.Empty;
                        textAreaMain.Text = string.Empty;
                    }
                }
                else if (result == MessageBoxResult.No)
                {
                    textAreaGlobal.Text = string.Empty;
                    textAreaMain.Text = string.Empty;
                }
                else
                {
                    return;
                }
            }
            else
            {
                textAreaGlobal.Text = string.Empty;
                textAreaMain.Text = string.Empty;
            }
        }

        void openFileClick(object sender, RoutedEventArgs e)
        {
            if (IsNeedSaveFile())
            {
                var result = MessageBox.Show("Do you want to save changes?", "Open", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (save())
                    {
                        open();
                    }
                }
                else if (result == MessageBoxResult.No)
                {
                    open();
                }
            }
            else
            {
                open();
            }
        }

        void saveAsFileClick(object sender, RoutedEventArgs e)
        {
            if (IsNeedSaveFile())
            {
                save();
                HasChanges = false;
            }
        }


        bool IsNeedSaveFile()
        {
            if (
                ((textAreaGlobal.CanUndo && textAreaGlobal.Text != "") ||
                (textAreaMain.CanUndo && textAreaMain.Text != ""))
                )
            {
                return true;
            }
            else return false;
        }

        public void InsertTextToCaretPosition(string text)
        {
            var tab = tabs.SelectedItem as TabItem;
            if (tab.Name == "tab1")
            {
                textAreaGlobal.TextArea.Document.Insert(textAreaGlobal.CaretOffset, text);
            }
            else if (tab.Name == "tab2")
            {
                textAreaMain.TextArea.Document.Insert(textAreaMain.CaretOffset, text);
            }
        }

    }
}
