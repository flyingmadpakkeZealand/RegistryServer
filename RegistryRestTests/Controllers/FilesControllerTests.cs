using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegistryRest.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using ModelLib;

namespace RegistryRest.Controllers.Tests
{
    [TestClass()]
    public class FilesControllerTests
    {
        
        //private static Dictionary<string, List<FileEndPoint>> files;
        //private static FilesController controller;
        //private string baseFile = "File1";
        //[ClassInitialize]
        //public static void Init(TestContext context)
        //{
        //    controller = new FilesController();
        //    controller.Files.Add("File1", new List<FileEndPoint>()
        //    {
        //        new FileEndPoint(IPAddress.Loopback.ToString(), "4321"),
        //        new FileEndPoint(IPAddress.Loopback.ToString(), "4322")
        //    });

        //    files = controller.Files;
        //}


        //[TestMethod()]
        //public void GetAllTest()
        //{
        //    Assert.AreEqual(files[baseFile].Count, controller.GetAll(baseFile).Count());
        //}

        //[TestMethod()]
        //public void PostTest()
        //{
        //    string newFile = "FileT";
        //    int count = 0;

        //    FileEndPoint endPoint = new FileEndPoint("Test", "Test");
        //    if (files.ContainsKey(newFile))
        //    {
        //        count = files[newFile].Count;

        //        int index = files[newFile].FindIndex(listItem =>
        //            listItem.Port == endPoint.Port && listItem.IpAddress == endPoint.IpAddress);
        //        if (index != -1)
        //        {
        //            files[newFile].RemoveAt(index);
        //        }
        //    }


        //    controller.Post(newFile, endPoint);
        //    Assert.AreEqual(count + 1, files[newFile].Count); //Added.

        //    int last = files[newFile].Count - 1;
        //    Assert.AreEqual(files[newFile][last].Port, endPoint.Port); //Correct state.
        //    Assert.AreEqual(files[newFile][last].IpAddress, endPoint.IpAddress);

        //    Assert.AreEqual(0, controller.Post(newFile, endPoint)); //Duplicate rejected.
        //}

        //[TestMethod()]
        //public void DeRegisterTest()
        //{
        //    FileEndPoint fileTest = new FileEndPoint("Test2","Test2");
        //    List<FileEndPoint> listTest = new List<FileEndPoint>(){fileTest};

        //    string fileName = "FileTest2";
        //    files.Add(fileName,listTest);

        //    int response = controller.DeRegister(fileName, fileTest);

        //    Assert.AreEqual(files.ContainsKey(fileName),false); //Root file removed.
        //    Assert.AreEqual(1, response); //Correct response.

        //    response = controller.DeRegister(fileName, fileTest);
        //    Assert.AreEqual(0, response); //Root file has been removed, correct response.

        //    files[baseFile].Add(fileTest);
        //    response = controller.DeRegister(baseFile, fileTest); 
        //    Assert.AreEqual(files.ContainsKey(baseFile), true); //Peer is removed, root stays intact.
        //    Assert.AreEqual(1, response); //Correct response.
        //}
    }
}