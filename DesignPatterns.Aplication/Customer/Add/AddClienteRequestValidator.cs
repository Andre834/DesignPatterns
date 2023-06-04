using AlphaCentauri.Application;
using FluentValidation;

namespace DesignPatterns.Aplication;

public sealed class AddClienteRequestValidator : AbstractValidator<AddClienteRequest>
{
    public AddClienteRequestValidator()
    {
        RuleFor(request => request.Nome).Name();
        RuleFor(request => request.Email).Email();
    }
}
