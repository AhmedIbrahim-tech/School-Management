using Data.Entities.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IApplicationUserService
    {
        Task<string> AddUserAsync(User user, string password);
    }
}
