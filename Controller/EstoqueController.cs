using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MarmorariaProjeto.UseCases.ItemEstoque;

namespace MarmorariaProjeto.Controller.EstoqueController
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class EstoqueController : ControllerBase
    {
        private readonly RegisterItemEstoqueUseCase _registerUseCase;
        private readonly GetItemEstoqueByIdUseCase _getByIdUseCase;
        private readonly GetItemEstoqueAllUseCase _getAllUseCase;
        private readonly UpdateItemEstoqueUseCase _updateUseCase;
        private readonly DeleteItemEstoqueUseCase _deleteUseCase;

        public EstoqueController(
            RegisterItemEstoqueUseCase registerUseCase,
            GetItemEstoqueByIdUseCase getByIdUseCase,
            GetItemEstoqueAllUseCase getAllUseCase,
            UpdateItemEstoqueUseCase updateUseCase,
            DeleteItemEstoqueUseCase deleteUseCase)
        {
            _registerUseCase = registerUseCase;
            _getByIdUseCase = getByIdUseCase;
            _getAllUseCase = getAllUseCase;
            _updateUseCase = updateUseCase;
            _deleteUseCase = deleteUseCase;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ResponseItemEstoqueJson>> GetAll()
        {
            return Ok(_getAllUseCase.ExecuteGetAll());
        }

        [HttpGet("{id:long}")]
        public ActionResult<ResponseItemEstoqueJson> GetById(long id)
        {
            try
            {
                return Ok(_getByIdUseCase.Execute(id));
            }
            catch (ArgumentException exception)
            {
                return NotFound(new ResponseErrorMenssageJson(exception.Message));
            }
        }

        [HttpPost]
        public ActionResult<ResponseItemEstoqueJson> Register([FromBody] RequestItemEstoqueJson request)
        {
            try
            {
                var response = _registerUseCase.Execute(request);
                return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new ResponseErrorMenssageJson(exception.Message));
            }
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<ResponseItemEstoqueJson>> Update(long id, [FromBody] RequestItemEstoqueJson request)
        {
            try
            {
                return Ok(await _updateUseCase.Execute(id, request));
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new ResponseErrorMenssageJson(exception.Message));
            }
            catch (KeyNotFoundException exception)
            {
                return NotFound(new ResponseErrorMenssageJson(exception.Message));
            }
        }

        [HttpDelete("{id:long}")]
        public ActionResult Delete(long id)
        {
            try
            {
                _deleteUseCase.ExecuteDelete(id);
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
