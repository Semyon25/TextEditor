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

        public static DependencyProperty CodeTextProperty;
        public static DependencyProperty fontSizeProperty;


        static CodeEditor()
        {
            var metadata = new FrameworkPropertyMetadata(new PropertyChangedCallback(OnCodeTextChanged));
            CodeTextProperty = DependencyProperty.Register("CodeText", typeof(string), typeof(CodeEditor), metadata);
            metadata = new FrameworkPropertyMetadata(new PropertyChangedCallback(OnfontSizeChanged));
            fontSizeProperty = DependencyProperty.Register("fontSize", typeof(int), typeof(CodeEditor), metadata);
        }

        private static void OnfontSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CodeEditor userControl = (CodeEditor)d;
            int newVal = (int)e.NewValue;
            if (newVal == 0) return;
            if (newVal == (int)e.OldValue) return;
            userControl.textArea.FontSize = newVal;
        }

        private static void OnCodeTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CodeEditor userControl = (CodeEditor)d;
            string newVal = (string)e.NewValue;
            if (newVal == (string)e.OldValue || newVal==null) return;
            userControl.Coder.GiveMeTheText = newVal;
            if (HasChanges == false) HasChanges = true;
        }

        public string CodeText
        {
            get { return (string)GetValue(CodeTextProperty); }
            set
            {
                SetValue(CodeTextProperty, value); OnPropertyChanged();
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


        bool open()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Text files (*.txt)|*.txt|All Files (*.*)|*.*";
            dlg.CheckFileExists = true;
            if (dlg.ShowDialog() ?? false)
            {
                var fileName = dlg.FileName;
                var sr = new StreamReader(fileName, Encoding.Default);
                textArea.Text = sr.ReadToEnd();
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
                            sw.WriteLine(textArea.Text);
                        }
                    }
                    return true;
                }
                return false;
            }
            else
            {
                //using (FileStream fs = File.Create(file))
                //{
                //    using (StreamWriter sw = new StreamWriter(fs))
                //    {
                //        sw.WriteLine(textArea.Text);
                //    }
                //}
                return true;
            }
        }

        void newFileClick(object sender, RoutedEventArgs e)
        {
            if (textArea.CanUndo && HasChanges && textArea.Text!="")
            {
                var result = MessageBox.Show("Do you want to save changes?", "New", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (save())
                    {
                        textArea.Text=string.Empty;
                    }
                }
                else if (result == MessageBoxResult.No)
                {
                    textArea.Text = string.Empty;
                }
                else
                {
                    return;
                }
            }
            else
            {
                textArea.Text = string.Empty;
            }
            //fileName = "";
        }

        void openFileClick(object sender, RoutedEventArgs e)
        {
            if (textArea.CanUndo && HasChanges && textArea.Text!="")
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

        //void saveFileClick(object sender, RoutedEventArgs e)
        //{
        //    save(fileName);
        //    HasChanges = false;
        //}

        void saveAsFileClick(object sender, RoutedEventArgs e)
        {
            if (textArea.CanUndo && HasChanges)
            {
                save();
                HasChanges = false;
            }
        }


    }
}
