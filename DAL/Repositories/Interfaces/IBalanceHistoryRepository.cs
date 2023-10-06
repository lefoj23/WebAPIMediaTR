using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IBalanceHistoryRepository
    {
        Task<bool> AddAsync(BalanceHistoryDTO dto);
        Task<bool> AddRangeAsync(List<BalanceHistoryDTO> dto);
        Task<BalanceInquiryDTO?> BalanceInquiry(Guid accountId);
        Task<BalanceHistoryDTO> GetLatestBalanceHistory(Guid accountId);

    }
}
