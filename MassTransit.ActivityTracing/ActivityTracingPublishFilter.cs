using System.Diagnostics;
using System.Threading.Tasks;
using GreenPipes;

namespace MassTransit.ActivityTracing
{
    public class OpenTracingPublishFilter : IFilter<PublishContext>, IFilter<SendContext>
    {
        public async Task Send(SendContext context, IPipe<SendContext> next)
        {
            var activity = StartActivity();

            InjectHeaders(activity, context);

            try
            {
                await next.Send(context);
            }
            finally
            {
                activity.Stop();
            }
        }

        public void Probe(ProbeContext context)
        { }

        public async Task Send(PublishContext context, IPipe<PublishContext> next)
        {
            var activity = StartActivity();

            InjectHeaders(activity, context);
            try
            {
                await next.Send(context);
            }
            finally
            {
                activity.Stop();
            }
        }

        private static void InjectHeaders(
            Activity activity,
            SendContext context)
        {
            if (activity.IdFormat == ActivityIdFormat.W3C)
            {
                if (!context.Headers.TryGetHeader(Constants.TraceParentHeaderName,  out _))
                {
                    context.Headers.Set(Constants.TraceParentHeaderName, activity.Id);
                    if (activity.TraceStateString != null)
                    {
                        context.Headers.Set(Constants.TraceStateHeaderName , activity.TraceStateString);
                    }
                }
            }
            else
            {
                if (!context.Headers.TryGetHeader(Constants.RequestIdHeaderName,  out _))
                {
                    context.Headers.Set(Constants.RequestIdHeaderName, activity.Id);
                }
            }
        }

        private static Activity StartActivity()
        {
            var activity = new Activity(Constants.ProducerActivityName);

            activity.Start();

            return activity;
        }

    }
}
