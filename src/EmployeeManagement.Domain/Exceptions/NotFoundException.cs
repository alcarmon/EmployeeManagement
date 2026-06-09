namespace EmployeeManagement.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }

    public static NotFoundException ForEntity(string entityName, int id)
        => new($"{entityName} with ID {id} was not found.");

    public static NotFoundException ForEntity<T>(int id)
        => ForEntity(typeof(T).Name, id);
}
