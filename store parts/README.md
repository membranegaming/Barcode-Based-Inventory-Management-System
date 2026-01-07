Store Parts — Quick README

Purpose
- Small reference for developers/admins to quickly deploy the database with Docker and configure the application.

Quick Docker database setup
1. Create a folder on the server: `C:\StoreParts-Docker`
2. Place `docker-compose.yml` and your init scripts (optional) in that folder.
3. Start the DB:
   - Run `setup.bat` from `C:\StoreParts-Docker` (this script starts the container and runs the init SQL explicitly).
4. Verify the container: `docker ps` and check logs: `docker-compose logs -f`.

Important application setting to update
- The application reads its default connection string from the application setting named `MainDBConnectionString` (see `Properties\Settings.settings`).
- At runtime users can override using these user-scoped settings:
  - `CustomConnectionString` (string)
  - `UseCustomConnection` (bool)

Example connection string for Docker SQL Server
- `Server=SERVER_IP,1433;Database=MainDB;User Id=sa;Password=YourStrong@Password123;TrustServerCertificate=True;`

Notes & security
- The Microsoft SQL Server Linux container does NOT auto-run SQL files from `/docker-entrypoint-initdb.d`. Run initialization scripts explicitly via `sqlcmd` (the provided `setup.bat` does this).
- Do not hard-code or commit SA passwords into source control. Change the SA password and keep it secret.
- If `sqlcmd` is not available in your chosen image tag, use a helper container or install `mssql-tools` appropriately.

Where to find more details
- Full step-by-step instructions and examples are in `Steps.txt` in this folder.

Contact
- See project maintainers in project metadata or repository owner for access and further help.
