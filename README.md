# [Swagger](https://awwcor.yushchenko.site/api/index.html?url=/api/specification.json) - https://awwcor.yushchenko.site/api

# Description

Domain
```
https://awwcor.yushchenko.site/
```
<br>

## /api/Advertisement/CreateAd - POST
``` c#
public async Task<int> Handle(CreateAdvertisementCommand request, CancellationToken cancellationToken)
    {
        var entity = new Advertisement()
        {
            Title = request.Title,
            Description = request.Description,
            Amount = request.Amount,
        };

        entity.DomainEvents.Add(new AdvertisementCreatedEvent(entity));

        _context.Advertisements.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        foreach (var imageUrl in request.ImagesUrl)
        {
            await _mediator.Send(new CreateImageCommand() { AdvertisementId = entity.Id, Url = imageUrl }, cancellationToken);
        }
        return entity.Id;
    }
```


## /api/Advertisement/GetAdvertisementsWithPagination - PUT
``` c#
public async Task<PaginatedList<AdvertisementBriefDto>> Handle(GetAdvertisementsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        PaginatedList<AdvertisementBriefDto>? advertisements = null;
        if (request.OrderBy == EOrderByType.CreatedDate && request.OrderType == EOrderType.Increase)
        {
            advertisements = await _context.Advertisements
                        .AsNoTracking()
                        .OrderBy(x => x.Created)
                        .ProjectTo<AdvertisementBriefDto>(_mapper.ConfigurationProvider)
                        .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else if (request.OrderBy == EOrderByType.CreatedDate && request.OrderType == EOrderType.Descending)
        {
            advertisements = await _context.Advertisements
                        .AsNoTracking()
                        .OrderByDescending(x => x.Created)
                        .ProjectTo<AdvertisementBriefDto>(_mapper.ConfigurationProvider)
                        .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else if (request.OrderBy == EOrderByType.Amount && request.OrderType == EOrderType.Increase)
        {
            advertisements = await _context.Advertisements
                        .AsNoTracking()
                        .OrderByDescending(x => x.Amount)
                        .ProjectTo<AdvertisementBriefDto>(_mapper.ConfigurationProvider)
                        .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else if(request.OrderBy == EOrderByType.Amount && request.OrderType == EOrderType.Descending)
        {
            advertisements = await _context.Advertisements
                        .AsNoTracking()
                        .OrderByDescending(x => x.Amount)
                        .ProjectTo<AdvertisementBriefDto>(_mapper.ConfigurationProvider)
                        .PaginatedListAsync(request.PageNumber, request.PageSize);
        }

        foreach (var item in advertisements.Items)
        {
            List<Image> images = new();
            if(item.Images?.Count >= 1) images.Add(item.Images[0]);
            item.Images = images;
        }
        return advertisements;
    }
```
## /api/Advertisement/GetAdvertisementById - PUT
``` c#
public async Task<object> Handle(GetAdvertisementByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.AllFields)
        {
            var advertisement = _context.Advertisements.FirstOrDefault(x => x.Id == request.Id);
            if (advertisement != null) advertisement.Images = _context.Images.Where(x => x.AdvertisementId == advertisement.Id).ToList();
            return advertisement;
        }
        else
        {
            return _context.Advertisements
                .Where(x => x.Id == request.Id)
                .Select(x => new AdvertisementAbbreviated
                {
                    Title = x.Title,
                    Amount = x.Amount,
                    Image = x.Images[0]
                });
        }
    }
```
<img align="left" width="116" height="116" src="https://raw.githubusercontent.com/jasontaylordev/CleanArchitecture/main/.github/icon.png" />
 
 # Clean Architecture Solution Template
![.NET Core](https://github.com/jasontaylordev/CleanArchitecture/workflows/.NET%20Core/badge.svg) 
[![Clean.Architecture.Solution.Template NuGet Package](https://img.shields.io/badge/nuget-6.0.1-blue)](https://www.nuget.org/packages/Clean.Architecture.Solution.Template) 
[![NuGet](https://img.shields.io/nuget/dt/Clean.Architecture.Solution.Template.svg)](https://www.nuget.org/packages/Clean.Architecture.Solution.Template)
[![Discord](https://img.shields.io/discord/893301913662148658?label=Discord&logo=discord&logoColor=white)](https://discord.gg/p9YtBjfgGe)
[![Twitter Follow](https://img.shields.io/twitter/follow/jasontaylordev.svg?style=social&label=Follow)](https://twitter.com/jasontaylordev)


<br/>

This is a solution template for creating a Single Page App (SPA) with Angular and ASP.NET Core following the principles of Clean Architecture. Create a new project based on this template by clicking the above **Use this template** button or by installing and running the associated NuGet package (see Getting Started for full details). 

## Learn about Clean Architecture

[![Clean Architecture with ASP.NET Core 3.0 ??? Jason Taylor ??? GOTO 2019](https://img.youtube.com/vi/dK4Yb6-LxAk/0.jpg)](https://www.youtube.com/watch?v=dK4Yb6-LxAk)

## Technologies

* [ASP.NET Core 6](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0)
* [Entity Framework Core 6](https://docs.microsoft.com/en-us/ef/core/)
* [Angular 12](https://angular.io/)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://fluentvalidation.net/)
* [NUnit](https://nunit.org/), [FluentAssertions](https://fluentassertions.com/), [Moq](https://github.com/moq) & [Respawn](https://github.com/jbogard/Respawn)
* [Docker](https://www.docker.com/)

## Getting Started

The easiest way to get started is to install the [NuGet package](https://www.nuget.org/packages/Clean.Architecture.Solution.Template) and run `dotnet new ca-sln`:

1. Install the latest [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
2. Install the latest [Node.js LTS](https://nodejs.org/en/)
3. Run `dotnet new --install Clean.Architecture.Solution.Template` to install the project template
4. Create a folder for your solution and cd into it (the template will use it as project name)
5. Run `dotnet new ca-sln` to create a new project
6. Navigate to `src/WebUI/ClientApp` and run `npm install`
7. Navigate to `src/WebUI/ClientApp` and run `npm start` to launch the front end (Angular)
8. Navigate to `src/WebUI` and run `dotnet run` to launch the back end (ASP.NET Core Web API)

Check out my [blog post](https://jasontaylor.dev/clean-architecture-getting-started/) for more information.

### Docker Configuration

In order to get Docker working, you will need to add a temporary SSL cert and mount a volume to hold that cert.
You can find [Microsoft Docs](https://docs.microsoft.com/en-us/aspnet/core/security/docker-https?view=aspnetcore-6.0) that describe the steps required for Windows, macOS, and Linux.

For Windows:
The following will need to be executed from your terminal to create a cert
`dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p Your_password123`
`dotnet dev-certs https --trust`

NOTE: When using PowerShell, replace %USERPROFILE% with $env:USERPROFILE.

FOR macOS:
`dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p Your_password123`
`dotnet dev-certs https --trust`

FOR Linux:
`dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p Your_password123`

In order to build and run the docker containers, execute `docker-compose -f 'docker-compose.yml' up --build` from the root of the solution where you find the docker-compose.yml file.  You can also use "Docker Compose" from Visual Studio for Debugging purposes.
Then open http://localhost:5000 on your browser.

To disable Docker in Visual Studio, right-click on the **docker-compose** file in the **Solution Explorer** and select **Unload Project**.

### Database Configuration

The template is configured to use an in-memory database by default. This ensures that all users will be able to run the solution without needing to set up additional infrastructure (e.g. SQL Server).

If you would like to use SQL Server, you will need to update **WebUI/appsettings.json** as follows:

```json
  "UseInMemoryDatabase": false,
```

Verify that the **DefaultConnection** connection string within **appsettings.json** points to a valid SQL Server instance. 

When you run the application the database will be automatically created (if necessary) and the latest migrations will be applied.

### Database Migrations

To use `dotnet-ef` for your migrations please add the following flags to your command (values assume you are executing from repository root)

* `--project src/Infrastructure` (optional if in this folder)
* `--startup-project src/WebUI`
* `--output-dir Persistence/Migrations`

For example, to add a new migration from the root folder:

 `dotnet ef migrations add "SampleMigration" --project src\Infrastructure --startup-project src\WebUI --output-dir Persistence\Migrations`

## Overview

### Domain

This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

### Application

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.

### Infrastructure

This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

### WebUI

This layer is a single page application based on Angular 10 and ASP.NET Core 5. This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection. Therefore only *Startup.cs* should reference Infrastructure.

## Support

If you are having problems, please let us know by [raising a new issue](https://github.com/jasontaylordev/CleanArchitecture/issues/new/choose).

## License

This project is licensed with the [MIT license](LICENSE).
