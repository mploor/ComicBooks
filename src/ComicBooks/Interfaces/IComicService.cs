using ComicBooks.Models;
using ComicBooks.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBooks.Interfaces
{
    public interface IComicService
    {
        void AddComic(Comic comic);
        void DeleteComic(int id);
        Comic GetComic(int id);
        List<Comic> ListComics(string user);
        List<Comic> ListAllComics();
        List<Comic> GetWishList(string user);
        void UpdateComic(Comic comic);
    }
}
