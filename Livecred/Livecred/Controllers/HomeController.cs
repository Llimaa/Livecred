using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Livecred.Models;
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
            // ViewBag.emprestimos = await _repositoryLoan.GetAll();
            ViewBag.ValorRecebido = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", await _repositoryParcela.GetTotalRecebido(EStatusParcela.Paga));
            ViewBag.valorTotalDeEmprestimos = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", await _repositoryLoan.GetMoneyTotal());
            ViewBag.emprestimosDoUtimoMes = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", await _repositoryLoan.GetTotalByThirtyToday(DateTime.Now.AddMonths(-1), DateTime.Now));
            ViewBag.emprestimosUltimos15Dias = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", await _repositoryLoan.GetTotalByThirtyToday(DateTime.Now.AddDays(-15), DateTime.Now));
            ViewBag.idLoan = Guid.NewGuid();
            //var emprestimosAberto = await _repositoryLoan.GetAllOrderByStatus(EStatusEmprestimo.Aberto);

            if (HttpExtensionsValidate.IsAjaxRequest(Request))
                return PartialView("Index", loanQuery);

            return View(loanQuery);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
