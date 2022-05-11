**This library is no longer maintained, please use https://www.nuget.org/packages/OpenTelemetry.Instrumentation.MassTransit**

# MassTransit.ActivityTracing

![.NET Core](https://github.com/zidad/MassTransit.ActivityTracing/workflows/.NET%20Core/badge.svg)
[![NuGet Status](http://nugetstatus.com/MassTransit.ActivityTracing.png)](http://nugetstatus.com/packages/MassTransit.ActivityTracing)

MassTransit W3C activity tracing/propagation


Trying to support propagating the W3C trace context traceId and spanId properties from http calls -> publisher -> consumer -> http call within MassTransit.
https://masstransit-project.com/advanced/monitoring/diagnostic-source.html

Based on this MassTransit.OpenTracing: https://github.com/yesmarket/MassTransit.OpenTracing
And this reference for NServiceBus: https://jimmybogard.com/building-end-to-end-diagnostics-and-tracing-a-primer-trace-context/


Use this to configure trace propagation between asynchronous message broker operations.

```c#
var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
   var host = cfg.Host(new Uri("rabbitmq://localhost"), h =>
   {
      h.Username(brokerSettings.UserName);
      h.Password(brokerSettings.Password);
   });

   cfg.PropagateActivityTracingContext();
});
```

To install from nuget:

```
Install-Package MassTransit.ActivityTracing
```
