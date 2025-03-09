using Core.DataModel.Entities.Entities;

namespace Dida.Waylen.Onboarding.Demo.Service.Open.Infrastructure.Events;

public record EntityUpdatedEvent<TEntity>(TEntity entity) where TEntity : IEntity
{
}
