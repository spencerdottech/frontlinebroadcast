#!/bin/bash

function add_migration() {
  dotnet ef migrations add "$1" --project FrontlineBroadcast.API
}

function update_database() {
  dotnet ef database update --project FrontlineBroadcast.API
}

function remove_last_migration() {
  dotnet ef migrations remove --project FrontlineBroadcast.API
}

function list_migrations() {
  dotnet ef migrations list --project FrontlineBroadcast.API
}

case "$1" in
  "add")
    if [ -z "$2" ]; then
      echo "Usage: ./db-manage.sh add MigrationName"
      exit 1
    fi
    add_migration "$2"
    ;;
  "update")
    update_database
    ;;
  "remove")
    remove_last_migration
    ;;
  "list")
    list_migrations
    ;;
  *)
    echo "Usage: ./db-manage.sh [add|update|remove|list] [migration-name]"
    echo "  add [name]  - Add a new migration"
    echo "  update      - Update the database to the latest migration"
    echo "  remove      - Remove the last migration"
    echo "  list        - List all migrations"
    exit 1
    ;;
esac