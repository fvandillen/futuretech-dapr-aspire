var builder = DistributedApplication.CreateBuilder(args);

builder.AddDapr();

builder
    .AddProject<Projects.Futuretech_Services_Airport>("airport")
    .WithDaprSidecar();

builder
    .AddProject<Projects.Futuretech_Services_RegulatoryInspector>("regulatory-inspector")
    .WithDaprSidecar();

builder.Build().Run();