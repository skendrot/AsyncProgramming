using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AsyncDemos.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private Page _currentPage;

        public MainViewModel(Page startPage)
        {
            CurrentPage = startPage;
        }

        public Page CurrentPage
        {
            get { return _currentPage; }
            set { SetProperty(ref _currentPage, value); }
        }

    }
}
