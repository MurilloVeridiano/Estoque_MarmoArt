using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MarmorariaProjeto.Comunication.Responses;
using MarmorariaProjeto.UseCases.ItemEstoque;
using MarmorariaProjeto.UseCases.Movimentacao;

namespace MarmorariaProjeto.Controller.MovimentacaoController
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class MovimentacaoController : ControllerBase
    {
        private readonly RegisterMovimentacaoUseCase _registerUseCase;
        private readonly GetMovimentacaoById _getByIdUseCase;
        private readonly GetMovimentacaoAllUseCase _getAllUseCase;
        private readonly UpdateMovimentacaoUseCase _updateUseCase;
        private readonly DeleteMovimentacaoUseCase _deleteUseCase;

        public MovimentacaoController(
            RegisterMovimentacaoUseCase registerUseCase,
            GetMovimentacaoById getByIdUseCase,
            GetMovimentacaoAllUseCase getAllUseCase,
            UpdateMovimentacaoUseCase updateUseCase,
            DeleteMovimentacaoUseCase deleteUseCase)
        {
            _registerUseCase = registerUseCase;
            _getByIdUseCase = getByIdUseCase;
            _getAllUseCase = getAllUseCase;
            _updateUseCase = updateUseCase;
            _deleteUseCase = deleteUseCase;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ResponseMovimentacaoJson>>> GetAll()
        {
            return Ok(await _getAllUseCase.Execute());
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ResponseMovimentacaoJson>> GetById(long id)
        {
            try
            {
                return Ok(await _getByIdUseCase.Execute(id));
            }
            catch (KeyNotFoundException exception)
            {
                return NotFound(new ResponseErrorMenssageJson(exception.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponseMovimentacaoJson>> Register([FromBody] MovimentacaoRequest request)
        {
            try
            {
                var movement = await _registerUseCase.Execute(request);
                var response = await _getByIdUseCase.Execute(movement.Id);
                return CreatedAtAction(nameof(GetById), new { id = movement.Id }, response);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new ResponseErrorMenssageJson(exception.Message));
            }
            catch (KeyNotFoundException exception)
            {
                return NotFound(new ResponseErrorMenssageJson(exception.Message));
            }
            catch (InvalidOperationException exception)
            {
                return Conflict(new ResponseErrorMenssageJson(exception.Message));
            }
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, [FromBody] MovimentacaoRequest request)
        {
            try
            {
                await _updateUseCase.Execute(id, request);
                return NoContent();
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new ResponseErrorMenssageJson(exception.Message));
            }
            catch (KeyNotFoundException exception)
            {
                return NotFound(new ResponseErrorMenssageJson(exception.Message));
            }
            catch (InvalidOperationException exception)
            {
                return Conflict(new ResponseErrorMenssageJson(exception.Message));
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _deleteUseCase.Execute(id);
                return NoContent();
            }
            catch (KeyNotFoundException exception)
            {
                return NotFound(new ResponseErrorMenssageJson(exception.Message));
            }
            catch (InvalidOperationException exception)
            {
                return Conflict(new ResponseErrorMenssageJson(exception.Message));
            }
        }
    }
}
