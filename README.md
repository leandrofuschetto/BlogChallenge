# Blog Challenge
WebAPI application for Blogs

## Table of contents
* [How to excecute](#How-to-excecute)
* [Technologies](#Technologies)
* [Endpoints](#Endpoints)
* [DB](#DB)
 
***
<br />


## How to excecute

The app was developed in **Visual Studio 2022**, so only this IDE is enough to make it run.

For Database, in the first start of the application, the DB is created and some initial records are inserted. Will run in **(localdb)\MSSQLLocalDB**.

***
<br />

## Technlogies
* C#
* NET 8
* EF Core
* SQL Express
***
<br />

## Endpoints

1. BlogPosts
	* GET /api/posts: This endpoint should return a list of all blog posts, including their titles and the number of comments associated with each post.
  * POST /api/posts: Create a new blog post.
  * GET /api/posts/{id}: Retrieve a specific blog post by its ID, including its title, content, and a list of associated comments.
  * POST /api/posts/{id}/comments: Add a new comment to a specific blog post


## DB Entities

1. Comments 
2. Posts 
	
	






