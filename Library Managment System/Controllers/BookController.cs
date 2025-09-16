using ApplicationCore.DomainServices;
using ApplicationCore.Interfaces;
using EF_layer;
using Library_Managment_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using RepositoryLayer.Interfaces;
using X.PagedList;
using X.PagedList.Extensions;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library_Managment_System.Controllers
{
    //[Authorize(Roles = "Admin,Manager")]
    public class BookController : Controller

    {
        private readonly IBookService BookService;
        public BookController(IBookService _BookService)
        {
            BookService = _BookService;
        }

        [Authorize]
        public async Task<IActionResult> Index(int? page)
        {
            var Items = await BookService.GetAllAsync();
            
            var pagination = Items.ToPagedList(page ?? 1, 5);
            return View("index", pagination);
        }

        public async Task<IActionResult> GetBookList()
        {
            var Items = await BookService.GetAllAsync();
            var pagelist = Items.ToPagedList(1, 5);
            return PartialView("_BookList", pagelist);
        }

        [HttpGet]
        public IActionResult AddItem()
        {
            return PartialView("_Save_EditBook",new Books());
        }
        [HttpPost]
        public async Task<IActionResult> SaveBook(Books books)
        {
            if (ModelState.IsValid)
            {

                await BookService.saveBook(books);
              
                return Json(new { success = true });
            }
            return PartialView("_Save_EditBook", books);
        }
     
        public async Task<IActionResult> EditBook(int id)
        {
            var book = await BookService.FindById(id);
            return PartialView("_Save_EditBook", book);
        }


           public async Task<IActionResult> DeleteBook(int id)
        {

          
         await   BookService.DeleteBook(id);
           
            return Json(new { success = true });
        }
        

    }
}
