using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models.SchoolViewModels;
using ContosoUniversity.Data;

namespace ContosoUniversity.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly SchoolContext _context;

        public IndexModel(SchoolContext context)
        {
            _context = context;
        }

        /*public IList<Course> Courses { get;set; }

        public async Task OnGetAsync()
        {
            Courses = await _context.Course
                .Include(c => c.Department)
                .AsNoTracking()
                .ToListAsync();
        }*/

        public IList<CourseVM> CoursesVM { get; set; }

        public async Task OnGetAsync()
        {
            CoursesVM = await _context.Course
                    .Select(p => new CourseVM
                    {
                        CourseID = p.CourseID,
                        Title = p.Title,
                        Credits = p.Credits,
                        DepartmentName = p.Department.Name
                    }).ToListAsync();
        }
    }
}
