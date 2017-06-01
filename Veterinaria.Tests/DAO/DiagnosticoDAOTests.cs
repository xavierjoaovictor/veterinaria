using Microsoft.VisualStudio.TestTools.UnitTesting;
using Veterinaria.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Models;
using Veterinaria.Models.Enums;

namespace Veterinaria.DAO.Tests
{
    [TestClass()]
    public class DiagnosticoDAOTests
    {
        private Diagnostico diagnostico;
        private DiagnosticoDAO diagnosticos;

        [TestInitialize()]
        public void Startup()
        {
            this.InstantiateDependenciesObjects();
            this.InstantiateDependenciesDAO();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            this.ClearDatabase();
            this.DisposeDependenciesDAO();
        }

        private void DisposeDependenciesDAO()
        {
            this.diagnosticos.Dispose();
        }

        private void InstantiateDependenciesDAO()
        {
            this.diagnosticos = new DiagnosticoDAO(new Connection());
        }

        private void InstantiateDependenciesObjects()
        {
            this.diagnostico = new Diagnostico
            {
                Id = 1,
                Posologia = "Poso",
                Medicacao = "Med",
                Descricao = "Desc"
            };
        }

        private void ClearDatabase()
        {
            this.diagnosticos.DeleteAll();
        }

        //[TestMethod()]
        //public void DeleteTest()
        //{
        //    this.diagnosticos.Insert(this.diagnostico);
        //    Assert.IsTrue(this.diagnosticos.Delete(this.diagnostico));
        //}
        
    }
}