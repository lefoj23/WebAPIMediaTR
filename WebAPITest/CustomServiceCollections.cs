using DAL.Repositories.Interfaces;
using DAL.Repositories;
using MediatR;
using CQRSServices.Handlers;
using FluentValidation;
using CQRSServices.Validators;
using CQRSServices.Requests;
using CQRSServices;
using DemoTest.Services;
using CQRSServices.Commands;

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



            return services;
        }

    
    }
}
