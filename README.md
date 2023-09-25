# BookStore
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

 for .Tests:
 -Fluent Assertions
 -xUnit
 -EntityFrameworkCore.InMemory

## Setting up
Clone the repository normally. After the cloning process is done, make sure that Books.db exists in the same folder as BookStoreAPI. 

### The Database
Database is hosted on Sqlite. So, make sure you have that set up before booting the project. Make sure to run the following command on Package Manager Console:
```db-update```
The database will be set up during that process.

## Booting up. 
After setting up the database, the initial startup will give you the swagger page. 
![image](https://github.com/n0laja00/BookStoreAPI/assets/73889850/643c62a6-29e8-4bb7-acb3-8479bc08ae6a)
