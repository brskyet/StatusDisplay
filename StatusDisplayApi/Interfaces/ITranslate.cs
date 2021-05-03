using StatusDisplayApi.Models;
using System.Threading.Tasks;

namespace StatusDisplayApi.Interfaces
{
    public interface ITranslate
    {
        Task<EngTranslatedWordModel> GetTranslation(EngWordModel engWordModel);
    }
}
