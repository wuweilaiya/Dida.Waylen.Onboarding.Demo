namespace Dida.Waylen.Onboarding.Demo.Service.Open.Applications.Samples
{
    public class SampleCommandHandler(DemoDbContext _didaDbContext)
    {
        [LocalEventHandler(1)]
        public async Task AddAsync(AddSampleCommand command)
        {
            var sample = command.Dto.Adapt<Sample>();

            //TODO:Do some validation

            await _didaDbContext.Samples.AddAsync(sample);
            await _didaDbContext.SaveChangesAsync();
        }

        [LocalEventHandler(1)]
        public async Task UpdateAsync(UpdateSampleCommand command)
        {
            var sample = await _didaDbContext.Samples.FindAsync(command.Id);
            command.Dto.Adapt(sample);

            //TODO:Do some validation

            await _didaDbContext.SaveChangesAsync();
        }

        [LocalEventHandler(1)]
        public async Task DeleteAsync(DeleteSampleCommand command)
        {
            var sample = await _didaDbContext.Samples.AsNoTracking().FirstOrDefaultAsync(e => e.Id == command.Id);
            if (sample == null)
            {
                return;
            }

            _didaDbContext.Remove(sample);
            await _didaDbContext.SaveChangesAsync();
        }
    }
}
