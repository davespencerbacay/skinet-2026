# Skinet – E-commerce Tutorial App

A full-stack e-commerce web application built as part of a tutorial project. Skinet provides an online shopping experience where users can browse products, filter items, manage their shopping basket, and proceed through the checkout process.

## 🚀 Features

- **Product Catalog** – Browse products with images, descriptions, and prices.
- **Product Filtering & Sorting** – Find products by category, brand, and price.
- **Shopping Basket** – Add, remove, and manage items before checkout.
- **User Authentication** – Register and log in to an account.
- **Checkout Process** – Complete the steps required to place an order.
- **Responsive Design** – Access the application on desktop, tablet, and mobile devices.
- **RESTful API** – Retrieve and manage application data through backend endpoints.

*Note: Features depend on the implementation in your project.*

## 🛠️ Technologies Used

Update this section to reflect the technologies in your application.

- **Frontend:** Angular, TypeScript, HTML, CSS
- **Backend:** ASP.NET Core Web API, C#
- **Database:** SQL Server
- **ORM:** Entity Framework Core
- **Authentication:** JWT (if implemented)
- **Payment Integration:** Stripe (if implemented)
- **Version Control:** Git and GitHub

## 📋 Prerequisites

Before running the project, ensure you have the following installed:

- [Node.js](https://nodejs.org/)
- [Angular CLI](https://angular.dev/tools/cli)
- [.NET SDK](https://dotnet.microsoft.com/download)
- SQL Server or another database configured for the project
- Git

Check your installed versions:

```bash
node --version
npm --version
dotnet --version
ng version
```

## ⚙️ Installation and Setup

### 1. Clone the Repository

```bash
git clone <your-repository-url>
cd <your-project-folder>
```

Replace the placeholders with your GitHub repository URL and project folder.

### 2. Configure the Backend

Navigate to the backend project directory:

```bash
cd <backend-directory>
```

Restore the required .NET packages:

```bash
dotnet restore
```

Configure your database connection string in `appsettings.json` or your preferred configuration source.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=SkinetDb;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

Use the connection string format appropriate for your database and environment. Do not commit production credentials or secrets.

### 3. Apply Database Migrations

If the project uses Entity Framework Core migrations, run:

```bash
dotnet ef database update
```

If the EF Core CLI is not installed, install it using the official [EF Core tools documentation](https://learn.microsoft.com/en-us/ef/core/cli/dotnet).

### 4. Run the Backend API

From the backend project directory, execute:

```bash
dotnet run
```

Check the terminal output for the API URL and port. If Swagger is configured, use its URL to explore the available endpoints.

### 5. Install and Run the Frontend

Open another terminal and navigate to the Angular application directory:

```bash
cd <frontend-directory>
```

Install the dependencies:

```bash
npm install
```

Start the development server:

```bash
ng serve
```

Open the local URL shown in the terminal, commonly:

```text
http://localhost:4200
```

Make sure the frontend API configuration points to the running backend.

## 📁 Project Structure

The following is an example structure. Adjust it to match your repository.

```text
Skinet/
├── API/
│   ├── Controllers/
│   ├── Data/
│   ├── Entities/
│   ├── Extensions/
│   ├── Middleware/
│   └── Program.cs
│
├── client/
│   ├── src/
│   │   └── app/
│   │       ├── core/
│   │       ├── shared/
│   │       ├── shop/
│   │       └── app.routes.ts
│   ├── angular.json
│   └── package.json
│
├── .gitignore
└── README.md
```

## 🧪 Running Tests

Run the backend tests, if available:

```bash
dotnet test
```

Run the frontend tests using the test script configured in `package.json`, for example:

```bash
npm test
```

## 🔐 Environment Configuration

Before running the application, verify the following:

- Database connection settings are correct.
- API URLs match the local backend configuration.
- Required environment variables are configured.
- Authentication and payment credentials are stored securely when applicable.

Never commit passwords, API keys, payment secrets, or production credentials to version control.

## 📚 Learning Objectives

This project can help demonstrate practical experience with:

- Full-stack web application development
- RESTful API design and integration
- Angular components, services, and routing
- Database design and Entity Framework Core
- Authentication and authorization
- Shopping basket and checkout workflows
- Frontend and backend integration

## 🛣️ Future Improvements

Potential enhancements include:

- Product reviews and ratings
- Wishlist functionality
- Order history and tracking
- Admin dashboard for product management
- Improved search and pagination
- Automated testing and deployment

## 👨‍💻 Author

**Your Name**

- GitHub: [Your GitHub Profile](https://github.com/your-username)

## 📄 License

This project is intended for educational purposes as part of a tutorial. Add a license file if you intend to distribute the project under specific license terms.
