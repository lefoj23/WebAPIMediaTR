using AutoMapper;
using Azure.Core;
using DAL.AutoMapper;
using DAL.AutoMapper.MappingProfiles;
using DAL.DTO;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class BalanceHistoryRepository: IBalanceHistoryRepository
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;
        public BalanceHistoryRepository(MainDbContext context,
            IMapper mapper) {
            _mapper = mapper;
            _context = context;
        }

        public async Task<bool> AddAsync(BalanceHistoryDTO dto)
        {

            var model = _mapper.Map<BalanceHistory>(dto);

            _context.BalanceHistory.Add(model);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> AddRangeAsync(List<BalanceHistoryDTO> dto)
        {

            var models = _mapper.Map<List<BalanceHistory>>(dto);

            _context.BalanceHistory.AddRange(models);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
        public async Task<BalanceInquiryDTO?> BalanceInquiry(Guid accountId)
        {       
            var result = await _context.BalanceHistory
                                .Include(i => i.Account)
                                .Where(w => w.AccountId == accountId)
                                .AsNoTracking()
                                .OrderByDescending(o => o.CreatedDate)
                                .Select(s => new BalanceInquiryDTO
                                { 
                                    CurrentBalance = s.CurrentBalance,
                                    AvailableBalance = s.CurrentBalance,
                                    TransactionDate = DateTime.Now
                                })
                                .FirstOrDefaultAsync();
   



            return result;
        }

        public async Task<BalanceHistoryDTO> GetLatestBalanceHistory(Guid accountId)
        {
            var balanceHistory = await _context.BalanceHistory
                                .AsNoTracking()
                                .OrderByDescending(o => o.CreatedDate)
                                .FirstOrDefaultAsync(f => f.AccountId == accountId);



            var result = _mapper.Map<BalanceHistoryDTO>(balanceHistory ?? null);

            return result ;
        }

    }
}
