using CsvHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMS_Student_Management_System_.Data;
using SMS_Student_Management_System_.Models;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace SMS_Student_Management_System_.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ParentsGuardiansController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ParentsGuardiansController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<ParentGuardian> GetParentsGuardians()
        {
            return _context.ParentsGuardians.ToList();
        }

        [HttpGet("{parentId}/{studentId}")]
        public ActionResult<ParentGuardian> GetParentGuardian(int parentId, int studentId)
        {
            var parentGuardian = _context.ParentsGuardians.Find(parentId, studentId);

            if (parentGuardian == null)
            {
                return NotFound();
            }

            return parentGuardian;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<ParentGuardian> CreateParentGuardian(ParentGuardian parentGuardian)
        {
            _context.ParentsGuardians.Add(parentGuardian);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetParentGuardian), new { parentId = parentGuardian.ParentId, studentId = parentGuardian.StudentId }, parentGuardian);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{parentId}/{studentId}")]
        public IActionResult UpdateParentGuardian(int parentId, int studentId, ParentGuardian parentGuardian)
        {
            if (parentId != parentGuardian.ParentId || studentId != parentGuardian.StudentId)
            {
                return BadRequest();
            }

            _context.Entry(parentGuardian).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{parentId}/{studentId}")]
        public IActionResult DeleteParentGuardian(int parentId, int studentId)
        {
            var parentGuardian = _context.ParentsGuardians.Find(parentId, studentId);

            if (parentGuardian == null)
            {
                return NotFound();
            }

            _context.ParentsGuardians.Remove(parentGuardian);
            _context.SaveChanges();

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("import")]
        public IActionResult ImportParents(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();

                var records = csv.GetRecords<ParentGuardian>();

                foreach (var record in records)
                {
                    _context.ParentsGuardians.Add(record);
                }

                _context.SaveChanges();
            }

            return Ok("Parents imported successfully.");
        }
    }
}