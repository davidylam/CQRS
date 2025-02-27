using CQRS.Core.Domain;
using CQRS.Core.Infrastructure;
using MongoDB.Driver;
using Post.Cmd.Domain.Aggregate;

namespace Post.Cmd.Infrastructure.Handler
{
    public class EventSourcingHandler : IEventSourcingHandler<PostAggregate>
    {
        private readonly IEventStore _eventStore;

        public EventSourcingHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<PostAggregate> GetByIdAsync(Guid aggregateId)
        {
            var aggregate = new PostAggregate();
            var events = await _eventStore.GetEventsAsync(aggregateId);

            if (events == null || !events.Any()) { return aggregate; }

            aggregate.ReplayEvents(events);
            aggregate.Version = events.Select(x => x.Version).Max();

            return aggregate;
        }

        public Task SaveAsync(AggregateRoot aggregate)
        {
            throw new NotImplementedException();
        }
    }
}
