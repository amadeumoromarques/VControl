using Manufacturer.Project.Core.Services;
using Manufacturer.Project.Domain.Models;
using Manufacturer.Project.Domain.Repositories;
using Manufacturer.Project.Domain.Services;
using Moq;
using NUnit.Framework;
using System;

namespace Manufacturer.Project.UnitTests.MigrationTest
{
    public class PlantOptionsMigrationUnitTest
    {
        private Mock<IPlantOptionsRepository> _plantOptionsRepositoryMock;
        private IPlantOptionsService _plantOptionsService;

        [SetUp]
        public void Setup()
        {
            _plantOptionsRepositoryMock = new Mock<IPlantOptionsRepository>();
            _plantOptionsService = new PlantOptionsService(_plantOptionsRepositoryMock.Object);
        }

        [Test]
        public void ApplyMigrations_Test()
        {
            var plantOption = new PlantOptions
            {
                Key = "OPT123",
                DisplayName = "Option 123",
                Created = DateTime.Now
            };

            var objectTest = _plantOptionsService.Save(plantOption);
            if (objectTest == null)
            {
                throw new Exception("Não foi possível criar uma opção de planta, erro de banco de dados!");
            }

            _plantOptionsRepositoryMock.Verify(repo => repo.Insert(plantOption), Times.Once, "Método de Inserir no banco de dados falhou!");
            _plantOptionsRepositoryMock.Verify(repo => repo.SaveChanges(), Times.Once, "Método de SaveChanges falhou no banco de dados!");
        }
    }
}
