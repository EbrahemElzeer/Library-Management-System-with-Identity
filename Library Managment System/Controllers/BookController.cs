using ApplicationCore.DomainServices;
using ApplicationCore.Interfaces;
using EF_layer;
using Library_Managment_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            var pageNumber = Math.Max(page ?? 1, 1);
            const int pageSize = 5;
            var items = await BookService.GetAllAsync() ?? new List<Books>();
            var pagination = items.ToPagedList(pageNumber, pageSize);
            return View("Index", pagination);
        }

       
        public async Task<IActionResult> GetBookList(int? page) 
        {
            var pageNumber = Math.Max(page ?? 1, 1);
            const int pageSize = 5;

            var items = await BookService.GetAllAsync() ?? new List<Books>();

            var pagelist = items.ToPagedList(pageNumber, pageSize);

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
