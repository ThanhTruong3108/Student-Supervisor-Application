using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Helpers.MemoryCache
{
    public interface ICacheManager
    {
        Task Set(string key, object value, int time);

        Task<T> Get<T>(string key);

        Task Remove(string key);
    }
}
