﻿using ICSharpCode.AvalonEdit;
using System;
using System.Windows;
using System.Windows.Interactivity;

namespace CoderEditor
{
    public sealed class AvalonEditBehaviour : Behavior<TextEditor>
    {
        
        public static readonly DependencyProperty GiveMeTheTextProperty =
        DependencyProperty.Register("GiveMeTheText", typeof(string), typeof(AvalonEditBehaviour),
        new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, PropertyChangedCallback));

        public string GiveMeTheText
        {
            get { return (string)GetValue(GiveMeTheTextProperty); }
            set { SetValue(GiveMeTheTextProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            if (AssociatedObject != null)
                AssociatedObject.TextChanged += AssociatedObjectOnTextChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (AssociatedObject != null)
                AssociatedObject.TextChanged -= AssociatedObjectOnTextChanged;
        }

        private void AssociatedObjectOnTextChanged(object sender, EventArgs eventArgs)
        {
            //var textEditor = sender as TextEditor;
            //if (textEditor != null)
            //{
            //    if (textEditor.Document != null)
            //        GiveMeTheText = textEditor.Document.Text;
            //}
        }

        private static void PropertyChangedCallback(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var behavior = dependencyObject as AvalonEditBehaviour;
            if (behavior.AssociatedObject != null)
            {
                var editor = behavior.AssociatedObject as TextEditor;
                if (editor.Document != null)
                {
                    var caretOffset = editor.CaretOffset;

                    string newVal = (string)dependencyPropertyChangedEventArgs.NewValue;
                    string oldVal = (string)dependencyPropertyChangedEventArgs.OldValue;

                    if (newVal == null)
                    {
                        editor.Document.Text = "";
                    }
                    else
                    {
                        editor.Document.Text = dependencyPropertyChangedEventArgs.NewValue.ToString();
                    } 

                    if (caretOffset > editor.Text.Length) editor.Select(editor.Text.Length, 0);
                    else editor.CaretOffset = caretOffset;
                }
            }
        }

    }
}
