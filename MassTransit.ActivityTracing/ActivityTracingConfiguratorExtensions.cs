namespace MassTransit.ActivityTracing
{
    public static class ActivityTracingConfiguratorExtensions
    {
        public static void PropagateActivityTracingContext(this IBusFactoryConfigurator value)
        {
            value.ConfigurePublish(configurator => configurator.AddPipeSpecification(new ActivityTracingPipeSpecification()));
            value.ConfigureSend(configurator => configurator.AddPipeSpecification(new ActivityTracingPipeSpecification()));
            value.AddPipeSpecification(new ActivityTracingPipeSpecification());
        }
    }
}
