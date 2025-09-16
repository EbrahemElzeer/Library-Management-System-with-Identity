using ApplicationCore.DomainServices;
using ApplicationCore.Interfaces;
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
    public class MemberController : Controller
    {

      
        private readonly IMemberService MemberService;

        public MemberController(IMemberService _MemberService)
        {
            MemberService = _MemberService;
        }
        [Authorize]
        public async Task<IActionResult> Index(int? page)
        {

            var Item = await MemberService.GetAllAsync();
            IPagedList<Member> pagination = Item.ToPagedList(page ?? 1, 5);
            return View("Index", pagination);
        }
        public async Task<IActionResult> GetMemberList(int? page)
        {
            var members = await MemberService.GetAllAsync();
            var pagelist = members.ToPagedList(page ?? 1, 5);
            return PartialView("_MemberList", pagelist);
        }

        public IActionResult AddItem()
        {
           
            return PartialView("_Save_EditMember", new Member());
        }

        [HttpPost]
        public async Task<IActionResult> SaveAdd(Member member)
        {
            if (ModelState.IsValid) {
               
                await MemberService.SaveAdd(member);


            return Json(new { success = true });
            }return PartialView("_Save_EditMember", member);
        }
        public async Task<IActionResult> EditMember(int id )
        {
            var Member=await MemberService.FindById(id);
            return PartialView("_Save_EditMember",Member);
        }


        public  async Task<IActionResult> DeleteMember (int id)
        {
           
          await MemberService.Delete(id);
            return Json(new { success = true });
        }
    }
}
