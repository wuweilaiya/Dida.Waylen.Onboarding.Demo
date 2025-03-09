using Core.DataModel.Entities.Entities;

namespace Dida.Waylen.Onboarding.Demo.Service.Open.Infrastructure.Events;

public record EntityCreatedEvent<TEntity>(TEntity entity) where TEntity : IEntity
{
}
