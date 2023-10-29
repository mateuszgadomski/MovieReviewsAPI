# MovieReviewsAPI
ASP.NET Core Web API created to manage a simple movie rating portal.

# Stack
* ASP.NET Core
* Microsoft SQL Server
* Entity Framework Core 
* Fluent Validation 
* NLog
* JWT.Net 
* Auto Mapper
* Swagger

# Endopints
## Account:
* POST /api/account/register - Registers a new user.
* POST /api/account/login - Logs in the user and returns an access token.

## Category:
* GET /api/category - Retrieves all available categories using pagination, filtering and sorting.
* POST /api/category - Adds new category.
* GET /api/category/{id} - Retrieves the category with the specified ID.
* DELETE /api/category{id} - Removes the category with the specified ID.
* PUT /api/category{id} - Updates the category with the specified ID.

## Movie:
* GET /api/movie - Retrieves all available movies using pagination, filtering and sorting.
* POST /api/movie - Adds new movie.
* GET /api/movie/{id} - Retrieves the movie with the specified ID.
* DELETE /api/movie{id} - Removes the movie with the specified ID.
* PUT /api/movie{id} - Updates the movie with the specified ID.

## Review:
* GET /api/review - Retrieves all available reviews using pagination, filtering and sorting.
* POST /api/review - Adds new review.
* GET /api/review/{id} - Retrieves the review with the specified ID.
* DELETE /api/review{id} - Removes the review with the specified ID.
* PUT /api/review{id} - Updates the review with the specified ID.
