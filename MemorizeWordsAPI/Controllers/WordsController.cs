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
    public class WordsController : ControllerBase
    {
        private readonly MWContext _context;

        public WordsController(MWContext context)
        {
            _context = context;
        }

        // GET: api/Words
        [HttpGet("{userName}")]
        public IEnumerable<Word> GetWords(string userName)
        {
            IEnumerable<Word> wdsList = _context.Words.Where(w => w.UserName == userName);
            return wdsList;
        }



        //// GET: api/Words/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Word>> GetWord(int id)
        //{
        //    var word = await _context.Words.FindAsync(id);

        //    if (word == null)
        //    {
        //        return NotFound();
        //    }

        //    return word;
        //}

        // PUT: api/Words/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWord(int id, Word word)
        {
            if (id != word.WordID)
            {
                return BadRequest();
            }

            _context.Entry(word).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WordExists(id))
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

        // POST: api/Words
        [HttpPost]
        public async Task<ActionResult<Word>> PostWord(Word word)
        {
            _context.Words.Add(word);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWord", new { id = word.WordID }, word);
        }

        // DELETE: api/Words/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Word>> DeleteWord(int id)
        //{
        //    var word = await _context.Words.FindAsync(id);
        //    if (word == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Words.Remove(word);
        //    await _context.SaveChangesAsync();

        //    return word;
        //}

        [HttpDelete("{userName}")]
        public async Task<ActionResult<Word>> DeleteAll(string userName)
        {
            var rows = from o in _context.Words select o;
            foreach (var row in rows)
            {
                if(row.UserName == userName)
                {
                _context.Words.Remove(row);
                }
            }
            _context.SaveChanges();
            return NoContent();
        }
        private bool WordExists(int id)
        {
            return _context.Words.Any(e => e.WordID == id);
        }
    }
}
