# User Microservice

## Overview

The User Microservice is a .NET Core 8 application that provides CRUD operations for user management. This microservice is integrated with Active Directory using Microsoft Graph API, and it employs Redis for caching to improve performance.

## Features

- **CRUD Operations**: Create, Read, Update, and Delete users.
- **Active Directory Integration**: Manage users via Microsoft Graph API.
- **Caching**: Utilize Redis to cache user data for improved performance.
- **Secure**: Uses Azure AD for authentication and authorization.

## Prerequisites

- .NET Core SDK 8.0
- Azure Active Directory (Azure AD) Tenant
- Redis Server
- Azure Subscription (for Microsoft Graph API)

## Getting Started

### 1. Clone the Repository

```sh
git clone https://github.com/AbdelrahmanNasser1/UserIdentityService
cd UserIdentityService
