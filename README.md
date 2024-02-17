# traffic_lights_sytem_app
# Tarffic Light dsiplay Assessment

## Table of Contents

1. [Introduction](#introduction)
2. [Prerequisites](#prerequisites)
3. [Getting Started](#getting-started)
4. [Production Release](#production-release)
5. [Usage](#usage)
6. [Logs](#logs)
7. [Handover](#handover)
8. [Process](#process)
9. [Screenshots](#screenshots)

## Introduction <a name="introduction"></a>

# Implement a traffic light system. We are required to have 4 sets of lights, as follows. 

Lights 1: Traffic is travelling south 
Lights 2: Traffic is travelling west 
Lights 3: Traffic is travelling north
Lights 4: Traffic is travelling east

The lights in which traffic is travelling on the same axis can be green at the same time. During normal hours all lights stay green for 20 seconds, but during peak times north and south lights are green for 40 seconds while west and east are green for 10 seconds. Peak hours are 0800 to 1000 and 1700 to 1900. Yellow lights are shown for 5 seconds before red lights are shown. Red lights stay on until the cross-traffic is red for at least 4 seconds, once a red light goes off then the green is shown for the required time(eg the sequence is reset). 

Bonus: At this intersection north bound traffic has a green right-turn signal, which stops the south bound traffic and allows north bound traffic to turn right. This is green at the end of north/south green light and stays green for 10 seconds. During this time north bound is green, north right-turn is green and all other lights are red. 

Implementation/Outcomes:
1.Implement a front-end and backend (you can use ‘dotnet new’ templates of your choice)
2.The backend will contain the logic and state of the running traffic lights. The front-end will be a visual representation of the traffic lights, with the data served from the backend. 
3.There’s no need to have a perfect design on the front end, something simple and functional is fine (unless this is an area of strength you would like to show off). Noting* we will review the client side code.
4.There’s no need to implement entity framework (or similar) to store the data in a database, a in-memory store is fine
Code needs to follow architecture & best practices for enterprise grade system

## Prerequisites <a name="prerequisites"></a>

List the prerequisites that the user needs to have installed before running the project.
 # Install Required NuGet Packages
- [.NET SDK](https://dotnet.microsoft.com/download)
- [Visual Studio](https://visualstudio.microsoft.com/) (optional but recommended)
- [SQL Server] instance and connection string
# List packages need to install if required
   1. Microsoft.Extensions.Logging
   2. log4Net
   3. Microsoft.AspNetCore.Cors
   4. Microsoft.EntityFrameworkCore
   5. Microsoft.EntityFrameworkCore.SqlServer
   6. Microsoft.EntityFrameworkCore.Tools
   7. Moq
   8. NUnit


## Getting Started <a name="getting-started"></a> 

Provide step-by-step instructions to set up the project locally.

1. Clone the Repository

    ```bash
    git clone https://github.com/venkatweb1234/traffic_lights_sytem_app.git
    ```

2. Open the project in Visual Studio or your preferred IDE.

3. ###  Configure the database connection string in `appsettings.json`. <a name="configure-database-connection"></a>
   -  "ConnectionStrings": {
   "SqlConn": "Data Source=.\\sqlexpress;Initial Catalog=trafficLightSystemapp;Integrated Security=True;Encrypt=False"
 } (Aready is there no need to config)

4. Run the following commands in the terminal to apply database migrations:

    ```bash
    Add-Migration "Traffic Light Task Migration"
    Update-Database
    ```

5. Run the application:

    ```bash
    dotnet run or directly run under Visual studio it will open the swagger for API end points
    ```

- Visit `https://localhost:7286/swagger/index.html` to access Swagger documentation and test your API endpoints.

## Production Release <a name="production-release"></a>  

The steps to prepare the project for production release.

1. Update the `appsettings.json` file with production-specific configurations.

2. Set the environment variable to `Production`:

    ```bash
    export ASPNETCORE_ENVIRONMENT=Production
    ```

3. Publish the application:

    ```bash
    dotnet publish -c Release
    ```

4. Deploy the published files to your production server.

### Usage  <a name="usage"></a>

### API Endpoints

# Traffic Light App
# First store few records before starting the actual test like below.
# Here are the commands:
1. **insert into TrafficLights values('South','Green')**
2. **insert into TrafficLights values('North','Yellow')**
3. **insert into TrafficLights values('West','Red')**
4. **insert into TrafficLights values('East','Green')**
1.
1. **Get the TrafficLights Data below end point(Get)**
   - Endpoint: `/TrafficLight/GetTrafficData`
   - Description: Get the TrafficLights Data.
   - Request Body:
     ```json
      [
          {
            "id": 1,
            "direction": 0,
            "color": 2
          },
          {
            "id": 2,
            "direction": 2,
            "color": 2
          },
          {
            "id": 3,
            "direction": 3,
            "color": 2
          },
          {
            "id": 4,
            "direction": 1,
            "color": 2
          }
    ]
     ```
   - Response:
     - HTTP 200 - Sucess - Response is array of Traffic lights with direction and color.
     - If any error occured during getting Traffic lights data 500 (Internal server error)
   For Example:
      {
           "id": 1,
           "direction": 0,
           "color": 2
       },

   # above object from response represents Single traffic light direction and color, here I took Enum, it has given response as number
     color: Red = 0;
     color: Yellow = 1;
     color: Green = 2;

     direction: South = 0;
     direction: West = 1;
     direction: North = 2;
     direction: East = 3;




#  Asynchronous Programming
- Added async/await all controllers (TrafficLightController) and asynchronous database operations

# Task 4: Exception Handling
- Implemented robust exception handling for all API endpoint. Added common errors to find try/catch/finally. Implemented required HTTP status codes for Traffic light(end points)

### Additionally added Log4net for log the all logs <a name="logs"></a>
- I added Log4net for log the files.
- Look at the logs folder for all logs


### Handover Instructions <a name="handover"></a> 

1. **Project Structure:**
   - The project follows a standard ASP.NET Core Web API structure.
   - Important directories include `Controllers`, `Modal`, and `Data` for database-related code.
   - Added DbContext create the migrations without inserting data directly.

2. **Database Schema:**
   - The database schema includes table for TafficLights.
   - Migrations have been applied using Entity Framework Core.

3. **API Endpoints:**
   - Refer to the "Usage" section for a list of key API endpoints.

4. **Development Environment:**
   - The project was developed using Visual Studio with .NET SDK installed.
   - Ensure the target .NET runtime version is compatible.

5. **Deployment:**
   - Deploy the application by publishing it in Release mode (`dotnet publish -c Release`).
   - Set the environment variable `ASPNETCORE_ENVIRONMENT` to `Production` for production-specific configurations.

6. **Known Issues:**
   - No known issues at the time of handover.

  ### Process <a name="process"></a> 
   1) Getting Traffic Lights Data (Traffic Light  Controller)
   3) For DB, used SQL Server , With DBContext, tables (TrafficLights) are created automatically once run the program
   4) For Tasks , we have to insert data manually
      insertion script is
    1. **insert into TrafficLights values('South','Green')**
    2. **insert into TrafficLights values('North','Yellow')**
    3. **insert into TrafficLights values('West','Red')**
    4. **insert into TrafficLights values('East','Green')**

   5) Async/await, Exception Handling, Logging were implemented
   6) Use /swagger/index.html to view and test end points
   7) All logs are captured in log.txt file
  
  screen shots for each end point  are attached
  ## Screen Shot for GetTraffic Data end point <a name="screenshots"></a> 
 ## How to excute endpoint in Swagger
     ## Go to GetTrafficData end point click on Tryit button
     - Please refer this path:  traffic_lights_sytem_app\screenshots\Swagger_output1.png
     ## Change request body and press execute button
     - Please refer this path: traffic_lights_sytem_app\screenshots\Swagger_output2.png
