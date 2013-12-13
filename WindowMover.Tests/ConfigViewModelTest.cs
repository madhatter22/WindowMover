using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;
using WindowMover.Models;
using WindowMover.Services;
using WindowMover.ViewModels;

namespace WindowMover.Tests
{
    [TestClass]
    public class ConfigViewModelTest
    {
        private Mock<ISettingsService> _settingsServiceMock;
        private ConfigViewModel _viewModel;
        private List<string> _changedProperties;
        
        [TestInitialize]
        public void SetUp()
        {
            _settingsServiceMock = new Mock<ISettingsService>();
            _changedProperties = new List<string>();
        }

        [TestCleanup]
        public void TearDown()
        {
            if (_viewModel != null) _viewModel.PropertyChanged -= ViewModelPropertyChanged;
        }

        [TestMethod]
        public void Constructor_NullSettingsService_ThrowsException()
        {
            Action work = () => { new ConfigViewModel(null); };

            Executing.This(work)
                     .Should()
                     .Throw<ArgumentNullException>()
                     .And.Exception.ParamName.Should().Be.EqualTo("settings");
        }

        [TestMethod]
        public void Constructor_Initializes_Correctly()
        {
            //arrange
            _settingsServiceMock.Setup(x => x.GetAllModifiers())
                .Returns(GetAllModifiers());

            _settingsServiceMock.Setup(x => x.GetSavedModifiers())
                .Returns(new List<KeyModifiers>());

            _settingsServiceMock.Setup(x => x.GetDefaultKey())
                .Returns('X');

            //act
            _viewModel = new ConfigViewModel(_settingsServiceMock.Object);

            //assert
            _viewModel.CurrentModifiers.Should().Contain(KeyModifiers.Nomod).And.Have.Count.EqualTo(1);
            _viewModel.AvailableModifiers.Should()
                .Have.SameSequenceAs(GetAllModifiers().Where(m => m != KeyModifiers.Nomod));
            _viewModel.DefaultKey.Should().Be.EqualTo('X');
            _viewModel.SaveCommand.Should().Not.Be.Null();
        }

        [TestMethod]
        public void AddModifier_RemovesNomod_AndCorrectlyNotifies()
        {
            //arrange
            _settingsServiceMock.Setup(x => x.GetAllModifiers())
                .Returns(new List<KeyModifiers> { KeyModifiers.Alt });
            _settingsServiceMock.Setup(x => x.GetSavedModifiers())
                .Returns(new List<KeyModifiers>());
            _settingsServiceMock.Setup(x => x.GetDefaultKey())
                .Returns('A');

            _viewModel = new ConfigViewModel(_settingsServiceMock.Object);
            _viewModel.PropertyChanged += ViewModelPropertyChanged;

            //act
            _viewModel.AddModifier(KeyModifiers.Alt);

            //assert
            _viewModel.AvailableModifiers.Should().Not.Contain(KeyModifiers.Alt);
            _viewModel.CurrentModifiers.Should().Contain(KeyModifiers.Alt).And.Have.Count.EqualTo(1);
            _changedProperties.Should().Contain("CanSave").And.Have.Count.EqualTo(1);
        }

        private void ViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _changedProperties.Add(e.PropertyName);
        }

        private static IEnumerable<KeyModifiers> GetAllModifiers()
        {
            return Enum.GetNames(typeof(KeyModifiers))
                .Select(s => Enum.Parse(typeof(KeyModifiers), s, true))
                .Cast<KeyModifiers>()
                .ToList();
        } 
    }
}
