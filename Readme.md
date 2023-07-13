# ApiGateway Compudile
Sistema intermediario entre el ecosistema de microservicios del 
backend y el resto de la aplicaciones externas a Compudile. Esta realizado 
sobre la libreria Ocelot de Net Core.
Esta integrado con el sistema de identidad de Compudile y permite
manejar balanceo de carga y limite de accesos por clientes
## Instalación
* Instalar la SDK 6 de [Net Core](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
* Descargar el proyecto del repositorio de Compudile
```
git clone https://Compudile@dev.azure.com/Compudile/ApiGateway/_git/ApiGateway
```
* Configurar en el appsettings.Development.json del proyecto la 
integracion con el sistema de identidad de Compudile
```
"IdentityServer": {
    "Host": "https://localhost:6001",
    "ClientId": "api-gateway",
    "ClientSecret": "client_secret_get_to_identity"
  }
```
* Ejecutar en la raiz del proyecto 
```
dotnet run
```
### Agregar nuevo Routing
Revisar la documentacion de [Ocelot](https://ocelot.readthedocs.io/en/latest/features/routing.html) para mas detalles.
* Agregar un archivo con el formato ocelot.nombre_routing.json en la carpeta
OcelotConfigDev en caso de desarrollo o OcelotConfig si es en produccion.
* Agregar Routing en dependencia de las especificaciones de la API, ejemplo:
```
{
	"Routes": [
		{
			"DownstreamPathTemplate": "/api/{everything}",
			"DownstreamScheme": "https",
			"DownstreamHostAndPorts": [
				{
					"Host": "localhost",
					"Port": 6001
				}
			],
			"UpstreamPathTemplate": "/api/user/{everything}",
			"UpstreamHttpMethod": [
				"Get"
			],
			"SwaggerKey": "user",
			"DangerousAcceptAnyServerCertificateValidator": true
		}
	]
}
```
El Downstream configura los accesos a la API a la que se tendra acceso, y el
Upstream seria la ruta de como lo veran las explicaciones externas o front-end
* En caso de que la API tenga swagger se debe configurar en el archivo 
ocelot.SwaggerEndPoints.json en la misma carpeta de los Routing.
Debes agregar un nuevo objeto al array que ya existe, ejemplo
```
{
      "Key": "user",
      "Config": [
        {
          "Name": "User API",
          "Version": "v1",
          "Url": "https://localhost:6001/swagger/v1/swagger.json"
        }
      ]
}
```
el nombre de la "Key" es el que se debe agregar a los routing agregados en los
routing del punto anterior en la variable
```
"SwaggerKey": "user"
```
## Crear Swagger Personalizado

Si tiene una API y no tiene la documentación en Swagger, puedes crear 
tu propia documentación de swagger y agregarla al ApiGateway, para el acceso 
a esa API. Crear un archivo en la carpeta wwwroot con un nombre determinado,
ejemplo "swagger.sms.json". En este archivo se tiene la descripción
en OpenApi del acceso a la API. Despues en el archivo descriptor del Swagger 
"ocelot.SwaggerEndPoints.json" de la carpeta OcelotConfig adicionar la documentación
de la API, ejemplo: 
```
{
      "Key": "user",
      "Config": [
        {
          "Name": "User API",
          "Version": "v1",
          "Url": "https://dominio_api_gateway/swagger.sms.json"
        }
      ]
}
```
Con estos pasos ya debes tener la documentación de la API lista en el 
swagger del ApiGateway. En caso de que no cargue bien los endpoint
configurados en OpenApi adicionar el parametro "TransformByOcelotConfig" en falso
dentro del archivo "ocelot.SwaggerEndPoints.json", ejemplo:
```
{
      "Key": "sms",
      "TransformByOcelotConfig": false,
      "Config": [
        {
          "Name": "Sms API",
          "Version": "v1",
          "Url": "https://localhost:3001/swagger.sms.json"
        }
      ]
    }
```
Esto evita que la libreria "MMLib.SwaggerForOcelot" haga cambios 
en tiempo de ejecucion a lo previamente configurado.

## Agregar manejador de peticiones
Se debe crear una dll en Net Core 6 como un proyecto de ClassLibrary
la clase debe tener el formato
```
public class FakeHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        //do stuff and optionally call the base handler..
        return await base.SendAsync(request, cancellationToken);
    }
}
```
Para mas información revisar la documentación de Ocelot
referente a los manejadores de peticiones: [Delegating Handlers](https://ocelot.readthedocs.io/en/latest/features/delegatinghandlers.html)
Despues de creada la dll copiarla para la carpeta Handlers del ApiGateway
y configurar en el appsettings.json para que sea cargada por el sistema. Se debe agregar un nuevo objeto 
al parametro "DelegatingHandlers" ejemplo:
```
"DelegatingHandlers": [
    {
      "Namespace": "HttpLog",
      "ClassName": "FakeHandler",
      "Global": false
    }
  ]
```
En el ejemplo anterior HttpLog es el namespaces en el que se creo la clase,
FakeHandler es la clase que se llamara en tiempo de ejecucion y
Global especifica si ese manejador es para todas las rutas
o solo para una especifica
Para que un manejador funcione para una ruta especifica
de ocelot se debe especificar en el archivo de configuración
de ocelot los siguientes valores
```
"DelegatingHandlers": [
    "FakeHandlerTwo",
    "FakeHandler"
]
```
se le especifica la cantidad de manejadores que se vallan a utilizar.
