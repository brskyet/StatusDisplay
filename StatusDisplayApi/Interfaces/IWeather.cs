using StatusDisplayApi.Models;
using System.Threading.Tasks;

namespace StatusDisplayApi.Interfaces
{
    public interface IWeather
    {
        Task<WeatherModel> GetForecast();
    }
}
