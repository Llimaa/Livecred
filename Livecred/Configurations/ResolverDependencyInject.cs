using Livecred.Domain.Commands.Handlers;
using Livecred.Domain.Commands.Inputs.Client;
using Livecred.Domain.Commands.Inputs.Loan;
using Livecred.Domain.Commands.Inputs.Parcela;
using Livecred.Domain.Commands.output;
using Livecred.Domain.Repositories;
using Livecred.Domain.Shared.Commands;
using Livecred.Infra.Context;
using Livecred.Infra.Context.Core;
using Livecred.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Livecred.Configurations
{
    public static class ResolverDependencyInject
    {
        public static IServiceCollection ResolverDependencies(this IServiceCollection services)
        {
            services.AddScoped<IDbConfiguration, DbConfiguration>();
            services.AddScoped<IDB, MSSQLDB>();

            services.AddScoped<IRepositoryClient, RepositoryClient>();
            services.AddScoped<IRepositoryLoan, RepositorioLoan>();
            services.AddScoped<IRepositoryParcela, RepositoryParcela>();

            services.AddScoped<Icommand, ClientInsert>();
            services.AddScoped<Icommand, ClientUpdate>();

            services.AddScoped<Icommand, LoanInsert>();
            services.AddScoped<Icommand, LoanUpdate>();
            services.AddScoped<Icommand, ParcelaInput>();
            services.AddScoped<Icommand, ParcelaUpdateDataVencimento>();
            services.AddScoped<Icommand, ParcelaUpdateJuroAtraso>();
            services.AddScoped<Icommand, ParcelaUpdateStatus>();
            services.AddScoped<Icommand, ParcelaUpdateValor>();
            services.AddScoped<ICommandOutput, GenericCommandResult>();

            services.AddScoped<ICommandOutput, GenericCommandResult>();
            services.AddScoped<ICommandOutput, GenericCommandResult>();
            services.AddScoped<ICommandOutput, GenericCommandResult>();

            services.AddScoped<ClientHandler>();
            services.AddScoped<LoanHandler>();
            services.AddScoped<ParcelaHandler>();

            return services;
        }
    }
}
