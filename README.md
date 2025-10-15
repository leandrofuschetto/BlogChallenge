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

BlogPosts
  * GET /api/posts: This endpoint should return a list of all blog posts, including their titles and the number of comments associated with each post.
  * POST /api/posts: Create a new blog post.
  * GET /api/posts/{id}: Retrieve a specific blog post by its ID, including its title, content, and a list of associated comments.
  * POST /api/posts/{id}/comments: Add a new comment to a specific blog post


## DB Entities

1. Comments 
2. Posts


## Next Steps - Suggestions

1. API and Contracts
	* Pagination and sorting on GET /api/posts: add page, pageSize, sortBy
	* Add publicationDate in BlogPOst and comments
	* Filtering: by date range, title keyword, minimum comments

2. Data Model
	* Timestamps: add CreatedAt and UpdatedAt to BlogPost and Comment (set by EF/DB).
	* Soft delete: IsDeleted with EF global query filters if logical deletes are needed.

3. Authentication
	* Users: add User/Author entities
	* Authentication: JWT Bearer; protect write endpoints.
	* Authorization: roles/policies (Author can create, Admin can moderate/delete).

4. Testing
    * Add unit test and achieve 100% coverage of business layer
	* Add Integration Tests to cover DB's transactions are well performed
	
	






