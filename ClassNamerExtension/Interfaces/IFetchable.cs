using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace App.Interfaces
{
    public interface IFetchable
    {
        Task Fetch(ConcurrentStack<string> stack);
    }
}
