using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankingApp.Controllers;
using System.Web.Mvc;

namespace BankingApp.Tests.Controllers
{
    [TestClass]
    public class LoginControllerTest
    {
        private LoginController controller;
        private ViewResult result;

        [TestInitialize]
        public void SetupContext()
        {
            controller = new LoginController();
            result = controller.Index() as ViewResult;
        }

        [TestMethod]
        public void IndexViewModelNotNull()
        {
            Assert.IsNotNull(result.Model);
        }
       
    }
}
