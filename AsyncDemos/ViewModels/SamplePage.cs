using AsyncDemos.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDemos.ViewModels
{
    class SamplePage : BaseViewModel
    {
        private bool _loading;

        public string Title { get; set; }
        public Type Page { get; set; }

        public bool Loading
        {
            get { return _loading; }
            set { SetProperty(ref _loading, value); }
        }
    }
}