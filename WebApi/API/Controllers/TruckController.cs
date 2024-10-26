namespace BaseWebApi.Controllers
{
    using Manufacturer.BaseWebApi.API;
    using Manufacturer.Project.Domain.Models;
    using Manufacturer.Project.Domain.Services;
    using Microsoft.AspNet.OData.Routing;

    [ODataRoutePrefix("Truck")]
    public class TruckController : BaseODataController<Truck, ITruckService, long>
    {
        /// <summary>
        /// Initialize a new instance of controller <see cref="TruckController"/> class.
        /// </summary>
        /// <param name="objTruckService">Service from controller.</param>
        public TruckController(ITruckService objTruckService)
            : base(objTruckService)
        {
        }
    }
}