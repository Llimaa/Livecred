using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livecred.Configurations;
using Livecred.Domain.Commands.Handlers;
using Livecred.Domain.Commands.Inputs.Parcela;
using Livecred.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Livecred.Controllers
{
    public class ParcelaController : Controller
    {

        private readonly ParcelaHandler _parcelaHandler;
        private readonly IRepositoryParcela _repositoryParcela;

        private static Guid _idLoan;

        public ParcelaController(ParcelaHandler parcelaHandler, IRepositoryParcela repositoryParcela)
        {
            _parcelaHandler = parcelaHandler;
            _repositoryParcela = repositoryParcela;
        }

        // GET: Parcela
        public async Task<ActionResult> Index(Guid idLoan)
        {
            _idLoan = idLoan;
            var res = await _repositoryParcela.GetAllByIdLoan(idLoan);
            if (HttpExtensionsValidate.IsAjaxRequest(Request))
                return PartialView("ListaPorLoan", res);

            return View(res);
        }

        // GET: Parcela/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Parcela/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Parcela/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ParcelaInput parcelaInput)
        {
            try
            {
                parcelaInput.IdLoan = _idLoan;
                await _parcelaHandler.Handler(parcelaInput);
                return RedirectToAction(nameof(Index), new { IdLoan = _idLoan });
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: Parcela/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Parcela/Edit/5
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

        // GET: Parcela/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Parcela/Delete/5
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