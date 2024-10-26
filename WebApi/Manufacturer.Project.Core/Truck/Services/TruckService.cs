namespace Manufacturer.Project.Core.Services
{
    using Manufacturer.Project.Core.Base;
    using Manufacturer.Project.Domain.Models;
    using Manufacturer.Project.Domain.Repositories;
    using Manufacturer.Project.Domain.Services;

    public class TruckService: BaseEntityModifiedService<Truck, long, ITruckRepository>, ITruckService
    {
        private readonly ITruckRepository objTruckRepository;
        private readonly ITruckTypeService objTruckTypeService;
        private readonly IPlantOptionsService objPlantOptionsService;
        private readonly IColorService objColorService;

        public TruckService(ITruckRepository objTruckRepository, 
            ITruckTypeService objTruckTypeService, 
            IPlantOptionsService objPlantOptionsService,
            IColorService objColorService)
            : base(objTruckRepository)
        {
            this.objTruckRepository = objTruckRepository;
            this.objTruckTypeService = objTruckTypeService;
            this.objPlantOptionsService = objPlantOptionsService;
            this.objColorService = objColorService;
            ////ValidateField();    //// Desabilitado para não ter o insert inicial no banco
        }

        public override Truck Save(Truck obj)
        {
            if (string.IsNullOrEmpty(obj.ChassisCode) || string.IsNullOrWhiteSpace(obj.ChassisCode))
            {
                throw new Exception($"Valor do Chassi inválido, não permitido somente espaço vazio!");
            }

            var allItens = objTruckRepository.GetAllNoTracking().ToList();
            var hasAlreadyChassisCode = allItens.Any(x => x.ChassisCode == obj.ChassisCode && x.Id != obj.Id);
            if (hasAlreadyChassisCode)
            {
                throw new Exception($"Já encontrado uma caminhão com este Chassi cadastrado: {obj.ChassisCode}, por favor verifique o Chassi digitado e tente novamente!");
            }

            if(obj.ManufacturerYear > DateTime.Now.Year)
            {
                throw new Exception($"O ano digitado {obj.ManufacturerYear} é maior do que ano atual: {DateTime.Now.Year}");
            }

            if (obj.ManufacturerYear < 2000)
            {
                throw new Exception($"O ano digitado {obj.ManufacturerYear} é menor do que o limite permitido de 2000!");
            }

            return base.Save(obj);
        }

        private void ValidateField()
        {
            var hasOptions = this.objTruckRepository.GetAllNoTracking().FirstOrDefault();

            if (hasOptions == null)
            {
                var allFieldType = objTruckTypeService.GetAllNoTracking().ToList();
                var allPlantOptions = objPlantOptionsService.GetAllNoTracking().ToList();
                var allColors = objColorService.GetAllNoTracking().ToList();

                this.objTruckRepository.Insert(new Truck()
                {
                    Created = DateTime.Now,
                    ChassisCode = "1HGCM82633A123456",
                    ManufacturerYear = 2023,
                    IdColor = allColors[new Random().Next(allColors.Count)].Id,
                    IdTruckType = allFieldType[new Random().Next(allFieldType.Count)].Id,
                    IdPlantOptions = allPlantOptions[new Random().Next(allPlantOptions.Count)].Id
                });

                this.objTruckRepository.Insert(new Truck()
                {
                    Created = DateTime.Now,
                    ChassisCode = "2T3ZF4DV3BW123789",
                    ManufacturerYear = 2023,
                    IdColor = allColors[new Random().Next(allColors.Count)].Id,
                    IdTruckType = allFieldType[new Random().Next(allFieldType.Count)].Id,
                    IdPlantOptions = allPlantOptions[new Random().Next(allPlantOptions.Count)].Id
                });

                this.objTruckRepository.Insert(new Truck()
                {
                    Created = DateTime.Now,
                    ChassisCode = "3VWDX7AJ7DM123654",
                    ManufacturerYear = 2024,
                    IdColor = allColors[new Random().Next(allColors.Count)].Id,
                    IdTruckType = allFieldType[new Random().Next(allFieldType.Count)].Id,
                    IdPlantOptions = allPlantOptions[new Random().Next(allPlantOptions.Count)].Id
                });

                this.objTruckRepository.Insert(new Truck()
                {
                    Created = DateTime.Now,
                    ChassisCode = "JN1BJ1CR4KW123321",
                    ManufacturerYear = 2024,
                    IdColor = allColors[new Random().Next(allColors.Count)].Id,
                    IdTruckType = allFieldType[new Random().Next(allFieldType.Count)].Id,
                    IdPlantOptions = allPlantOptions[new Random().Next(allPlantOptions.Count)].Id
                });

                this.objTruckRepository.SaveChanges();
            }
        }
    }
}
