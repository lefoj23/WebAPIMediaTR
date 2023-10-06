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
    public class AccountsRepository: IAccountsRepository
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;
        public AccountsRepository(MainDbContext context) {
            _mapper = MapperConfig.InitializeAutoMapper(new MappingProfile());
            _context = context;
        }
        public async Task<bool> AddAsync(AccountsDTO dto)
        {
            dto.Password = PasswordHasher.HashPassword(dto.Password);

            var model =_mapper.Map<Accounts>(dto);

            _context.Accounts.Add(model);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public bool IsAccountExists(string accountIdentity)
        {
            return _context.Accounts.Any(a =>  a.ContactNumber == accountIdentity ||  a.Email == accountIdentity);

        }

        public async Task<AccountsDTO> Get(string accountIdentity)
        {

            var account = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(f => f.ContactNumber == accountIdentity || f.Email == accountIdentity);

            var model = _mapper.Map<AccountsDTO>(account);

            return model;
        }


        public async Task<AccountsDTO> Authenticate(string accountIdentity, string password)
        {

            if (!this.IsAccountExists(accountIdentity)) AuthFailed();


            var account = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(f => f.ContactNumber == accountIdentity || f.Email == accountIdentity);


            var verifyPassword = PasswordHasher.VerifyPassword(password, account.Password);
            if (!verifyPassword) AuthFailed();


            var model = _mapper.Map<AccountsDTO>(account);

            return model;
        }

        private void AuthFailed()
        {
            throw new Exception("Authentication failed, Please check your username and password.");
        }
    }
}
