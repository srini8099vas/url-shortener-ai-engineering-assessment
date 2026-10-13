# Brownfield Scenario

## Existing System

Initial implementation supported:

- Create URL
- Retrieve URL
- Delete URL

## New Requirement

Add:

- Custom URL Codes
- URL Expiration

## Impact Analysis

Affected Files:

- ShortUrl.cs
- CreateUrlRequest.cs
- CreateUrlResponse.cs
- UrlService.cs

## Changes Implemented

### Custom URL Codes

Users can provide custom aliases.

Example:

google

### Expiration Feature

Users can define expiration duration.

Example:

30 days

## Validation

- Unit Tests
- Swagger Tests
- Manual Verification

## Risks

- Duplicate custom codes
- Expired URL access

## Result

Enhancement successfully delivered without impacting existing functionality.
``