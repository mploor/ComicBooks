using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComicBooks.Models;
using ComicBooks.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComicBooks.API
{
    [Route("api/[controller]")]
    public class ComicsController : Controller
    {
        private IComicService _comService;

        public ComicsController(IComicService comService)
        {
            _comService = comService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public Comic Get(int id)
        {
            return _comService.GetComic(id);
        }

        [HttpGet]
        [Authorize]
        public List<Comic> Get()
        {
            return _comService.ListComics(User.Identity.Name);
        }

        [HttpGet("wish")]
        [Authorize]
        public List<Comic> GetWishList()
        {
            return _comService.GetWishList(User.Identity.Name);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]Comic comic)
        {
            if (comic == null)
            {
                return BadRequest();
            }
            else if (comic.Id == 0)
            {
                comic.User = User.Identity.Name;
                _comService.AddComic(comic);
                return Ok();
            }
            else
            {
                comic.User = User.Identity.Name;
                _comService.UpdateComic(comic);
                return Ok();
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            _comService.DeleteComic(id);
            return Ok();
        }
    }
}
