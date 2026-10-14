using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarmorariaProjeto.Domain.Entities;
using MarmorariaProjeto.Infrastructure.Persistence;
using MarmorariaProjeto.Infrastructure.Security;

namespace MarmorariaProjeto.Controller.AuthController
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly MarmorariaDbContext _dbContext;
        private readonly IPasswordHasher<Login> _passwordHasher;
        private readonly TokenService _tokenService;
        private readonly IConfiguration _configuration;

        public AuthController(
            MarmorariaDbContext dbContext,
            IPasswordHasher<Login> passwordHasher,
            TokenService tokenService,
            IConfiguration configuration)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        [HttpGet("bootstrap-status")]
        public async Task<IActionResult> BootstrapStatus()
        {
            try
            {
                return Ok(new { required = !await _dbContext.Logins.AnyAsync() });
            }
            catch (SqlException exception) when (exception.Number == 208)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new
                {
                    message = "O banco ainda não está atualizado. Execute 'dotnet ef database update' no terminal do projeto e reinicie a aplicação."
                });
            }
        }

        [HttpPost("bootstrap")]
        public async Task<IActionResult> Bootstrap([FromBody] LoginRequest request)
        {
            var validationError = Validate(request);
            if (validationError != null)
            {
                return BadRequest(new { message = validationError });
            }

            if (await _dbContext.Logins.AnyAsync())
            {
                return Conflict(new { message = "A configuração inicial já foi concluída." });
            }

            var user = new Login { Username = request.Username.Trim() };
            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
            _dbContext.Logins.Add(user);
            await _dbContext.SaveChangesAsync();
            return Ok(CreateTokenResponse(user.Username));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { message = "Informe usuário e senha." });
            }

            var username = request.Username.Trim();
            var user = await _dbContext.Logins.FirstOrDefaultAsync(
                candidate => candidate.Username == username);
            if (user == null ||
                _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
            {
                return Unauthorized(new { message = "Usuário ou senha inválidos." });
            }

            return Ok(CreateTokenResponse(user.Username));
        }

        private object CreateTokenResponse(string username)
        {
            var minutes = int.Parse(_configuration["JwtSettings:ExpirationTimeInMinutes"] ?? "60");
            return new
            {
                token = _tokenService.GenerateToken(username),
                expiresAt = DateTime.UtcNow.AddMinutes(minutes),
                username
            };
        }

        private static string? Validate(LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || request.Username.Trim().Length > 100)
            {
                return "Informe um usuário com até 100 caracteres.";
            }

            if (string.IsNullOrEmpty(request.Password) || request.Password.Length < 8)
            {
                return "A senha deve conter pelo menos 8 caracteres.";
            }

            return null;
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
