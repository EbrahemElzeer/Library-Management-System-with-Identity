using Library_Managment_System.Models;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Interfaces;
using X.PagedList.Extensions;
using X.PagedList;
using Library_Managment_System.View_Model;
using Microsoft.AspNetCore.Authorization;
using ApplicationCore.DomainServices;
using AutoMapper;
using ApplicationCore.Interfaces;


namespace Library_Managment_System.Controllers
{
    [AllowAnonymous]
    public class BorrowController : Controller
    {

        private readonly IBorrowService BorrowService;
        private readonly IBookService BookService;
        private readonly IMemberService MemberService;
        private readonly IMapper _mapper;
        public BorrowController(IBorrowService _BorrowService
            , IBookService _BookService, IMemberService _MemberService, IMapper mapper
            )
        {

            BorrowService = _BorrowService;
            BookService = _BookService;
            MemberService = _MemberService;
            _mapper = mapper;
        }
      
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = Math.Max(page ?? 1, 1);
            const int pageSize = 5;

            var items = await BorrowService.GetAllAsync() ?? new List<Borrow>(); 

            var viewModelList = _mapper.Map<List<BorrowViewModel>>(items);

            IPagedList<BorrowViewModel> pagination = viewModelList.ToPagedList(pageNumber, pageSize);

            return View(pagination);
        }


        public async Task<IActionResult> GetBorrowList()
        {
            var Borrow = await BorrowService.GetAllAsync();
           

            var ViewModelList = _mapper.Map<List<BorrowViewModel>>(Borrow);
            var pagelist = ViewModelList.ToPagedList(1, 5);
            return PartialView("_BorrowList", pagelist);
        }

   
        [HttpGet]
        public async Task<IActionResult> AddItem()
        {
            var books = await BookService.GetAllAsync();
            var members = await MemberService.GetAllAsync();

            var vm = new BorrowViewModel
            {
                BookList = books.ToList(),
                MemberList = members.ToList()
            };

            return PartialView("_Save_EditBorrow",vm);
        }

        [HttpPost]
        public async Task<IActionResult> SaveBorrow(BorrowViewModel vm)
        {
            if (ModelState.IsValid)
            {

              
                var borrow = _mapper.Map<Borrow>(vm);
                await BorrowService.AddSaveBorrow(borrow);

                return Json(new { success = true });
            }
            var allErrors = ModelState.Values
                                         .SelectMany(v => v.Errors)
                                         .Select(e => e.ErrorMessage)
                                         .ToList();

            return Json(new
            {
                success = false,
                message = string.Join(" | ", allErrors)
            });
        }


        public async Task<IActionResult> EditBorrow(int id)
        {

            var borrow =await BorrowService.FindById(id);
            var books =await BookService.GetAllAsync();
            var members =await MemberService.GetAllAsync();
       
            var vm = _mapper.Map<BorrowViewModel>(borrow);
            vm.BookList=books.ToList();
            vm.MemberList = members.ToList();
           
          return PartialView("_Save_EditBorrow", vm);
        }


        public async Task<IActionResult> DeleteBorrow(int id)
        {
          await  BorrowService.Delete(id);
        
            return Json(new { success = true });
        }
    }
}
