using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemorizeWordsAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MemorizeWordsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordListController : ControllerBase
    {
        private readonly MWContext _context;

        public WordListController(MWContext context)
        {
            _context = context;
        }

        // GetAll() is automatically recognized as
        // http://localhost:<port #>/api/todo
        [HttpGet]
        public IEnumerable<WordList> GetAll()
        {
            return _context.WordLists.ToList();
        }

        // GetById() is automatically recognized as
        // http://localhost:<port #>/api/todo/{id}

        // For example:
        // http://localhost:<port #>/api/todo/1

        [HttpGet("{id}", Name = "GetWordList")]
        public IActionResult GetById(long id)
        {
            var item = _context.WordLists.FirstOrDefault(t => t.WordListID == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWordList(int id, WordList wordList)
        {
            if (id != wordList.WordListID)
            {
                return BadRequest();
            }

            _context.Entry(wordList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WordListExists(id))
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

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WordList>> DeleteWordList(int id)
        {
            var wordList = await _context.WordLists.FindAsync(id);
            if (wordList == null)
            {
                return NotFound();
            }

            _context.WordLists.Remove(wordList);
            await _context.SaveChangesAsync();

            return wordList;
        }
        private bool WordListExists(int id)
        {
            return _context.WordLists.Any(e => e.WordListID == id);
        }

    }
}