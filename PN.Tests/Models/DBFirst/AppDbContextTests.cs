using Microsoft.VisualStudio.TestTools.UnitTesting;
using PN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PN.Models.Tests
{
    [TestClass]
    public class AppDbContextTests
    {
        [TestMethod]
        public async void StoredProcedureRegisterUserTest()
        {
            // Ini
            AppDbContext db = new AppDbContext();

            // Body
            var resoult = await db.StoredProcedureRegisterUser("XxX", "m");

            // Asset
            Assert.IsTrue(resoult);
        }
    }
}