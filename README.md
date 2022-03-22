# Vehicle Sales App

This project use the following frameworks, libraries and tools:

- .Net 6.0
- C# 10
- Swagger
- EF Core 6 (Code first)
- AutoMapper
- React.js 17
## Initial configuration

### .Net App

Adjust the `appSettings.json` file specially in the following properties as needed.
- `AllowedOrigins` Allowed hosts to request data separated by `;`, <strong>put your ReactApp host here</strong>.
- `ConnectionStrings` Update the connectionString for the `Default` property.

The project will create the tables needed by the app using migrations.

### React App

If you plan to run the project using Visual Studio, Rider, etc just press run then it will install
all the necessary packages to run the project, in the other hand if you run the project through the
console follow this steps:
- Enter to the path `{ProjectRootFolder}/VehicleSales.Web/ClientApp`.
- Open the console and run `npm install`
- Once the command finishes to install all the packages, run `npm start`