# 📝 To-Do List

### Autor: Camilo Gómez Cristancho

Esta aplicación permite a los usuarios autenticarse, gestionar tareas (CRUD), visualizar notificaciones y ver estadísticas clave desde un dashboard. Fue desarrollada como parte del proceso técnico para **ON OFF Soluciones en Línea**, aplicando buenas prácticas en arquitectura y diseño de software.

La solución incluye un **frontend en Angular 17** y un **backend en .NET 9**, con funcionalidades de autenticación, gestión de tareas, notificaciones y un dashboard con métricas clave.

---

## Requisitos Previos

### Frontend
- Node.js (versión 18 o superior)
- Angular CLI (versión 17)

### Backend
- SDK de .NET 9
- SQL Server
- Entity Framework Core CLI

### Base de Datos
- SQL Server

---

## Instrucciones de Configuración

### 1. Configuración de la Base de Datos

- Ubique el archivo `SCRIPT.sql` en la raíz del repositorio.
- Ejecute el script en su instancia de SQL Server para crear el esquema y las tablas necesarias.
- Actualice la cadena de conexión en el archivo `appsettings.json` del proyecto `ToDoListAPI` con las credenciales de su servidor:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=su_servidor;Database=ToDoListDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

### 2. Configuración del Backend

```bash
cd ToDoListApi
dotnet restore
dotnet ef database update
dotnet run
```

## 3. Configuración del Frontend

```bash
cd ToDoListFrontend
npm install
ng serve
```

## Decisiones Técnicas Tomadas
### Backend (.NET 9)
- DB First con Scaffold: Generación de modelos desde SCRIPT.sql con Scaffold-DbContext.

- Arquitectura DDD (Domain-Driven Design): Separación en capas Application, Domain, Infrastructure.

- AutoMapper: Mapear DTOs ↔ Entidades de dominio, reduciendo código repetitivo.

- Inyección de Dependencias (DI): Uso del contenedor nativo de .NET.

- Autenticación JWT: Protección de endpoints mediante tokens con middleware personalizado.

### Frontend (Angular 17)
- Angular Standalone Components: Componentes sin NgModules para arquitectura más modular y ligera.

- Angular Material: Diseño UI consistente y accesible.

- Tailwind CSS: Estilizado rápido y responsive (utility-first).

- Diseño Responsive: Uso de clases como md:grid-cols-4, flex, justify-center, etc.

## Cómo Ejecutar el Proyecto
Configure la base de datos como se explicó anteriormente.

### Inicie el backend:

```bash
cd ToDoListApi
dotnet run
```

## Inicie el frontend:

```bash
cd ToDoListFrontend
ng serve
```

## Registrese

Debe crear un usuario y contraseña para poder ingresar.

## ✅ Notas Finales

Este proyecto fue desarrollado aplicando principios de clean code, DI y una arquitectura sólida tanto en backend como frontend.

📬 Para dudas o comentarios, no dudes en contactarme.

📅 **Fecha de entrega:** 18 de julio de 2025