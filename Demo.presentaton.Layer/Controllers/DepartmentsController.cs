
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace Demo.presentaton.Layer.Controllers
{
    public class DepartmentsController : Controller
    {
        //private readonly IGenearicRepository<Department> _repository;

        private IDepartmentRepository _repository;

        public DepartmentsController(IDepartmentRepository repo)
        {
            _repository = repo;
        }

        [HttpGet]
        public async Task <IActionResult> Index()
        { 
            var departments = await _repository.GetAllAsync();
            return View(departments);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Department department)
        {
            if(!ModelState.IsValid) return View(department);
                _repository.AddAsync(department);
            return RedirectToAction(nameof(Index));       
        }



        private async Task <IActionResult> DepartmentControllerHandler(int ? id , string viewname)
        {
            if (!id.HasValue) return BadRequest();
            var department =await _repository.GetAsync(id.Value);
            if (department is null) return NotFound();
            return View(viewname, department);
        }
        public async Task <IActionResult> Details(int? id) => await DepartmentControllerHandler(id, nameof(Details));
        public async Task <IActionResult> Edit(int? id) =>await  DepartmentControllerHandler(id, nameof(Edit));
        public async Task <IActionResult> Delete(int? id) =>await  DepartmentControllerHandler(id, nameof(Delete));


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id,Department department)
        {
            if(id != department.Id) { return BadRequest(); }
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update(department);
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    {
                        // log Exception
                        ModelState.AddModelError("", ex.Message);
                    }
                }
            }
            return View(department);
        }



        [HttpPost]

        public IActionResult Delete([FromRoute] int id, Department department)
        {
            if (id != department.Id) { return BadRequest(); }
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Delete(department);
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    {
                        // log Exception
                        ModelState.AddModelError("", ex.Message);
                    }
                }
            }
            return View(department);
        }

    }
} 
