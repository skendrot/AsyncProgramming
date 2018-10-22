using AsyncDemos.Models;
using AsyncDemos.Pages;
using AsyncDemos.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AsyncDemos.ViewModels
{
    class ConfigureAwaitSamplePage : SamplePage
    {
        private CatClient _client = new CatClient();
        private ICollection<Cat> _cats;

        public ConfigureAwaitSamplePage()
        {
            Title = "ConfigureAwait";
            Page = typeof(ConfigureAwaitPage);

            LoadImagesCommand = new SimpleDelegateCommand(LoadImages);
            Cats = new ObservableCollection<Cat>();
        }

        public ICommand LoadImagesCommand { get; private set; }

        public ICollection<Cat> Cats
        {
            get { return _cats; }
            set { SetProperty(ref _cats, value); }
        }

        private async void LoadImages(object obj)
        {
            var cats = await _client.GetCatsAsync(10);
            foreach (var cat in cats)
            {
                Cats.Add(cat);
            }
        }
    }
}
