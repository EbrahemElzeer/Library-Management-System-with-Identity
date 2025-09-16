using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DTOS;
using AutoMapper;
using EF_layer.Model;
using Library_Managment_System.Models;
using Library_Managment_System.View_Model;

namespace ApplicationCore.Auto_Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Borrow, BorrowViewModel>()

                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book != null ? src.Book.Title : "UnKnown"))
                .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member != null ? src.Member.Name : "UnKnown"));
           
            CreateMap<BorrowViewModel, Borrow>()
         .ForMember(dest => dest.Book, opt => opt.Ignore())  
         .ForMember(dest => dest.Member, opt => opt.Ignore());

            CreateMap<RegisterViewModel,RegisterDto>();
            CreateMap<LoginUserViewModel, LoginDto>();
            CreateMap<ChangePasswordViewModel, ChangePasswordDto>();
            CreateMap<ResetPasswordViewModel,ResetPasswordDto>();
            CreateMap<RolesViewModel,RoleDto>().ReverseMap();
            CreateMap<AssignRoleViewModel, AssignRoleDto>();
            CreateMap<UserWithRoleDto, UserWithRoleViewModel>()
           .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
            CreateMap<ApplicationUser, UserWithRoleDto>();
            CreateMap<AssignRoleViewModel, AssignRoleDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.SelectedUserId))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.SelectedRoleName));

        }
    }
}
