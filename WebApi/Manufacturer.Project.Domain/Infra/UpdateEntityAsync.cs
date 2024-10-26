namespace Manufacturer.Project.Domain.Infra
{
    public delegate Task UpdateEntityAsync<TEntity>(TEntity obj) where TEntity : class;
}