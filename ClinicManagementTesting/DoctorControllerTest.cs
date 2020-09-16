using Clinic_Management_API.Controllers;
using Clinic_Management_API.MODELS;
using Clinic_Management_API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace ClinicManagementTesting
{
    public class DoctorControllerTest
    {
        ClinicContext db;
        [SetUp]
        public void Setup()
        {
            var doc = new List<Doctor>
            {
                new Doctor{DoctorId=1,Name="Dummy 1",Email="DD1",Password="12"},
                new Doctor{DoctorId=2,Name="Dummy 2",Email="DD2",Password="23"},
                new Doctor{DoctorId=3,Name="Dummy 3",Email="DD3",Password="34"}

            };

            var docdata = doc.AsQueryable();
            var mockSet = new Mock<DbSet<Doctor>>();
            mockSet.As<IQueryable<Doctor>>().Setup(m => m.Provider).Returns(docdata.Provider);
            mockSet.As<IQueryable<Doctor>>().Setup(m => m.Expression).Returns(docdata.Expression);
            mockSet.As<IQueryable<Doctor>>().Setup(m => m.ElementType).Returns(docdata.ElementType);
            mockSet.As<IQueryable<Doctor>>().Setup(m => m.GetEnumerator()).Returns(docdata.GetEnumerator());

            var mockContext = new Mock<ClinicContext>();
            mockContext.Setup(c => c.Doctors).Returns(mockSet.Object);
            db = mockContext.Object;

        }

        [Test]
        public void GetDetailsTest()
        {
            var res = new Mock<DoctorRep>(db);
            DOCTORSCONTROLLER obj = new DOCTORSCONTROLLER(res.Object);
            var data = obj.Get();
            var okResult = data as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);

        }

        [Test]
        public void Add_Valid_Detail()
        {
            var res = new Mock<DoctorRep>(db);
            DOCTORSCONTROLLER obj = new DOCTORSCONTROLLER(res.Object);
            Doctor doc = new Doctor {Name = "Dummy 3", Email = "DD", Password = "12" };

            var data = obj.Post(doc);
            //var okResult = data as OkObjectResult;
            Assert.AreEqual("added", data);
        }





        [Test]
        public void GetDetailTest()
        {
            DoctorRep res = new DoctorRep(db);
            DOCTORSCONTROLLER obj = new DOCTORSCONTROLLER(res);
            var data = obj.Get1(1);
            var okResult = data as ObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }



        [Test]
        public void Update_Valid_Detail()
        {

            Doctor doc = new Doctor { Name = "Dummy 3", Email = "DD", Password = "12" };
            DoctorRep res = new DoctorRep(db);
            DOCTORSCONTROLLER obj = new DOCTORSCONTROLLER(res);
            var data = obj.Put(1, doc);
            var okResult = data as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }
        [Test]
        public void Delete_Valid_Detail()
        {
            DoctorRep loandata = new DoctorRep(db);
            DOCTORSCONTROLLER obj = new DOCTORSCONTROLLER(loandata);
            var data = obj.Delete(1);
            var okResult = data as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }

    }
}