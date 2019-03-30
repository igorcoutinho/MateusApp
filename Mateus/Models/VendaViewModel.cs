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

            public int IdProduto { get; set; }

            public int QntComprada { get; set; }

            public int IdFilial { get; set; }
            
        }

        public class Filial
        {
            public int Id { get; set; }

            public string Nome { get; set; }


        }

        public class FilialEstoque
        {
            public int Id { get; set; }

            public int IdFilial { get; set; }

            public int IdProduto { get; set; }

            public int QntProduto { get; set; }
        }

        public class Produto
        {
            public int Id { get; set; }

            public string Nome { get; set; }

            public double Valor { get; set; }

            public bool Disponibilidade { get; set; }
        }

        public List<Produto> ListaProduto { get; set; } = new List<Produto>();

        public List<Filial> ListaFiliais { get; set; } = new List<Filial>();

        public List<FilialEstoque> Estoque { get; set; } = new List<FilialEstoque>();

        public List<Carrinho> ListaCarrinho { get; set; } = new List<Carrinho>();

        public async Task<JsonResult> CarregarDados(Controller controller)
        {


            int auxId = 0;

            #region Criando Prudutos

            //produtos adicionados estaticamente. Lista Produto contem todos os produtos
            var item = new Produto
            {
                Id = 1,
                Nome = "Carne",
                Valor= 30.00,
                Disponibilidade =  true
            };

            auxId++;
            ListaProduto.Add(item);

            var item2 = new Produto
            {
                Id = auxId,
                Nome = "Shampoo",
                Valor = 6.00,
                Disponibilidade = true
            };

            auxId++;
            ListaProduto.Add(item2);


            var item3 = new Produto
            {
                Id = auxId,
                Nome = "Arroz",
                Valor = 6.00,
                Disponibilidade = true
            };
            auxId++;
            ListaProduto.Add(item3);

            var item4 = new Produto
            {
                Id = auxId,
                Nome = "Feijao",
                Valor = 7.00,
                Disponibilidade = true
            };
            auxId++;
            ListaProduto.Add(item4);

            var item5 = new Produto
            {
                Id = auxId,
                Nome = "Macarrao",
                Valor = 2.50,
                Disponibilidade = true
            };
            auxId++;
            ListaProduto.Add(item5);

            #endregion Criando Produtos

            #region Criar as filiais

            var filial = new Filial
            {
                Id = 1,
                Nome =  "São Luís",

            };

            ListaFiliais.Add(filial);


            var filial2 = new Filial
            {
                Id = 2,
                Nome = "Maiobão",

            };

            ListaFiliais.Add(filial2);


            var filial3 = new Filial
            {
                Id = 3,
                Nome = "Raposa",

            };

            ListaFiliais.Add(filial3);

            #endregion

            #region Criando um estoque


            foreach (var produto in ListaProduto.ToList())
            {
               
                //Os produtos so serao adicionados no estoque se estiverem disponiveis.
                //Eles podem existem no "banco" pórem podem não estar disponiveis atualmente

                int auxEstoque = 1;
                if (produto.Disponibilidade)
                {
                    //Para cada filial sera adicionado
                    foreach (var itemFilial in ListaFiliais) {
                        var itemEstoque = new FilialEstoque
                        {
                            Id = auxEstoque,
                            IdFilial = itemFilial.Id,
                            IdProduto = produto.Id,
                            QntProduto = RandomNumber(0, 50)
                        };
                        auxEstoque++;
                        Estoque.Add(itemEstoque);
                    }

                }

            }

            #endregion


            return null;
        }

        public JsonResult PreencherCarrinho(Controller controller)
        {
            //Para simular uma possivel consulta no banco. no caso as Listas receberiam de um query, select.. etc
            var qntProdutosListadosBanco = ListaProduto.Count();
            var qntFilais = ListaFiliais.Count();

            //Os Ids de carrinho são gerados de 1 a 10000 pra diminuir a probabilidade de se repetir.
            var idItemCarrinho = RandomNumber(1, 10000);

            //Uma verificacao caso seja igual. Como não tem um banco de dados que trave isto por Id, dessa forma diminuo a probabilidade
            //de dar errado ao quadrado
            if (ListaCarrinho.Any(a => a.Id == idItemCarrinho))
            {
                idItemCarrinho = RandomNumber(1, 10000);
            }
            var idProduto = RandomNumber(1, qntProdutosListadosBanco);
            var idFilial = RandomNumber(1, qntFilais);
            var produto = ListaProduto.FirstOrDefault(p => p.Id == idProduto);
            var itemEstoque = Estoque.FirstOrDefault(x => x.IdProduto == idProduto && x.IdFilial == idFilial);

            var itemCarrinho = new Carrinho
            {
                Id = idItemCarrinho,
                IdProduto = idProduto,
                QntComprada = RandomNumber(1, itemEstoque.QntProduto),
                IdFilial = idFilial

            };
            ListaCarrinho.Add(itemCarrinho);

            //para facilitar o codigo será considerado o estoque será considerado como se fosse uma prateleira apenas e a medida que o cliente pega o item,
            //o mesmo é decrementado da quantidade do estoque.


            //var result = Estoque.FirstOrDefault(x=> x.Id == itemEstoque.Id).Select(i => 
            //{
            //    i. = itemEstoque.QntProduto - itemCarrinho.QntComprada
               
            //}).ToList();


            //.FirstOrDefault(x => x.Id == itemCarrinho.Id).QntProduto = Estoque.FirstOrDefault(x => x.Id == itemCarrinho.Id).QntProduto - itemEstoque.QntProduto;


            return null;
        }

        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }


}