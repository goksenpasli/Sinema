using System;
using System.Windows;
using System.Windows.Controls;

namespace Sinema
{
    public sealed class ShowCapLock : ContentControl
    {
        public static readonly DependencyProperty ShowMessageProperty =
            DependencyProperty.Register("ShowMessage", typeof(bool), typeof(ShowCapLock), new PropertyMetadata(false));

        public bool ShowMessage
        {
            get => (bool)GetValue(ShowMessageProperty);
            set => SetValue(ShowMessageProperty, value);
        }

        static ShowCapLock() => DefaultStyleKeyProperty.OverrideMetadata(typeof(ShowCapLock),
                new FrameworkPropertyMetadata(typeof(ShowCapLock)));

        public ShowCapLock()
        {
            IsKeyboardFocusWithinChanged += (s, e) => RecomputeShowMessage();
            PreviewKeyDown += (s, e) => RecomputeShowMessage();
            PreviewKeyUp += (s, e) => RecomputeShowMessage();
        }

        private void RecomputeShowMessage() => ShowMessage = IsKeyboardFocusWithin && Console.CapsLock;
    }
}