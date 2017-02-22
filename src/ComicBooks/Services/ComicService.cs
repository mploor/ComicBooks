using ComicBooks.Interfaces;
using ComicBooks.Models;
using ComicBooks.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBooks.Services
{
    public class ComicService : IComicService
    {
        private IGenericRepository _repo;
        private ITitleService _titleService;

        public ComicService(IGenericRepository repo, ITitleService titleService)
        {
            _repo = repo;
            _titleService = titleService;
        }

        // Get List of comics for specified user
        public List<Comic> ListComics(string user)
        {
            List<Comic> comics = (from c in _repo.Query<Comic>()
                                  where c.User == user && !c.WishList
                                  select new Comic {
                                      Id = c.Id,
                                      IssueNum = c.IssueNum,
                                      Condition = c.Condition,
                                      PurchasePrice = c.PurchasePrice,
                                      Value = c.Value,
                                      Cgc = c.Cgc,
                                      ImageUrl = c.ImageUrl,
                                      User = c.User,
                                      Title = c.Title
                                  }).ToList();
            return comics;
        }

        // Get Wish List for specified user
        public List<Comic> GetWishList(string user)
        {
            List<Comic> comics = (from c in _repo.Query<Comic>()
                                  where c.User == user && c.WishList
                                  select new Comic
                                  {
                                      Id = c.Id,
                                      IssueNum = c.IssueNum,
                                      Condition = c.Condition,
                                      PurchasePrice = c.PurchasePrice,
                                      Value = c.Value,
                                      Cgc = c.Cgc,
                                      ImageUrl = c.ImageUrl,
                                      User = c.User,
                                      Title = c.Title
                                  }).ToList();
            return comics;
        }

        // Get List of all comics
        public List<Comic> ListAllComics()
        {
            List<Comic> comics = (from c in _repo.Query<Comic>()
                                  where !c.WishList
                                  select new Comic
                                  {
                                      Id = c.Id,
                                      IssueNum = c.IssueNum,
                                      Condition = c.Condition,
                                      PurchasePrice = c.PurchasePrice,
                                      Value = c.Value,
                                      Cgc = c.Cgc,
                                      ImageUrl = c.ImageUrl,
                                      User = c.User,
                                      Title = c.Title
                                  }).ToList();
            return comics;
        }

        // Get specified comic by id
        public Comic GetComic(int id)
        {
            Comic com = (from c in _repo.Query<Comic>()
                         where c.Id == id
                         select new Comic {
                             Id = c.Id,
                             IssueNum = c.IssueNum,
                             Condition = c.Condition,
                             PurchasePrice = c.PurchasePrice,
                             Value = c.Value,
                             Cgc = c.Cgc,
                             ImageUrl = c.ImageUrl,
                             User = c.User,
                             Title = c.Title
                         }).FirstOrDefault();
            return com;
        }

        public void AddComic(Comic comic)
        {
            comic.Title = _titleService.GetOneTitle(comic.Title.Id);
            _repo.Add(comic);
        }

        public void UpdateComic(Comic comic)
        {
            comic.Title = _titleService.GetOneTitle(comic.Title.Id);
            _repo.Update(comic);
        }

        public void DeleteComic(int id)
        {
            _repo.Delete(GetComic(id));
        }
    }
}
