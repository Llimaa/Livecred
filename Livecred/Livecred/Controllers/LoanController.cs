using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livecred.Configurations;
using Livecred.Domain.Commands.Handlers;
using Livecred.Domain.Commands.Inputs.Loan;
using Livecred.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Livecred.Controllers
{
    public class LoanController : Controller
    {
        private readonly IRepositoryLoan _repositoryLoan;
        private readonly LoanHandler _loanHandler;

        private static Guid IdClient;

        public LoanController(IRepositoryLoan repositoryLoan, LoanHandler loanHandler)
        {
            _repositoryLoan = repositoryLoan;
            _loanHandler = loanHandler;
        }

        // GET: Loan
        public async Task<ActionResult> Index()
        {
            var loan = await _repositoryLoan.GetAll();

            if (HttpExtensionsValidate.IsAjaxRequest(Request))
                return PartialView("Index", loan);

            return View(loan);
        }


        // GET: Loan/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Loan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Loan/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LoanInsert loanInsert)
        {
            try
            {
                loanInsert.Validate();
                if (loanInsert.Valid)
                {
                    loanInsert.IdClient = IdClient;
                    var res = await _loanHandler.Handler(loanInsert);
                }

                return RedirectToAction(nameof(Index), new { IdCLient = IdClient });
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: Loan/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Loan/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Loan/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Loan/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}