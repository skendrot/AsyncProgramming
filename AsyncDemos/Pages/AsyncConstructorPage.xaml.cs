using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AsyncDemos.Pages
{
    /// <summary>
    /// Interaction logic for AsyncConstructorPage.xaml
    /// </summary>
    public partial class AsyncConstructorPage : Page
    {
        public AsyncConstructorPage()
        {
            InitializeComponent();
        }

        private void ContinueWithButton_Click(object sender, RoutedEventArgs e)
        {
            var settings = new Settings();
            settings.Loaded += OnSettingsLoaded;
        }

        private void OnSettingsLoaded(object sender, EventArgs e)
        {
            MessageBox.Show("Settings Loaded");
        }

        private async void LoadingTask_Click(object sender, RoutedEventArgs e)
        {
            var settings = new Settings2();
            await settings.LoadingTask;
            MessageBox.Show("Settings Loaded");
        }
    }

    internal class Settings
    {
        public Settings()
        {
            Task optionsTask = LoadOptionsAsync();

            optionsTask.ContinueWith(t =>
                {
                    OnLoaded();
                });
        }

        public event EventHandler Loaded;

        protected virtual void OnLoaded()
        {
            Loaded?.Invoke(this, EventArgs.Empty);
        }

        private Task LoadOptionsAsync()
        {
            return Task.Delay(1500);
        }
    }


    internal class Settings2
    {
        public Settings2()
        {
            LoadingTask = LoadOptionsAsync();
        }

        public Task LoadingTask { get; private set; }

        protected Task LoadOptionsAsync()
        {
            return Task.Delay(1500);
        }
    }
}
