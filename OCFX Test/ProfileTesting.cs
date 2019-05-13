using Microsoft.VisualStudio.TestTools.UnitTesting;
using OCFX.DataModels;
using System;

namespace OCFX_Test
{
    [TestClass]
    public class ProfileTests
    {
        [TestMethod]
        public void SimpleTest()
        {
            Profile result = new Profile
            {
                Id = 1,
                FirstName = "Tony",
                LastName = "T"
            };

            Assert.IsNotNull(result);
            
        }

        [TestMethod]
        public void AgeTest()
        {
            Profile result = new Profile
            {
                DOB = new DateTime(1984, 5, 18)
            };

            var age = result.Age;
            var projectedAge = 35;

            Assert.IsNotNull(age);
            Assert.AreEqual(age, projectedAge);
        }
    }
}
