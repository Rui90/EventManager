### Event Manager

Basic REST Api Sekeleton on top of .NET Core 3.1 with Entity Framework 3.1, integrated with a SPA on top of Angular 9.

The project will increment across the time.

### Run API With Docker ###

In the base folder where the solution are run the following command to build the application:
```
docker build -f EventManager.WebApi/Dockerfile -t  webapi .
```
Then to run:
```
docker run -p 8080:80 --name the_experiment_container the_experiment
```
Finally you can consult the swagger documentation of the ip at:

<http://localhost:8080/swagger/index.html>

### .NET Core introduction

Small service that returns events that are stored in an sql lite database.
It uses the Identity Authentication System with the normal JWT Authorization Token.

.NET Core Organization into multiple projects:

- WebApi: available controllers
- Repository: Database accessors
- Domain: All the models that we're going to use with Entity Framework and relations betweens entities

All the endpoints current available can be accessed be seen on Swagger available through:

http://localhost:5000/swagger/index.html

### Angular introduction (Currently not working as expected waiting for Authentication integration)

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

- Docker
- Project clean up (Avoid repetition, make it generic)
- Permissions
- Internationalization
- Unit testing
