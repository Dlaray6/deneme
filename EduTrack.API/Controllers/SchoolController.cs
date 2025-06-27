using EduTrack.Application.Interfaces;
using EduTrack.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace EduTrack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Idare")]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly Random _random = new();

        public SchoolController(ISchoolRepository schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetSchools()
        {
            var schools = await _schoolRepository.GetAllSchoolsAsync();
            return Ok(schools);
        }

        [HttpPost]
        public async Task<IActionResult> AddSchool([FromBody] SchoolCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newSchool = new School
            {
                Name = request.Name,
                AccessCode = GenerateAccessCode(),
                Classes = new List<ClassRoom>(),
                Users = new List<User>()
            };

            await _schoolRepository.AddSchoolAsync(newSchool);
            await _schoolRepository.SaveChangesAsync();

            return Ok(new { message = "Okul başarıyla eklendi.", AccessCode = newSchool.AccessCode });
        }

        private string GenerateAccessCode()
        {
            const string chars = "0123456789";
            return new string(Enumerable.Range(0, 5).Select(_ => chars[_random.Next(chars.Length)]).ToArray());
        }
    }

    public class SchoolCreateRequest
    {
        [Required(ErrorMessage = "Okul adı zorunludur.")]
        public string Name { get; set; } = "";
    }
}
