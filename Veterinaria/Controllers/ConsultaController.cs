using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veterinaria.DAO;
using Veterinaria.Models;
using Veterinaria.Models.Enums;

namespace Veterinaria.Controllers
{
    public class ConsultaController : Controller
    {
        private PetDAO pets;
        private PessoaDAO pessoas;

        private ConsultaDAO consultas;
        private Connection connection;

        private DiagnosticoDAO diagnosticos;

        public ConsultaController()
        {
            this.connection = new Connection();
            this.pets = new PetDAO(this.connection);
            this.pessoas = new PessoaDAO(this.connection);
            this.consultas = new ConsultaDAO(this.connection);
            this.diagnosticos = new DiagnosticoDAO(this.connection);
        }

        // GET: Consulta
        public ActionResult Index()
        {
            ViewBag.Message = "Consultas";
            return View(this.consultas.ListAll());
        }

        // GET: Consulta/Details/5
        public ActionResult Details(int id)
        {
            return View(this.consultas.Search(new Consulta { Id = id }));
        }

        // GET: Consulta/Create
        public ActionResult Create()
        {
            var consulta = new Consulta();
            consulta.Data = DateTime.Today;
            return View(consulta);
        }

        // POST: Consulta/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                this.consultas.Insert(new Consulta
                {
                    Data = DateTime.Parse(collection["data"]),
                    Status = (StatusConsulta)int.Parse(collection["status"]),
                    Pet = this.pets.Search(new Pet() { Id = int.Parse(collection["idpet"]) }),
                    Veterinario = new Pessoa() { Funcionario = new Funcionario() { Id = int.Parse(collection["idveterinario"]) } },
                    Atendente = new Pessoa() { Funcionario = new Funcionario() { Id = int.Parse(collection["idatendente"]) } }
                });
                return RedirectToAction("Index");
            }
            catch (Exception) { return View(new Consulta()); }
        }

        // GET: Consulta/Edit/5
        public ActionResult Edit(int id)
        {
            return View(this.consultas.Search(new Consulta { Id = id }));
        }

        // POST: Consulta/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            Consulta consulta = new Consulta();
            try
            {
                this.consultas.Update(consulta = new Consulta {
                    Id = int.Parse(collection["idconsulta"]),
                    Data = DateTime.Parse(collection["data"]),
                    Status = (StatusConsulta)int.Parse(collection["status"]),
                    Pet = this.pets.Search(new Pet() { Id = int.Parse(collection["idpet"]) }),
                    Veterinario = this.pessoas.Search(new Pessoa() { Funcionario = new Funcionario() { Id = int.Parse(collection["idveterinario"]) } }),
                    Atendente = this.pessoas.Search(new Pessoa() { Funcionario = new Funcionario() { Id = int.Parse(collection["idatendente"]) } }),
                    Diagnostico = new Diagnostico() { Id = int.Parse(collection["iddiagnostico"]),
                                                      Posologia = collection["posologia"],
                                                      Medicacao = collection["medicacao"],
                                                      Descricao = collection["descricao"] }
                });
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Erro = ex.Message;
                return View(consulta);
             }
        }

        // GET: Consulta/Delete/5
        public ActionResult Delete(int id)
        {
            this.consultas.Delete(new Consulta { Id = id });
            return RedirectToAction("Index");
        }

        // POST: Consulta/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                this.consultas.Delete(new Consulta { Id = id });
                return RedirectToAction("Index");
            }
            catch { return View(); }
        }

        public ActionResult ListarPets()
        {
            return Json(this.pets.ListAll().Select(item => new
            {
                data = item.Id,
                value = item.Cliente.Nome + " - " + item.Nome
            }).ToArray(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarAtendentes()
        {
            return Json(this.pessoas.ListAllFuncionarios()
                .Where(pessoa => pessoa.Funcionario.Funcao == FuncaoFuncionario.Atendente)
                .Select(item => new
                {
                    data = item.Funcionario?.Id,
                    value = item.Nome
                }).ToArray(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarVeterinarios()
        {
            return Json(this.pessoas.ListAllFuncionarios()
                .Where(pessoa => pessoa.Funcionario.Funcao == FuncaoFuncionario.Veterinario)
                .Select(item => new
                {
                    data = item.Funcionario?.Id,
                    value = item.Nome
                }).ToArray(), JsonRequestBehavior.AllowGet);
        }
    }
}
