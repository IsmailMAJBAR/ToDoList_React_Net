
## 1 : List my TODOs

### Description :
As a user I would like to list my current todos
### Acceptance criterias :
- Each todo could have, at minimal, a related state and title
- Some hard-coded todos will be initialized in this context to demonstrate the tool 

## 2 : Change a TODO state

### Description :
As a user I would like to change a todo state by checking a "box"
### Acceptance criterias :
- When a todo is done, it should be placed at the bottom of the list and should be crossed out

## 3 : Detail a TODO

### Description :
As a user I would like to display one of my todo in a separate or dedicated view.
This todo will contain its title and a description (which is a new information not shown in the previous view).
### Acceptance criterias :
- We can click on a todo (by any way) to access the details view of the todo

## 4 : Add a new TODO

### Description :
As a user I would like to add a new todo in my list
### Acceptance criterias :
- The todo title is required
- The todo description can be empty
- The newly created todo has to be on top of the list of todos
- You are free to choose the design / interaction 

## 5 : How to lunch 
- Execute the script sql to create the DB in your sqlserver
- Modify the connection string , i used my own servername and windows authentification, change the server name and the way you set up your credential 
- Lunch the solution sln and run it ( in your bash run the command "dotnet run") ( this should start a server in your console to listen to your request from the front,
mine is configured on the port 5288, if its not availaible then you need to change the port in all the api requests so that the back and front can reach each other)
- Lunch the front react client , run the following commands : "npm i" to install node module, after that run "npm start" to lunch the client
- the client will start on port 3000 again if its different then go and change it in the server so that the server allow the request comming from your port 

# Technical environment
- The backend application based on .Net7 c# and React
- To keep the UI simple
- Code quality is very important, so all the code has to be covered by unit tests (some)
