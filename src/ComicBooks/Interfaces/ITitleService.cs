using ComicBooks.Models;
using ComicBooks.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBooks.Interfaces
{
    public interface ITitleService
    {
        void AddTitle(Title title);
        void DeleteTitle(int id);
        List<Title> FullTitles();
        List<Comic> GetTitle(int id, string user);
        List<TitleSummary> TitleSummary(List<Comic> comics);
        List<TitleOnly> ListTitles();
        void UpdateTitle(Title title);
        Title GetOneTitle(int id);
    }
}
