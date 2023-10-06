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
    public class TransactionsRepository: ITransactionsRepository
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;
        public TransactionsRepository(MainDbContext context) {
            _mapper = MapperConfig.InitializeAutoMapper(new MappingProfile());
            _context = context;
        }
        public async Task<bool> AddAsync(TransactionsDTO dto)
        {
   
            var model = _mapper.Map<Transactions>(dto);

            _context.Transactions.Add(model);

            //var result = await _context.SaveChangesAsync();

            return true;
        }


    }
}
