## Houses API
This project is an ASP.NET Core Web API for managing and retrieving information about houses. It fetches house data from an external API and populates a local SQLite database. It also includes caching to improve performance and reduce database queries.

>[This idea came to me through this repository](https://github.com/jogarriot/prova-2024/blob/master/test1/README.md)

### Features
- Fetches house data from an external API [An API of Ice and Fire](https://anapioficeandfire.com/)
- Stores the house data in a local SQLite database
- Provides caching to minimize database access for frequently requested data
- Includes basic API endpoints to list all houses or retrieve a house by its ID

### Technologies Used
- C# / .NET 8
- ASP.NET Core Web API
- SQLite (via Entity Framework Core)

## Setup Instructions

### Prerequisites
Before you begin, make sure you have the following dependencies installed on yout system:
- [.NET 8 SDK](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)
- [Entity Framework Core](#entity-framework-core-cli-install)
- [SQLite](https://www.sqlite.org/) (optional, since it's automatically handled by Entity Framework)


> #### Entity Framework Core CLI install
> To install the Entity Framework Core, run the code below in you terminal
> ```bash 
> $ dotnet tool install --global dotnet-ef
> ````


### 1. Clone the repository
Run the following command in your terminal to clone the repository:
> replace `<project-name>` with the name of the folder you want
```bash
$ git clone https://github.com/your-username/house-management-api.git <project-name>
$ cd <project-name>
```

### 2. Install Dependencies
Run the following command to restore the .NET dependencies:
```bash
$ dotnet restore
```


### 3. Configure the SQLite Database
The connection string is set in `Program.c`s to use a local SQLite database named `houses.db`.

If you need to change the database location or settings, modify the connection string in `Program.cs`:
```csharp
string connectionString = "Data Source=houses.db";
```

### 4. Run Database Migrations
Before running the project, apply migrations to set up the database schema:
```bash
$ dotnet ef database update
```

### 5. Run the Application
To start the application, use:
```bash
$ dotnet run
```
The API will be available at `http://localhost:5020`.

### 6. Access API Documentation - Swagger (Optional)
You can explore the available API endpoints using the automatically generated Swagger UI. After running the application, navigate to: [http://localhost:5020/swagger]