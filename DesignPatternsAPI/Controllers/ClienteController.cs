using DesignPatterns.Aplication;
using DesignPatterns.CrossCutting;
using Microsoft.AspNetCore.Mvc;

namespace DesignPatternsAPI;

[ApiController]
[Route("clientes")]
public class ClienteController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClienteController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public IActionResult Add(AddClienteRequest request) => _mediator.HandleAsync<AddClienteRequest, AddClienteResponse>(request).PostResult();

    [HttpDelete("{id:long}")]
    public IActionResult Delete(long id) => _mediator.HandleAsync(new DeleteClienteRequest(id)).DeleteResult();

    [HttpGet("{id:long}")]
    public IActionResult Get(long id) => _mediator.HandleAsync<GetClienteRequest, GetClienteResponse>(new GetClienteRequest(id)).GetResult();

    [HttpGet]
    public IActionResult List() => _mediator.HandleAsync<ListClienteRequest, ListClienteResponse>(new ListClienteRequest()).GetResult();

    [HttpPut("{id:long}")]
    public IActionResult Update(UpdateClienteRequest request) => _mediator.HandleAsync(request).PutResult();
}
