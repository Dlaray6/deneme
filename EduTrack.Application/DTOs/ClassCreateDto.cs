using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    using System.ComponentModel.DataAnnotations;

    namespace EduTrack.Application.DTOs
    {
        public class ClassCreateDto
        {
            [Required]
            public string Grade { get; set; } = "";

            [Required]
            public string Branch { get; set; } = "";

            [Required]
            public int SchoolId { get; set; }
        }
    
}
