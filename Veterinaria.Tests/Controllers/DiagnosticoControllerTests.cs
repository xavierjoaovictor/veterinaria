using Microsoft.VisualStudio.TestTools.UnitTesting;
using Veterinaria.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.DAO;
using Veterinaria.Models;
using System.Web.Mvc;
using System.Collections.ObjectModel;
using Veterinaria.Models.Enums;

namespace Veterinaria.Controllers.Tests
{
    [TestClass()]
    public class DiagnosticoControllerTests
    {
        private DiagnosticoController controller;

        private Diagnostico diagnostico;
        private DiagnosticoDAO diagnosticos;

        private FormCollection form;

        [TestInitialize()]
        public void Startup()
        {
            this.InstantiateDependenciesDAO();
            this.InstantiateDependenciesObjects();
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.ClearDatabase();
            this.DisposeDependenciesDAO();
        }

        private void InstantiateDependenciesDAO()
        {
            this.diagnosticos = new DiagnosticoDAO(new Connection());
        }

        private void InstantiateDependenciesObjects()
        {
            this.controller = new DiagnosticoController();
            this.diagnostico = new Diagnostico
            {
                Id = 1,
                Posologia = "xD",
                Medicacao = "Med",
                Descricao = "Desc"
            };
            this.form = new FormCollection();
            this.form.Add("id", this.diagnostico.Id.ToString());
            this.form.Add("posologia", this.diagnostico.Posologia);
            this.form.Add("medicacao", this.diagnostico.Medicacao);
            this.form.Add("descricao", this.diagnostico.Descricao);
        }

        private void DisposeDependenciesDAO()
        { 
            this.diagnosticos.Dispose();
        }

        private void ClearDatabase()
        {
            this.diagnosticos.DeleteAll();
        }

        //[TestMethod()]
        //public void IndexTest()
        //{
        //    this.diagnosticos.Insert(this.diagnostico);
        //    this.diagnostico.Id = 2;
        //    this.diagnosticos.Insert(this.diagnostico);

        //    var result = this.controller.Index() as ViewResult;
        //    var diagnosticosCollection = (List<Diagnostico>)result.Model;

        //    Assert.AreEqual(2, diagnosticosCollection.Count);
        //}

        //[TestMethod()]
        //public void DetailsTest()
        //{
        //    this.diagnosticos.Insert(this.diagnostico);

        //    var result = this.controller.Details(1) as ViewResult;
        //    Diagnostico diagnosticoIstance = (Diagnostico)result.Model;

        //    Assert.AreEqual("Desc", diagnosticoIstance.Descricao);
        //}

        //[TestMethod()]
        //public void CreateTest()
        //{
        //    this.controller.Create(this.form);
        //    Assert.AreEqual(1, this.diagnosticos.ListAll().Count);
        //}

        //[TestMethod()]
        //public void EditTest()
        //{
        //    this.diagnosticos.Insert(this.diagnostico);

        //    this.form["descricao"] = "Something";
        //    this.controller.Edit(this.diagnostico.Id, this.form);

        //    Assert.AreEqual("Something", this.diagnosticos.Search(this.diagnostico).Descricao);
        //}

        //[TestMethod()]
        //public void DeleteTest()
        //{
        //    this.diagnosticos.Insert(this.diagnostico);

        //    this.controller.Delete(this.diagnostico.Id);

        //    Assert.IsNull(this.diagnosticos.Search(this.diagnostico));
        //}
    }
}