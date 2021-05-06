using StatusDisplayApi.Models;
using System.Threading.Tasks;

namespace StatusDisplayApi.Interfaces
{
    public interface INews
    {
        Task<NewsModel> GetNews();
    }
}
