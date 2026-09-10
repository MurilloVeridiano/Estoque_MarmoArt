using MarmorariaProjeto.UseCases.ItemEstoque.Register;
using Microsoft.AspNetCore.Mvc;
using MarmorariaProjeto.UseCases.ItemEstoque.GetById;
using MarmorariaProjeto.UseCases.ItemEstoque.GetAll;

namespace MarmorariaProjeto.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstoqueController : ControllerBase
    {

        private readonly RegisterItemEstoqueUseCase _registerItemEstoqueUseCase;
        private readonly GetItemEstoqueByIdUseCase _getItemEstoqueByIdUseCase;

        private readonly GetItemEstoqueAllUseCase _getAllItemEstoqueUseCase;

        public EstoqueController(
            RegisterItemEstoqueUseCase registerItemEstoqueUseCase,
            GetItemEstoqueByIdUseCase getItemEstoqueByIdUseCase,
            GetItemEstoqueAllUseCase getAllItemEstoqueUseCase)
        {
            _registerItemEstoqueUseCase = registerItemEstoqueUseCase;
            _getItemEstoqueByIdUseCase = getItemEstoqueByIdUseCase;
            _getAllItemEstoqueUseCase = getAllItemEstoqueUseCase;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseItemEstoqueJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorMenssageJson), StatusCodes.Status404NotFound)]
        public ActionResult<ResponseItemEstoqueJson> GetById(long id)
        {
            try
            {
                var response = _getItemEstoqueByIdUseCase.Execute(id);

                return Ok(response); // Retorna HTTP 200 com os dados encontrados
            }
            catch (ArgumentException ex)
            {
                return NotFound(new ResponseErrorMenssageJson(ex.Message)); // Retorna HTTP 404 se não achar
            }
        }



        [HttpPost]
        //Para o Swagger mostrar certinho os StatusCode e documentar certinho os dados,
        //usamos o atributo ProducesResponseType, que indica o tipo de retorno e o código de status HTTP esperado para a ação.
        //typeof(object) indica que o tipo de retorno é um objeto genérico, e StatusCodes.Status201Created indica que a ação retorna um código de status HTTP 201 Created.
        [ProducesResponseType(typeof(ResponseItemEstoqueJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorMenssageJson), StatusCodes.Status400BadRequest)]
        public ActionResult Register([FromBody] RequestItemEstoqueJson request) //Usei o [FromBody] para indicar que o parâmetro request deve ser desserializado a partir do corpo da requisição HTTP.
        //O parâmetro request é do tipo RequestItemEstoqueJson, que é uma classe que representa os dados enviados na requisição para registrar um item no estoque.
        {
            try
            {
                // declarei uma variavel UseCase com registerItemEstoqueUseCase, que é a classe que implementa a lógica de negócio para registrar um item no estoque.
                var response = _registerItemEstoqueUseCase.Execute(request);// chamei o método Execute da classe RegisterItemEstoqueUseCase, passando o parâmetro request, que contém os dados do item a ser registrado.
                return Created(string.Empty, response); //retornei um resultado HTTP 201 Created, indicando que o item foi registrado com sucesso. O segundo parâmetro response é o objeto que contém os dados do item registrado, que será serializado em JSON e enviado na resposta.
                }
            catch (ArgumentException ex) //Aqui estou capturando a exceção do tipo ArgumentException, que é lançada quando os dados recebidos na requisição são inválidos.
            {
                return BadRequest(new ResponseErrorMenssageJson(ex.Message)); //Aqui estou retornando um resultado HTTP 400 Bad Request, indicando que os dados recebidos na requisição são inválidos. O parâmetro ex.Message contém a mensagem de erro que foi lançada na exceção.
            }
            catch
            {
                return (StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorMenssageJson("Erro Desconhecido!."))); //Aqui estou retornando um resultado HTTP 500 Internal Server Error, indicando que ocorreu um erro inesperado ao processar a requisição. A mensagem de erro é genérica, pois não queremos expor detalhes do erro para o cliente.
            }
        }



        [HttpPut("{id}")]
        public ActionResult<object> Put()
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return NoContent();
        }

        [HttpGet()]
        [ProducesResponseType(typeof(ResponseItemEstoqueJson), StatusCodes.Status200OK)]
        public ActionResult<ResponseItemEstoqueJson> GetAll()
        {
            return Ok(_getAllItemEstoqueUseCase.ExecuteGetAll());

        }


    }

}
