using Manufacturer.Project.Core.Services;
using Manufacturer.Project.Domain.Models;
using Manufacturer.Project.Domain.Repositories;
using Manufacturer.Project.Domain.Services;
using Moq;
using NUnit.Framework;
using System;

namespace Manufacturer.Project.UnitTests.MigrationTest
{
    public class TruckTypeMigrationUnitTest
    {
        private Mock<ITruckTypeRepository> _truckTypeRepositoryMock;
        private ITruckTypeService _truckTypeService;

        [SetUp]
        public void Setup()
        {
            _truckTypeRepositoryMock = new Mock<ITruckTypeRepository>();
            _truckTypeService = new TruckTypeService(_truckTypeRepositoryMock.Object);
        }

        [Test]
        public void ApplyMigrations_Test()
        {
            var truckType = new TruckType
            {
                Type = "Heavy Duty",
                Created = DateTime.Now
            };

            var objectTest = _truckTypeService.Save(truckType);
            if (objectTest == null)
            {
                throw new Exception("N�o foi poss�vel criar um tipo de caminh�o, erro de banco de dados!");
            }

            _truckTypeRepositoryMock.Verify(repo => repo.Insert(truckType), Times.Once, "M�todo de Inserir no banco de dados falhou!");
            _truckTypeRepositoryMock.Verify(repo => repo.SaveChanges(), Times.Once, "M�todo de SaveChanges falhou no banco de dados!");
        }
    }
}
