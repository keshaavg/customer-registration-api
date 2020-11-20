# Customer Registration API

Customer Registration API is ASP.Net Core Web API. This is created as an exercise to build example  REST API to allow customers to register 

## Prerequisites

* .Net Core 3.1
* Visual Studio 2019 with C# / Visual Studio code (for dev environment)

## Requirements

This API should contain one endpoint. This endpoint should validate the customers information and return a unique online Customer ID. The registration data required is:
* Policy holder’s first name
* Policy holder’s surname
* Policy Reference number
* Either the policy holders DOB OR the policy holder’s email

### Validation Rules
* Policy holder’s first name and surname are both required and should be between 3 and 50 chars.
* Policy Reference number is required and should match the following format XX-999999. Where XX are any capitalised alpha character followed by a hyphen and 6 numbers. If supplied the policy holders DOB should mean the customer is at least 18 years old at the point of registering.
* If supplied the policy holders email address should contain a string of at least 4 alpha numeric chars followed by an ‘@’ sign and then another string of at least 2 alpha numeric
chars. The email address should end in either ‘.com’ or ‘.co.uk’.

## Run in Dev environement

Clone [Repository](https://github.com/keshaavg/customer-registration-api.git) on local machine and build solution in Visual studio

## Test in Azure with Swagger open API UI

Application is deployed in Azure web app and can be accessed publicity. For simplicity no authehtication is needed

[Swagger Link](https://customerregistrationapi20201120120427.azurewebsites.net/swagger/index.html)

### Endpoints

* GET https://customerregistrationapi20201120120427.azurewebsites.net/customers  (Helper method to view all customer data in database) 
* POST https://customerregistrationapi20201120120427.azurewebsites.net/customers (Registers new customers)

## Some Test Cases

### Valid Customer should succeed Registration and return customer with new unique id assigned

```
{
  "customerId": 0,
  "firstName": "Test",
  "lastName": "Surname",
  "policyReferenceNumber": "AA-111111",
  "dateOfBirth": "1990-01-01T00:00:00",
  "email": "test@test.com"
}
```

### Invalid Customer Case 1 - Empty First name - Should Fail 

```
{
  "customerId": 0,
  "lastName": "Surname",
  "policyReferenceNumber": "AA-111111",
  "dateOfBirth": "1990-01-01T00:00:00",
  "email": "test@test.com"
}
```

### Invalid Customer Case 2 - Empty Last name - Should Fail with BadRequest

```
{
  "customerId": 0,
  "firstName": "Test",
  "policyReferenceNumber": "AA-111111",
  "dateOfBirth": "1990-01-01T00:00:00",
  "email": "test@test.com"
}
```

### Invalid Customer Case 3 - Empty policyReferenceNumber - Should Fail with BadRequest

```
{
  "customerId": 0,
  "firstName": "Test",
  "lastName": "Surname",
  "dateOfBirth": "1990-01-01T00:00:00",
  "email": "test@test.com"
}
```

### Invalid Customer Case 4 - Invalid policyReferenceNumbe pattern - Should Fail with BadRequest

```
{
  "customerId": 0,
  "firstName": "Test",
  "lastName": "Surname",
  "policyReferenceNumber": "AAA-1A11111",
  "dateOfBirth": "1990-01-01T00:00:00",
  "email": "test@test.com"
}
```

### Invalid Customer Case 5 - Empty Date of Birth and Email - Should Fail with BadRequest

```
{
  "customerId": 0,
  "firstName": "Test",
  "lastName": "Surname",
  "policyReferenceNumber": "AA-111111"
}
```

### Invalid Customer Case 6 - Customer Age less than 18 - Should Fail with BadRequest

```
{
  "customerId": 0,
  "firstName": "Test",
  "lastName": "Surname",
  "policyReferenceNumber": "AA-111111",
  "dateOfBirth": "2010-01-01T00:00:00"
}
```

### Invalid Customer Case 8 - Wrong email pattern - Should Fail with BadRequest

```
{
  "customerId": 0,
  "firstName": "Test",
  "lastName": "Surname",
  "policyReferenceNumber": "AA-111111",
  "email": "tes@test.eu"
}
```

### Invalid Customer Case 9 - Customer Age less than 18 - Should Fail with Conflict as customer id already exist

```
{
  "customerId": 1,
  "firstName": "Test",
  "lastName": "Surname",
  "policyReferenceNumber": "AA-111111",
  "dateOfBirth": "2000-01-01T00:00:00"
}
```

## Design Consideration

* For Validation I used Fluent validation library but Data Annotations could have been used as well. Both are great solutions that provide powerful validation without littering controller code. 
* Did not implemeted any duplication checks for customer data(such as name and policy) considering it out of scope