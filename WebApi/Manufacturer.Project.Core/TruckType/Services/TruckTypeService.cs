namespace Manufacturer.Project.Core.Services
{
    using Manufacturer.Project.Core.Base;
    using Manufacturer.Project.Domain.Models;
    using Manufacturer.Project.Domain.Repositories;
    using Manufacturer.Project.Domain.Services;
    using System;
    using System.Linq;

    public class TruckTypeService: BaseEntityModifiedService<TruckType, long, ITruckTypeRepository>, ITruckTypeService
    {
        private readonly ITruckTypeRepository objTruckTypeRepository;

        public TruckTypeService(ITruckTypeRepository objTruckTypeRepository)
            : base(objTruckTypeRepository)
        {
            this.objTruckTypeRepository = objTruckTypeRepository;
            InitFirstLoad();
        }

        public override TruckType Save(TruckType obj)
        {
            if (string.IsNullOrEmpty(obj.Type) || string.IsNullOrWhiteSpace(obj.Type))
            {
                throw new Exception($"Valor do Tipo do Caminhão inválido, não permitido somente espaço vazio!");
            }

            var allItens = objTruckTypeRepository.GetAllNoTracking().ToList();

            var hasAlreadyTypeOnDataBase = allItens.Any(x => x.Type == obj.Type);
            if (hasAlreadyTypeOnDataBase)
            {
                throw new Exception($"Já encontrado um tipo de caminhão desses no banco de dados: {obj.Type}, por favor altere o código e tente novamente!");
            }

            return base.Save(obj);
        }

        public override IQueryable<TruckType> GetAllNoTracking()
        {
            return base.GetAllNoTracking().OrderBy(x => x.Deleted);
        }

        private void InitFirstLoad()
        {
            var teste = this.objTruckTypeRepository.GetAllNoTracking().FirstOrDefault();

            if (teste == null)
            {
                TruckType type1 = new TruckType()
                {
                    Created = DateTime.Now,
                    Type = "FH"
                };
                this.objTruckTypeRepository.Insert(type1);

                TruckType type2 = new TruckType()
                {
                    Created = DateTime.Now,
                    Type = "FM"
                };
                this.objTruckTypeRepository.Insert(type2);

                TruckType type3 = new TruckType()
                {
                    Created = DateTime.Now,
                    Type = "VM"
                };
                this.objTruckTypeRepository.Insert(type3);
                this.objTruckTypeRepository.SaveChanges();
            }
        }
    }
}
