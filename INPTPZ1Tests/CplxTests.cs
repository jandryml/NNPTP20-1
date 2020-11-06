using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace INPTPZ1.Mathematics.Tests
{
    [TestClass()]
    public class CplxTests
    {

        [TestMethod()]
        public void AddTest()
        {
            Complex a = new Complex(10, 20);
            Complex b = new Complex(1, 2);

            Complex actual = a + b;
            Complex shouldBe = new Complex(11, 22);

            Assert.AreEqual(shouldBe, actual);
        }
    }
}


