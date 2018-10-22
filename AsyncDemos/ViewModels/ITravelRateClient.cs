using AsyncDemos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncDemos.ViewModels
{
    internal interface ITravelRateClient
    {
        Task<IEnumerable<HotelRate>> GetHotelRatesAsync(string city, string state, string hotelName = null);
    }
}