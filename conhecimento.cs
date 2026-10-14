using Microsoft.AspNetCore.Mvc;

namespace MarmorariaProjeto.Application.DTO;

public class FuncionariorequestDto
{
    public string Nome {get; set;}
    public string Cargo {get; set;}
}

public class FuncionarioResponseDto
{
    public long Id {get; set;}
    public string Nome {get; set;}
    public string Cargo {get; set;}
    public bool Ativo {get; set;}
}
/*
//using Microsoft.AspNetCore.Mvc;
//namespace MarmorariaProjeto.Controller.FuncionarioController;

[Route ("api/[controller]")]
[ApiController]
public class FuncionarioController : ControllerBase
{
    private readonly DbContext _dbContext;

    public FuncionarioController(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("cadastrar")]
    public async Task<ActionResult<FuncionarioResponseDto>> CadastrarUsuario ([FromBody] FuncionarioRequestDto request)
    {
        if(string.IsNullOrEmpty(request.Nome) || string.IsNullOrEmpty(request.Cargo))
        {
            return BadRequest("Nome e Cargo são obrigatórios.");
        }

        var funcionario = new Funcionario //DTO -> Banco de Dados
        {
            Nome = request.Nome,// resposta vem do Dto que o cliente mandou preenchendo Nome e Cargo somente
            Cargo = request.Cargo,
            Ativo = true //Como aqui ja estou no COntroller de criar funcionario, ja passo como true.
        };

        _dbContext.Funcionarios.Add(funcionario);
        await _dbContext.SaveChangesAsync();
        return Ok(new FuncionarioResponseDto
        {
            Id = funcionario.Id,
            Nome = funcionario.Nome,
            Cargo = funcionario.Cargo,
            Ativo = funcionario.Ativo
        });
    }
}*/ 
