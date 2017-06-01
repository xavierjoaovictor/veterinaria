using Microsoft.VisualStudio.TestTools.UnitTesting;
using Veterinaria.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Models;

namespace Veterinaria.DAO.Tests
{
    [TestClass()]
    public class FuncionarioDAOTests
    {
        private Funcionario veterinario;
        private Funcionario atendente;
        private FuncionarioDAO funcionarios;

        [TestInitialize()]
        public void Startup()
        {
            this.InstantiateDependenciesDAO();
            this.InstantiateDependenciesObjects();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            this.ClearDatabase();
            this.DisposeDependenciesDAO();
        }

        private void InstantiateDependenciesDAO()
        {
            this.funcionarios = new FuncionarioDAO(new Connection());
        }

        private void InstantiateDependenciesObjects()
        {
            this.veterinario = new Funcionario
            {
                Id = 1,
                Salario = 250,
                NumeroContrato = "1111",
                NumeroCRMV = "2222",
                DataAdmisao = DateTime.Now,
                Funcao = Models.Enums.FuncaoFuncionario.Veterinario
            };
            this.atendente = new Funcionario
            {
                Id = 2,
                Salario = 250,
                NumeroContrato = "1111",
                NumeroCRMV = "2222",
                DataAdmisao = DateTime.Now,
                Funcao = Models.Enums.FuncaoFuncionario.Atendente
            };
        }

        private void DisposeDependenciesDAO()
        {
            this.funcionarios.Dispose();
        }

        private void ClearDatabase()
        {
            this.funcionarios.DeleteAll();
        }


        //[TestMethod()]
        //public void InsertFailsTest()
        //{
        //    this.funcionarios.Insert(this.veterinario);

        //    Assert.AreEqual(-1, this.funcionarios.Insert(this.veterinario));
        //}

        //[TestMethod()]
        //public void DeleteTest()
        //{
        //    this.funcionarios.Insert(this.veterinario);

        //    Assert.IsTrue(this.funcionarios.Delete(this.veterinario));
        //}

        //[TestMethod()]
        //public void DeleteFailsTest()
        //{
        //    Assert.IsFalse(this.funcionarios.Delete(this.veterinario));
        //}
    }
}