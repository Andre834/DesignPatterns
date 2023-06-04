using FluentValidation;

namespace DesignPatterns.Aplication;

public sealed class GetClienteRequestValidator : AbstractValidator<GetClienteRequest>
{
    public GetClienteRequestValidator()
    {
        RuleFor(request => request.Id).Id();
    }
}
