### User Documentation for Sisusa.Common.Domain

This documentation provides an overview of several core classes and interfaces found in the `Sisusa.Common` namespace. These include **Entity**, **EntityId**, **IRepository**, **IPaginatedRepository**, and **ValueObject**, which are key components for managing entities and repositories in a domain-driven design context.

---

### **Entity Class**

#### Description:
The `Entity<TId>` class represents a base entity that has a unique identifier of type `TId`. It provides common functionality for entities, such as equality comparison and hash code generation, based on the entity's identifier.

#### Properties:
- **Id** (`TId`): The unique identifier for the entity.

#### Methods:
- **Equals(object? obj)**: Checks if two entities are equal by comparing their identifiers.
- **GetHashCode()**: Abstract method to be implemented in derived classes to return a hash code based on the entity's identifier.
- **Operator ==(Entity<TId>? a, Entity<TId>? b)**: Compares two entities for equality.
- **Operator !=(Entity<TId>? a, Entity<TId>? b)**: Compares two entities for inequality.

#### Example Usage:

```csharp
public class Customer : Entity<Guid>
{
    public string Name { get; set; }

    public Customer(Guid id, string name) : base(id)
    {
        Name = name;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
```

In this example, the `Customer` class inherits from `Entity<Guid>` and implements the `GetHashCode()` method based on the `Id`.

---

### **EntityId Class**

#### Description:
The `EntityId<TType>` class represents a unique identifier for an entity. It provides the ability to compare entity IDs for equality and supports various primitive types as the underlying ID.

#### Properties:
- **Value** (`TType`): The value representing the unique identifier.

#### Methods:
- **Equals(EntityId<TType>? other)**: Compares two entity IDs for equality.
- **Equals(object? obj)**: Checks equality with another object.
- **GetHashCode()**: Returns a hash code for the entity ID.
- **Operator ==(EntityId<TType>? left, EntityId<TType>? right)**: Compares two entity IDs for equality.
- **Operator !=(EntityId<TType>? left, EntityId<TType>? right)**: Compares two entity IDs for inequality.

#### Example Usage:

```csharp
public class CustomerId : EntityId<Guid>
{
    public CustomerId(Guid idValue) : base(idValue) { }
}
```

In this example, `CustomerId` is a custom ID type that inherits from `EntityId<Guid>`. This allows the `CustomerId` to be used as the unique identifier for `Customer` entities.

---

### **IRepository Interface**

#### Description:
The `IRepository<T, TId>` interface provides methods for interacting with a data store for entities of type `T`, identified by `TId`. It includes methods for finding, adding, updating, and deleting entities.

#### Methods:
- **FindByIdAsync(TId id)**: Asynchronously retrieves an entity by its unique identifier.
- **HasByIdAsync(TId id)**: Asynchronously checks if an entity with the specified identifier exists.
- **FindAllAsync()**: Asynchronously retrieves all entities.
- **FindAllByFilter(Expression<Func<T, bool>> filter)**: Finds entities matching a specified filter.
- **CountAsync()**: Returns the total number of entities.
- **CountByFilterAsync(Expression<Func<T, bool>> filter)**: Returns the count of entities matching a filter.
- **UpdateAsync(T entity)**: Updates an existing entity.
- **DeleteByIdAsync(TId id)**: Deletes an entity by its identifier.
- **AddNewAsync(T entity)**: Adds a new entity to the data store.

#### Example Usage:

```csharp
public class CustomerRepository : IRepository<Customer, CustomerId>
{
    public Task<Customer?> FindByIdAsync(CustomerId id)
    {
        // Implementation goes here
    }

    public Task<bool> HasByIdAsync(CustomerId id)
    {
        // Implementation goes here
    }

    // Other methods implemented similarly
}
```

In this example, `CustomerRepository` implements `IRepository<Customer, CustomerId>` for managing `Customer` entities using `CustomerId` as the unique identifier.

---

### **IPaginatedRepository Interface**

#### Description:
The `IPaginatedRepository<T, TId>` interface extends `IRepository<T, TId>` by adding support for pagination when retrieving collections of entities.

#### Methods:
- **FindAllWithPagingAsync(int page, int pageSize)**: Asynchronously retrieves a collection of entities for the specified page and page size.

#### Example Usage:

```csharp
public class PaginatedCustomerRepository : IPaginatedRepository<Customer, CustomerId>
{
    public Task<ICollection<Customer>> FindAllWithPagingAsync(int page, int pageSize)
    {
        // Implementation goes here
    }

    // Other IRepository methods would be implemented here
}
```

In this example, `PaginatedCustomerRepository` implements `IPaginatedRepository<Customer, CustomerId>`, allowing for paginated retrieval of `Customer` entities.

---

### **ValueObject Class**

#### Description:
The `ValueObject` class represents a domain object that does not have a unique identifier but is defined by its attributes. It provides methods for comparing value objects based on their attributes.

#### Methods:
- **Equals(object? other)**: Compares the current value object with another object.
- **GetHashCode()**: Returns a hash code for the value object.
- **Operator ==(ValueObject? left, ValueObject? right)**: Compares two value objects for equality.
- **Operator !=(ValueObject? left, ValueObject? right)**: Compares two value objects for inequality.

#### Example Usage:

```csharp
public class Address : ValueObject
{
    public string Street { get; set; }
    public string City { get; set; }

    public override bool Equals(object? other)
    {
        if (other is Address address)
        {
            return Street == address.Street && City == address.City;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Street, City);
    }
}
```

In this example, the `Address` class inherits from `ValueObject` and defines equality based on its attributes (`Street` and `City`).

---

### Conclusion

The `Sisusa.Common` namespace provides essential components for managing entities, their identifiers, and repositories in a domain-driven design context. These components support the implementation of business logic and persistence mechanisms, making it easier to maintain and scale applications while ensuring consistency and integrity of domain entities. The `Entity` and `EntityId` classes ensure that entities are uniquely identified and comparable, while the repository interfaces allow for standard CRUD operations and pagination. The `ValueObject` class helps model domain objects that are defined by their attributes rather than identity.