namespace Manufacturer.BaseWebApi.API
{
    using Manufacturer.Project.Domain.Models;
    using Microsoft.AspNet.OData.Builder;

    /// <summary>
    /// Classe de inicialização do sistema inteiro, sub classe.
    /// </summary>
    public partial class Startup
    {
        private ODataModelBuilder objODataBuilder;

        /// <summary>
        /// Configure Odata entity.
        /// </summary>
        public void ConfigureServicesOData()
        {
            objODataBuilder = new ODataConventionModelBuilder();

            ConfigureEntitySets(objODataBuilder);
            ConfigureEntityTypes(objODataBuilder);
        }

        /// <summary>
        /// Configure OData Entity set.
        /// </summary>
        /// <param name="builder">Odata builder.</param>
        private void ConfigureEntitySets(ODataModelBuilder builder)
        {
            ////GENERATOR.ODATA.ConfigureEntitySets
            builder.EntitySet<Color>("Color").EntityType.HasKey(t => t.Id);
            builder.EntitySet<Truck>("Truck").EntityType.HasKey(t => t.Id);
            builder.EntitySet<TruckType>("TruckType").EntityType.HasKey(t => t.Id);
            builder.EntitySet<PlantOptions>("PlantOptions").EntityType.HasKey(t => t.Id);
        }

        /// <summary>
        /// Configure OData functions and actions.
        /// </summary>
        /// <param name="builder">Odata builder.</param>
        private void ConfigureEntityTypes(ODataModelBuilder builder)
        {
            ////GENERATOR.ODATA.ConfigureEntityTypes
        }
    }
}
