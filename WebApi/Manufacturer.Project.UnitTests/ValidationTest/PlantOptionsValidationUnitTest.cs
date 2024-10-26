using Manufacturer.Project.Core.Services;
using Manufacturer.Project.Domain.Models;
using Manufacturer.Project.Domain.Repositories;
using Moq;

namespace Manufacturer.Project.UnitTests.ValidationTests
{
    public class PlantOptionsValidationUnitTest
    {
        private Mock<IPlantOptionsRepository> _plantOptionsRepositoryMock;
        private PlantOptionsService _plantOptionsService;

        [SetUp]
        public void Setup()
        {
            _plantOptionsRepositoryMock = new Mock<IPlantOptionsRepository>();

            // Simulando uma lista de opções de plantas existente para validações de duplicidade
            _plantOptionsRepositoryMock.Setup(repo => repo.GetAllNoTracking()).Returns(new List<PlantOptions>
            {
                new PlantOptions { Id = 1, Key = "0001", DisplayName = "Brasil" },
                new PlantOptions { Id = 2, Key = "0002", DisplayName = "Suécia" }
            }.AsQueryable());

            _plantOptionsService = new PlantOptionsService(_plantOptionsRepositoryMock.Object);
        }

        [Test]
        public void Save_ValidPlantOptions_ShouldSaveSuccessfully()
        {
            var plantOption = new PlantOptions
            {
                Key = "0005",
                DisplayName = "Alemanha",
                Created = DateTime.Now
            };

            _plantOptionsRepositoryMock.Setup(repo => repo.Insert(It.IsAny<PlantOptions>()));

            // Act
            var result = _plantOptionsService.Save(plantOption);

            // Assert
            Assert.IsNotNull(result, "Plant option should be saved successfully.");
        }

        [Test]
        public void Save_EmptyKey_ShouldThrowException()
        {
            var plantOption = new PlantOptions
            {
                Key = "   ",  // Código da planta vazio ou com espaços
                DisplayName = "Itália",
                Created = DateTime.Now
            };

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _plantOptionsService.Save(plantOption));
            Assert.AreEqual("Valor do Código da Planta inválido, não permitido somente espaço vazio!", ex.Message);
        }

        [Test]
        public void Save_DuplicateKey_ShouldThrowException()
        {
            var plantOption = new PlantOptions
            {
                Key = "0001",  // Código da planta duplicado
                DisplayName = "Portugal",
                Created = DateTime.Now
            };

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _plantOptionsService.Save(plantOption));
            Assert.AreEqual("Já encontrado uma planta com este código: 0001, por favor altere o código e tente novamente!", ex.Message);
        }
    }
}
