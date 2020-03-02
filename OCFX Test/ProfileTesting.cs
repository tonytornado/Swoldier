using Microsoft.VisualStudio.TestTools.UnitTesting;
using OCFX.DataModels;
using System;

namespace OCFXTest
{
    [TestClass]
    public class ProfileTests
    {
        [TestMethod]
        public void SimpleProfileTest()
        {
            ProfileSheet result = new ProfileSheet
            {
                Id = 1,
                FirstName = "Tony",
                LastName = "T",
            };

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AgeTest()
        {
            ProfileSheet result = new ProfileSheet
            {
                Dob = new DateTime(1984, 5, 18)
            };

            var age = result.Age;
            var projectedAge = 35;

            Assert.IsNotNull(age);
            Assert.AreEqual(age, projectedAge);
        }
    }
}
