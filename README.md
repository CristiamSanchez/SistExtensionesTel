# SistExtensionesTel

[![Build](https://github.com/CristiamSanchez/SistExtensionesTel/actions/workflows/dotnet-build.yml/badge.svg)](https://github.com/CristiamSanchez/SistExtensionesTel/actions/workflows/dotnet-build.yml)

Aplicación web para gestionar extensiones telefónicas con ASP.NET Core MVC, ASP.NET Identity y SQL Server. El proyecto está pensado como una pequeña solución de CRUD con buenas prácticas de validación de negocio y contenedorización con Docker.

## Objetivo

Este repositorio muestra una implementación funcional de:

- CRUD de extensiones telefónicas
- autenticación y autorización con Identity
- acceso a base de datos con Entity Framework Core
- despliegue local reproducible con Docker Compose

## Stack

- ASP.NET Core MVC
- Razor Pages
- ASP.NET Identity
- Entity Framework Core
- SQL Server 2022
- Docker + Docker Compose
- GitHub Actions

## Funcionalidades

- registrar, editar y borrar extensiones
- validación para evitar números duplicados
- flujo de autenticación seguro con Identity
- base de datos creada e inicializada automáticamente

## Demo flow

Sigue estos pasos para mostrar el proyecto en una demo rápida:

1. Clona el repositorio.
2. Crea un archivo `.env` con tus variables de entorno.
3. Ejecuta:

```bash
docker compose up -d --build
```

4. Abre la aplicación en `http://localhost:8091`.
5. Registra o inicia sesión con una cuenta.
6. Agrega una extensión telefónica.
7. Prueba un caso de número duplicado para ver la validación de negocio.

## Requisitos

- Docker Desktop o Docker Engine
- Docker Compose
- .NET SDK 10

## Variables de entorno

Ejemplo de configuración segura para correr el stack local:

```env
MSSQL_SA_PASSWORD=YourStrongPassword123!
APP_DB_USER=appuser
APP_DB_PASSWORD=ChangeMe123!
```

## Estructura del proyecto

- `Controllers/`: endpoints del MVC
- `Models/`: entidades del dominio
- `Data/`: contexto y seeding de base de datos
- `Views/`: interfaz Razor
- `compose.yaml`: stack Docker Compose
- `Dockerfile`: imagen de la aplicación
- `ST-portable.sql`: script de inicialización SQL

## Qué aprendí en este proyecto

- cómo integrar Identity con EF Core en una app ASP.NET Core MVC
- cómo organizar un stack de base de datos y app con Docker Compose
- cómo manejar validaciones de negocio usando índices únicos en SQL Server
- cómo dejar el proyecto preparado para versionado y CI/CD en GitHub

## Validación de build

La compilación del proyecto se validó con:

```bash
dotnet build SistemaTelefonico.csproj --configuration Release
```

Resultado verificado: compilación exitosa con warnings de dependencias, pero sin bloqueo funcional.

## Recomendaciones para repositorio público

No publiques credenciales reales. Usa variables de entorno locales o secretos del entorno de despliegue.
