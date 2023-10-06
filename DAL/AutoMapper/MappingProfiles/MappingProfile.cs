using AutoMapper;
using DAL.DTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.AutoMapper.MappingProfiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile() {
            CreateMap<Accounts, AccountsDTO>().ReverseMap();
            CreateMap<BalanceHistory, BalanceHistoryDTO>().ReverseMap();
            CreateMap<Transactions, TransactionsDTO>().ReverseMap();

        }
    }
}
