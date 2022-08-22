####### Choose your own adventure - Backend App ######

#### The backend API application is created in .NET 6 and C# using materialized path technique (Tree Structure), Application uses 
SQL server as a backend database. Hint: MongoDB also works well with materialized path technique.

#### The following endpoints are for Creators to create, update and retrieve the full or partial adventure tree.
		* GET /API/CreateAdventure/{Path} - The endpoint gets the adventure tree based on the materialized path
		* GET /API/CreateAdventure - The endpoint gets the full adventure tree 
		* CREATE /API/CreateAdventure - The endpoint creates the adventure tree 
		* PUT /API/CreateAdventure - The endpoint updates the adventure tree 

#### The following endpoints are for the frontend application to call and create, update and get user choices.	
		* GET /API/TakeAdventure/{UserId} - The endpoint get the user selected adventures tree
		* CREATE /API/TakeAdventure - The endpoint creates user tree selection.
		* PUT /API/TakeAdventure - The endpoint updates user tree selection

#### The following endpoint is for the frontend application to get a full adventure tree with user selection ("isUserSelected": true )	
		* GET /API/MyAdventure/{UserId} - The endpoint get adventures tree with highlighted user choices - "isUserSelected": true 
		




