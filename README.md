
# Sisusa.Common  

Sisusa.Common is a lightweight library designed to simplify handling of method call outcomes, enabling streamlined success and failure paths. The package provides two main return types: `FailureOrNothing` and `FailureOr<T>`. Additionally, it includes base classes to facilitate quick and pragmatic Domain-Driven Design (DDD) implementation.  

## Purpose  
This package was primarily created for personal projects and serves as a proof of concept for managing potential outcomes in a clean and consistent way.  

---

## Features  
- **Two core return types**:
  - `FailureOrNothing`: For scenarios where the outcome is either a failure or a success with no associated data.
  - `FailureOr<T>`: For scenarios where success results in a value of type `T`.  
- **Facilitates error handling**: Provides intuitive APIs for managing success and failure outcomes.  
- **DDD Support**: Includes base classes(`ValueObject`, `EntityId<TType>`, `Entity<in EntityId>`) to speed up Domain-Driven Design implementations.  

---

## Installation  

Install via the NuGet Package Manager:  

```bash  
dotnet add package Sisusa.Common  
```  

Or, search for **Sisusa.Common** in your IDE's NuGet Package Manager and add it to your project.  

---

## Usage  

### Traditional Approach:  
```csharp  
// Without the package  
var client = await _clientService.FindByAsync(123);  
if (client != null)  
{  
    Console.WriteLine(client.Name);  
}  
else  
{  
    Console.WriteLine("Client not found");  
}  
```  

### Using Sisusa.Common:  

#### Client Service Implementation:  
```csharp  
public Task<FailureOr<Client>> FindByIdAsync(int id)  
{  
    // Your implementation here  
}  
```  

#### Caller Code:  
```csharp  
var serviceResponse = await _clientService.FindByIdAsync(123);  

// Option 1: Using Try-Catch  
try  
{  
    var client = serviceResponse.Value;  
    Console.WriteLine(client.Name);  
}  
catch (InvalidOperationException)  
{  
    // Handle the 'not found' case  
}  

// Option 2: Use IfSuccess  
serviceResponse.IfSuccess(client => Console.WriteLine(client.Name));  

// Option 3: Null Object Pattern  
var client = serviceResponse.GetOr(Client.None);  
Console.WriteLine(client.Name);  

// Option 4: Node.js Callback Style  
serviceResponse.Handle(  
    client => Console.WriteLine(client.Name),  
    err => Console.WriteLine($"Failed to obtain client: {err.Message}")  
);  
```  

---

## Dependencies  
This package has no dependencies, making it lightweight and easy to integrate into any project.  

---

## Target Audience  
While Sisusa.Common was created mainly for personal projects, it is suitable for any developer looking to improve how they handle success and failure paths in their .NET applications.  

---

## Feedback and Contributions  
This project is a proof of concept, but feedback and contributions are welcome! Please open an issue or submit a pull request on the repository.  

--- 

This `README.md` should work as an effective introduction for users of your NuGet package!