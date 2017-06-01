using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veterinaria.DAO;
using Veterinaria.Models;

namespace Veterinaria.Controllers
{
    public class DiagnosticoController : Controller
    {
        private DiagnosticoDAO diagnosticos;

        public DiagnosticoController()
        {
            this.diagnosticos = new DiagnosticoDAO(new Connection());
        }

        // GET: Diagnostico
        public ActionResult Index()
        {
            ViewBag.Message = "Diagnosticos";
            return View(this.diagnosticos.ListAll());
        }

        // GET: Diagnostico/Details/5
        public ActionResult Details(int id)
        {
            return View(this.diagnosticos.Search(new Diagnostico { Id = id }));
        }

        // GET: Diagnostico/Create
        public ActionResult Create()
        {
            return View(new Consulta());
        }

        // POST: Diagnostico/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var diagnostico = new Diagnostico
                {
                    Posologia = collection["posologia"],
                    Medicacao = collection["medicacao"],
                    Descricao = collection["descricao"]
                };
                this.diagnosticos.Insert(diagnostico);
                return RedirectToAction("Index");
            }
            catch { return View(); }
        }

        // GET: Diagnostico/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Diagnostico/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var diagnostico = new Diagnostico
                {
                    Id = int.Parse(collection["id"]),
                    Posologia = collection["posologia"],
                    Medicacao = collection["medicacao"],
                    Descricao = collection["descricao"]
                };
                this.diagnosticos.Update(diagnostico);
                return RedirectToAction("Index");
            }
            catch { return View(); }
        }

        // GET: Diagnostico/Delete/5
        public ActionResult Delete(int id)
        {
            this.diagnosticos.Delete(new Diagnostico { Id = id });
            return RedirectToAction("Index");
        }

        // POST: Diagnostico/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                this.diagnosticos.Delete(new Diagnostico { Id = id });
                return RedirectToAction("Index");
            }
            catch { return View(); }
        }
    }
}
