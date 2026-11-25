# AgroManager

**Sistema de gestión de pedidos e inventario para un distribuidor de insumos agrícolas.**

AgroManager es un proyecto personal desarrollado con el objetivo de profundizar en C#, .NET y el desarrollo backend mediante la resolución de problemas habituales de gestión comercial.

## Objetivo

Construir una aplicación que permita administrar productos, controlar existencias, gestionar clientes y procesar pedidos, priorizando la consistencia de los datos, la seguridad y la mantenibilidad del código.

El sistema está pensado para funcionar con una API centralizada, a la que podrán conectarse distintos clientes de escritorio dentro de una organización.

## Tecnologías

- **Lenguaje:** C#
- **Backend:** ASP.NET Core Web API (.NET 10)
- **ORM:** Entity Framework Core
- **Base de datos actual:** SQLite
- **Base de datos prevista:** PostgreSQL
- **Autenticación:** JWT (JSON Web Tokens)
- **Gestión de contraseñas:** ASP.NET Core PasswordHasher
- **Control de versiones:** Git y GitHub
- **Entorno de despliegue previsto:** Linux / Ubuntu Server

## Funcionalidades implementadas

### Gestión comercial

- [x] Gestión básica de productos (CRUD).
- [x] Gestión de categorías.
- [x] Gestión de clientes.
- [x] Gestión básica de pedidos.
- [x] Modelado inicial de entidades relacionadas con inventario.
- [x] Definición de unidades de medida y tipos de movimientos de inventario.
- [x] Validador de cantidades según la unidad de medida.

### Autenticación y usuarios

- [x] Inicialización del primer administrador.
- [x] Inicio de sesión mediante JWT.
- [x] Almacenamiento seguro de contraseñas mediante hashing.
- [x] Creación de empleados por parte de administradores.
- [x] Listado y consulta de usuarios.
- [x] Desactivación de empleados.
- [x] Autorización de endpoints según roles.
- [x] Validación del estado de la cuenta en cada petición autenticada.
- [x] Invalidación efectiva del acceso de cuentas desactivadas, incluso con JWT previamente emitidos.
- [x] Validación de nombres de usuario únicos, sin distinguir mayúsculas y minúsculas.

## Funcionalidades en desarrollo y planificadas

- [ ] Registro de movimientos de inventario.
- [ ] Gestión de lotes y pallets.
- [ ] Recepción de mercadería de proveedores.
- [ ] Control de existencias y disponibilidad de productos.
- [ ] Seguimiento del estado de los pedidos.
- [ ] Validación de reglas de negocio.
- [ ] Transacciones y consistencia de las operaciones de inventario.
- [ ] Trazabilidad de operaciones realizadas por usuarios.
- [ ] Pruebas automatizadas.
- [ ] Procesamiento de tareas en segundo plano.
- [ ] Migración de SQLite a PostgreSQL.
- [ ] Despliegue en Linux.

## Autenticación y autorización

AgroManager utiliza JWT para autenticar a los usuarios y proteger los endpoints de la API.

El sistema contempla dos roles:

- **Admin:** puede gestionar las cuentas de empleados y acceder a las operaciones administrativas.
- **Employee:** puede acceder a las funcionalidades operativas autorizadas, pero no administrar usuarios.

No existe un registro público de usuarios. El primer administrador se crea mediante un proceso de inicialización protegido por un secreto configurado fuera del código fuente. A partir de ese momento, los administradores pueden crear empleados.

Las contraseñas no se almacenan en texto plano: se procesan mediante `PasswordHasher<User>`.

Los JWT incluyen la identidad y el rol del usuario, y tienen una duración configurable. Además, un middleware consulta el estado actual de la cuenta en SQLite para impedir que una cuenta desactivada continúe utilizando un token previamente emitido.

### Configuración local

La configuración sensible se administra mediante **.NET User Secrets**, evitando almacenar claves y secretos en el repositorio.

Las claves necesarias son:

- `Jwt:SigningKey`: clave utilizada para firmar y validar los JWT. Debe ser una clave aleatoria segura, codificada en Base64, de al menos 32 bytes.
- `AdminSetup:Secret`: secreto independiente utilizado para autorizar la inicialización del primer administrador.

Para configurar el proyecto en un entorno local:

```powershell
dotnet user-secrets init
dotnet user-secrets set "Jwt:SigningKey" "<CLAVE_BASE64_SEGURA>"
dotnet user-secrets set "AdminSetup:Secret" "<SECRETO_ALEATORIO_SEGURO>"
```

Los valores anteriores son marcadores de posición: deben reemplazarse por secretos generados de forma segura. Nunca deben incluirse en Git.

Los parámetros no sensibles del JWT se configuran en `appsettings.json`, incluyendo issuer, audience y tiempo de expiración.

### Endpoints de autenticación

| Método | Endpoint | Descripción |
|---|---|---|
| POST | `/api/auth/initialize` | Inicializa el primer administrador |
| POST | `/api/auth/login` | Inicia sesión y genera un JWT |

La inicialización requiere enviar el secreto configurado en el encabezado `X-Admin-Setup`. Solo puede realizarse cuando todavía no existen usuarios registrados.

### Endpoints de usuarios

Todos estos endpoints requieren autenticación y el rol `Admin`.

| Método | Endpoint | Descripción |
|---|---|---|
| POST | `/api/users` | Crear empleado |
| GET | `/api/users` | Listar usuarios |
| GET | `/api/users/{id}` | Consultar usuario |
| PATCH | `/api/users/{id}/deactivate` | Desactivar empleado |

Para acceder a endpoints protegidos se debe enviar el token recibido durante el inicio de sesión mediante el encabezado:

```http
Authorization: Bearer <JWT>
```

## Objetivos técnicos

Además de implementar las funcionalidades, el proyecto busca explorar:

- Separación de responsabilidades y diseño de aplicaciones backend.
- Modelado relacional y consultas SQL.
- Uso de Entity Framework Core y LINQ.
- Manejo de errores y validación de datos.
- Transacciones y consistencia de operaciones.
- Concurrencia y prevención de inconsistencias en el inventario.
- Autenticación y seguridad de aplicaciones web.
- Identificación y trazabilidad de operaciones realizadas por usuarios.
- Pruebas de lógica de negocio.
- Ejecución, configuración y diagnóstico de aplicaciones en Linux.

## Arquitectura

AgroManager está pensado como un sistema centralizado:

- Una API central desarrollada con ASP.NET Core.
- Una base de datos centralizada.
- Clientes de escritorio que se comunicarán con la API.
- Usuarios individuales para identificar a los empleados que realizan operaciones.

La lógica de negocio y las validaciones importantes estarán del lado del backend, evitando depender de la aplicación cliente para garantizar la integridad de los datos.

## Estado del proyecto

**En desarrollo.**

El proyecto se construye de forma incremental. Las funcionalidades se marcan como completadas cuando están implementadas y verificadas.

## Aprendizajes y documentación

Las decisiones técnicas, los problemas encontrados y las soluciones adoptadas se documentarán a medida que avance el desarrollo.