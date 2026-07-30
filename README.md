# SistExtensionesTel

[![Build](https://github.com/CristiamSanchez/SistExtensionesTel/actions/workflows/dotnet-build.yml/badge.svg)](https://github.com/CristiamSanchez/SistExtensionesTel/actions/workflows/dotnet-build.yml)
[![ASP.NET Core MVC](https://img.shields.io/badge/ASP.NET-Core%20MVC-512BD4?logo=dotnet&logoColor=white)](https://learn.microsoft.com/aspnet/core/)
[![Docker Compose Ready](https://img.shields.io/badge/Docker-Compose%20Ready-2496ED?logo=docker&logoColor=white)](https://docs.docker.com/compose/)

Una pequeña solución CRUD para gestionar extensiones telefónicas, desarrollada con ASP.NET Core MVC, Identity y SQL Server, y preparada para ejecutarse de forma reproducible con Docker Compose.

## Skills / Tech Stack / Impact

### Skills
- ASP.NET Core MVC
- ASP.NET Identity
- Entity Framework Core
- SQL Server
- Docker Compose
- GitHub Actions

### Impact
- Validación de negocio con restricciones únicas para evitar duplicados
- Autenticación y autorización para un flujo real de usuarios
- Estructura reproducible para demo local y presentación como proyecto de portfolio

## Project Snapshot

### Home
![Home](wwwroot/images/Porfolio/Home.png)

### Login
![Login](wwwroot/images/Porfolio/Login.png)

### Create
![Create](wwwroot/images/Porfolio/Create.png)

### Edit
![Edit](wwwroot/images/Porfolio/Edit.png)

### Users
![Users](wwwroot/images/Porfolio/Users.png)

## Quick Demo

```bash
docker compose -f SistemaTelefonico/compose.yaml up -d --build
```

Luego abre la app en `http://localhost:8091` y valida el flujo de:
- login
- creación
- edición
- validación de número duplicado

## Notes

Este proyecto me permitió practicar de forma integral:
- la integración de Identity con EF Core
- la configuración de SQL Server con Docker
- la estructura de un proyecto MVC listo para GitHub y CI
- la preparación de documentación profesional para portfolio

## Build Validation

```bash
dotnet build SistemaTelefonico.csproj --configuration Release
```

La verificación realizada confirmó una compilación exitosa del proyecto.

## Release Notes

### v1.0 - Portfolio Edition
- CRUD funcional con validaciones de negocio
- autenticación con Identity
- integración con SQL Server y Docker Compose
- documentación orientada a GitHub
- workflow de CI básico
