using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using ICSharpCode.AvalonEdit.Folding;

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

            textAreaGlobal.TextArea.IndentationStrategy = 
                new ICSharpCode.AvalonEdit.Indentation.CSharp.CSharpIndentationStrategy(textAreaGlobal.Options);
            foldingStrategy = new BraceFoldingStrategy();
            if (foldingStrategy != null)
            {
                if (foldingManager == null)
                    foldingManager = FoldingManager.Install(textAreaGlobal.TextArea);
                UpdateFoldings();
            }

            DispatcherTimer foldingUpdateTimer = new DispatcherTimer();
            foldingUpdateTimer.Interval = TimeSpan.FromSeconds(2);
            foldingUpdateTimer.Tick += delegate { UpdateFoldings(); };
            foldingUpdateTimer.Start();
        }

        FoldingManager foldingManager;
        object foldingStrategy;

        void UpdateFoldings()
        {
            if (foldingStrategy is BraceFoldingStrategy)
            {
                ((BraceFoldingStrategy)foldingStrategy).UpdateFoldings(foldingManager, textAreaGlobal.Document);
            }

        }

        string[] separator = new string[] { "//Main" };
        string fileName;

        
        public static DependencyProperty fontSizeProperty;

        public string Code1 => textAreaGlobal.Text;
        public string Code2 => textAreaMain.Text;

        static CodeEditor()
        {
            var metadata = new FrameworkPropertyMetadata(new PropertyChangedCallback(OnfontSizeChanged));
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

        //private static bool HasChanges;
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
                this.fileName = dlg.FileName;
                var sr = new StreamReader(fileName);
                var textForOpen = sr.ReadToEnd().Split(separator, StringSplitOptions.None);
                textAreaGlobal.Text = textForOpen[0].Trim();
                textAreaMain.Text = textForOpen.Length == 2 ? textForOpen[1].Trim() : "";
                return true;
            }
            return false;
        }

        bool save(string file)
        {
            if (string.IsNullOrEmpty(file))
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Text files (*.txt)|*.txt|All Files (*.*)|*.*";
                if (sfd.ShowDialog() == true)
                {
                    fileName = sfd.FileName;
                }
                else
                {
                    return false;
                }
            }
            using (FileStream fs = File.Create(fileName))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    var textForSaving = $"{textAreaGlobal.Text}\n{separator[0]}\n{textAreaMain.Text}";
                    sw.WriteLine(textForSaving);
                }
            }
            return true;
        }

        void newFileClick(object sender, RoutedEventArgs e)
        {
            if (IsNeedSaveFile())
            {
                var result = MessageBox.Show("Do you want to save changes?", "New", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (save(fileName))
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
                    if (save(fileName))
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

        void saveFileClick(object sender, RoutedEventArgs e)
        {
            if (IsNeedSaveFile())
            {
                save(fileName);
                //HasChanges = false;
            }
        }

        void saveAsFileClick(object sender, RoutedEventArgs e)
        {
            save(null);
            //HasChanges = false;
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
