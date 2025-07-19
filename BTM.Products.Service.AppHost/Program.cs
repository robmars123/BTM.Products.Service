var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.BTM_Products_Api>("btm-products-api");

builder.Build().Run();
