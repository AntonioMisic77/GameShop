# GameShop

Repository for the 2023/2024 Information System Project by Antonio Mišić and Marin Pudić

## Getting Started

To get started with this project, follow the steps below:

### Prerequisites

- Install Angular CLI: [Angular CLI Installation Guide](https://angular.io/cli)
- Install .NET Core SDK: [Download .NET Core](https://dotnet.microsoft.com/download)

### Database Setup

1. Install Postgres: [Postgres Installation Guide](https://www.postgresql.org/download/)
2. Create a new database named "GameShop" in Postgres.

### Backend Setup

1. Open the `ProdavaonicaIgaraAPI` folder in your preferred IDE.
2. Restore the NuGet packages.
3. Update the connection string in the `appsettings.json` file to point to your local Postgres database.
4. Run the database initialization script `ProdavaonicaIgara_baza.sql` to create the necessary tables and seed data.

### Frontend Setup

1. Open the `ProdavaonicaIgaraFrontend` folder in your preferred IDE.
2. Install the required dependencies by running `npm install` in the terminal.
3. Update the API base URL in the `environment.ts` file to match your backend API URL.
4. Start the Angular development server by running `npm start` in the terminal.

## Usage

Once the backend and frontend are set up, you can access the application by navigating to `http://localhost:4200` in your web browser.