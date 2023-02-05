## Table of contents
* [General info](#general-info)
* [Technologies](#technologies)
* [Requirements](#requirements)
* [Setup](#setup)
## General info
This project is home barender application that helps managing coctails.

Project main features:
* Create ingridients
* Create coctails recipies
* Manage informations about quantity of stored ingridients and avaible coctails
* Creates users and allow them to :
  * Create comments about coctails
  * Add coctails to favorites
  * Order available coctails
	
## Technologies
Project is created with:
- React as fronted
- Python as gateway
- .NET Core as microservice
- MS SQL as database
	
## Requirements	
Before trying to run program following enviroments has to be download and setup
* Node.js
* Python 3.0
* Net SDK 6.0
* MS SQL Server 2019
* Visual Studio 2022
## Setup
To run this project, after downloading it create following things in SQL server:
* Database
* Login
* User

Then in folder
> CoctailService 

edit **appsetings.json** file and configure it by changing connection string, user and password fileds with yours informations that you create oon SQL server. 

```json
  "ConnectionStrings": {
    "DevelopmentConnectionString": "Server = CoctailServer; Database = CoctailDB; User Id = $DbUser; Password = $DbPassword; trusted_connection = true; TrustServerCertificate = true"
  },
  "DbUser": "CoctailLogin",
  "DbPassword": "VeryConfidentialPasswordForCoctails123",
  ```

Then using visual stuido open **CoctailService.csproj** file and run CoctailService project.

After that run **FaslApi.sln** project form folder
>python

Finally from folder
> fronapp

using cmd run project using comand
>npm start
