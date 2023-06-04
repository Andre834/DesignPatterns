
using FluentValidation;

namespace DesignPatterns.Aplication;

public sealed class UpdateClienteRequestValidator : AbstractValidator<UpdateClienteRequest>
{
    public UpdateClienteRequestValidator()
    {
        RuleFor(request => request.Id).Id();
        RuleFor(request => request.Nome).Nome();
        RuleFor(request => request.Email).Email();
    }
}
