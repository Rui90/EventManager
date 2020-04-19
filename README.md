### Pro agil

Basic REST Api Sekeleton on top of .NET Core 3.1 with Entity Framework 3.1, integrated with a SPA on top of Angular 9.

The project will increment across the time.

### .NET Core introduction

Small service that returns events that are stored in an sql lite database.

.NET Core Organization into multiple projects:

- WebApi: where are the controllers that we will access
- Repository:
- Domain: All the models that we're going to use with Entity Framework and relations betweens entities

### Angular introduction

First components integrated with bootstrap that requests the .NET CORE Rest Api.
Playground with grids, bindings, interpolation and data-binding using bootstrap. It also uses reactive forms for form validation.

List of ngx-bootstrap components examples that are being used:

- Modal
- Dropdown
- Datepicker
- Tooltip

Angular Organization:

- Models: where we replicate the .NET models that we want to provide to the FE Single Page
- Services: Where we make the connection between web api and FE
- Components: Where is the actual content and logic that we will access within the page
- Helpers: Where are the pipes and directives
- Util: Static methods and constants

### Upcoming

- AutoMapper
- Routes
- Identity
- Angular Authentication
- Docker
- Project clean up (Avoid repetition, make it generic)
- Internationalization
- Unit testing
