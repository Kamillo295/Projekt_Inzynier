using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Projekcik.application.Users;
using Projekcik.Infrastructure.Persistance;

namespace Projekcik.Controllers
{
    public class ChangePasswordController : Controller
    {
        private readonly AplicationDbContext _context;
        private readonly IMapper _mapper;

        public ChangePasswordController(AplicationDbContext context, IMapper mapper)     //tutaj mapowanie dto
        {
            _context = context;
            _mapper = mapper;
        }

       
    }
}
