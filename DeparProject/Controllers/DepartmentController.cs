using DeparProject.DBService;
using DeparProject.Events;
using DeparProject.Interfaces;
using DeparProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DeparProject.Controllers
{
    public class DepartmentController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IEmailServiceSender _emailServiceSender;

        public DepartmentController(ApplicationDbContext context,IEmailServiceSender emailServiceSender)
        {
            _context = context;
            this._emailServiceSender = emailServiceSender;
        }

        public async Task<IActionResult> Index()
        {
            var departments = await _context.Departments.ToListAsync();
            return View(departments);
        }

        public async Task<IActionResult> Details(string id)
        {
            var department = await _context.Departments
                .Include(d => d.SubDepartments)
                .FirstOrDefaultAsync(m => m.ID.ToString() == id);

            if (department == null)
            {
                return NotFound();
            }
            var parentDepartments = _context.Departments
          .Where(d => d.SubDepartments.Any(sd => sd.ID.ToString() == id))
          .ToList();
            ViewBag.ParentDepartments = await GetParentDepartments(department);

            return View(department);
        }

        public async Task<ActionResult> Delete(Guid id)
        {
      
            var dep = new Department { ID = id };
            _context.Departments.Remove(dep);
            var department = await _context.Departments.Where(x => x.ID.ToString() == id.ToString()).FirstOrDefaultAsync();

            _emailServiceSender.HandleDepartmentDelete(new DemartMentEmailService { DepartmentName = department!.Name });
            await _context.SaveChangesAsync();


            return RedirectToAction("Index");
        }

        public ActionResult CreateChild(Guid parentId)
        {
            // Create a new instance of the Department model and set ParentId
            var model = new Department { ParentDepartmentId = parentId };
           
            return View(model);
        }


        [HttpPost]
        public async Task<ActionResult> CreateChild(Department model)
        {

            var parentDep = await _context.Departments.AsNoTracking().Where(d=>d.ID == model.ParentDepartmentId).FirstOrDefaultAsync();


            var existingDepartment = _context.Departments
           .FirstOrDefault(d => d.Name == model.Name);
            if (existingDepartment!=null) {
            return View(model);
            }

            if (parentDep == null) {
                return RedirectToAction("Details", new { id = model.ParentDepartmentId });
            }
        //    model.ParentDepartment = parentDep;
            model.ID = Guid.NewGuid();

          
                // Save the new child department to the database
                // For example, assuming you have a DbContext named _context
                _context.Departments.Add(model);
                _context.SaveChanges();

            _emailServiceSender.HandleDepartmentCreation(new DemartMentEmailService { DepartmentName = model.Name });

            return View(model);
        }

        private async Task<List<Department>> GetParentDepartments(Department department)
        {
            var parents = new List<Department>();
            var current = department;

            while (current.ParentDepartmentId.HasValue)
            {
                current = await _context.Departments.FindAsync(current.ParentDepartmentId.Value);
                parents.Add(current);
            }
            return parents;
        }

        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name", department.ParentDepartmentId);
            _emailServiceSender.HandleDepartmentCreation(new DemartMentEmailService { DepartmentName = department.Name });
            return View(department);
        }
    }
}
