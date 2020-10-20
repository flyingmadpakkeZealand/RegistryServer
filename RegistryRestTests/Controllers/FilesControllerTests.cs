using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegistryRest.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegistryRest.Controllers.Tests
{
    [TestClass()]
    public class FilesControllerTests
    {
        private static Dictionary<string, List<FileEndPoint>> files;
        private static FilesController controller;
        private string baseFile = "File1";
        [ClassInitialize]
        public static void Init(TestContext context)
        {
            controller = new FilesController();

            files = controller.Files;
        }

        [TestMethod()]
        public void GetAllTest()
        {
            Assert.AreEqual(files[baseFile].Count, controller.GetAll(baseFile).Count());
        }

        [TestMethod()]
        public void GetTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PostTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PutTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }
    }
}