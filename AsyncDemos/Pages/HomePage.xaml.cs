using AsyncDemos.Models;
using AsyncDemos.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AsyncDemos.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();

            SampleList.Items.Add(new SamplePage { Title = "async and await - Magic words?", Page = typeof(AsyncAwaitPage) });
            SampleList.Items.Add(new SamplePage { Title = "async void - When to use", Page = typeof(AsyncVoidPage) });
            SampleList.Items.Add(new SamplePage { Title = "Async events", Page = typeof(AsyncEventsPage) });
            SampleList.Items.Add(new SamplePage { Title = "Don't mix async and sync code", Page = typeof(AsyncWaitPage) });
            SampleList.Items.Add(new ConfigureAwaitSamplePage());
            SampleList.Items.Add(new WhenAllSamplePage());
            SampleList.Items.Add(new SamplePage { Title = "async in your constructor", Page = typeof(AsyncConstructorPage) });
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = (ListBox)sender;
            var sample = list.SelectedItem as SamplePage;
            if (sample == null) return;

            var page = (Page)Activator.CreateInstance(sample.Page);
            page.DataContext = sample;
            NavigationService.Navigate(page);
            list.SelectedItem = null;
        }
    }
}
