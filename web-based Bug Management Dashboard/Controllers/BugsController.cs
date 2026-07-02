using web_based_Bug_Management_Dashboard.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using web_based_Bug_Management_Dashboard.Models.Domain;
using web_based_Bug_Management_Dashboard.Models.DTOs;

namespace web_based_Bug_Management_Dashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugsController : ControllerBase
    {
        private readonly IBugRepository bugRepository;

        public BugsController(IBugRepository bugRepository)
        {
            this.bugRepository = bugRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] BugStatus? status)
        {
            var bugs = await bugRepository.GetAllAsync(status);
            return Ok(bugs.Select(MapToDto));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var bug = await bugRepository.GetByIdAsync(id);

            if (bug == null)
            {
                return NotFound();
            }

            return Ok(MapToDto(bug));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBugRequestDto request)
        {
            var bug = new Bug
            {
                Title = request.Title.Trim(),
                Description = request.Description.Trim(),
                Status = request.Status,
                ReporterName = request.ReporterName.Trim(),
                AssignedTo = string.IsNullOrWhiteSpace(request.AssignedTo) ? null : request.AssignedTo.Trim()
            };

            await bugRepository.CreateAsync(bug);
            return CreatedAtAction(nameof(GetById), new { id = bug.Id }, MapToDto(bug));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateBugRequestDto request)
        {
            var bug = new Bug
            {
                Title = request.Title.Trim(),
                Description = request.Description.Trim(),
                Status = request.Status,
                ReporterName = request.ReporterName.Trim(),
                AssignedTo = string.IsNullOrWhiteSpace(request.AssignedTo) ? null : request.AssignedTo.Trim()
            };

            var updatedBug = await bugRepository.UpdateAsync(id, bug);

            if (updatedBug == null)
            {
                return NotFound();
            }

            return Ok(MapToDto(updatedBug));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedBug = await bugRepository.DeleteAsync(id);

            if (deletedBug == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        private static BugDto MapToDto(Bug bug)
        {
            return new BugDto
            {
                Id = bug.Id,
                Title = bug.Title,
                Description = bug.Description,
                Status = bug.Status,
                ReporterName = bug.ReporterName,
                AssignedTo = bug.AssignedTo,
                CreatedAtUtc = bug.CreatedAtUtc,
                UpdatedAtUtc = bug.UpdatedAtUtc
            };
        }
    }
}
