﻿using FastReport.Data;
using FastReport.Utils;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace Sinema
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            RegisteredObjects.AddConnection(typeof(SqlCeDataConnection));

            AppDomain.CurrentDomain.AssemblyLoad += CurrentDomain_AssemblyLoad;
            EventManager.RegisterClassHandler(typeof(TextBox), UIElement.KeyDownEvent, new KeyEventHandler(KeyDown));
            EventManager.RegisterClassHandler(typeof(ComboBox), UIElement.KeyDownEvent, new KeyEventHandler(KeyDown));
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            base.OnStartup(e);
        }

        [System.Diagnostics.Conditional("RELEASE")]
        private static void HataMesajı(System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Sinema", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }

        private static void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && (sender as TextBox)?.AcceptsReturn == false)
            {
                MoveToNextUiElement(e);
                return;
            }

            if (e.Key == Key.Enter && sender is ComboBox) MoveToNextUiElement(e);
        }

        private static void MoveToNextUiElement(RoutedEventArgs e)
        {
            const FocusNavigationDirection focusDirection = FocusNavigationDirection.Next;
            var request = new TraversalRequest(focusDirection);
            if (!(Keyboard.FocusedElement is UIElement elementWithFocus)) return;

            if (elementWithFocus.MoveFocus(request)) e.Handled = true;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e) => HataMesajı(e);

        private void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            var t = args.LoadedAssembly.GetLoadedModules();

            if (t[0].Name.Substring(t[0].Name.Length - 3, 3) == "exe") Shutdown();
        }
    }
}