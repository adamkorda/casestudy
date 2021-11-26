# Products API

### Provides simple access to Products `api/products`
There are two API versions available V1 and V2. Both can be accessed by either URL e.g. `api/v1/products` or by QueryString e.g. `api/products?api-version=1`
Requests without specified version will default to V1.

You can perform following actions:
#### 1) V1
  * Get all the products `GET api/products`
  * Get one specific product by its identifier `GET api/products/[id]`
  * Update description of any product `PATCH api/products`
#### 2) V2
* Get all the products with pagination `GET api/v2/products?pagenumber=5&pagesize=100`

# Solution
Repository contains two projects **Products.Api** and **Products.Api.Tests**
Both project are build upon **ASP.NET Core 6**
**Product.Api** project is using **MSSQL database** with **EF Core as ORM**. It includes **EF Core Migrations** and **Swagger** documentation
**Product.Api.Tests** project is using **InMemoryDatabase** as is persistent store to perform integration tests

### Prerequisites:
* Installed ASP.NET Core 6
* Accessible some kind of MSSQL database (you need to change **ConnectionStrings.Default** in your appsettings.`environment`.json, depends on your environment)
* To perform migrations you need to have install **dotnet-ef** tool (**optional**)
```
dotnet tool install --global dotnet-ef
```

### Steps to run:

1) In solution directory
```
dotnet build
```
2) In Products.Api directory (**optional**, database will be create even if you run application without migrations, but with migrations you get other versioning benefits)
```
dotnet ef database update
```
3) In Project.Api directory
```
dotnet run
```
4) In solution directory (**optional**, to perform unit tests)
```
dotnet test
```





