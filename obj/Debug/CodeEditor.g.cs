﻿#pragma checksum "..\..\CodeEditor.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "36754E24BC1A76694F1DEB48CD18643E15522C61D5929477528BFAE87CD1FD22"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using CoderEditor;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Rendering;
using ICSharpCode.AvalonEdit.Search;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace CoderEditor {
    
    
    /// <summary>
    /// CodeEditor
    /// </summary>
    public partial class CodeEditor : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\CodeEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CoderEditor.CodeEditor UserControl0;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\CodeEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button increaseFontButton;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\CodeEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button decreaseFontButton;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\CodeEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl tabs;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\CodeEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem tab1;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\CodeEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ICSharpCode.AvalonEdit.TextEditor textAreaGlobal;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\CodeEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem tab2;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\CodeEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ICSharpCode.AvalonEdit.TextEditor textAreaMain;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CoderEditor;component/codeeditor.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\CodeEditor.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.UserControl0 = ((CoderEditor.CodeEditor)(target));
            return;
            case 2:
            
            #line 19 "..\..\CodeEditor.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.newFileClick);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 22 "..\..\CodeEditor.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.openFileClick);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 25 "..\..\CodeEditor.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.saveFileClick);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 28 "..\..\CodeEditor.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.saveAsFileClick);
            
            #line default
            #line hidden
            return;
            case 6:
            this.increaseFontButton = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\CodeEditor.xaml"
            this.increaseFontButton.Click += new System.Windows.RoutedEventHandler(this.increaseFontClick);
            
            #line default
            #line hidden
            return;
            case 7:
            this.decreaseFontButton = ((System.Windows.Controls.Button)(target));
            
            #line 55 "..\..\CodeEditor.xaml"
            this.decreaseFontButton.Click += new System.Windows.RoutedEventHandler(this.decreaseFontClick);
            
            #line default
            #line hidden
            return;
            case 8:
            this.tabs = ((System.Windows.Controls.TabControl)(target));
            return;
            case 9:
            this.tab1 = ((System.Windows.Controls.TabItem)(target));
            return;
            case 10:
            this.textAreaGlobal = ((ICSharpCode.AvalonEdit.TextEditor)(target));
            return;
            case 11:
            this.tab2 = ((System.Windows.Controls.TabItem)(target));
            return;
            case 12:
            this.textAreaMain = ((ICSharpCode.AvalonEdit.TextEditor)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

