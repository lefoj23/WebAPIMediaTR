using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IAccountsRepository
    {
        Task<bool> AddAsync(AccountsDTO dto);
        bool IsAccountExists(string accountIdentity);
        Task<AccountsDTO> Get(string accountIdentity);
        Task<AccountsDTO> Authenticate(string accountIdentity, string password);
    }
}
