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
- **Autenticación prevista:** JWT
- **Control de versiones:** Git y GitHub
- **Entorno de despliegue previsto:** Linux / Ubuntu Server

Las tecnologías y herramientas se incorporarán progresivamente durante el desarrollo.

## Funcionalidades implementadas

- [x] Gestión básica de productos (CRUD).
- [x] Gestión de categorías.
- [x] Gestión de clientes.
- [x] Gestión básica de pedidos.
- [x] Modelado inicial de entidades relacionadas con inventario.
- [x] Definición de unidades de medida y tipos de movimientos de inventario.
- [x] Validador de cantidades según la unidad de medida.

## Funcionalidades en desarrollo y planificadas

- [ ] Registro de movimientos de inventario.
- [ ] Gestión de lotes y pallets.
- [ ] Recepción de mercadería de proveedores.
- [ ] Control de existencias y disponibilidad de productos.
- [ ] Seguimiento del estado de los pedidos.
- [ ] Validación de reglas de negocio.
- [ ] Transacciones y consistencia de las operaciones de inventario.
- [ ] Registro de usuarios y empleados.
- [ ] Autenticación mediante JWT.
- [ ] Desactivación de usuarios y trazabilidad de operaciones.
- [ ] Autorización y protección de endpoints.
- [ ] Pruebas automatizadas.
- [ ] Procesamiento de tareas en segundo plano.
- [ ] Migración de SQLite a PostgreSQL.
- [ ] Despliegue en Linux.

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

## Arquitectura prevista

AgroManager está pensado como un sistema centralizado:

- Una API central desarrollada con ASP.NET Core.
- Una base de datos centralizada.
- Clientes de escritorio que se comunicarán con la API.
- Usuarios individuales para identificar a los empleados que realizan operaciones.

La lógica de negocio y las validaciones importantes estarán del lado del backend, evitando depender de la aplicación cliente para garantizar la integridad de los datos.

## Estado del proyecto

**En desarrollo.**

El proyecto se construye de forma incremental. Las funcionalidades se marcarán como completadas cuando estén implementadas y verificadas.

## Aprendizajes y documentación

Las decisiones técnicas, los problemas encontrados y las soluciones adoptadas se documentarán a medida que avance el desarrollo.