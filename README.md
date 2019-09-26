## Simplest Ocelot API Gateway example

The aim of this example is to demonstrate the basic usage of the Ocelot API gateway. For a better understanding of Ocelot and the advanced features such as Service Discovery, Load Balancer or Rate Limiting etc.,  please refer to this  [MSDN blog post](https://devblogs.microsoft.com/cesardelatorre/designing-and-implementing-api-gateways-with-ocelot-in-a-microservices-and-container-based-architecture/) and the [Ocelot official document]().

This project uses three Asp.Net Core Web API projects:
1) **ProductsService** : a micro service containing 2 end points:
*http://{baseuri}:2002/api/products/* and *http://{baseuri}:2002/api/products/{id}*
For example: 
http://localhost:2002/api/products returns all products
http://localhost:2002/api/products/123 returns a product whose Id is 123

2) **ClientsService** : a micro service containing 2 end points: http://{baseuri}:2001/api/clients/ and http://{baseuri}:2002/api/clients/{id}
For example:
http://localhost:2001/api/clients returns all clients
http://localhost:2001/api/clients/123 returns a client whose Id is 123

3) **ApiGateway** : the gateway service where the **Ocelot NuGet package** is installed and configured. The configuration is defined in the OcelotConfig.json file.

The following json bloc defines how Ocelot will route incoming requests:
```json
{
"DownstreamPathTemplate": "/api/clients",
"DownstreamScheme": "http",
"DownstreamHostAndPorts": [
{
"Host": "localhost",
"Port": 2001
}
],
"UpstreamPathTemplate": "/clients",
"UpstreamHttpMethod": [ "Get" ]
}
```
In above configuration: the "*DownstreamPathTemplate*" defines the URI template exposed by the backend service wheres the "*UpstreamPathTemplate*" defines the URI template exposed by the gateway.
As such, if a request arrives at the gateway, for example http://localhost:2000/clients, it will be forwarded to the http://localhost:2001/api/clients end point which will return all clients.

## Testing on local machine
1) Run the ClientService and ProductService and verify that they work as expected: 

![Image of endpoints test](https://github.com/caiyu8/OcelotDemo/blob/master/TestEndpoints.gif)

2) Run the gateway, and verify that the gateway routes incoming requests to the corresponding end points as defined in the configuration :
- a request to http://localhost:2000/clients is forwarded to http://localhost:2001/api/clients
- a request to http://localhost:2000/clients/123 is forwarded to  http://localhost:2001/api/clients/123
- a request to http://localhost:2002/api/products is forwarded to http://localhost:2000/api/clients

![Image of endpoints test](https://github.com/caiyu8/OcelotDemo/blob/master/TestGateway.gif)


