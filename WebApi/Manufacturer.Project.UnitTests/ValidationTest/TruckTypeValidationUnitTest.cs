using Manufacturer.Project.Core.Services;
using Manufacturer.Project.Domain.Models;
using Manufacturer.Project.Domain.Repositories;
using Moq;

namespace Manufacturer.Project.UnitTests.ValidationTests
{
    public class TruckTypeValidationUnitTest
    {
        private Mock<ITruckTypeRepository> _truckTypeRepositoryMock;
        private TruckTypeService _truckTypeService;

        [SetUp]
        public void Setup()
        {
            _truckTypeRepositoryMock = new Mock<ITruckTypeRepository>();

            // Simulando uma lista de tipos de caminhões existente para validações de duplicidade
            _truckTypeRepositoryMock.Setup(repo => repo.GetAllNoTracking()).Returns(new List<TruckType>
            {
                new TruckType { Id = 1, Type = "FH" },
                new TruckType { Id = 2, Type = "FM" }
            }.AsQueryable());

            _truckTypeService = new TruckTypeService(_truckTypeRepositoryMock.Object);
        }

        [Test]
        public void Save_ValidTruckType_ShouldSaveSuccessfully()
        {
            var truckType = new TruckType
            {
                Type = "VN",
                Created = DateTime.Now
            };

            _truckTypeRepositoryMock.Setup(repo => repo.Insert(It.IsAny<TruckType>()));

            // Act
            var result = _truckTypeService.Save(truckType);

            // Assert
            Assert.IsNotNull(result, "Truck type should be saved successfully.");
        }

        [Test]
        public void Save_EmptyType_ShouldThrowException()
        {
            var truckType = new TruckType
            {
                Type = "   ",  // Tipo vazio ou com espaços
                Created = DateTime.Now
            };

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _truckTypeService.Save(truckType));
            Assert.AreEqual("Valor do Tipo do Caminhão inválido, não permitido somente espaço vazio!", ex.Message);
        }

        [Test]
        public void Save_DuplicateType_ShouldThrowException()
        {
            var truckType = new TruckType
            {
                Type = "FH",  // Tipo duplicado
                Created = DateTime.Now
            };

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _truckTypeService.Save(truckType));
            Assert.AreEqual("Já encontrado um tipo de caminhão desses no banco de dados: FH, por favor altere o código e tente novamente!", ex.Message);
        }
    }
}
