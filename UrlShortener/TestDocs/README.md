# URL Shortener Service

## Overview

This project is a URL Shortener Service built using .NET 8 Web API.

The application allows users to:

- Create short URLs
- Retrieve original URLs
- Generate analytics
- Track clicks
- Configure URL expiration
- Create custom short codes
- Delete URLs
- View application logs

## Technology Stack

- .NET 8 Web API
- C#
- xUnit
- Moq
- FluentAssertions
- Swagger
- JSON File Storage

## Features

### Create URL

Users can create short URLs from long URLs.

### Custom Short Codes

Users can create custom aliases.

Example:

https://localhost:7241/api/url/google

### URL Expiration

Users can define URL expiration period.

### Analytics

Provides:

- Click Count
- Click History
- Creation Date

### Logging

Logs:

- URL Creation
- URL Access
- Analytics Access
- URL Deletion

## Running the Application

1. Open the solution in Visual Studio 2022.
2. Build the solution.
3. Run the application.
4. Open Swagger UI.

Example:

https://localhost:7241/swagger

## Testing

Unit tests are implemented using xUnit.

Total Unit Tests: 8

All tests are passing.

## Limitations

- Uses JSON file storage instead of database
- No authentication
- No authorization
- Single node deployment

## Future Improvements

- SQL Server persistence
- Redis caching
- JWT Authentication
- Cloud deployment
- Distributed logging