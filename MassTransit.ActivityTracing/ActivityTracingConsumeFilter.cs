using System.Diagnostics;
using System.Threading.Tasks;
using GreenPipes;

namespace MassTransit.ActivityTracing
{
    public class ActivityTracingConsumeFilter : IFilter<ConsumeContext>
    {
        public void Probe(ProbeContext context)
        { }

        public Task Send(ConsumeContext context, IPipe<ConsumeContext> next)
        {
            var activity = StartActivity(context);
            try
            {
                return next.Send(context);
            }
            finally
            {
                activity.Stop();
            }
        }

        private static Activity StartActivity(MessageContext context)
        {
            var operationName = $"Consuming Message: {context.DestinationAddress.GetExchangeName()}";
            var activity = new Activity(Constants.ConsumerActivityName + "::" + operationName);

            if (!context.Headers.TryGetHeader(Constants.TraceParentHeaderName, out var requestId))
            {
                context.Headers.TryGetHeader(Constants.RequestIdHeaderName, out requestId);
            }

            if (!string.IsNullOrEmpty(requestId?.ToString()))
            {
                // This is the magic
                activity.SetParentId(requestId?.ToString());

                if (context.Headers.TryGetHeader(Constants.TraceStateHeaderName, out var traceState))
                {
                    activity.TraceStateString = traceState?.ToString();
                }
            }

            // The current activity gets an ID with the W3C format
            activity.Start();

            return activity;
        }
    }
}
