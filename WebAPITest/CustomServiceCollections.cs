using CQRSServices;
using CQRSServices.Commands;
using CQRSServices.Handlers;
using CQRSServices.Requests;
using CQRSServices.Validators;
using DAL.AutoMapper.MappingProfiles;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using DemoTest.Services;
using FluentValidation;
using MediatR;

namespace WebAPITest
{
    public static class CustomServiceCollections
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {


            #region Validators


            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddTransient<IValidator<RegistrationCommand>, RegistrationCommandValidator>();
            services.AddTransient<IValidator<BalanceInquiryCommand>, BalanceInquiryCommandValidator>();
            services.AddTransient<IValidator<DepositCommand>, DepositCommandValidator>();
            services.AddTransient<IValidator<WithdrawalCommand>, WithdrawalCommandValidator>();
            services.AddTransient<IValidator<TransferMoneyCommand>, TransferMoneyCommandValidator>();


            #endregion


            #region Repositories
            services.AddScoped<IAccountsRepository, AccountsRepository>();
            services.AddScoped<IBalanceHistoryRepository, BalanceHistoryRepository>();
            services.AddScoped<ITransactionsRepository, TransactionsRepository>();

            #endregion


            #region MediaTR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
             typeof(RegistrationHandler).Assembly
             //typeof(GetOrderListQuery).Assembly
             ));
            #endregion

            #region AutoMapper  
            services.AddAutoMapper(cfg => { }, typeof(MappingProfile));
            #endregion

            return services;
        }

    
    }
}
