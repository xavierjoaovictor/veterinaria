using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veterinaria.DAO;
using Veterinaria.Models;

namespace Veterinaria.Controllers
{
    public class PetController : Controller
    {
        private PetDAO pets;
        private PessoaDAO pessoas;

        private ClienteDAO clientes;

        private Connection connection;

        public PetController()
        {
            this.connection = new Connection();
            this.pets = new PetDAO(this.connection);
            this.pessoas = new PessoaDAO(this.connection);
            this.clientes = new ClienteDAO(this.connection);
        }

        // GET: Pet
        public ActionResult Index()
        {
            return View(this.pets.ListAll());
        }

        // GET: Pet/Details/5
        public ActionResult Details(int id)
        {
            return View(this.pets.Search(new Pet { Id = id }));
        }

        // GET: Pet/Create
        public ActionResult Create()
        {
            var pet = new Pet();
            pet.ListaClientes = GetClientes();
            return View(pet);
        }
        private IEnumerable<SelectListItem> GetClientes()
        {
            var listaClientes = this.pessoas.ListAllClientes().Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Cliente?.Id.ToString(),
                                    Text = x.Nome
                                });

            return new SelectList(listaClientes, "Value", "Text");
        }

        // POST: Pet/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                this.pets.Insert(new Pet
                {
                    Nome = collection["nome"].ToString(),
                    DataNascimento = DateTime.Parse(collection["data_nascimento"]),
                    Raca = collection["raca"].ToString(),
                    Sexo = int.Parse(collection["sexo"]),
                    Tipo = int.Parse(collection["tipo"]),
                    Cliente = new Pessoa() { Cliente = new Cliente() { Id = int.Parse(collection["idcliente"]) } }
                });
                return RedirectToAction("Index");
            }
            catch (Exception) { return View(); }
        }

        // GET: Pet/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Pet/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                this.pets.Update(new Pet
                {
                    Id = int.Parse(collection["id"]),
                    Nome = collection["nome"].ToString(),
                    DataNascimento = DateTime.Parse(collection["data_nascimento"]),
                    Raca = collection["raca"],
                    Sexo = int.Parse(collection["sexo"]),
                    Tipo = int.Parse(collection["tipo"]),
                    Cliente = new Pessoa() { Cliente = new Cliente() { Id = int.Parse(collection["idcliente"]) } }
                });
                return RedirectToAction("Index");
            }
            catch { return View(); }
        }

        // GET: Pet/Delete/5
        public ActionResult Delete(int id)
        {
            this.pets.Delete(new Pet { Id = id });
            return RedirectToAction("Index");
        }

        // POST: Pet/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                this.pets.Delete(new Pet { Id = id });
                return RedirectToAction("Index");
            }
            catch { return View(); }
        }
    }
}
