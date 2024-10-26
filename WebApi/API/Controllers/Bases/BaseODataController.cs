namespace Manufacturer.BaseWebApi.API
{
    using global::Manufacturer.Project.Domain.Infra;
    using Microsoft.AspNet.OData;
    using Microsoft.AspNet.OData.Query;
    using Microsoft.AspNet.OData.Routing;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    /// <summary>
    /// The base odata controller.
    /// </summary>
    /// <typeparam name="TEntity">Entidade Utilizada no serviço.</typeparam>
    /// <typeparam name="TService">Service Utilizada no controller.</typeparam>
    /// <typeparam name="TIDTYPE">Entity id type</typeparam>
    ////[IDMAuthorize]
    public abstract class BaseODataController<TEntity, TService, TIDTYPE> : ODataController
            where TService : IDataService<TEntity, TIDTYPE>
            where TEntity : class, IBaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseODataController{TEntity, TService, IDTYPE}"/> class.
        /// </summary>
        /// <param name="objTService">the service.</param>
        /// <param name="objUserIdentityProvider">objUserIdentityProvider.</param>
        protected BaseODataController(TService objTService)
        {
            ObjService = objTService;
        }

        /// <summary>
        /// Gets or sets Service of controller.
        /// </summary>
        protected TService ObjService { get; set; }

        /// <summary>
        /// Obter TEntitys por filtro.
        /// </summary>
        /// <returns>Lista de TEntity gerados pelo sistema.</returns>
        [HttpGet]
        [ODataRoute("")]
        [EnableQuery(MaxNodeCount = 36556, HandleNullPropagation = HandleNullPropagationOption.False)]
        public virtual IQueryable<TEntity> Get()
        {
            var query = ObjService.GetAllNoTracking();

            return query;
        }

        /// <summary>
        /// Obter TEntity Por ID.
        /// </summary>
        /// <param name="key">Id do TEntity.</param>
        /// <returns>Um <see cref="SingleResult"/>. </returns>
        [HttpGet]
        [ODataRoute("({key})")]
        [EnableQuery(MaxNodeCount = 36556, HandleNullPropagation = HandleNullPropagationOption.False, MaxExpansionDepth = 15)]
        public virtual SingleResult<TEntity> Get(TIDTYPE key)
        {
            return SingleResult.Create(ObjService.GetByIdNoTracking(key));
        }

        /// <summary>
        /// Insere um novo registro.
        /// </summary>
        /// <param name="obj">registro a ser inserido.</param>
        /// <returns>Um <see cref="IActionResult"/> do  inserido.</returns>
        [HttpPost]
        [ODataRoute("")]
        public virtual IActionResult Post([FromBody] TEntity obj)
        {
            try
            {
                return Ok(ObjService.Save(obj));
            }
            catch (Exception ex)
            {
                var response = new
                {
                    Code = 0,
                    ErrorCode = ex.Message,
                    Details = ex.Message
                };

                return BadRequest(response);
            }
        }

        /// <summary>
        /// Atualiza os dados do registro.
        /// </summary>
        /// <param name="obj">registro a ser atualizado.</param>
        /// <returns>Um <see cref="IActionResult"/> do  atualizado.</returns>
        [HttpPut]
        [ODataRoute("")]
        public virtual IActionResult Put([FromBody] TEntity obj)
        {
            try
            {
                return Ok(ObjService.Save(obj));
            }
            catch (Exception ex)
            {
                var response = new
                {
                    Code = 0,
                    ErrorCode = ex.Message,
                    Details = ex.Message
                };

                return BadRequest(response);
            }
        }

        /// <summary>
        /// Atualiza os dados do registro.
        /// </summary>
        /// <param name="obj">registro a ser atualizado.</param>
        /// <returns>Um <see cref="IActionResult"/> do  atualizado.</returns>
        [HttpPatch]
        [ODataRoute("")]
        public virtual IActionResult Patch([FromBody] Delta<TEntity> obj)
        {
            try
            {
                return Ok(ObjService.Patch(obj));
            }
            catch (Exception ex)
            {
                var response = new
                {
                    Code = 0,
                    ErrorCode = ex.Message,
                    Details = ex.Message
                };

                return BadRequest(response);
            }
        }

        /// <summary>
        /// Excluí o colaborador..
        /// </summary>
        /// <param name="key">Id do registro a ser excluído.</param>
        /// <returns>Um <see cref="IActionResult"/> informando que o método rodou OK.</returns>
        [HttpDelete]
        [ODataRoute("")]
        public virtual IActionResult Delete(TIDTYPE key)
        {
            try
            {
                return Ok(ObjService.Remove(key));
            }
            catch (Exception ex)
            {
                var response = new
                {
                    Code = 0,
                    ErrorCode = ex.Message,
                    Details = ex.Message
                };

                return BadRequest(response);
            }
        }
    }
}