# BookStoreAPI
This is a bookstore or just a book api written in c# using asp.net core 6 core. THe goal of this project was to make an api, that returns books to anyone who calls it. It's also designed to be somewhat resistant to XSS attacks and has some rudimentary unit tests.

Most notable packages one should know:
 
 -asp.net 6 core
 
 -runs on port 9000 (http) and 9001 (https)
 
 -c#
 
 -AspNetCore.Identity.EntityFrameworkCore
 
 -Microsoft.EntityFrameworkCore.Sqlite
 
 -Microsoft.EntityFrameworkCore.Tools
 
 -Newtonsoft
 
 -HtmlSanitizer 

## Setting up
Clone the repository normally. After the cloning process is done, make sure that Books.db exists in the same folder as BookStoreAPI. 

### The Database
Database is hosted on Sqlite (in the file Books.db). So, make sure you have the file before booting the project. Make sure to run the following command on Package Manager Console:
```db-update```
The database will be set up during that process.

### Folder categorisation

![image](https://github.com/n0laja00/BookStoreAPI/assets/73889850/2619f2e3-dee4-4ad9-97db-4df83f033929)

Starting from the top, we have controllers, that contains BooksController, which is reponsible for getting data. 

Data folder contains datacontext and db migration files. 

Middleware contains errorhandling and anti-Xss middleware. 

Models houses three datamodels, Book, HttpResponseException, and PostRequestResponseMessage.

Utilities consists of two utility classes, HttpResponseUtilities and QueryUtilities. HttpResponseUtilities is used for throwing exceptions and AueryUtilities is used for depositing functions from BooksController to make it cleaner.


## Booting up 
After setting up the database, the initial startup will give you the swagger page. 

![image](https://github.com/n0laja00/BookStoreAPI/assets/73889850/643c62a6-29e8-4bb7-acb3-8479bc08ae6a)

Responses containing books are sent back in Json.

![image](https://github.com/n0laja00/BookStoreAPI/assets/73889850/f1416e66-af17-46eb-a7f5-557d611b15ed)


# BookStoreAPI.Tests

 Things to know about .Tests:
 
 -asp.net 6 core
 
 -Fluent Assertions
 
 -xUnit
 
 -EntityFrameworkCore.InMemory

 ## Database for tests
 Database is held inside InMemory database. It doesn't need any setup to work. It's filled with dummy data. 

 ![image](https://github.com/n0laja00/BookStoreAPI/assets/73889850/988eb9e5-2d33-48ea-bc27-2f35d7d1ae41)

## running tests
Tests can be run from the top bar as normal. The .Tests file should automatically a connection to BookStoreAPI solution. If all goes well, the tests should execute successfully.

![image](https://github.com/n0laja00/BookStoreAPI/assets/73889850/75624d3d-01df-44c7-8ea6-3c71e2fa0144)

