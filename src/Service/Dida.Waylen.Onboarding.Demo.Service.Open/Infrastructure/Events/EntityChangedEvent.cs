using Core.DataModel.Entities.Entities;
using Core.Plugin.PubSub.EventBus;
using Dida.Waylen.Onboarding.Demo.Shared.Enums;
using EventBus;

namespace Dida.Waylen.Onboarding.Demo.Service.Open.Infrastructure.Events;

public record EntityChangedEvent<TEntity>(TEntity Entity, EntityChangedTypeEnum EntityChangedType) : Event where TEntity : IEntity
{
}
