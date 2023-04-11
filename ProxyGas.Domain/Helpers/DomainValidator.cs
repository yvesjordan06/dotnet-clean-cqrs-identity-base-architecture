using FluentValidation.Results;
using ProxyGas.Domain.Exceptions;

namespace ProxyGas.Domain.Helpers;

internal static class DomainValidator
{
    /// <summary>
    ///   Validates the entity and throws a DomainValidationException if the entity is invalid
    /// </summary>
    /// <param name="entity">
    ///  The entity on which the validation is performed
    /// </param>
    /// <param name="result">
    /// The result of the validation
    /// </param>
    /// <typeparam name="TEntity">
    /// The type of the entity
    /// </typeparam>
    /// <returns>
    ///  The entity if the validation is successful
    /// </returns>
    /// <exception cref="DomainValidationException">
    /// Thrown if the entity is invalid
    /// </exception>
    internal static TEntity ValidateAndThrow<TEntity>(TEntity entity, ValidationResult result) where TEntity : class
    {
        if (!result.IsValid)
        {
            throw new DomainValidationException()
            {
                Errors =  result.Errors.Select(x => x.ErrorMessage).ToList()
            };
        }

        return entity;
    }
}