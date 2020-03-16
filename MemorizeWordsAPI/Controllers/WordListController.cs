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

        [HttpGet("{userName}", Name = "GetWordList")]
        public IActionResult GetByUserName(string userName)
        {
            var item = _context.WordLists.FirstOrDefault(t => t.UserName == userName);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        // PUT: api/Tasks/5
        [HttpPut("{userName}")]
        public async Task<IActionResult> PutWordList(string userName, WordList wordList)
        {
            if (userName != wordList.UserName)
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
                if (!WordListExists(userName))
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

        // POST: api/WordList/mingwang
        [HttpPost]
        public async Task<ActionResult<WordList>> PostWordList(WordList wordList)
        {
            //var l = _context.LearningSchedules.FindAsync(learningSchedule.UserName);
            if (WordListExists(wordList.UserName))
            {
                _context.WordLists.Update(wordList);
                await _context.SaveChangesAsync();
                return wordList;
            }
            else
            {
                _context.WordLists.Add(wordList);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetWordList", new { userName = wordList.UserName }, wordList);
            }
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{userName}")]
        public async Task<ActionResult<WordList>> DeleteWordList(string userName)
        {
            var wordList = _context.WordLists.FirstOrDefault(e=>e.UserName==userName);
            if (wordList == null)
            {
                return NotFound();
            }

            _context.WordLists.Remove(wordList);
            await _context.SaveChangesAsync();

            return wordList;
        }
        private bool WordListExists(string userName)
        {
            return _context.WordLists.Any(e => e.UserName == userName);
        }

    }
}