# cs2-honors-project-knowledge-base-app
Web application that stores user notes and exerpts from source material for future reference and citation generation.

Features a tiered web application architecture, using Microsoft SQL Server Express as a database, .NET 6 as a domain and backend API layer, and Angular 17 for the user interface. The database was designed in a normalized format, converting the "business rules and entities" into their respective tables and relations. Furthermore the service layer (backend) performed the majority of the  business logic for this simple application.

Authorization and Authentication are performed by maintaining user login info, checking for matching user login credentials with that info and providing JSON Web Tokens as a key for authorized users to access their data.


## Application Modules:


### KB.Common

Contains libraries, utilities, and exceptions used between multiple layers in the backend.

### KB.Domain

Main service layer of the application. Performs Object-Relational-Mapping (ORM) between the service layer and database using Microsoft Entity Framework. 

### KB.Web.API

Provides an interface between the service layer and the outside world, particularly the user interface layer of the application.

### Website

The user interface for the application. Uses Angular 17 framework for its structure and logic. Requires Authentication and Authorization for users to access the application beyond the landing page.


## Future Features and Ideas


### Employ knowledge graph for search and explore functionality
A knowledge graph employes Graph Theory, a discrete mathematics concept, to store entities as "nodes", and relationships between entities as "edges". Search engines, for example, may have a node, "San Francsico", and an edge, "Is a city in" connecting it to another entity, "California". Likewise the node "California" may have a "has national park" connecting to "Yosemite".

Other cities will have the "Is a city in" edge, like New York City, Las Vegas, Portland, etc. and by that logic can be queried in a manner such as "Cities in the United States". By parsing this query and accessing the knowledge graph, any node that "Is a city in" a "state in" 'The United States'.

This data structure could be tremendously useful in expanding the capabilities of the knowledge base app. As the name of the app suggests, it is a computerized application of knowledge, and what better a way to more naturally translate human abstract thinking into a computer form.

In its infant stage, the Knowledge Base App currently uses a SQL relational database, and relates its entities by way of keywords and titles. At a small scale this works, but the added functionality and fluidity of knowledge graphs, especially when relating seemingly distant ideas and mapping them together semantically and visually, is a significant reason to explore the design and implementation of a knowledge graph data management system.
