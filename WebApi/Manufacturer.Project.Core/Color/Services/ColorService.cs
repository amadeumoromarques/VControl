namespace Manufacturer.Project.Core.Services
{
    using Manufacturer.Project.Core.Base;
    using Manufacturer.Project.Domain.Models;
    using Manufacturer.Project.Domain.Repositories;
    using Manufacturer.Project.Domain.Services;
    using System;
    using System.Linq;

    public class ColorService: BaseEntityModifiedService<Color, long, IColorRepository>, IColorService
    {
        private readonly IColorRepository objColorRepository;

        public ColorService(IColorRepository objColorRepository)
            : base(objColorRepository)
        {
            this.objColorRepository = objColorRepository;
            InitFirstLoad();
        }

        public override Color Save(Color obj)
        {
            if (string.IsNullOrEmpty(obj.Name) || string.IsNullOrWhiteSpace(obj.Name))
            {
                throw new Exception($"Valor do Nome da Cor inválido, não permitido somente espaço vazio!");
            }

            if (string.IsNullOrEmpty(obj.SapCode) || string.IsNullOrWhiteSpace(obj.SapCode))
            {
                throw new Exception($"Valor do Código da Cor inválido, não permitido somente espaço vazio!");
            }

            if (string.IsNullOrEmpty(obj.HexaColor) || string.IsNullOrWhiteSpace(obj.HexaColor))
            {
                throw new Exception($"Valor do Hexadecimal da cor inválido, não permitido somente espaço vazio!");
            }

            var allItens = objColorRepository.GetAllNoTracking().ToList();

            var hasAlreadySapCode = allItens.Any(x => x.SapCode == obj.SapCode);
            if (hasAlreadySapCode)
            {
                throw new Exception($"Já encontrado uma Cor com este Código: {obj.SapCode}, por favor altere o código e tente novamente!");
            }

            var hasAlreadyHexaColor = allItens.Any(x => x.HexaColor == obj.HexaColor);
            if (hasAlreadyHexaColor)
            {
                throw new Exception($"Já encontrado uma Cor com este Hexadecimal: {obj.HexaColor}, por favor altere o código e tente novamente!");
            }

            return base.Save(obj);
        }

        public override IQueryable<Color> GetAllNoTracking()
        {
            return base.GetAllNoTracking().OrderBy(x => x.Deleted);
        }

        /// <summary>
        /// First load on database to create minimum data on database.
        /// </summary>
        private void InitFirstLoad()
        {
            var hasData = this.objColorRepository.GetAllNoTracking().FirstOrDefault();

            if (hasData == null)
            {
                this.objColorRepository.Insert(new Color()
                {
                    Created = DateTime.Now,
                    Name = "Azul Volvo",
                    SapCode = "F00HG8710",
                    HexaColor = "#003057"
                });

                this.objColorRepository.Insert(new Color()
                {
                    Created = DateTime.Now,
                    Name = "Azul Claro",
                    SapCode = "F00HG8711",
                    HexaColor = "#5C7F9F"
                });

                this.objColorRepository.Insert(new Color()
                {
                    Created = DateTime.Now,
                    Name = "Preto",
                    SapCode = "P29GH1247",
                    HexaColor = "#2d2926"
                });

                this.objColorRepository.Insert(new Color()
                {
                    Created = DateTime.Now,
                    Name = "Cinza Industrial",
                    SapCode = "P29GH1248",
                    HexaColor = "#4B4B4B"
                });

                this.objColorRepository.Insert(new Color()
                {
                    Created = DateTime.Now,
                    Name = "Prata Metálico",
                    SapCode = "P29GH1249",
                    HexaColor = "#A1A1A1"
                });

                this.objColorRepository.Insert(new Color()
                {
                    Created = DateTime.Now,
                    Name = "Cinza Claro",
                    SapCode = "P29GH1250",
                    HexaColor = "#D1D1D1"
                });

                this.objColorRepository.SaveChanges();
            }
        }
    }
}
