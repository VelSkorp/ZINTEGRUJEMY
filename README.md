# ZINTEGRUJEMY

## Introduction

Test task from ZINTEGRUJEMY.pl

## Prerequisites

Before you start, make sure you have the following installed on your machine:

- .NET 5.0 or later
- SQLite database engine
- Postman (optional, for API testing)

## Getting Started

1. **Clone the Repository:**

   ```bash
   git clone https://github.com/VelSkorp/ZINTEGRUJEMY.git
   cd ZINTEGRUJEMY/ZINTEGRUJEMY

2. **Build and Run:**

   ```bash
	dotnet build
	dotnet run
This will build and run the project. The API will be accessible at http://localhost:5000.

3. Open in Postman (Optional):

You can use Postman to test the API. 
Import the provided ZINTEGRUJEMY.postman_collection.json file into Postman to get started.
For HTTP Post request there is a PostRequestBody.json file with the request body pimer.

## API Endpoints

### Processing CSV files
- URL: http://localhost:5000/ZINTEGRUJEMY.pl/api/products
- Method: POST
- Body: Upload a Json file with links to CSV files (Example can be found PostRequestBody.json).
- Response: Returns a success message upon successful file upload.

### Get Product Information
- URL: http://localhost:5000/ZINTEGRUJEMY.pl/api/product-info/{sku}
- Method: GET
- Parameters: Replace {sku} with the SKU (Stock Keeping Unit) of the product.
- Response: Retrieves detailed information about the product.