using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mateus.Models;
using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp;
using FireSharp.Response;
using Newtonsoft.Json;

namespace Mateus.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {

            VendaViewModel model = new VendaViewModel();


            await model.CarregarDados(this);

            var listaProdutos = model.ListaProduto;
           


            for (int i = 0; i < 10; i++)
            {
                model.PreencherCarrinho(this);
            }
            if (ModelState.IsValid)
            {
                var itensDoCarrinho = model.ListaCarrinho;
                var listaEstoqueAtualizada = model.Estoque;

            }
            else
            {
                var mensagem = model.Mensagem;
            }




            return View("~/Views/Venda.cshtml", model);
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
