using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Veterinaria;
using Veterinaria.Controllers;

namespace Veterinaria.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private HomeController controller;

        [TestInitialize()]
        public void Startup()
        {
            // Arrange
            this.controller = new HomeController();
        }

        [TestMethod]
        public void Index()
        {
            // Act / Assert
            Assert.IsNotNull(controller.Index() as ViewResult);
        }

        [TestMethod]
        public void About()
        {
            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Act / Assert
            Assert.IsNotNull(controller.Contact() as ViewResult);
        }
    }
}
