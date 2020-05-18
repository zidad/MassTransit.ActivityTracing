namespace MassTransit.ActivityTracing
{
    internal static class Constants
    {
        public const string TraceParentHeaderName = "traceparent";
        public const string TraceStateHeaderName = "tracestate";
        public const string RequestIdHeaderName = "Request-Id";
        public const string ConsumerActivityName = "MassTransit.Diagnostics.Receive";
        public const string ProducerActivityName = "MassTransit.Diagnostics.Send";
    }
}