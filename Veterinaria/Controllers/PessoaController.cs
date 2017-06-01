using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veterinaria.DAO;
using Veterinaria.Models;

namespace Veterinaria.Controllers
{
    public class PessoaController : Controller
    {
        private PessoaDAO pessoas;
        private FuncionarioDAO funcionarios;

        private ClienteDAO clientes;
        private CidadeDAO cidades;

        private Connection connection;

        public PessoaController()
        {
            this.connection = new Connection();
            this.pessoas = new PessoaDAO(this.connection);
            this.funcionarios = new FuncionarioDAO(this.connection);
            this.clientes = new ClienteDAO(this.connection);
            this.cidades = new CidadeDAO(this.connection);
            this.connection.Close();
        }

        // GET: Pessoa
        public ActionResult Index()
        {
            return View(this.pessoas.ListAll());
        }

        // GET: Pessoa/Details/5
        public ActionResult Details(int id)
        {
            return View(this.pessoas.Search(new Pessoa { Id = id }));
        }

        // GET: Pessoa/Create
        public ActionResult Create()
        {
            return View(new Pessoa());
        }

        // POST: Pessoa/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var pessoa = new Pessoa()
                {
                    Nome = collection["nome"],
                    Cpf = Util.RemoverCaracters(collection["Cpf"].ToString()),
                    DataNascimento = DateTime.Parse(collection["data_nascimento"]),
                    TelefoneFixo = Util.RemoverCaracters(collection["telefone_fixo"].ToString()),
                    TelefoneCelular = Util.RemoverCaracters(collection["telefone_celular"].ToString()),
                    Endereco = collection["endereco"],
                    Bairro = collection["bairro"],
                    Complemento = collection["complemento"]
                };

                if (!String.IsNullOrEmpty(collection["numero"]))
                    pessoa.Numero = int.Parse(collection["numero"]);

                if (!String.IsNullOrEmpty(collection["email"]))
                    pessoa.Cliente = new Cliente() { Email = collection["email"] };

                if (!String.IsNullOrEmpty(collection["numerocontrato"]) && !String.IsNullOrEmpty(collection["salario"]) && !String.IsNullOrEmpty(collection["data_admissao"]) && !String.IsNullOrEmpty(collection["setor"]))
                {
                    pessoa.Funcionario = new Funcionario()
                    {
                        NumeroContrato = collection["numerocontrato"],
                        NumeroCRMV = collection["numerocrmv"],
                        Salario = double.Parse(collection["salario"]),
                        DataAdmisao = DateTime.Parse(collection["data_admissao"]),
                        Funcao = (Models.Enums.FuncaoFuncionario)int.Parse(collection["setor"])
                    };
                }

                if (!String.IsNullOrEmpty(collection["Idcidade"]))
                    pessoa.Cidade = new Cidade() { Id = int.Parse(collection["Idcidade"]) };
                else
                {
                    ViewBag.Erro = "Cidade Inválida";
                    return View(pessoa);
                }

                if (pessoas.Search(new Pessoa { Cpf = Util.RemoverCaracters(collection["Cpf"].ToString()) }) != null)
                {
                    ViewBag.Erro = "Pessoa Ja Cadastrada";
                    return View(pessoa);
                }

                if (pessoa.Funcionario == null && pessoa.Cliente == null)
                {
                    ViewBag.Erro = "E necessario Preencher os dados de Cliente ou Funcionario";
                    return View(pessoa);
                }

                if (pessoa.Cliente != null)
                    pessoa.Cliente.Id = (int)clientes.Insert(pessoa.Cliente);

                if (pessoa.Funcionario != null)
                    pessoa.Funcionario.Id = (int)funcionarios.Insert(pessoa.Funcionario);

                this.pessoas.Insert(pessoa);
                return RedirectToAction("Index");
            }
            catch (Exception) { return View(new Pessoa()); }
        }

        // GET: Pessoa/Edit/5
        public ActionResult Edit(int id)
        {
            return View(this.pessoas.Search(new Pessoa { Id = id }));
        }

        // POST: Pessoa/Edit/5
        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            try
            {
                var pessoa = new Pessoa()
                {
                    Id = int.Parse(collection["idpessoa"]),
                    Nome = collection["nome"],
                    Cpf = Util.RemoverCaracters(collection["Cpf"].ToString()),
                    DataNascimento = DateTime.Parse(collection["data_nascimento"]),
                    TelefoneFixo = Util.RemoverCaracters(collection["telefone_fixo"].ToString()),
                    TelefoneCelular = Util.RemoverCaracters(collection["telefone_celular"].ToString()),
                    Endereco = collection["endereco"],
                    Bairro = collection["bairro"],
                    Complemento = collection["complemento"]
                };

                if (!string.IsNullOrEmpty(collection["numero"]))
                    pessoa.Numero = int.Parse(collection["numero"]);

                if (!String.IsNullOrEmpty(collection["email"]))
                {
                    pessoa.Cliente = new Cliente() { Email = collection["email"] };
                    if (!String.IsNullOrEmpty(collection["idcliente"]))
                    {
                        pessoa.Cliente.Id = int.Parse(collection["idcliente"]);
                        this.clientes.Update(pessoa.Cliente);
                    }
                    else
                        pessoa.Cliente.Id = (int)this.clientes.Insert(pessoa.Cliente);
                }
                if (!String.IsNullOrEmpty(collection["numerocontrato"]) && !String.IsNullOrEmpty(collection["salario"]) && !String.IsNullOrEmpty(collection["data_admissao"]) && !String.IsNullOrEmpty(collection["setor"]))
                {
                    pessoa.Funcionario = new Funcionario()
                    {
                        NumeroContrato = collection["numerocontrato"],
                        NumeroCRMV = collection["numerocrmv"],
                        Salario = double.Parse(collection["salario"]),
                        DataAdmisao = DateTime.Parse(collection["data_admissao"]),
                        Funcao = (Models.Enums.FuncaoFuncionario)int.Parse(collection["setor"])
                    };
                    if (!String.IsNullOrEmpty(collection["idfuncionario"]))
                    {
                        pessoa.Funcionario.Id = int.Parse(collection["idfuncionario"]);
                        this.funcionarios.Update(pessoa.Funcionario);
                    }
                    else
                        pessoa.Funcionario.Id = (int)this.funcionarios.Insert(pessoa.Funcionario);
                }
                if (!String.IsNullOrEmpty(collection["Idcidade"]))
                    pessoa.Cidade = new Cidade() { Id = int.Parse(collection["Idcidade"]) };

                this.pessoas.Update(pessoa);
                return RedirectToAction("Index");
            }
            catch (Exception) { return View(); }
        }

        // GET: Pessoa/Delete/5
        public ActionResult Delete(int id)
        {
            this.pessoas.Delete(new Pessoa { Id = id });
            return RedirectToAction("Index");
        }

        // POST: Pessoa/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                this.pessoas.Delete(new Pessoa { Id = id });
                return RedirectToAction("Index");
            }
            catch { return View(); }
        }

        public ActionResult ListarCidadesAutocomplete(string filtro)
        {
            return Json(this.cidades.ListFiltro(filtro).Select(item => new
            {
                data = item.Id,
                value = item.Nome
            }).ToArray(), JsonRequestBehavior.AllowGet);
        }
    }
}
