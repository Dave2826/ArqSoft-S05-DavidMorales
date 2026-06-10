# Documentación – Migración de MVC a Arquitectura Hexagonal

**Alumno:** David Morales Guerrero
**Materia:** Arquitectura de Software
**Profesor:** Jorge Javier Pedrozo
**Proyecto:** CitasApp
**Fecha:** 10/06/2026

---

# 1. Introducción

CitasApp es una aplicación web desarrollada con ASP.NET Core MVC para la gestión básica de pacientes, médicos y citas médicas.

Inicialmente el proyecto fue construido utilizando una arquitectura MVC tradicional. Posteriormente se realizó una migración hacia una arquitectura hexagonal (Ports & Adapters) con el objetivo de desacoplar la lógica de negocio de los mecanismos de almacenamiento y mejorar la mantenibilidad del sistema.

La aplicación utiliza persistencia mediante archivos JSON y permite consultar información relacionada con pacientes, médicos y citas desde una interfaz web.

---

# 2. Arquitectura Inicial (MVC)

La primera versión del sistema estaba organizada de la siguiente manera:

```text
Controllers/
Models/
Repositories/
ViewModels/
Views/
Data/
```

En esta estructura los controladores dependían directamente de los repositorios, y estos accedían a los archivos JSON donde se almacenaba la información.

Flujo de funcionamiento:

```text
Usuario
   ↓
Controller
   ↓
Repository
   ↓
JSON
```

Esta arquitectura es adecuada para aplicaciones pequeñas, pero genera una dependencia directa entre la lógica de la aplicación y el mecanismo de almacenamiento.

---

# 3. Problema Identificado

La principal limitación del enfoque inicial es el acoplamiento entre la aplicación y la tecnología utilizada para almacenar los datos.

Por ejemplo, si en el futuro se requiere reemplazar los archivos JSON por SQL Server, PostgreSQL o cualquier otra tecnología, sería necesario modificar varias partes del sistema.

Además, si un archivo JSON se corrompe o se elimina accidentalmente, la información almacenada podría perderse.

---

# 4. Objetivo de la Migración

La migración a arquitectura hexagonal tuvo como objetivo:

* Separar responsabilidades.
* Reducir el acoplamiento entre componentes.
* Facilitar futuras migraciones a otros mecanismos de persistencia.
* Mejorar el mantenimiento del sistema.
* Aplicar principios de arquitectura utilizados en proyectos empresariales.

---

# 5. Arquitectura Hexagonal Implementada

La solución fue reorganizada en tres proyectos principales:

```text
Citas.App.sln
│
├── CitasApp.Domain
│   ├── Models
│   └── Interfaces
│
├── CitasApp.Infrastructure
│   └── Repositories
│
└── Citas.App.sln
    ├── Controllers
    ├── Views
    ├── ViewModels
    └── Program.cs
```

Esta estructura permite aislar la lógica de negocio de la infraestructura y de la interfaz de usuario.

---

# 6. Función de Cada Proyecto

## CitasApp.Domain

Contiene el núcleo del sistema.

Incluye:

### Entidades

```text
Paciente
Medico
Cita
```

### Interfaces

```text
IPacienteRepository
IMedicoRepository
ICitaRepository
```

Las interfaces actúan como contratos que definen cómo se accede a la información sin depender de una tecnología específica.

---

## CitasApp.Infrastructure

Contiene las implementaciones concretas de los repositorios.

Ejemplos:

```text
PacienteRepository
MedicoRepository
CitaRepository
```

Actualmente estos repositorios utilizan archivos JSON para almacenar la información.

---

## Proyecto Web (ASP.NET Core MVC)

Contiene la capa de presentación:

```text
Controllers
Views
ViewModels
```

Los controladores interactúan con las interfaces definidas en Domain mediante Dependency Injection.

---

# 7. Flujo de Funcionamiento Actual

La aplicación ya no depende directamente de los archivos JSON.

El acceso a los datos se realiza a través de interfaces.

```text
Usuario
   ↓
Controller
   ↓
IPacienteRepository
   ↓
PacienteRepository
   ↓
JSON
```

Este diseño permite reemplazar fácilmente el mecanismo de almacenamiento sin modificar la lógica principal del sistema.

---

# 8. Ejemplo de Escalabilidad

Actualmente la aplicación utiliza:

```text
PacienteRepository
↓
JSON
```

Sin embargo, en el futuro podría utilizar:

```text
PacienteSqlRepository
↓
SQL Server
```

o

```text
PacientePostgresRepository
↓
PostgreSQL
```

sin modificar los controladores ni las vistas.

Únicamente sería necesario registrar la nueva implementación mediante Dependency Injection.

---

# 9. Ventajas Obtenidas

La migración permitió:

* Separar la lógica de negocio de la infraestructura.
* Reducir dependencias directas.
* Facilitar el mantenimiento.
* Mejorar la escalabilidad.
* Facilitar futuras migraciones a bases de datos.
* Aplicar principios de arquitectura empresarial.

---


# 10. Conclusión

La migración de MVC tradicional a arquitectura hexagonal permitió desacoplar la lógica de negocio de la infraestructura de almacenamiento.

Aunque actualmente la aplicación continúa utilizando archivos JSON, la nueva estructura permite reemplazar esta tecnología por soluciones más robustas como SQL Server o PostgreSQL sin modificar los controladores ni las vistas.

La arquitectura obtenida es más flexible, mantenible y cercana a los estándares utilizados en proyectos reales de desarrollo de software.
