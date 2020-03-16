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
        [HttpGet("{userName}")]
        public async Task<ActionResult<LearningSchedule>> GetLearningSchedule(string userName)
        {
            var learningSchedule = _context.LearningSchedules.FirstOrDefault(e => e.UserName == userName);

            if (learningSchedule == null)
            {
                return NotFound();
            }

            return learningSchedule;
        }

        // PUT: api/LearningSchedules/5
        [HttpPut("{userName}")]
        public async Task<IActionResult> PutLearningSchedule(string userName, LearningSchedule learningSchedule)
        {
            if (userName != learningSchedule.UserName)
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
                if (!LearningScheduleExists(userName))
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
            //var l = _context.LearningSchedules.FindAsync(learningSchedule.UserName);
            if (LearningScheduleExists(learningSchedule.UserName))
            {
                _context.LearningSchedules.Update(learningSchedule);
                await _context.SaveChangesAsync(); 
                return learningSchedule;
            }
            else
            {
                _context.LearningSchedules.Add(learningSchedule);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetLearningSchedule", new { userName = learningSchedule.UserName }, learningSchedule);
            }
        }

        // DELETE: api/LearningSchedules/5
        [HttpDelete("{userName}")]
        public async Task<ActionResult<LearningSchedule>> DeleteLearningSchedule(string userName)
        {
            var learningSchedule = await _context.LearningSchedules.FindAsync(userName);
            if (learningSchedule == null)
            {
                return NotFound();
            }

            _context.LearningSchedules.Remove(learningSchedule);
            await _context.SaveChangesAsync();

            return learningSchedule;
        }

        private bool LearningScheduleExists(string userName)
        {
            return _context.LearningSchedules.Any(e => e.UserName == userName);
        }
    }
}
