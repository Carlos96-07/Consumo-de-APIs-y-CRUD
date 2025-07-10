# Sistema Web de Gestión - Usuarios, Productos y Facturación

Este proyecto es una solución web completa que utiliza **.NET Core + Entity Framework Core** para el backend (API REST), y **ASP.NET MVC con Razor Pages** para el frontend. Ambos están conectados a una **base de datos SQL Server**.

---

## Características del sistema

### 🔙 Backend (.NET Core + EF Core - API REST)

- Arquitectura por capas: `Controllers`, `Services`, `Repositories`, `Models`.
- Exposición de endpoints REST para:
  - Usuarios
  - Productos
  - Facturas
- Conexión a **SQL Server** mediante **Entity Framework Core**.
- Manejo de migraciones y base de datos con EF Core.
- Validaciones:
  - Contraseñas seguras
  - Campos obligatorios
  - Formatos específicos (correo, número, etc.)

---

### Frontend (ASP.NET MVC con Razor)

- Interfaces con HTML5, Bootstrap 5, JavaScript y Razor Pages.
- Validación en frontend y backend.
- Consumo de la API REST usando `HttpClient` u otras técnicas.
- Funcionalidades completas para:
  - Administración de usuarios
  - Registro y edición de productos
  - Creación y visualización de facturas

---

### Base de datos (SQL Server)

- Modelo de datos gestionado con EF Core.
- Migraciones automáticas para la creación de tablas.
- Relaciones entre entidades: Usuarios → Facturas → Productos.

---

## Tecnologías utilizadas

- ASP.NET Core 6.0+
- Entity Framework Core
- ASP.NET MVC con Razor
- SQL Server
- Bootstrap 5
- HTML/CSS/JavaScript

---
