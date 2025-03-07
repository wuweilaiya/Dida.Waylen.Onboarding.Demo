namespace Dida.Waylen.Onboarding.Demo.Service.Open.Applications.Samples.Commands
{
    public record UpdateSampleCommand(int Id, UpsertSampleDto Dto) : Command
    {
    }
}
