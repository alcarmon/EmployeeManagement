# Employee Management API

Solucion para la prueba tecnica de Backend .NET. Implementa una API de gestion de empleados con ASP.NET Core, Entity Framework Core, JWT, autorizacion por roles, Clean Architecture, validaciones, middleware personalizado y patrones de diseño.

## Tecnologias

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core 8
- SQL Server
- MediatR
- FluentValidation
- JWT Bearer Authentication
- Swagger / OpenAPI
- xUnit

## Arquitectura

La solucion usa Clean Architecture:

```text
src/
├── EmployeeManagement.API
├── EmployeeManagement.Application
├── EmployeeManagement.Domain
├── EmployeeManagement.Infrastructure
└── EmployeeManagement.Persistence
```

- `Domain`: entidades, reglas de negocio, constantes y estrategias de bono.
- `Application`: casos de uso, DTOs, servicios, CQRS, validaciones e interfaces.
- `Infrastructure`: JWT, hashing de passwords y factory de estrategias.
- `Persistence`: DbContext, configuraciones EF Core, repositorios y migraciones.
- `API`: controladores, Swagger, autenticacion, autorizacion y middleware.

Los controladores son delgados: reciben requests, invocan servicios y retornan respuestas HTTP. Las entidades de dominio no dependen de Entity Framework y no se exponen directamente desde la API.

## Patrones Implementados

- **Repository Pattern**: interfaces en Application e implementaciones en Persistence. No se expone `IQueryable`.
- **Strategy Pattern**: calculo de bono por cargo en `BonusStrategies`.
- **Factory Pattern**: `IBonusStrategyFactory` resuelve la estrategia adecuada segun el cargo.
- **CQRS/Mediator**: comandos y queries se procesan con MediatR.

## SOLID

- **SRP**: cada capa tiene una responsabilidad clara; controllers, handlers, repositories y entities no mezclan responsabilidades.
- **OCP**: se pueden agregar nuevos cargos y estrategias de bono sin modificar los consumidores del calculo.
- **LSP**: las estrategias de bono implementan el mismo contrato y son intercambiables.
- **ISP**: los repositorios exponen contratos especificos por agregado.
- **DIP**: Application depende de abstracciones; Infrastructure y Persistence entregan implementaciones por Dependency Injection.

## Requisitos Cubiertos

### C# Programming

La entidad `Employee` modela:

- `Id`
- `IdentificationNumber`
- `Name`
- `Salary`
- `CurrentPositionId`
- `DepartmentId`
- `HireDate`
- `PositionHistory`
- `Projects`

El historial de cargos se modela con `PositionHistory`. El calculo de bono se implementa con Strategy Pattern y se expone desde la API.

Regla adicional implementada: el empleado solo recibe bono si tiene al menos 1 ano de antiguedad. (No está explícito en la solicitud, sin embargo en un escenario real este punto se dicutiría con el PO)

### ASP.NET Core API

Endpoints principales:

```http
GET    /api/employees
GET    /api/employees/{id}
GET    /api/employees/department/{departmentId}/with-projects
POST   /api/employees
PUT    /api/employees/{id}
DELETE /api/employees/{id}
```

Autenticacion:

```http
POST /api/auth/register
POST /api/auth/login
```

### EF Core y Base de Datos

El esquema incluye:

- `Employees`
- `Departments`
- `Positions`
- `PositionHistories`
- `Projects`
- `EmployeeProjects`
- `Users`

La configuracion EF Core se realiza con Fluent API mediante `IEntityTypeConfiguration<TEntity>`. Se configuran llaves, relaciones, indices, restricciones, precision decimal y seed data.

### Consulta LINQ Solicitada

La consulta para obtener empleados de un departamento especifico que trabajan en al menos un proyecto esta expuesta en:

```http
GET /api/employees/department/{departmentId}/with-projects
```

La implementacion esta en `EmployeeRepository.GetByDepartmentWithActiveProjectsAsync`, usando EF Core con `Where` y `Any`.

## Respuestas Conceptuales

### Como implementar autenticacion y autorizacion

La API usa JWT Bearer Authentication. El cliente realiza login enviando email y password; si las credenciales son validas, la API retorna un token JWT firmado. En cada request protegido, el cliente envia:

```http
Authorization: Bearer {token}
```

ASP.NET Core valida la firma, issuer, audience y expiracion del token. La autorizacion se aplica con roles:

- `Admin`: puede ejecutar CRUD completo sobre empleados.
- `User`: solo puede consultar empleados.

Ejemplo:

```csharp
[Authorize(Roles = Roles.Admin)]
```

### Que es middleware en ASP.NET Core

Middleware es un componente del pipeline HTTP que puede inspeccionar, modificar o detener una solicitud antes o despues de que llegue al controller. En esta solucion hay:

- `RequestLoggingMiddleware`: registra metodo, ruta, query string, status code y tiempo de ejecucion usando `ILogger`.
- `ExceptionHandlingMiddleware`: captura excepciones no controladas y retorna una respuesta JSON uniforme.
- Desde los anteriores Middleware podríamos manejar registro de observabilidad con proveedores de opentelemetry

### Performance en aplicaciones .NET

Problemas comunes:

- Consultas lentas por falta de indices.
- Carga excesiva de datos.
- Uso innecesario de tracking en EF Core.
- Operaciones sincronas bloqueantes.
- N+1 queries.
- Serializacion de respuestas muy grandes.
- Falta de cache en datos de lectura frecuente.

Formas de mitigarlo:

- Usar indices adecuados.
- Proyectar solo los campos necesarios.
- Usar `AsNoTracking()` en consultas de lectura.
- Usar async/await para I/O.
- Revisar queries generadas por EF Core.
- Paginar listados.
- Medir antes de optimizar.

### Como perfilar y optimizar una query lenta

1. Identificar los procesos que se identifican lentos según mediciones, y registros de telemtría.
2. Medir duracion y cantidad de consultas ejecutadas.
3. Verificar indices, joins y filtros.
4. Evitar traer entidades completas si solo se necesitan DTOs.
5. Aplicar `AsNoTracking()` cuando no se vaya a modificar la entidad.
6. Comparar metricas antes y despues del cambio.

## Configuracion

Revisar `src/EmployeeManagement.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=SERVERNAME;Database=EmployeeManagementDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
  },
  "Jwt": {
    "SecretKey": "ThisIsASecretKeyForJwtTokenGeneration12345",
    "Issuer": "EmployeeManagement",
    "Audience": "EmployeeManagementAPI",
    "ExpirationMinutes": 15
  }
}
```

Antes de ejecutar, ajustar `DefaultConnection` si el SQL Server local tiene otro nombre. La clave JWT incluida es solo para ambiente local/desarrollo.

## Puesta en Marcha

1. Clonar el repositorio.

2. Restaurar paquetes:

```powershell
dotnet restore EmployeeManagement.sln
```

3. Compilar:

```powershell
dotnet build EmployeeManagement.sln
```

4. Aplicar migracion inicial:

```powershell
dotnet ef database update `
  --project src/EmployeeManagement.Persistence `
  --startup-project src/EmployeeManagement.Persistence
```

5. Ejecutar la API:

```powershell
dotnet run --project src/EmployeeManagement.API --launch-profile https
```

6. Abrir Swagger:

```text
https://localhost:7298
```

Tambien puede ejecutarse por HTTP:

```powershell
dotnet run --project src/EmployeeManagement.API --launch-profile http
```

```text
http://localhost:5223
```

## Probar JWT

Usuario Admin sembrado:

```text
Email: admin@employeemanagement.local
Password: Admin123
Role: Admin
```

1. Ejecutar:

```http
POST /api/auth/login
```

Body:

```json
{
  "email": "admin@employeemanagement.local",
  "password": "Admin123"
}
```

2. Copiar el token de la respuesta.

3. En Swagger, seleccionar `Authorize`.

4. Ingresar:

```text
Bearer {token}
```

5. Probar endpoints protegidos.

Para crear un usuario con rol `User`:

```http
POST /api/auth/register
```

Los usuarios registrados pueden consultar empleados, pero no crear, actualizar ni eliminar.

## Seed Data para Pruebas

La migracion inicial crea datos base para facilitar las pruebas.

### Departamentos

```text
1 - Engineering
2 - Sales
3 - Human Resources
4 - Finance
```

### Cargos

```text
1 - Employee
2 - Manager
3 - Senior Manager
4 - Director
```

### Empleados

```text
1 - Martin Sales Manager
    Cargo: Manager
    Departamento: Sales
    Antiguedad: 5 anos

2 - Laura Engineering Manager
    Cargo: Manager
    Departamento: Engineering
    Antiguedad: 5 anos
    Historial: inicio como Employee y luego paso a Manager

3 - Andres Regular Engineer
    Cargo: Employee
    Departamento: Engineering
    Antiguedad: 2 anos
    Proyectos asignados: 2

4 - Sofia Junior Engineer
    Cargo: Employee
    Departamento: Engineering
    Antiguedad: 2 meses
    Proyectos asignados: 1
```

### Proyectos

La fecha de inicio se calcula al ejecutar la migracion:

```text
StartDate = fecha de migracion - 15 dias
EndDate   = fecha de migracion + 15 dias
Vigencia  = 30 dias
```

Proyectos:

```text
1 - Clinical Scheduling Platform
2 - Mobile Care Portal
3 - Billing Automation
```

### Pruebas Sugeridas con Seed

Validar listado:

```http
GET /api/employees
```

Validar detalle del manager de ingenieria con historial:

```http
GET /api/employees/2
```

Validar bono de manager con mas de un ano:

```http
GET /api/employees/2/bonus
```

Debe retornar `isBonusEligible = true` y bono de manager.

Validar bono de empleado regular con 2 anos:

```http
GET /api/employees/3/bonus
```

Debe retornar `isBonusEligible = true` y bono de empleado regular.

Validar bono de empleado regular con 2 meses:

```http
GET /api/employees/4/bonus
```

Debe retornar:

```json
{
  "isBonusEligible": false,
  "bonusAmount": 0,
  "ineligibilityReason": "Employee must have at least one year of tenure to receive a bonus."
}
```

Validar empleados de ingenieria con proyectos:

```http
GET /api/employees/department/1/with-projects
```

Debe retornar los empleados regulares de ingenieria que tienen proyectos activos asignados.

## Ejecutar Pruebas

```powershell
dotnet test EmployeeManagement.sln
```

## Notas

- La solucion usa una unica migracion inicial para representar el modelo final.
