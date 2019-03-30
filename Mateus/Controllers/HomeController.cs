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

            //IFirebaseConfig config = new FirebaseConfig
            //{
            //    AuthSecret = "hjrYW5sNhzAfbkPG1b6uyMqAMNP2AkvRWygBfz9i",
            //    BasePath = "https://app-mateus.firebaseio.com"
            //};

            //IFirebaseClient client = new FirebaseClient(config);

            //var produto = new Produto
            //{
            //    Nome ="Celular",
            //    Disponibilidade = false,
            //     Valor = 1
            //};

            //SetResponse response = await client.SetAsync("produto/"+produto.Nome, produto);
            //Produto result = response.ResultAs<Produto>();

            VendaViewModel model = new VendaViewModel();

            await model.CarregarDados(this);
            //FirebaseResponse response = await client.UpdateAsync("produto/"+produto.Nome, produto);
            //Produto result = response.ResultAs<Produto>();

            //FirebaseResponse response = await client.GetAsync("produto/Celular");
            //var result = response.ResultAs<Produto>();

            //FirebaseResponse response = await client.GetAsync("produto");

            //var produtos = (JsonConvert.DeserializeObject(response.Body, typeof(List<Produto>)) as List<Produto>);
            //var teste = "";
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
