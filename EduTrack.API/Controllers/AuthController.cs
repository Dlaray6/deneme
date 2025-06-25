using EduTrack.Application.Interfaces;
using EduTrack.Application.Services;
using EduTrack.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EduTrack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtService _jwtService;

        public AuthController(IUserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var isValid = await _userService.CheckPasswordAsync(request.TcNo, request.Password);
                if (!isValid)
                    return Unauthorized(new { message = "TC veya şifre hatalı." });

                var user = await _userService.GetUserByTCAsync(request.TcNo);
                if (user == null)
                    return Unauthorized(new { message = "TC veya şifre hatalı." });

                var token = _jwtService.GenerateToken(user);

                return Ok(new { Token = token, User = new { user.Id, user.Name, user.Role } });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Sunucu hatası: " + ex.Message });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                // Temel validasyon
                if (string.IsNullOrWhiteSpace(request.TcNo) || string.IsNullOrWhiteSpace(request.Password))
                    return BadRequest(new { message = "TC ve şifre zorunludur." });

                if (string.IsNullOrWhiteSpace(request.Name))
                    return BadRequest(new { message = "Ad zorunludur." });

                if (request.TcNo.Length != 11 || !request.TcNo.All(char.IsDigit))
                    return BadRequest(new { message = "TC kimlik numarası 11 haneli ve sadece rakam olmalıdır." });

                // Mevcut kullanıcı kontrolü
                var existingUser = await _userService.GetUserByTCAsync(request.TcNo);
                if (existingUser != null)
                    return BadRequest(new { message = "Bu TC ile kayıtlı kullanıcı zaten var." });

                // Rol kontrolü
                if (request.Role != "Student" && request.Role != "Teacher" && request.Role != "Idare")
                    return BadRequest(new { message = "Geçersiz rol. (Student, Teacher, Idare)" });

                // Rol bazlı validasyon
                if (request.Role == "Student" && string.IsNullOrWhiteSpace(request.SchoolNumber))
                    return BadRequest(new { message = "Öğrenci için okul numarası zorunludur." });

                if (request.Role == "Teacher" && string.IsNullOrWhiteSpace(request.PhoneNumber))
                    return BadRequest(new { message = "Öğretmen için telefon numarası zorunludur." });

                // Şifre uzunluk kontrolü
                if (request.Password.Length < 6)
                    return BadRequest(new { message = "Şifre en az 6 karakter olmalıdır." });

                // Yeni kullanıcı oluştur
                var newUser = new User
                {
                    TcNo = request.TcNo,
                    Name = request.Name,
                    Role = request.Role,
                    Password = request.Password, // UserService'te hash'lenecek
                    SchoolNumber = request.SchoolNumber,
                    PhoneNumber = request.PhoneNumber,
                    // SchoolId şimdilik null, daha sonra okul seçim mekanizması eklenebilir
                    SchoolId = null
                };

                // Kullanıcıyı ekle
                await _userService.AddUserAsync(newUser);

                // Değişiklikleri kaydet
                await _userService.SaveChangesAsync();

                return Ok(new { message = "Kayıt başarılı." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Sunucu hatası: " + ex.Message });
            }
        }

        public class LoginRequest
        {
            public string TcNo { get; set; } = "";
            public string Password { get; set; } = "";
        }

        public class RegisterRequest
        {
            public string TcNo { get; set; } = "";
            public string Name { get; set; } = "";
            public string Password { get; set; } = "";
            public string Role { get; set; } = "";
            public string? SchoolNumber { get; set; }
            public string? PhoneNumber { get; set; }
        }
    }
}