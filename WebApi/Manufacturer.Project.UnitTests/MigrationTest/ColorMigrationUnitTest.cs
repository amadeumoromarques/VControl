using Manufacturer.Project.Core.Services;
using Manufacturer.Project.Domain.Models;
using Manufacturer.Project.Domain.Repositories;
using Manufacturer.Project.Domain.Services;
using Moq;

namespace Manufacturer.Project.UnitTests.MigrationTest
{
    public class ColorMigrationUnitTest
    {
        private Mock<IColorRepository> _colorRepositoryMock;
        private IColorService _colorService;

        [SetUp]
        public void Setup()
        {
            _colorRepositoryMock = new Mock<IColorRepository>();
            _colorService = new ColorService(_colorRepositoryMock.Object);
        }

        [Test]
        public void ApplyMigrations_Test()
        {
            var color = new Color
            {
                Name = "Blue",
                SapCode = "C123",
                HexaColor = "#0000FF",
                Created = DateTime.Now
            };

            var objectTest = _colorService.Save(color);
            if (objectTest == null)
            {
                throw new Exception($"Não foi possível criar uma cor, erro de banco de dados!");
            }

            _colorRepositoryMock.Verify(repo => repo.Insert(color), Times.Once, "Método de Inserir no banco de dados falhou!");
            _colorRepositoryMock.Verify(repo => repo.SaveChanges(), Times.Once, "Método de SaveChanges falhou no banco de dados!");
        }
    }
}