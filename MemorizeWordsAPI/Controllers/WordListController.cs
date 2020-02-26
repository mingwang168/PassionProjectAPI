using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemorizeWordsAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

    }
}