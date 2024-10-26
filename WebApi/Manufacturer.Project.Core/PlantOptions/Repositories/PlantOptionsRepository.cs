namespace Manufacturer.Project.Core.Repositories
{
    using Manufacturer.Project.Core.Base;
    using Manufacturer.Project.Core.DataContext;
    using Manufacturer.Project.Domain.Models;
    using Manufacturer.Project.Domain.Repositories;

    public class PlantOptionsRepository : BaseRepository<PlantOptions, long, ProjectDBContext>, IPlantOptionsRepository
    {
        /// <summary>
        /// Inicializa uma nova instância do DBContext na aplicação <see cref="PlantOptionsRepository"/> class.
        /// </summary>
        /// <param name="objContext">Database context.</param>
        public PlantOptionsRepository(ProjectDBContext objContext)
            : base(objContext)
        {            
        }
    }
}
