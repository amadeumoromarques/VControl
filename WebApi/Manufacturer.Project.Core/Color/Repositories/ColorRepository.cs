namespace Manufacturer.Project.Core.Repositories
{
    using Manufacturer.Project.Core.Base;
    using Manufacturer.Project.Core.DataContext;
    using Manufacturer.Project.Domain.Models;
    using Manufacturer.Project.Domain.Repositories;

    public class ColorRepository : BaseRepository<Color, long, ProjectDBContext>, IColorRepository
    {
        /// <summary>
        /// Inicializa uma nova instância do DBContext na aplicação <see cref="ColorRepository"/> class.
        /// </summary>
        /// <param name="objContext">Database context.</param>
        public ColorRepository(ProjectDBContext objContext)
            : base(objContext)
        {            
        }
    }
}
