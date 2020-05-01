﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livecred.Configurations;
using Livecred.Domain.Commands.Handlers;
using Livecred.Domain.Commands.Inputs.Client;
using Livecred.Domain.Repositories;
using Livecred.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Livecred.Controllers
{
    public class ClientController : Controller
    {
        private readonly ClientHandler _clientHandler;
        private readonly IRepositoryClient _repositoryClient;

        public ClientController(ClientHandler clientHandler, IRepositoryClient repositoryClient)
        {
            _clientHandler = clientHandler;
            _repositoryClient = repositoryClient;
        }

        // GET: Client
        public async Task<ActionResult> Index()
        {
            var client = await _repositoryClient.GetAll();

            if (HttpExtensionsValidate.IsAjaxRequest(Request))
                return PartialView("Index", client);

            return View(client);
        }

        // GET: Client/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            ViewBag.success = false;
            return PartialView();
        }

        // POST: Client/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ClientInsert client)
        {
            client.Validate();
            client.Telephone = String.Join("", System.Text.RegularExpressions.Regex.Split(client.Telephone, @"[^\d]"));
            client.NuberDocument = String.Join("", System.Text.RegularExpressions.Regex.Split(client.NuberDocument, @"[^\d]"));
            if (client.Valid)
                await _clientHandler.Handler(client);
            ViewBag.success = true;

            return PartialView();
        }

        // GET: Client/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Client/Edit/5
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

        // GET: Client/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Client/Delete/5
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

        [HttpGet]
        public async Task<string> validarDocument(string document)
        {
            document = String.Join("", System.Text.RegularExpressions.Regex.Split(document, @"[^\d]"));
            Document _document = new Document(document);
            if (_document.Valid && document.Length == 11)
            {
                if (await _repositoryClient.ValidDocument(_document.Number))
                {

                    return "jà existe cliente com esse documento";
                }

                return "";
            }
            else
            {
                return "Documento inválido!";
            }
        }
    }
}