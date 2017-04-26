﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

using SharpGraphEditor.Helpers;

namespace SharpGraphEditor.Behaviors
{
    class InputBindingTrigger : TriggerBase<FrameworkElement>, ICommand
    {
        public static readonly DependencyProperty InputBindingProperty =
            DependencyProperty.Register("InputBinding",
                                        typeof(MultiKeyBinding),
                                        typeof(InputBindingTrigger),
                                        new UIPropertyMetadata(null));

        public InputBinding InputBinding
        {
            get { return (InputBinding)GetValue(InputBindingProperty); }
            set { SetValue(InputBindingProperty, value); }
        }

        public event EventHandler CanExecuteChanged = delegate { };

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            InvokeActions(parameter);
        }

        protected override void OnAttached()
        {
            if (InputBinding != null)
            {
                InputBinding.Command = this;
                if (AssociatedObject.Focusable)
                {
                    AssociatedObject.InputBindings.Add(InputBinding);
                }
                else
                {
                    Window window = null;
                    AssociatedObject.Loaded += delegate
                    {
                        window = GetWindow(AssociatedObject);
                        if (!window.InputBindings.Contains(InputBinding))
                        {
                            window.InputBindings.Add(InputBinding);
                        }
                    };
                    AssociatedObject.Unloaded += delegate
                    {
                        window.InputBindings.Remove(InputBinding);
                    };
                }
            }
            base.OnAttached();

        }

        private Window GetWindow(FrameworkElement frameworkElement)
        {
            if (frameworkElement is Window)
                return frameworkElement as Window;

            var parent = frameworkElement.Parent as FrameworkElement;

            return GetWindow(parent);
        }
    }
}
