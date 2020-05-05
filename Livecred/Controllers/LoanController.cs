using System.Threading.Tasks;
using Livecred.Configurations;
using Livecred.Domain.Commands.Handlers;
using Livecred.Domain.Commands.Inputs.Loan;
using Livecred.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Livecred.Controllers
{
    public class LoanController : Controller
    {
        private readonly IRepositoryLoan _repositoryLoan;
        private readonly LoanHandler _loanHandler;
        private readonly IRepositoryClient _repositoryClient;

        public LoanController(IRepositoryLoan repositoryLoan, LoanHandler loanHandler, IRepositoryClient repositoryClient)
        {
            _repositoryLoan = repositoryLoan;
            _loanHandler = loanHandler;
            _repositoryClient = repositoryClient;
        }

        public async Task<IActionResult> Index()
        {
            var loan = await _repositoryLoan.GetAll();

            if (HttpExtensionsValidate.IsAjaxRequest(Request))
                return PartialView("Index", loan);

            return View(loan);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.success = false;
            ViewBag.clientes = await _repositoryClient.GetAll();
            return PartialView();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoanInsert loanInsert)
        {
            ViewBag.clientes = await _repositoryClient.GetAll();

            loanInsert.Validate();
            if (loanInsert.Valid)
                await _loanHandler.Handler(loanInsert);
            ViewBag.success = true;
            return PartialView();
        }
    }
}