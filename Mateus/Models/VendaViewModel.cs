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


        public string Mensagem { get; set; }


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

            public bool Disponibilidade { get; set; }

            public string NomeFilial { get; set; }

            public string NomeProduto { get; set; }
        }

        public class Produto
        {
            public int Id { get; set; }

            public string Nome { get; set; }

            public double Valor { get; set; }

           
        }

        public List<Produto> ListaProduto { get; set; } = new List<Produto>();

        public List<Filial> ListaFiliais { get; set; } = new List<Filial>();

        public List<FilialEstoque> Estoque { get; set; } = new List<FilialEstoque>();

        public List<Carrinho> ListaCarrinho { get; set; } = new List<Carrinho>();
       

        public List<FilialEstoque> EstoqueAtualizado { get; set; } = new List<FilialEstoque>();

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

            };

            auxId++;
            ListaProduto.Add(item);

            var item2 = new Produto
            {
                Id = auxId,
                Nome = "Shampoo",
                Valor = 6.00,
               
            };

            auxId++;
            ListaProduto.Add(item2);


            var item3 = new Produto
            {
                Id = auxId,
                Nome = "Arroz",
                Valor = 6.00,
               
            };
            auxId++;
            ListaProduto.Add(item3);

            var item4 = new Produto
            {
                Id = auxId,
                Nome = "Feijao",
                Valor = 7.00,
               
            };
            auxId++;
            ListaProduto.Add(item4);

            var item5 = new Produto
            {
                Id = auxId,
                Nome = "Macarrao",
                Valor = 2.50,
               
            };
            auxId++;
            ListaProduto.Add(item5);

            var biscoito = new Produto
            {
                Id = auxId,
                Nome = "Biscoito",
                Valor = 1.50,

            };
            auxId++;
            ListaProduto.Add(biscoito);

            var caneta = new Produto
            {
                Id = auxId,
                Nome = "Caneta",
                Valor = 2.50,

            };
            auxId++;
            ListaProduto.Add(caneta);

            var copo = new Produto
            {
                Id = auxId,
                Nome = "Copo",
                Valor = 8.50,

            };
            auxId++;
            ListaProduto.Add(copo);

            var garrafa = new Produto
            {
                Id = auxId,
                Nome = "Garrafa",
                Valor = 2.50,

            };
            auxId++;
            ListaProduto.Add(garrafa);

            var refrigerante = new Produto
            {
                Id = auxId,
                Nome = "Refrigerante",
                Valor =5.50,

            };
            auxId++;
            ListaProduto.Add(refrigerante);


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

                    //Para cada filial sera adicionado
                    foreach (var filialSelecionada in ListaFiliais.ToList()) {
                    var qntAdicionar = RandomNumber(0, 100);
                        var itemEstoque = new FilialEstoque
                        {
                            Id = auxEstoque,
                            IdFilial = filialSelecionada.Id,
                            IdProduto = produto.Id,
                            QntProduto = qntAdicionar,
                            Disponibilidade = qntAdicionar != 0,
                            NomeFilial = filialSelecionada.Nome,
                            NomeProduto = produto.Nome
                        };
                        auxEstoque++;
                        Estoque.Add(itemEstoque);
                   

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

            if (itemEstoque.Disponibilidade)
            {
                var itemCarrinho = new Carrinho
                {
                    Id = idItemCarrinho,
                    IdProduto = idProduto,
                    QntComprada = RandomNumber(1, itemEstoque.QntProduto),
                    IdFilial = idFilial

                };
                ListaCarrinho.Add(itemCarrinho);

                var buscarItemEstoque = Estoque.Where(u => u.Id == itemEstoque.Id && u.IdProduto == produto.Id && u.IdFilial == idFilial);

                //para facilitar o codigo será considerado o estoque será considerado como se fosse uma prateleira apenas e a medida que o cliente pega o item,
                //o mesmo é decrementado da quantidade do estoque.
                foreach (var itemDoEstoque in buscarItemEstoque)
                {

                    //verificaçao de segurança para que não permitam que sejam "comprados" mais itens do que existam
                    if (itemEstoque.QntProduto >= itemCarrinho.QntComprada)
                    {
                        // subtrai do total do estoque
                        itemDoEstoque.QntProduto = itemEstoque.QntProduto - itemCarrinho.QntComprada;

                        //caso sejam compradas todas as unidades, o produto vira indisponivel
                        if (itemDoEstoque.QntProduto == 0)
                        {
                            itemEstoque.Disponibilidade = false;
                        }

                        var buscarFilial = ListaFiliais.FirstOrDefault(f => f.Id == idFilial);
                        itemEstoque = new FilialEstoque
                        {
                            Id = itemEstoque.Id,
                            IdFilial = itemEstoque.IdFilial,
                            IdProduto = produto.Id,
                            QntProduto = itemDoEstoque.QntProduto,
                            Disponibilidade = itemDoEstoque.QntProduto != 0,
                            NomeFilial = buscarFilial.Nome,
                            NomeProduto = produto.Nome
                        };
                       
                        EstoqueAtualizado.Add(itemEstoque);
                    }
                    else
                    {
                        var mensagem = "O produto " + $"{produto.Nome}" + " não possui a quantidade escolhida";
                        controller.ModelState.AddModelError(nameof(Mensagem), mensagem);
                    }

                    var qntEstoqueTeste = Estoque.FirstOrDefault(x => x.IdProduto == idProduto && x.IdFilial == idFilial).QntProduto;

                }

            }

           



           
            return null;
        }

        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }


}