# Healix 🏥

> A comprehensive healthcare management API designed to streamline medical services and improve patient care through modern backend architecture.

## 🎯 About The Project

Healix is a modern healthcare management API developed as a Computer Science graduation project. The system provides a robust backend solution to digitize and streamline healthcare operations, making medical services more accessible and efficient for healthcare facilities.

### Problem Statement
Traditional healthcare systems often suffer from:
- Inefficient appointment scheduling systems
- Poor medical record management and storage
- Limited integration with modern technologies
- Complex administrative processes and workflows

### Solution
Healix API addresses these challenges by providing:
- **Comprehensive Healthcare API**: Complete backend solution for healthcare applications
- **Clean Architecture**: Maintainable and scalable code structure
- **CQRS Implementation**: Optimized read and write operations
- **AI-Powered Insights**: Integration with Google Gemini for health analysis
- **Cloud-Native Design**: Built for modern cloud deployment

## ✨ Features

### Patient Management
- **Patient Registration & Profiles**: Complete patient information management
- **Medical History Tracking**: Comprehensive medical record storage
- **Document Management**: Secure file uploads and storage via AWS S3
- **Health Data Analytics**: Track patient health trends and statistics through summaries

### Healthcare Provider Operations
- **Doctor Profile Management**: Comprehensive provider information system
- **Appointment Scheduling**: Efficient booking and calendar management
- **Medical Record Creation**: Digital documentation and record keeping
- **Prescription Management**: Track and manage patient medications

### Administrative Features
- **User Authentication**: JWT-based secure authentication system
- **Role-Based Access**: Multi-level access control (Patient, Doctor, Admin)
- **Email Notifications**: Automated communications via SendGrid
- **AI Health Insights**: Google Gemini integration for medical analysis
- **Cloud Storage**: Secure document storage with AWS S3

### Technical Features
- **RESTful API Design**: Clean and intuitive API endpoints
- **CQRS Pattern**: Separation of command and query operations
- **Database Optimization**: Efficient PostgreSQL database design
- **Error Handling**: Comprehensive error management and logging
- **Input Validation**: FluentValidation for robust data validation

## 🛠 Built With

- **Backend Framework**: .NET Core 8.0 with ASP.NET Core Web API
- **Architecture**: Clean Architecture with CQRS pattern
- **Database**: PostgreSQL for data storage
- **Authentication**: JWT Bearer tokens
- **Mediator**: MediatR for CQRS implementation
- **Cloud Hosting**: AWS EC2 for application deployment
- **File Storage**: AWS S3 for document and image storage
- **Email Service**: SendGrid for email notifications
- **AI Integration**: Google Gemini for health insights and analysis

## 🚀 Getting Started

To get a local copy up and running, follow these steps.

### Prerequisites

Make sure you have the following installed:
- .NET 8.0 SDK or later
- PostgreSQL 13+ database
- AWS CLI (optional, for deployment)

### Installation

1. Clone the repository
   ```bash
   git clone https://github.com/MoamenZyan/Healix.git
   cd Healix
   ```

2. Restore NuGet packages
   ```bash
   dotnet restore
   ```

3. Set up configuration
   ```bash
   # Update appsettings.json with your database connection string
   # Add your SendGrid API key
   # Add your Google Gemini API key
   # Configure AWS credentials for S3
   ```

4. Set up the database
   ```bash
   dotnet ef database update --project Healix.Infrastructure --startup-project Healix.API
   ```

5. Run the application
   ```bash
   dotnet run --project Healix.API
   ```

6. Access the API documentation at `https://localhost:5136/swagger`

## 🏗 Architecture

Healix follows Clean Architecture principles with clear separation of concerns:

- **Healix.API**: Presentation layer with controllers and middleware
- **Healix.Application**: Business logic layer with CQRS handlers and services  
- **Healix.Domain**: Core domain entities, value objects, and business rules
- **Healix.Infrastructure**: Data access, external services, and infrastructure concerns

### Key Patterns Implemented
- **CQRS (Command Query Responsibility Segregation)**: Separate read and write operations
- **Mediator Pattern**: Decoupled request/response handling using MediatR
- **Repository Pattern**: Data access abstraction layer
- **Specification Pattern**: Encapsulated business rules and queries

## 🔗 External Integrations

### AWS and Cloud Services
- **EC2**: Application hosting and deployment
- **S3**: Secure file and document storage
- **Aiven**: Managed PostgreSQL database hosting

### Third-Party APIs
- **SendGrid**: Email delivery service for notifications and communications
- **Google Gemini AI**: Advanced AI integration for health insights and medical analysis

### Security & Authentication
- **JWT Tokens**: Secure authentication and authorization
- **Role-Based Access Control**: Multi-tier permission system
- **Data Encryption**: Secure handling of sensitive medical information

### Screenshots
## Onboarding
<img width="452" height="980" alt="onboarding" src="https://github.com/user-attachments/assets/a7c229bf-a9d8-4501-ba1b-743525a80511" />

## Signup
<img width="329" height="717" alt="signup" src="https://github.com/user-attachments/assets/95f54cfe-6376-4279-8489-33244ead824e" />

## Signin
<img width="335" height="714" alt="signin" src="https://github.com/user-attachments/assets/24fd89c4-a72d-4d58-b65d-c1ab9b38894b" />

## Homepage
<img width="326" height="998" alt="home" src="https://github.com/user-attachments/assets/ade495fa-07a7-46fa-8d09-e074f9215066" />

## Chatbot
<img width="329" height="713" alt="chatbot" src="https://github.com/user-attachments/assets/c0625880-278d-447e-b59b-219e17006823" />

## Medical History
<img width="329" height="713" alt="medical history" src="https://github.com/user-attachments/assets/4467c850-1e1e-41b0-a942-b412e9704bbd" />

## Doctor Booking
<img width="393" height="859" alt="doctors" src="https://github.com/user-attachments/assets/a7af50cf-f67e-4715-98c5-8aceb120d268" />

## Patients Booked Doctor Session
<img width="312" height="707" alt="patients" src="https://github.com/user-attachments/assets/d825fab1-42f6-4c68-8317-c4ff52c3c603" />


## 📧 Contact

**Moamen Zyan** - moamenzyan20@gmail.com - https://www.linkedin.com/in/moamenzyan/

---

<div align="center">
  <p>Made with ❤️ by Moamen Zyan</p>
  <p>Computer Science Graduation Project</p>
</div>
