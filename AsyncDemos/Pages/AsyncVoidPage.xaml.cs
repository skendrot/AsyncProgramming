using AsyncDemos.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace AsyncDemos.Pages
{
    /// <summary>
    /// Interaction logic for AsyncVoidPage.xaml
    /// </summary>
    public partial class AsyncVoidPage : BasePage
    {
        private HttpClient _httpClient = new HttpClient();
        private Account _account;
        private ImageSource _bitmap;

        public AsyncVoidPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            // Incorrect. The _bitmap hasn't been created yet because the HttpClient is still getting the file
            PageImage.Source = _bitmap;

            // Correct. Waits for the HttpClient to finish before setting
            // PageImage.Source = await GetImageAsync();;
        }

        // When uncommenting this method, also comment the LoadState method.
        //private async Task<BitmapImage> GetImageAsync()
        //{
        //    var bitmap = new BitmapImage();

        //    HttpClient client = new HttpClient();
        //    var response = await client.GetAsync("https://i.ytimg.com/vi/lXbHPsOLU64/maxresdefault.jpg");
        //    var stream = await response.Content.ReadAsStreamAsync();
        //    bitmap.BeginInit();
        //    bitmap.StreamSource = stream;
        //    bitmap.EndInit();
        //    return bitmap;
        //}

        protected override async void LoadState()
        {
            base.LoadState();
            var bitmap = new BitmapImage();

            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://i.ytimg.com/vi/lXbHPsOLU64/maxresdefault.jpg");
            var stream = await response.Content.ReadAsStreamAsync();
            bitmap.BeginInit();
            bitmap.StreamSource = stream;
            bitmap.EndInit();
            _bitmap = bitmap;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginAsyncVoidButton.IsEnabled = false;
            Login(UserTextBox.Text, PasswordTextBox.Password);
            //await Task.Delay(2000);
            UsernameTextBox.Text = _account.Username;
        }

        private async void Login(string username, string password)
        {
            using (var response = await _httpClient.GetAsync("http://live.com"))
            {
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    _account = new Account { Username = username };
                }
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoginAsyncButton.IsEnabled = false;
            var account = await LoginAsync(UserTextBox.Text, PasswordTextBox.Password);
            UsernameTextBox.Text = account.Username;
        }

        private async Task<Account> LoginAsync(string username, string password)
        {
            using (var response = await _httpClient.GetAsync("http://live.com"))
            {
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    return new Account { Username = username };
                }
            }
        }
    }
}