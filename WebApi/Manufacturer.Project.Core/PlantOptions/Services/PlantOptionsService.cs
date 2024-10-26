namespace Manufacturer.Project.Core.Services
{
    using Manufacturer.Project.Core.Base;
    using Manufacturer.Project.Domain.Models;
    using Manufacturer.Project.Domain.Repositories;
    using Manufacturer.Project.Domain.Services;
    using System.Linq;

    public class PlantOptionsService: BaseEntityModifiedService<PlantOptions, long, IPlantOptionsRepository>, IPlantOptionsService
    {
        private readonly IPlantOptionsRepository objPlantOptionsRepository;

        public PlantOptionsService(IPlantOptionsRepository objPlantOptionsRepository)
            : base(objPlantOptionsRepository)
        {
            this.objPlantOptionsRepository = objPlantOptionsRepository;
            InitFirstLoad();
        }

        public override PlantOptions Save(PlantOptions obj)
        {
            if (string.IsNullOrEmpty(obj.Key) || string.IsNullOrWhiteSpace(obj.Key))
            {
                throw new Exception($"Valor do Código da Planta inválido, não permitido somente espaço vazio!");
            }

            var allItens = objPlantOptionsRepository.GetAllNoTracking().ToList();

            var hasAlreadyPlantCode = allItens.Any(x => x.Key == obj.Key);
            if (hasAlreadyPlantCode)
            {
                throw new Exception($"Já encontrado uma planta com este código: {obj.Key}, por favor altere o código e tente novamente!");
            }

            return base.Save(obj);
        }

        public override IQueryable<PlantOptions> GetAllNoTracking()
        {
            return base.GetAllNoTracking().OrderBy(x => x.Deleted);
        }

        private void InitFirstLoad()
        {
            var hasOptions = this.objPlantOptionsRepository.GetAllNoTracking().FirstOrDefault();

            if (hasOptions == null)
            {
                this.objPlantOptionsRepository.Insert(new PlantOptions()
                {
                    Created = DateTime.Now,
                    DisplayName = "Brasil",
                    Key = "0001"
                });

                this.objPlantOptionsRepository.Insert(new PlantOptions()
                {
                    Created = DateTime.Now,
                    DisplayName = "Suécia",
                    Key = "0002"
                });

                this.objPlantOptionsRepository.Insert(new PlantOptions()
                {
                    Created = DateTime.Now,
                    DisplayName = "Estados Unidos",
                    Key = "0003"
                });

                this.objPlantOptionsRepository.Insert(new PlantOptions()
                {
                    Created = DateTime.Now,
                    DisplayName = "França",
                    Key = "0004"
                });

                this.objPlantOptionsRepository.SaveChanges();
            }
        }
    }
}
