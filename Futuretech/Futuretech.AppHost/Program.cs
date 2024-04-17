using System.Collections.Immutable;
using Aspire.Hosting.Dapr;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddDapr();

builder
    .AddProject<Projects.Futuretech_Services_Airport>("airport")
    .WithDaprSidecar(new DaprSidecarOptions()
    {
        ResourcesPaths = ImmutableHashSet.Create(Directory.GetCurrentDirectory() + "/../dapr/components")
    });

var sql = builder
    .AddSqlServer("sql")
    .AddDatabase("regulatory-inspector-sql");

builder
    .AddProject<Projects.Futuretech_Services_RegulatoryInspector>("regulatory-inspector")
    .WithReference(sql)
    .WithDaprSidecar(new DaprSidecarOptions()
    {
        ResourcesPaths = ImmutableHashSet.Create(Directory.GetCurrentDirectory() + "/../dapr/components")
    });

builder
    .AddProject<Projects.Futuretech_Services_Flight>("flight")
    .WithDaprSidecar(new DaprSidecarOptions()
    {
        ResourcesPaths = ImmutableHashSet.Create(Directory.GetCurrentDirectory() + "/../dapr/components")
    });

builder.Build().Run();