namespace Dida.Waylen.Onboarding.Demo.Services
{
    public class SampleService : ServiceBase
    {
        // more: https://didatravel.feishu.cn/wiki/Kai6wrE1Iia4zikfR8ScFvLjnEg

        public async Task<Results<Ok<PagedResultDto<SampleItemDto>>, BadRequest>> GetPagedListAsync([FromServices] ILocalEventBus localEventBus, [AsParameters] GetPagedSampleRequestDto dto)
        {
            var query = new SamplePagedQuery(dto);
            await localEventBus.PublishAsync(query);
            return TypedResults.Ok(query.Result);
        }

        public async Task<Results<Ok<SampleItemDto>, BadRequest>> GetAsync([FromServices] ILocalEventBus localEventBus, int id)
        {
            var query = new SampleDetailQuery(id);
            await localEventBus.PublishAsync(query);
            return TypedResults.Ok(query.Result);
        }


        public async Task<Results<Ok, BadRequest>> AddAsync([FromServices] ILocalEventBus localEventBus, UpsertSampleDto dto)
        {
            var command = new AddSampleCommand(dto);
            await localEventBus.PublishAsync(command);
            return TypedResults.Ok();
        }

        public async Task<Results<Ok, BadRequest>> UpdateAsync([FromServices] ILocalEventBus localEventBus, int Id, UpsertSampleDto dto)
        {
            var command = new UpdateSampleCommand(Id, dto);
            await localEventBus.PublishAsync(command);
            return TypedResults.Ok();
        }

        public async Task<Results<Ok, BadRequest>> DeleteAsync([FromServices] ILocalEventBus localEventBus, int Id)
        {
            var command = new DeleteSampleCommand(Id);
            await localEventBus.PublishAsync(command);
            return TypedResults.Ok();
        }
    }
}
