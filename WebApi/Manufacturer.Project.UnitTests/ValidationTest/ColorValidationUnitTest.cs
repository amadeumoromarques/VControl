using Manufacturer.Project.Core.Services;
using Manufacturer.Project.Domain.Models;
using Manufacturer.Project.Domain.Repositories;
using Moq;

namespace Manufacturer.Project.UnitTests.ValidationTests
{
    public class ColorValidationUnitTest
    {
        private Mock<IColorRepository> _colorRepositoryMock;
        private ColorService _colorService;

        [SetUp]
        public void Setup()
        {
            _colorRepositoryMock = new Mock<IColorRepository>();

            // Simulando uma lista de cores existente para valida��es de c�digo SAP e HexaColor duplicados
            _colorRepositoryMock.Setup(repo => repo.GetAllNoTracking()).Returns(new List<Color>
            {
                new Color { Id = 1, Name = "Red", SapCode = "R123", HexaColor = "#FF0000" },
                new Color { Id = 2, Name = "Green", SapCode = "G123", HexaColor = "#00FF00" }
            }.AsQueryable());

            _colorService = new ColorService(_colorRepositoryMock.Object);
        }

        [Test]
        public void Save_ValidColor_ShouldSaveSuccessfully()
        {
            var color = new Color
            {
                Name = "Blue",
                SapCode = "B123",
                HexaColor = "#0000FF",
                Created = DateTime.Now
            };

            _colorRepositoryMock.Setup(repo => repo.Insert(It.IsAny<Color>()));

            var result = _colorService.Save(color);

            Assert.IsNotNull(result, "Color should be saved successfully.");
        }


        [Test]
        public void Save_EmptyName_ShouldThrowException()
        {
            var color = new Color
            {
                Name = "   ",  // Nome vazio ou com espa�os
                SapCode = "C123",
                HexaColor = "#FFFFFF",
                Created = DateTime.Now
            };

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _colorService.Save(color));
            Assert.AreEqual("Valor do Nome da Cor inv�lido, n�o permitido somente espa�o vazio!", ex.Message);
        }

        [Test]
        public void Save_EmptySapCode_ShouldThrowException()
        {
            var color = new Color
            {
                Name = "Purple",
                SapCode = "   ",  // C�digo SAP vazio ou com espa�os
                HexaColor = "#A020F0",
                Created = DateTime.Now
            };

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _colorService.Save(color));
            Assert.AreEqual("Valor do C�digo da Cor inv�lido, n�o permitido somente espa�o vazio!", ex.Message);
        }

        [Test]
        public void Save_EmptyHexaColor_ShouldThrowException()
        {
            var color = new Color
            {
                Name = "Yellow",
                SapCode = "Y123",
                HexaColor = "   ",  // Hexadecimal vazio ou com espa�os
                Created = DateTime.Now
            };

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _colorService.Save(color));
            Assert.AreEqual("Valor do Hexadecimal da cor inv�lido, n�o permitido somente espa�o vazio!", ex.Message);
        }

        [Test]
        public void Save_DuplicateSapCode_ShouldThrowException()
        {
            var color = new Color
            {
                Name = "Blue",
                SapCode = "R123",  // C�digo SAP duplicado
                HexaColor = "#0000FF",
                Created = DateTime.Now
            };

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _colorService.Save(color));
            Assert.AreEqual("J� encontrado uma Cor com este C�digo: R123, por favor altere o c�digo e tente novamente!", ex.Message);
        }

        [Test]
        public void Save_DuplicateHexaColor_ShouldThrowException()
        {
            var color = new Color
            {
                Name = "Blue",
                SapCode = "B456",
                HexaColor = "#FF0000",  // Hexadecimal duplicado
                Created = DateTime.Now
            };

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _colorService.Save(color));
            Assert.AreEqual("J� encontrado uma Cor com este Hexadecimal: #FF0000, por favor altere o c�digo e tente novamente!", ex.Message);
        }
    }
}
