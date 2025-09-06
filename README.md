# TicTacToeApp
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-9.0-blue.svg)](https://dotnet.microsoft.com/)

Aplicación Blazor para registrar jugadores de Tic-Tac-Toe.  
Trabajo: **Registro de jugadores TicTacToe**

## Contenido del repo
- `Entities/` : entidad `Jugador`
- `DAL/` : `TicTacToeContext` (DbContext)
- `Services/` : `JugadoresService` (lógica CRUD y validaciones)
- `Components/Pages/Jugadores/` : páginas `Index.razor`, `Create.razor`
- `Migrations/` : migraciones EF Core
- `appsettings.json` : connection string (desarrollo)
- `Program.cs` : configuración y registro de servicios

## Requisitos para ejecutar (local)
1. .NET 9 SDK
2. SQL Server (LocalDB o SQL Express)
3. dotnet-ef (dotnet tool install --global dotnet-ef)

## Pasos para correr localmente
```bash
# Restaurar paquetes y compilar
dotnet build

# Aplicar migraciones (si no se han aplicado)
dotnet ef database update

# Ejecutar
dotnet run
