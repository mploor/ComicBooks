using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComicBooks.Interfaces;
using ComicBooks.ViewModels;
using ComicBooks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ComicBooks.API
{
    [Route("api/[controller]")]
    public class TitlesController : Controller
    {
        private ITitleService _titleService;
        private IComicService _comicService;

        public TitlesController(ITitleService titleService, IComicService comicService)
        {
            _titleService = titleService;
            _comicService = comicService;
        }

        [HttpGet]
        [Authorize]
        public List<TitleOnly> Get()
        {
            return _titleService.ListTitles();
        }

        [HttpGet("{id}")]
        [Authorize]
        public List<Comic> Get(int id)
        {
            return _titleService.GetTitle(id, User.Identity.Name);
        }

        [HttpGet("user")]
        [Authorize]
        public List<TitleSummary> GetTitles()
        {
            return _titleService.TitleSummary(_comicService.ListComics(User.Identity.Name));
        }

        [HttpGet("all")]
        [Authorize]
        public List<TitleSummary> GetFullSummary()
        {
            return _titleService.TitleSummary(_comicService.ListAllComics());
        }

        [HttpGet("full")]
        [Authorize]
        public List<Title> GetCompleteTitles()
        {
            return _titleService.FullTitles();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]Title title)
        {
            if (title == null)
            {
                return BadRequest();
            }
            else if (title.Id == 0)
            {
                _titleService.AddTitle(title);
                return Ok();
            }
            else
            {
                _titleService.UpdateTitle(title);
                return Ok();
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            _titleService.DeleteTitle(id);
            return Ok();
        }
    }
}
