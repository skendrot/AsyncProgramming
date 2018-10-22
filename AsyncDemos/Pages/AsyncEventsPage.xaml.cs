using AsyncDemos.Views;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace AsyncDemos.Pages
{
    /// <summary>
    /// Interaction logic for AsyncEventsPage.xaml
    /// </summary>
    public partial class AsyncEventsPage : BasePage
    {
        TaskCompletionSource<bool> _source = new TaskCompletionSource<bool>();

        public AsyncEventsPage()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await _source.Task;

            MessageBox.Show("You clicked the button!");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _source.TrySetResult(true);
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string username = await GetUsernameAsync();
            UsernameTextBlock.Text = username;
        }

        private Task<string> GetUsernameAsync()
        {
            var source = new TaskCompletionSource<string>();
            var window = new Window { Width = 300, Height = 200 };
            var view = new UserEntryView();
            window.Content = view;
            
            RoutedEventHandler handler = null;
            handler = (s, a) =>
            {
                view.OkButton.Click -= handler;
                window.Close();
                source.TrySetResult(view.UsernameTextBox.Text);
            };
            view.OkButton.Click += handler;
            window.Show();
            return source.Task;
        }
    }
}
