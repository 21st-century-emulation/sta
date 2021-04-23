using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

[ApiController]
public class ExecuteController : ControllerBase
{
    private readonly ILogger<ExecuteController> _logger;
    private readonly IHttpClientFactory _clientFactory;
    private readonly ConfigurationOptions _options;

    public ExecuteController(ILogger<ExecuteController> logger, IHttpClientFactory clientFactory, ConfigurationOptions options)
    {
        _logger = logger;
        _clientFactory = clientFactory;
        _options = options;
    }

    [HttpPost]
    [Route("/api/v1/execute")]
    public async Task<ActionResult<Cpu>> Execute(byte operand1, byte operand2, [FromBody] Cpu cpu)
    {
        var httpClient = _clientFactory.CreateClient();
        await httpClient.PostAsync($"{_options.WriteMemoryUrl}?address={(operand2 << 8) | operand1}&value={cpu.State.A}", null);
        cpu.State.Cycles += 13;
        return Ok(cpu);
    }

    #if DEBUG
    [HttpPost]
    [Route("api/v1/writeMemory")]
    public ActionResult WriteMemoryDebug(ushort address, byte value)
    {
        _logger.LogWarning($"Writing {value:X2} to {address:X4}");

        return Ok();
    }
    #endif
}