using ComicBooks.Interfaces;
using ComicBooks.Models;
using ComicBooks.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBooks.Services
{
    public class TitleService : ITitleService
    {
        private IGenericRepository _repo;

        public TitleService(IGenericRepository repo)
        {
            _repo = repo;
        }

        // Get complete list of titles with full info
        public List<Title> FullTitles()
        {
            List<Title> titles = (from t in _repo.Query<Title>()
                                  select new Title
                                  {
                                      Id = t.Id,
                                      Name = t.Name,
                                      Publisher = t.Publisher,
                                      PubYear = t.PubYear,
                                      Comics = t.Comics
                                  }).OrderBy(o => o.Name).ToList();
            return titles;
        }

        // Get list of titles without lists of actual comics
        public List<TitleOnly> ListTitles()
        {
            List<TitleOnly> titles = (from t in _repo.Query<Title>()
                                      select new TitleOnly
                                      {
                                          Id = t.Id,
                                          Name = t.Name,
                                          Publisher = t.Publisher,
                                          PubYear = t.PubYear
                                      }).OrderBy(o => o.Name).ToList();
            return titles;
        }

        // Return summary (names and count) of titles for specified comic list
        public List<TitleSummary> TitleSummary(List<Comic> comics)
        {
            List<TitleSummary> titles = new List<TitleSummary>();
            bool found = false;
            foreach (Comic comic in comics)
            {
                foreach (TitleSummary title in titles)
                {
                    found = false;
                    if (title.Title == comic.Title.Name)
                    {
                        title.Count++;
                        title.TotalPrice += comic.PurchasePrice;
                        title.TotalValue += comic.Value;
                        found = true;
                    }
                }
                if (!found)
                {
                    titles.Add(new TitleSummary {
                        Title = comic.Title.Name,
                        TitleId = comic.Title.Id,
                        Publisher = comic.Title.Publisher,
                        PubYear = comic.Title.PubYear,
                        TotalPrice = comic.PurchasePrice,
                        TotalValue = comic.Value,
                        Count = 1
                    });
                }
            }
            return titles.OrderBy(o => o.Title).ToList();
        }

        // Get summary of all books in database
        //public List<TitleSummary> FullBookSummary()
        //{
        //    List<Comic> comics = (from c in _repo.Query<Comic>()
        //                          select new Comic
        //                          {
        //                              Id = c.Id,
        //                              IssueNum = c.IssueNum,
        //                              Condition = c.Condition,
        //                              PurchasePrice = c.PurchasePrice,
        //                              Value = c.Value,
        //                              Cgc = c.Cgc,
        //                              ImageUrl = c.ImageUrl,
        //                              User = c.User,
        //                              Title = c.Title
        //                          }).ToList();
        //}

        // Get list of books of specified title id for specified user
        public List<Comic> GetTitle(int id, string user)
        {
            Title title = (from t in _repo.Query<Title>()
                           where t.Id == id
                           select new Title {
                               Id = t.Id,
                               Name = t.Name,
                               Publisher = t.Publisher,
                               PubYear = t.PubYear,
                               Comics = t.Comics
                           }).FirstOrDefault();
            List<Comic> userBooks = (from c in title.Comics
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
            return userBooks;
        }

        // Return single title by id complete with comic list
        public Title GetOneTitle(int id)
        {
            Title title = (from t in _repo.Query<Title>()
                           where t.Id == id
                           select t).FirstOrDefault();
            return title;
        }

        public void AddTitle(Title title)
        {
            _repo.Add(title);
        }

        public void UpdateTitle(Title title)
        {
            _repo.Update(title);
        }

        public void DeleteTitle(int id)
        {
            _repo.Delete(GetOneTitle(id));
        }
    }
}
