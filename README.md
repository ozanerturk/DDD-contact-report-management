# DDD-contact-report-management

## Prerequest
- Kafka server should be running at: localhost:9092
- Postresql server should be running at with no credential requirement: localhost:5432
- dotnet SDK 3.1


### ![Achitecture](/Architecture.png)

Each update/create action manipulates the report statistics published as an event in order to recieved by report service. Report service updates its records according to the event with respect to the Eventual Consistency.

## Datasets

### [Postman Collection](/setur.postman_collection.json)

 ## Skipped Features
 - Request FluentValidations
 - Data Migrations
 - Swagger
 - Meaningful response messages

## ContactService
### Migration
cd API folder and type 'dotnet ef database update'
### Unit Tests
cd UnitTest folder and type 'dotnet test'

## ReportService
### Migration
cd API folder and type 'dotnet ef database update'


