Pre-requisites
==============
Azure EventGrid Emulator (https://github.com/ravinsp/eventgrid-emulator)
Clone the repo to a folder named "AzureEventGridEmulator" as a sibling of this folder (i.e. the "FunctionsPlayground" folder).
Run "AzureEventGridEmulator\src\publish-win-64.bat" to build a publish it.

Azure Cosmos DB Emulator (https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator)
Download and install.

Postman (https://www.postman.com/downloads/)
Download and install.

One-Off Setup
=============
Start the Azure Cosmos DB Emulator from the Start menu.
Open the Azure Cosmos DB Emulator data explorer (https://localhost:8081/_explorer/index.html).
Create a new database called "PersonDb". Accept all defaults.
Create a new container called "Person" within the new database. Accept all defaults.

Set "FunctionsPlayground.Functions" as the start-up project.
Copy "FunctionsPlayground.Functions\local.settings.sample.json" as "local.settings.json"
In Azure Cosmos DB Emulator data explorer, go to "Quickstart" and copy the "Primary Connection String" value.
In "local.settings.json", replace "<connection string>" with that value.

Running
=======
Run "RunEventGrid.bat" to run the Azure EventGrid Simulator.
Start the Azure Cosmos DB Emulator from the Start menu.
Run the solution.

Use Postman to POST to http://localhost:7071/api/CreatePerson with the request body:

{
    "forename": "Bob",
    "surname": "Smith"
}

Open the Azure Cosmos DB Emulator data explorer (https://localhost:8081/_explorer/index.html).
Drill-down to the "Person" container: Explorer -> PersonDb -> Person -> Items
View the items created.