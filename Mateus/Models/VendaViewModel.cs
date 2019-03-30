using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNetCore.Mvc;

namespace Mateus.Models
{
    public class VendaViewModel
    {

        public double Valor { get; set; }

        public string Cliente { get; set; }

        public class Carrinho { 

            public int Id { get; set; }

            public string Produto { get; set; }

            public double Valor { get; set; }
           

        }

        public List<Carrinho> ListaProduto { get; set; } = new List<Carrinho>();


        public async Task<JsonResult> CarregarDados(Controller controller)
        {

            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "hjrYW5sNhzAfbkPG1b6uyMqAMNP2AkvRWygBfz9i",
                BasePath = "https://app-mateus.firebaseio.com"
            };

            IFirebaseClient client = new FirebaseClient(config);

            int auxId = 0;

            var item = new Carrinho
            {
                Id = 1,
                Produto = "Primeiro",
                Valor= 30.00
            };

            auxId++;
            ListaProduto.Add(item);

            var item2 = new Carrinho
            {
                Id = auxId,
                Produto = "Segundo",
                Valor = 5.20
            };

            auxId++;
            ListaProduto.Add(item2);


            var item3 = new Carrinho
            {
                Id = auxId,
                Produto = "Terceiro",
                Valor = 6.20
            };

            ListaProduto.Add(item3);

            foreach (var produto in ListaProduto.ToList())
            {
                FirebaseResponse responseBuscar = await client.GetTaskAsync("produto/" + produto.Produto);

                bool hasProdutoBanco = responseBuscar.ResultAs<bool>();
                if (hasProdutoBanco)
                {
                    FirebaseResponse responseUpdate = await client.UpdateTaskAsync("produto/" + produto.Produto, produto);
                    Produto result = responseUpdate.ResultAs<Produto>();

                }
                else
                {
                    FirebaseResponse responseSet = await client.SetTaskAsync("produto/" + produto.Produto, produto);
                    Produto result = responseSet.ResultAs<Produto>();
                }
                Console.WriteLine($"{produto.Produto} e valor {produto.Valor}");

                Valor = produto.Valor + Valor;

            }
            return null;
        }
    }


}