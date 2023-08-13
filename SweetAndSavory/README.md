## Pierres Sweet & Savory Treats

# By Shankaron Mohamed 

## Decription
This application enables users to log in and log out through user authentication while showcasing the implementation of a many-to-many relationship. Logged-in users can utilize features for creating, updating, and deleting, while all users possess read functionality. 


## Utilized Technologies
- C#
- Git
- MySQL Workbench
- Enity Framework Core 
- ASP.NET Core MVC
- HTML & CSS
- Razor
- Bootstrap

# Setup 

- In the terminal run these commands in order: $ git clone SweetAndSavory.Solution

- Cd SweetAndSavory.Solution

- Next in the command line: touch .gitignore, then copy/paste this into the .gitignore file: obj bin appsettings.json

- Navigate to this project's production directory called "SweetAndSavory" with $ cd SweetAndSavory. Within the production directory run the command $ touch appsettings.json. -In the appsettings.json file, paste in the following code, replacing [user-id] and [password] with your username and password for MySQL Workbench. (Remember to remove the square brackets when inputting your details): { `"ConnectionStrings": { "DefaultConnection": "Server=localhost;Port=3306;database=factory;uid=[user-id];pwd=[password];" } }`` Within the production directory "SweetAndSavory", run $ dotnet ef database update to instantiate the database.

- Still within the production directory, run $ dotnet watch run in the command line to launch the application in development mode in a browser, and interact with the application.
- Once on the application,follow instructions and create Login information. 
- Navigate webpage and enjoy


# Known Bugs
- Currently in the Treats class, There's an issue with adding multipe flavors to a specific treat. 
- Hopefully will get this figured out soon

# MIT License
Copyright (c) 2023 Shankaron Mohamed




