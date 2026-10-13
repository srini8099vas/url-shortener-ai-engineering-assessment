# Risks and Trade-Offs

## Trade-Off 1

JSON Storage vs Database

Chosen:

JSON Storage

Reason:

Faster implementation and easier evaluation.

Risk:

Not suitable for high-concurrency production workloads.

## Trade-Off 2

No Authentication

Reason:

Outside the assignment scope.

Risk:

Public access to APIs.

## Trade-Off 3

Local File Logging

Reason:

Simple and lightweight implementation.

Risk:

Log file size may grow over time.

## Known Limitations

- No SQL Server
- No Redis Cache
- No Authentication
- No Authorization
- No Distributed Deployment

## Production Recommendations

- SQL Server
- Redis
- Serilog
- Azure App Service
- Azure Monitor
- JWT Authentication