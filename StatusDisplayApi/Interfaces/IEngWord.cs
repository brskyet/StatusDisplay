using StatusDisplayApi.Models;
using System.Threading.Tasks;

namespace StatusDisplayApi.Interfaces
{
    public interface IEngWord
    {
        Task<EngWordModel> GetOriginalWord();
    }
}
