# SistExtensionesTel

[![Build](https://github.com/CristiamSanchez/SistExtensionesTel/actions/workflows/dotnet-build.yml/badge.svg)](https://github.com/CristiamSanchez/SistExtensionesTel/actions/workflows/dotnet-build.yml)

Aplicación web para gestionar extensiones telefónicas con ASP.NET Core MVC, ASP.NET Identity y SQL Server. El proyecto está pensado como una solución práctica de CRUD con validaciones de negocio, autenticación segura y una base de datos reproducible con Docker.

## ✨ Visión general

Este repositorio demuestra un flujo completo de desarrollo para una pequeña aplicación empresarial orientada a gestión de contactos y extensiones:

- CRUD funcional de registros telefónicos
- autenticación y autorización con ASP.NET Identity
- persistencia con Entity Framework Core
- contenedorización con Docker Compose para ejecución reproducible
- preparación para GitHub como proyecto de portfolio técnico

## 🛠️ Stack tecnológico

- ASP.NET Core MVC
- Razor Pages
- ASP.NET Identity
- Entity Framework Core
- SQL Server 2022
- Docker + Docker Compose
- GitHub Actions

## 📌 Funcionalidades clave

- registro, edición y eliminación de extensiones
- validación para evitar duplicados de número
- gestión de usuarios y acceso autenticado
- estructura lista para ser presentada como proyecto de evidencia técnica

## 🧭 Demo flow para GitHub

Este es un flujo simple para mostrar el proyecto en una demo rápida:

1. Clona el repositorio.
2. Crea un archivo `.env` con tus variables locales.
3. Levanta el stack con Docker:

```bash
docker compose -f SistemaTelefonico/compose.yaml up -d --build
```

4. Abre la app en `http://localhost:8091`.
5. Inicia sesión o registra una cuenta.
6. Crea una extensión, edítala y valida el flujo completo.
7. Prueba la validación de número duplicado para mostrar la lógica de negocio.

## 🖼️ Capturas de la demo

El proyecto incluye capturas visuales en la carpeta `wwwroot/images/Porfolio/`:

- `Home.png`
- `Login.png`
- `Create.png`
- `Edit.png`
- `Users.png`

Estas imágenes ayudan a narrar la experiencia de usuario y a presentar el proyecto como una pieza completa de portfolio.

## ⚙️ Requisitos

- Docker Desktop o Docker Engine
- Docker Compose
- .NET SDK 10

## 🔐 Variables de entorno

Ejemplo de configuración para correr el proyecto localmente de forma segura:

```env
MSSQL_SA_PASSWORD=YourStrongPassword123!
APP_DB_USER=appuser
APP_DB_PASSWORD=ChangeMe123!
```

## 🗂️ Estructura del repositorio

- `SistemaTelefonico/`: proyecto principal ASP.NET Core
- `ST-portable.sql`: script de inicialización de SQL Server
- `SistemaTelefonico/compose.yaml`: stack Docker Compose
- `SistemaTelefonico/Dockerfile`: imagen de la app
- `.github/workflows/dotnet-build.yml`: validador de build en Actions

## 📚 Qué aprendí en este proyecto

- integración de ASP.NET Identity con Entity Framework Core
- trabajo con SQL Server y Docker Compose en un flujo reproducible
- validación de negocio con restricciones únicas en base de datos
- preparación de un repositorio profesional con documentación y CI

## ✅ Validación de build

La compilación del proyecto se validó con:

```bash
dotnet build SistemaTelefonico/SistemaTelefonico.csproj --configuration Release
```

Resultado verificado: compilación exitosa con warnings de dependencias, pero sin bloqueo funcional.

## 🔒 Recomendaciones para repositorio público

No publiques credenciales reales. Usa `.env` local o secretos gestionados por el entorno de despliegue.

## 📝 Release Notes

La versión actual está orientada a una presentación profesional de portfolio y entrega una base sólida para demostrar:

- el manejo de un CRUD con autenticación
- integración con SQL Server y Docker
- validaciones server-side
- documentación y CI sobre GitHub
