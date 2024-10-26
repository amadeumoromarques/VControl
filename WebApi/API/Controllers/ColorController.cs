namespace BaseWebApi.Controllers
{
    using Manufacturer.BaseWebApi.API;
    using Manufacturer.Project.Domain.Models;
    using Manufacturer.Project.Domain.Services;
    using Microsoft.AspNet.OData.Routing;

    [ODataRoutePrefix("Color")]
    public class ColorController : BaseODataController<Color, IColorService, long>
    {
        /// <summary>
        /// Initialize a new instance of controller <see cref="ColorController"/> class.
        /// </summary>
        /// <param name="objColorService">Service from controller.</param>
        public ColorController(IColorService objColorService)
            : base(objColorService)
        {
        }
    }
}