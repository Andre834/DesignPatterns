using FluentValidation;

namespace DesignPatterns.Aplication;


public sealed class DeleteClienteRequestValidator : AbstractValidator<DeleteClienteRequest>
{
    public DeleteClienteRequestValidator()
    {
        RuleFor(request => request.Id).Id();
    }
}
