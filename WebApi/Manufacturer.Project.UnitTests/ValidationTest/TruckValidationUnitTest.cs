using Manufacturer.Project.Core.Services;
using Manufacturer.Project.Domain.Models;
using Manufacturer.Project.Domain.Repositories;
using Manufacturer.Project.Domain.Services;
using Moq;

namespace Manufacturer.Project.UnitTests.ValidationTests
{
    public class TruckValidationUnitTest
    {
        private Mock<ITruckRepository> _truckRepositoryMock;
        private Mock<ITruckTypeService> _truckTypeServiceMock;
        private Mock<IPlantOptionsService> _plantOptionsServiceMock;
        private Mock<IColorService> _colorServiceMock;
        private TruckService _truckService;

        [SetUp]
        public void Setup()
        {
            _truckRepositoryMock = new Mock<ITruckRepository>();
            _truckTypeServiceMock = new Mock<ITruckTypeService>();
            _plantOptionsServiceMock = new Mock<IPlantOptionsService>();
            _colorServiceMock = new Mock<IColorService>();

            // Simulando uma lista de caminhões existente para validações de chassi duplicado
            _truckRepositoryMock.Setup(repo => repo.GetAllNoTracking()).Returns(new List<Truck>
            {
                new Truck { Id = 1, ChassisCode = "1HGCM82633A123456", ManufacturerYear = 2022 }
            }.AsQueryable());

            _truckService = new TruckService(
                _truckRepositoryMock.Object,
                _truckTypeServiceMock.Object,
                _plantOptionsServiceMock.Object,
                _colorServiceMock.Object);
        }

        [Test]
        public void Save_ValidTruck_ShouldSaveSuccessfully()
        {
            var truck = new Truck
            {
                ChassisCode = "2T3ZF4DV3BW123789",
                ManufacturerYear = 2023,
                Created = DateTime.Now
            };

            _truckRepositoryMock.Setup(repo => repo.Insert(It.IsAny<Truck>()));

            // Act
            var result = _truckService.Save(truck);

            // Assert
            Assert.IsNotNull(result, "Truck should be saved successfully.");
        }

        [Test]
        public void Save_EmptyChassisCode_ShouldThrowException()
        {
            var truck = new Truck
            {
                ChassisCode = "   ",  // Chassi vazio ou com espaços
                ManufacturerYear = 2023,
                Created = DateTime.Now
            };

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _truckService.Save(truck));
            Assert.AreEqual("Valor do Chassi inválido, não permitido somente espaço vazio!", ex.Message);
        }

        [Test]
        public void Save_DuplicateChassisCode_ShouldThrowException()
        {
            var truck = new Truck
            {
                ChassisCode = "1HGCM82633A123456",  // Chassi duplicado
                ManufacturerYear = 2023,
                Created = DateTime.Now
            };

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _truckService.Save(truck));
            Assert.AreEqual("Já encontrado uma caminhão com este Chassi cadastrado: 1HGCM82633A123456, por favor verifique o Chassi digitado e tente novamente!", ex.Message);
        }

        [Test]
        public void Save_ManufacturerYearGreaterThanCurrentYear_ShouldThrowException()
        {
            var nextYear = DateTime.Now.Year + 1;
            var truck = new Truck
            {
                ChassisCode = "2T3ZF4DV3BW123789",
                ManufacturerYear = nextYear,  // Ano maior que o atual
                Created = DateTime.Now
            };

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _truckService.Save(truck));
            Assert.AreEqual($"O ano digitado {nextYear} é maior do que ano atual: {DateTime.Now.Year}", ex.Message);
        }

        [Test]
        public void Save_ManufacturerYearLessThan2000_ShouldThrowException()
        {
            var truck = new Truck
            {
                ChassisCode = "3VWDX7AJ7DM123654",
                ManufacturerYear = 1999,  // Ano menor que o permitido
                Created = DateTime.Now
            };

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _truckService.Save(truck));
            Assert.AreEqual("O ano digitado 1999 é menor do que o limite permitido de 2000!", ex.Message);
        }
    }
}
