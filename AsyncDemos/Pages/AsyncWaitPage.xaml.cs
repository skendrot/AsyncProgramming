using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AsyncDemos.Pages
{
    /// <summary>
    /// Interaction logic for AsyncWaitPage.xaml
    /// </summary>
    public partial class AsyncWaitPage : Page
    {
        public AsyncWaitPage()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var task = LongMethodAsync();
            var result = await task;

            while(task.IsCompleted == false)
            {

            }
            
            // Update UI with results...
        }

        private async Task<int> LongMethodAsync()
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            Console.WriteLine("long method starting");
            await Task.Delay(500);
            sw.Stop();
            Console.WriteLine($"long method finished: {sw.ElapsedMilliseconds}");

            return 500;
        }
    }
}
