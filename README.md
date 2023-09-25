# BookStoreAPI
Welcome to Bookstore API! Let's begin...

Most notable packages one should know:
 -asp.net 6 core
 -runs on port 9000
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

## Booting up 
After setting up the database, the initial startup will give you the swagger page. 
![image](https://github.com/n0laja00/BookStoreAPI/assets/73889850/643c62a6-29e8-4bb7-acb3-8479bc08ae6a)

# BookStoreAPI.Tests

 Things to know about .Tests:
 -asp.net 6 core
 -Fluent Assertions
 -xUnit
 -EntityFrameworkCore.InMemory

 ## Database for tests
 Database is held inside InMemory database. It doesn't need any setup to work. It's filled with dummy data. 
 ![image](https://github.com/n0laja00/BookStoreAPI/assets/73889850/988eb9e5-2d33-48ea-bc27-2f35d7d1ae41)
