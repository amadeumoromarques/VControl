using Manufacturer.Project.Core.Services;
using Manufacturer.Project.Domain.Models;
using Manufacturer.Project.Domain.Repositories;
using Manufacturer.Project.Domain.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Manufacturer.Project.UnitTests.MigrationTest
{
    public class TruckMigrationUnitTest
    {
        private Mock<ITruckTypeRepository> _truckTypeRepositoryMock;
        private ITruckTypeService objTruckTypeService;
        private Mock<IPlantOptionsRepository> _plantOptionsRepositoryMock;
        private IPlantOptionsService objPlantOptionsService;
        private Mock<IColorRepository> _colorRepositoryMock;
        private IColorService objColorService;

        private Mock<ITruckRepository> _truckRepositoryMock;
        private ITruckService _truckService;

        [SetUp]
        public void Setup()
        {
            _truckRepositoryMock = new Mock<ITruckRepository>();
            _truckTypeRepositoryMock = new Mock<ITruckTypeRepository>();
            _plantOptionsRepositoryMock = new Mock<IPlantOptionsRepository>();
            _colorRepositoryMock = new Mock<IColorRepository>();

            // Simula o retorno dos dados para GetAllNoTracking em cada serviço
            _truckTypeRepositoryMock.Setup(repo => repo.GetAllNoTracking())
                .Returns(new List<TruckType> { new TruckType { Id = 1, Type = "Heavy Duty" } }.AsQueryable());

            _plantOptionsRepositoryMock.Setup(repo => repo.GetAllNoTracking())
                .Returns(new List<PlantOptions> { new PlantOptions { Id = 1, Key = "PLANT123", DisplayName = "Main Plant" } }.AsQueryable());

            _colorRepositoryMock.Setup(repo => repo.GetAllNoTracking())
                .Returns(new List<Color> { new Color { Id = 1, Name = "Red", SapCode = "C456", HexaColor = "#FF0000" } }.AsQueryable());

            // Inicializa os serviços com os repositórios mockados
            objTruckTypeService = new TruckTypeService(_truckTypeRepositoryMock.Object);
            objPlantOptionsService = new PlantOptionsService(_plantOptionsRepositoryMock.Object);
            objColorService = new ColorService(_colorRepositoryMock.Object);

            // Inicializa o TruckService com todos os mocks
            _truckService = new TruckService(
                _truckRepositoryMock.Object,
                objTruckTypeService,
                objPlantOptionsService,
                objColorService);
        }

        [Test]
        public void ApplyMigrations_Test()
        {
            var allTruckType = objTruckTypeService.GetAllNoTracking().ToList();
            var allPlantOptions = objPlantOptionsService.GetAllNoTracking().ToList();
            var allColors = objColorService.GetAllNoTracking().ToList();

            var truck = new Truck
            {
                Created = DateTime.Now,
                ChassisCode = "1HGCM82633A123456",
                ManufacturerYear = 2023,
                IdColor = allColors[0].Id,
                IdTruckType = allTruckType[0].Id,
                IdPlantOptions = allPlantOptions[0].Id
            };

            var objectTest = _truckService.Save(truck);

            if (objectTest == null)
            {
                throw new Exception("Não foi possível criar um caminhão, erro de banco de dados!");
            }

            _truckRepositoryMock.Verify(repo => repo.Insert(truck), Times.Once, "Método de Inserir no banco de dados falhou!");
            _truckRepositoryMock.Verify(repo => repo.SaveChanges(), Times.Once, "Método de SaveChanges falhou no banco de dados!");
        }
    }
}
