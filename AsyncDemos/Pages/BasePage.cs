using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace AsyncDemos.Pages
{
    public class BasePage : Page
    {
        public BasePage()
        {
            MainWindow.Frame.NavigationService.Navigated += OnNavigatedTo;
        }

        protected virtual void OnNavigatedTo(NavigationEventArgs e)
        {
            LoadState();
        }

        protected virtual void LoadState()
        {
            
        }

        private void OnNavigatedTo(object sender, NavigationEventArgs e)
        {
            NavigationService.Navigated -= OnNavigatedTo;

            OnNavigatedTo(e);
        }
    }
}
