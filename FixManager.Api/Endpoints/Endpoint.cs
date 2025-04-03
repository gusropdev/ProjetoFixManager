using FixManager.Api.Common.Api;
using FixManager.Api.Endpoints.Customers;
using FixManager.Api.Endpoints.Devices;
using FixManager.Api.Endpoints.Parts;
using FixManager.Api.Endpoints.ServiceOrders;
using FixManager.Core.Requests.ServiceOrders;

namespace FixManager.Api.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("api/v1/");
        
        endpoints.MapGroup("/").WithTags("HealthCheck")
            .MapGet("/", () => new {message = "OK"});

        endpoints.MapGroup("/customers").WithTags("Customers")
            .MapEndpoint<CreateCustomerEndpoint>()
            .MapEndpoint<DeleteCostumerEndpoint>()
            .MapEndpoint<UpdateCostumerEndpoint>()
            .MapEndpoint<GetCostumerByIdEndpoint>()
            .MapEndpoint<GetAllCostumersEndpoint>();

        endpoints.MapGroup("/serviceOrders").WithTags("ServiceOrders")
            .MapEndpoint<CreateServiceOrderEndpoint>()
            .MapEndpoint<UpdateServiceOrderEndpoint>()
            .MapEndpoint<DeleteServiceOrderEndpoint>()
            .MapEndpoint<GetServiceOrderByIdEndpoint>()
            .MapEndpoint<GetAllServiceOrdersEndpoint>();
        
        endpoints.MapGroup("/devices").WithTags("Devices")
            .MapEndpoint<CreateDeviceEndpoint>()
            .MapEndpoint<UpdateDeviceEndpoint>()
            .MapEndpoint<DeleteDeviceEndpoint>()
            .MapEndpoint<GetDeviceByIdEndpoint>()
            .MapEndpoint<GetAllDevicesEndpoint>();
        
        endpoints.MapGroup("/parts").WithTags("Parts")
            .MapEndpoint<CreatePartEndpoint>()
            .MapEndpoint<UpdatePartEndpoint>()
            .MapEndpoint<DeletePartEndpoint>()
            .MapEndpoint<GetPartByIdEndpoint>()
            .MapEndpoint<GetAllPartsEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder endpoint)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(endpoint);
        return endpoint;
    }
}