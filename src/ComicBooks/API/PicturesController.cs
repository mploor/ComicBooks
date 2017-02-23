using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ComicBooks.Data;
using ComicBooks.Models;
using System.IO;
using ComicBooks.ViewModels;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ComicBooks.API
{
    [Route("api/[controller]")]
    public class PicturesController : Controller
    {
        public ApplicationDbContext _db;

        public PicturesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public byte[] imageToByteArray(FileStream imageIn)
        {
            byte[] byteme = new byte[1000000];
            imageIn.Read(byteme, 0, 100000);
            return byteme;
        }

        public FileStream byteArrayToImage(byte[] byteArrayIn)
        {
            FileStream returnImage = new FileStream("wwwroot/images/test.jpg", FileMode.Create);
            returnImage.Write(byteArrayIn, 0, 1000000);
            return returnImage;
        }

        [HttpGet]
        public FileStream GetPic()
        {
            var mypic = (from p in _db.Pictures
                         where p.Id == 4
                         select p).FirstOrDefault();
            return byteArrayToImage(mypic.Pic);
        }

        [HttpPost]
        public void AddPic([FromBody]PicView picView)
        {
            var testStream = new FileStream(picView.MyFile, FileMode.Open, FileAccess.Read);
            _db.Pictures.Add(new Picture { Pic = imageToByteArray(testStream) });
            _db.SaveChanges();
        }
    }
}
