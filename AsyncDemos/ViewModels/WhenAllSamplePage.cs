using AsyncDemos.Models;
using AsyncDemos.Pages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AsyncDemos.ViewModels
{
    class WhenAllSamplePage : SamplePage
    {
        private ICollection<HotelRate> _hotelRates;

        private PricelineClient _pricelineClient;
        private KayakClient _kayakClient;
        private TravelocityClient _travelocityClient;
        private HotwireClient _hotwireClient;
        private ExpediaClient _expediaClient;

        public WhenAllSamplePage()
        {
            Title = "WhenAll - Running multiple tasks at once";
            Page = typeof(WhenAllPage);

            GetHotelRatesCommand = new SimpleDelegateCommand(GetHotelRates);
            HotelRates = new ObservableCollection<HotelRate>();

            _pricelineClient = new PricelineClient();
            _kayakClient = new KayakClient();
            _travelocityClient = new TravelocityClient();
            _hotwireClient = new HotwireClient();
            _expediaClient = new ExpediaClient();
        }

        public ICommand GetHotelRatesCommand { get; private set; }

        public ICollection<HotelRate> HotelRates
        {
            get { return _hotelRates; }
            set { SetProperty(ref _hotelRates, value); }
        }

        private async void GetHotelRates(object obj)
        {
            HotelRates.Clear();

            string city = "Orlando";
            string state = "FL";

            Loading = true;

            //One at a time
            //var rates = await _pricelineClient.GetHotelRatesAsync(city, state);
            //AddRates(rates);

            //rates = await _kayakClient.GetHotelRatesAsync(city, state);
            //AddRates(rates);

            //rates = await _travelocityClient.GetHotelRatesAsync(city, state);
            //AddRates(rates);

            //rates = await _hotwireClient.GetHotelRatesAsync(city, state);
            //AddRates(rates);

            //rates = await _expediaClient.GetHotelRatesAsync(city, state);
            //AddRates(rates);

            #region all at once

            var tasks = new List<Task<IEnumerable<HotelRate>>>();

            tasks.Add(_pricelineClient.GetHotelRatesAsync(city, state));
            tasks.Add(_kayakClient.GetHotelRatesAsync(city, state));
            tasks.Add(_travelocityClient.GetHotelRatesAsync(city, state));
            tasks.Add(_hotwireClient.GetHotelRatesAsync(city, state));
            tasks.Add(_expediaClient.GetHotelRatesAsync(city, state));

            // Wait for all to complete
            //var allRates = await Task.WhenAll(tasks);
            //foreach (IEnumerable<HotelRate> rates in allRates)
            //{
            //    AddRates(rates);
            //}

            while(tasks.Count > 0)
            {
                var completedTask = await Task.WhenAny(tasks);
                var result = await completedTask;
                AddRates(result);
                tasks.Remove(completedTask);
            }

            #endregion

            Loading = false;
        }

        private void AddRates(IEnumerable<HotelRate> newRates)
        {
            foreach (var rate in newRates)
            {
                var currentRate = HotelRates.FirstOrDefault(r => r.Id == rate.Id);
                if (currentRate == null)
                {
                    HotelRates.Add(rate);
                }
                else if (currentRate.Rate > rate.Rate)
                {
                    HotelRates.Remove(currentRate);
                    HotelRates.Add(rate);
                }
            }
        }
    }
}
