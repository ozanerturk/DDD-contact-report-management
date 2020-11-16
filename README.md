# DDD-contact-report-management

##Prerequest

- Kafka server should be running at: localhost:9092
- Postresql server should be running at with no credential requirement: localhost:5432
 
 ## Skipped Features
 - Request FluentValidations
 - Data Migrations
 - Swagger
 - Meaningful response messages

##ContactService
### Migration
cd API folder and type 'dotnet ef database update'
### Unit Tests
cd UnitTest folder and type 'dotnet test'

##ReportService
### Migration
cd API folder and type 'dotnet ef database update'


