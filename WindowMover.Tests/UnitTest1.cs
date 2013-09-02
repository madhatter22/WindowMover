using System;
using WindowMover;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WindowMover.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            KeyModifiers mod = KeyModifiers.Alt | KeyModifiers.Ctrl;
            Assert.IsTrue((int)mod == (0x0001 + 0x0002));
        }
    }
}
