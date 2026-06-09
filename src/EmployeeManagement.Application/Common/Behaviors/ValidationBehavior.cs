namespace EmployeeManagement.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);
        List<FluentValidation.Results.ValidationFailure> failures = new List<FluentValidation.Results.ValidationFailure>();

        foreach (IValidator<TRequest> validator in _validators)
        {
            FluentValidation.Results.ValidationResult result = await validator.ValidateAsync(context, cancellationToken);
            failures.AddRange(result.Errors);
        }

        if (failures.Any())
        {
            throw new ValidationException(failures);
        }

        return await next();
    }
}
