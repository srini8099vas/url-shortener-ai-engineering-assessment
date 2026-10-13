# Architecture Overview

## Architecture Style

Layered Architecture

Client
↓
Controller
↓
Service
↓
Repository
↓
JSON Files

## Components

### Controller Layer

Responsibilities:

- Handle API requests
- Return API responses

File:

UrlController.cs

### Service Layer

Responsibilities:

- Business logic
- Validation
- Analytics
- Logging
- URL generation

File:

UrlService.cs

### Repository Layer

Responsibilities:

- Read JSON files
- Write JSON files

File:

UrlRepository.cs

### Storage Layer

Files:

urls.json

clicks.json

app.log

## Request Flow

Create URL

Client
→ Controller
→ Service
→ Repository
→ urls.json

Analytics

Client
→ Controller
→ Service
→ Repository
→ clicks.json

Logging

Client Action
→ Service
→ LogService
→ app.log

## Design Decisions

### Repository Pattern

Separates persistence logic from business logic.

### JSON Storage

Selected for rapid prototyping and easy evaluation.

### Logging

Implemented for debugging and auditing.

### Unit Testing

Implemented using xUnit framework.