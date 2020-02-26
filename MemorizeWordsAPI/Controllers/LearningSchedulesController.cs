using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MemorizeWordsAPI.Services;

namespace MemorizeWordsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearningSchedulesController : ControllerBase
    {
        private readonly MWContext _context;

        public LearningSchedulesController(MWContext context)
        {
            _context = context;
        }

        // GET: api/LearningSchedules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LearningSchedule>>> GetLearningSchedules()
        {
            return await _context.LearningSchedules.ToListAsync();
        }

        // GET: api/LearningSchedules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LearningSchedule>> GetLearningSchedule(int id)
        {
            var learningSchedule = await _context.LearningSchedules.FindAsync(id);

            if (learningSchedule == null)
            {
                return NotFound();
            }

            return learningSchedule;
        }

        // PUT: api/LearningSchedules/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLearningSchedule(int id, LearningSchedule learningSchedule)
        {
            if (id != learningSchedule.ScheduleID)
            {
                return BadRequest();
            }

            _context.Entry(learningSchedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LearningScheduleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/LearningSchedules
        [HttpPost]
        public async Task<ActionResult<LearningSchedule>> PostLearningSchedule(LearningSchedule learningSchedule)
        {
            _context.LearningSchedules.Add(learningSchedule);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLearningSchedule", new { id = learningSchedule.ScheduleID }, learningSchedule);
        }

        // DELETE: api/LearningSchedules/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LearningSchedule>> DeleteLearningSchedule(int id)
        {
            var learningSchedule = await _context.LearningSchedules.FindAsync(id);
            if (learningSchedule == null)
            {
                return NotFound();
            }

            _context.LearningSchedules.Remove(learningSchedule);
            await _context.SaveChangesAsync();

            return learningSchedule;
        }

        private bool LearningScheduleExists(int id)
        {
            return _context.LearningSchedules.Any(e => e.ScheduleID == id);
        }
    }
}
