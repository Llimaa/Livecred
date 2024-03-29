﻿using Flunt.Notifications;
using Flunt.Validations;
using Livecred.Domain.Enuns;
using Livecred.Domain.Shared.Commands;
using System;
namespace Livecred.Domain.Commands.Inputs.Parcela
{
    public class ParcelaInput : Notifiable, Icommand
    {
        public Guid IdLoan { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorComJuro { get; set; }
        public DateTime DataVencimento { get; set; }
        public double JuroAtraso { get; set; }
        public EStatusParcela Status { get; set; }

        public void Validate()
        {
            new Contract()
                .IsNotNullOrEmpty(Valor.ToString(), "Valor", "O campo valor não pode esta em branco.")
                .IsNotNullOrEmpty(DataVencimento.ToString(), "DataVencimento", "O campo Data de vencimento não pode esta em branco.")
                .IsNotNullOrEmpty("EStatusParcela", "EStatusParcela", "O campo Status não pode esta em branco.")
                .IsNotNullOrEmpty(IdLoan.ToString(), "IdLoan", "O campo identificador do Emprestimo não pode ficar em branco");
        }
    }
}
