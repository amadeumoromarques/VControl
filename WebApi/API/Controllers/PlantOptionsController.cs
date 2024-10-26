namespace BaseWebApi.Controllers
{
    using Manufacturer.BaseWebApi.API;
    using Manufacturer.Project.Domain.Models;
    using Manufacturer.Project.Domain.Services;
    using Microsoft.AspNet.OData.Routing;

    [ODataRoutePrefix("PlantOptions")]
    public class PlantOptionsController : BaseODataController<PlantOptions, IPlantOptionsService, long>
    {
        /// <summary>
        /// Initialize a new instance of controller <see cref="PlantOptionsController"/> class.
        /// </summary>
        /// <param name="objPlantOptionsService">Service from controller.</param>
        public PlantOptionsController(IPlantOptionsService objPlantOptionsService)
            : base(objPlantOptionsService)
        {
        }
    }
}