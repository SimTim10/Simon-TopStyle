# Simon-TopStyle

A web api that runs in Azure. The backend for an E-Commerce.
The solution is ASP.NET Web api and goes towards a SQL database that is set up in Azure. Using the Entity framework for the communication with the database (CRUD Functionality).
All users are managed via the database and a JSON web token is returned when the login is complete.
All data is saved in a database deployed to Azure. 
The connection string are managed via an Azure key vault.
User management is done with Core Identity. To differ between the roles of an Admin and a Customer in Authorizations
