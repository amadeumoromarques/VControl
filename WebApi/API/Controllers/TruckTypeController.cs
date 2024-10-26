namespace BaseWebApi.Controllers
{
    using Manufacturer.BaseWebApi.API;
    using Manufacturer.Project.Domain.Models;
    using Manufacturer.Project.Domain.Services;
    using Microsoft.AspNet.OData.Routing;

    [ODataRoutePrefix("TruckType")]
    public class TruckTypeController : BaseODataController<TruckType, ITruckTypeService, long>
    {
        /// <summary>
        /// Initialize a new instance of controller <see cref="TruckTypeController"/> class.
        /// </summary>
        /// <param name="objTruckTypeService">Service from controller.</param>
        public TruckTypeController(ITruckTypeService objTruckTypeService)
            : base(objTruckTypeService)
        {
        }
    }
}