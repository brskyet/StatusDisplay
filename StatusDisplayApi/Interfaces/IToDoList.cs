using StatusDisplayApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StatusDisplayApi.Interfaces
{
    public interface IToDoList
    {
        Task<List<ToDoListModel>> GetToDoList();
    }
}
