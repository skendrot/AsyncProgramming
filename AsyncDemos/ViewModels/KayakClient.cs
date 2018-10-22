using AsyncDemos.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncDemos.ViewModels
{
    internal class KayakClient : ITravelRateClient
    {
        public async Task<IEnumerable<HotelRate>> GetHotelRatesAsync(string city, string state, string hotelName = null)
        {
            await Task.Delay(new Random((int)DateTime.Now.Millisecond).Next(1000, 5000));
            return new[]
            {
                new HotelRate { Name = "Marriott", Rate=229.99, Id = 5,       Image = "/AsyncDemos;component/Assests/grand-pasa-hotel-8-Custom.jpg"},
                new HotelRate { Name = "Sheridan", Rate=179.99, Id = 2,       Image = "/AsyncDemos;component/Assests/Hotel-Tips.jpg"},
                new HotelRate { Name = "Hilton", Rate=252.99, Id = 1,         Image = "/AsyncDemos;component/Assests/hsp_389433577.jpg"},
                new HotelRate { Name = "Embassy Suites", Rate=183.99, Id = 3, Image = "/AsyncDemos;component/Assests/liberty-lara-hotel1.jpg"},
            };
        }
    }
}