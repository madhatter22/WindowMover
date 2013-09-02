using System;
using WindowMover;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;

namespace WindowMover.Tests
{
    [TestClass]
    public class ConfigViewModelTest
    {
        [TestMethod]
        public void Constructor_NullSettingsService_ThrowsException()
        {
            Action work = () => { new ConfigViewModel(null); };

            Executing.This(work)
                     .Should()
                     .Throw<ArgumentNullException>()
                     .And.Exception.ParamName.Should().Be.EqualTo("settings");
        }
    }
}
