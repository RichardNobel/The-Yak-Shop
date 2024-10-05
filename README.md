# The Yak Shop
<small>Chief IT Coding Assignment</small>

## Running the Visual Studio solution locally
1. Make sure SQL Server Express LocalDB is available.

   Or change the Connection String in `appsettings.Development.json`.
   By default it points to the server instance `(localdb)\\MSSQLLocalDB`.

    a. Assuming this condition is met, the database named `YakShop` should be automatically created upon starting the back-end project.

2. After loading the Solution in Visual Studio, simply clicking <strong>&#9658; Start</strong> should launch both the back-end project (Swagger UI at `https://localhost:7051/swagger/index.html`) and the front-end project (the "Yak Shop" Angular application at `https://localhost:4200/`) in separate browser windows.
