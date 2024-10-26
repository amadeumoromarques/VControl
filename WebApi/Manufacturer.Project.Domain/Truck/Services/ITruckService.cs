namespace Manufacturer.Project.Domain.Services
{
    using Manufacturer.Project.Domain.Models;
    using Manufacturer.Project.Domain.Infra;

    public interface ITruckService : IDataService<Truck, long>
    {
    }
}