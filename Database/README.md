# 🗃 Knowledge Base App SQL Server Database

Used for deploying and working with the database for the Knowledge Base App.

## Features

-   TSQL
-   3NF Level Normalized tables

## Using Microsoft SQL Server instance for the Database

### Creating a local version of the database for development and testing

Use **CreateDatabase.sql** to create the database on your SQL Server instance of choice. It is important to check the filegroup configuration code and ensure the following:

-   If you **have more than one drive on the device** in which the local instance is running, set the primary filegroup (knowledgebase_dat.mdf) to one drive, likely the C: drive. Set the log filegroup to a different drive. This helps with performance and the resiliency of your local database.

-   If you **Do Not** have more than one drive, make sure to check that both filegroups are being written to the same drive.

Code for doing either of these options is already written on the file. You may comment one of these code blocks out and use the other, depending on the conditions stated above.

Do this for each device that you need a local instance of this database to run on.
