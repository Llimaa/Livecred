using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Livecred.Domain.Repositories;
using Livecred.Domain.Enuns;
using System.Globalization;
using Livecred.Configurations;

namespace Livecred.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepositoryClient _repositoryClient;
        private readonly IRepositoryLoan _repositoryLoan;
        private readonly IRepositoryParcela _repositoryParcela;
        public HomeController(IRepositoryClient repositoryClient, IRepositoryLoan repositoryLoan, IRepositoryParcela repositoryParcela)
        {
            _repositoryClient = repositoryClient;
            _repositoryLoan = repositoryLoan;
            _repositoryParcela = repositoryParcela;
        }

        public async Task<IActionResult> Index()
        {

            var loanQuery = await _repositoryLoan.GetLoanQuery(EStatusEmprestimo.Aberto);
            ViewBag.TotalemprestimosAbertos = await _repositoryLoan.GetTotalOrderByStatus(EStatusEmprestimo.Aberto);
            ViewBag.ValorRecebido = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", await _repositoryParcela.GetTotalRecebido(EStatusParcela.Paga));
            ViewBag.valorTotalDeEmprestimos = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", await _repositoryLoan.GetMoneyTotal());
            ViewBag.emprestimosDoUtimoMes = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", await _repositoryLoan.GetTotalByThirtyToday(DateTime.Now.AddMonths(-1), DateTime.Now));
            ViewBag.emprestimosUltimos15Dias = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", await _repositoryLoan.GetTotalByThirtyToday(DateTime.Now.AddDays(-15), DateTime.Now));
            //var emprestimosAberto = await _repositoryLoan.GetAllOrderByStatus(EStatusEmprestimo.Aberto);

            if (HttpExtensionsValidate.IsAjaxRequest(Request))
                return PartialView("Index", loanQuery);

            return View(loanQuery);
        }
    }
}
