# MyQ.CleaningRobot
Solution for a coding test created by MyQ

# Build
1.	**Install .NET Core SDK:** If you haven't installed the .NET Core SDK yet, you can download it from the official Microsoft .NET Core download page. Make sure to download a version that is compatible with the project.
2.	**Navigate to the project directory:** Open a terminal (Command Prompt, PowerShell, or a Terminal in an IDE) and navigate to the project's root directory (the directory that contains the .csproj file).
3.	**Restore dependencies:** Run the following command to restore all the dependencies of the project:
dotnet restore

4.	**Build the project:** Run the following command to build the project:
dotnet build

5.	**Run the project:** Run the cleaning robot project by command to run the project:
dotnet run source_json_path result_json_path

The published program is invoked as:
cleaning_robot <source_json_path> <result_json_path>