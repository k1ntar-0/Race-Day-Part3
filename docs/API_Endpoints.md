# RaceDay API Endpoint Plan

| HTTP Method | Route | Description | Role Required | Request Body | Expected Response |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **POST** | `/api/auth/register` | Registers a new participant or organiser account. | None (Public) | `{ "firstName", "lastName", "email", "password", "roleId" }` | `201 Created` - User details & JWT token |
| **POST** | `/api/auth/login` | Authenticates a user and generates a JWT. | None (Public) | `{ "email", "password" }` | `200 OK` - JWT token & user profile |
| **GET** | `/api/profile` | Retrieves current logged-in user profile details. | Any (Authenticated) | None | `200 OK` - User object |
| **PUT** | `/api/profile` | Updates personal details for current logged-in user. | Any (Authenticated) | `{ "firstName", "lastName", "email" }` | `200 OK` - Updated user object |
| **GET** | `/api/events` | Retrieves all upcoming events with filtering options. | None (Public) | None | `200 OK` - Array of event objects |
| **GET** | `/api/events/{id}` | Retrieves detailed information for a specific event. | None (Public) | None | `200 OK` - Event details + categories |
| **POST** | `/api/events` | Creates a new race event. | Organiser | `{ "title", "description", "location", "eventDate" }` | `201 Created` - Created event object |
| **PUT** | `/api/events/{id}` | Updates existing event parameters. | Organiser | `{ "title", "description", "location", "eventDate" }` | `200 OK` / `403 Forbidden` |
| **DELETE** | `/api/events/{id}` | Removes an event from the system. | Organiser | None | `204 No Content` / `404 Not Found` |
| **GET** | `/api/categories` | Lists all available event categories. | None (Public) | None | `200 OK` - Array of categories |
| **POST** | `/api/categories` | Adds a new event category. | Organiser | `{ "categoryName", "eventType" }` | `201 Created` |
| **POST** | `/api/enrolments` | Registers a participant into an event category. | Participant | `{ "eventId", "categoryId" }` | `201 Created` - Enrolment object |
| **GET** | `/api/enrolments/my-enrolments` | Retrieves all entries for logged-in participant. | Participant | None | `200 OK` - Array of user enrolments |
| **GET** | `/api/events/{id}/enrolments` | Lists all participants enrolled in a specific event. | Organiser | None | `200 OK` - Array of participant enrolments |
| **POST** | `/api/results` | Records or updates a participant's race finish time. | Organiser | `{ "enrolmentId", "finishTime", "position" }` | `201 Created` / `200 OK` |
| **GET** | `/api/results/my-results` | Fetches personal race history and performance. | Participant | None | `200 OK` - Array of result objects |