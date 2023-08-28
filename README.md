# Code Homework Checker

Code Homework Checker is an application designed to facilitate the checking and grading of code homework for both teachers and students. Built with a robust C# backend and a user-friendly WPF client, it streamlines the process of homework submission, checking, and grading.

## Features

### For Teachers

- **Create courses and add homework assignments to each course.**
- **Set roles for various checks (e.g., file existence, code output).**
- **Specify penalty points for each role to be removed from the total grade.**
  
### For Students

- **View courses and related homework assignments.**
- **Submit code homework for grading.**

## Tech Stack

### Server-side

- **C#** for RESTful API endpoints.
- **Entity Framework** for data persistence.
- **SQL Server** as the database.
- **Repository pattern** for data abstraction.

### Client-side

- **C# WPF** for the desktop client.
- **MVVM** (Model-View-ViewModel) architectural pattern.

## Installation

Clone the repository:

\`\`\`bash
git clone https://github.com/your-username/code-homework-checker.git
\`\`\`

### Server-side

1. Navigate to the server folder.
2. Restore NuGet packages.
3. Update the database connection string in `appsettings.json`.
4. Run the application.

### Client-side

1. Navigate to the client folder.
2. Restore NuGet packages.
3. Run the WPF application.

## API Endpoints

All server-side logic is handled through RESTful API endpoints:

- **Create a Course**: `POST /api/courses`
- **Delete a Course**: `DELETE /api/courses/{courseId}`
- **Update a Course**: `PUT /api/courses/{courseId}`
- (Add more endpoints as needed)

## Usage

1. **Teacher's View**
   - Login as a teacher.
   - Create a course and add homework assignments.
   - Define roles and penalty points for each assignment.
   
2. **Student's View**
   - Login as a student.
   - Choose a course and view assignments.
   - Submit homework for automated grading.

## Contributing

If you'd like to contribute, please fork the repository and make changes as you'd like. Pull requests are warmly welcome.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
