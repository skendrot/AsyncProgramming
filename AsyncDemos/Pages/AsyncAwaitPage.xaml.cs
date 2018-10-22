using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AsyncDemos.Pages
{
    /// <summary>
    /// Interaction logic for AsyncAwaitPage.xaml
    /// </summary>
    public partial class AsyncAwaitPage : Page
    {
        public AsyncAwaitPage()
        {
            InitializeComponent();
        }

        private async void Button1_Click(object sender, RoutedEventArgs e)
        {
            var b = (Button)sender;
            b.IsEnabled = false;
            await MagicKeywordAsync();
            b.IsEnabled = true;
        }

        private async Task MagicKeywordAsync()
        {
            for (int i = 0; i < 100; i++)
            {
                await NestedMagicKeywordAsync(i);
            }
        }

        private async Task NestedMagicKeywordAsync(int index)
        {
            LongRunningMethod(index);
        }

        private void LongRunningMethod(int length)
        {
            Thread.Sleep(length);
            Console.WriteLine(length);
        }

        private async void Button2_Click(object sender, RoutedEventArgs e)
        {
            var b = (Button)sender;
            b.IsEnabled = false;
            await MagicKeywordAsync2();
            b.IsEnabled = true;
        }

        private async Task MagicKeywordAsync2()
        {
            for (int i = 0; i < 100; i++)
            {
                await NestedMagicKeywordAsync2(i);
            }
        }

        private async Task NestedMagicKeywordAsync2(int index)
        {
            await LongRunningMethodAsync(index);
        }

        private Task LongRunningMethodAsync(int length)
        {
            return Task.Delay(length);
        }
    }
}
