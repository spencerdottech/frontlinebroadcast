# Frontline Broadcast

A notification system for alerting staff with timely pertinent information.

## Development Setup

### Prerequisites

- .NET 8 SDK
- Docker and Docker Compose

### Database Setup

This project uses PostgreSQL for data storage. Docker Compose is set up to provide a PostgreSQL instance for local development.

1. Start the PostgreSQL database and PgAdmin:

```bash
docker-compose up -d
```

2. The database will be initialized with:
   - Host: localhost
   - Port: 5432
   - Database: frontline_broadcast
   - Username: postgres
   - Password: postgres

3. PgAdmin will be available at http://localhost:5050
   - Login: admin@example.com
   - Password: admin

### Database Migrations

A helper script is included for managing database migrations:

```bash
# Create a new migration
./db-manage.sh add MigrationName

# Apply migrations to the database
./db-manage.sh update

# Remove the last migration (if not applied to database)
./db-manage.sh remove

# List all migrations
./db-manage.sh list
```

### Running the Application

```bash
cd FrontlineBroadcast.API
dotnet run
```

The API will be available at:
- HTTP: http://localhost:5000
- HTTPS: https://localhost:5001
- Swagger: https://localhost:5001/swagger

## API Endpoints

The following endpoints are available:

- `GET /api/notifications` - Get all notifications
- `GET /api/notifications/active` - Get active notifications
- `GET /api/notifications/{id}` - Get notification by ID
- `POST /api/notifications` - Create a new notification
- `PUT /api/notifications/{id}` - Update an existing notification
- `DELETE /api/notifications/{id}` - Delete a notification
