using AutoMapper;
using Demo.presentaton.Layer.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Demo.presentaton.Layer.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public EmployeesController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [AllowAnonymous]
        //[IgnoreAntiforgeryToken]
        public async  Task <IActionResult> Index(string? searchValue)
        {
            ViewBag.Message = "Employee Created successfully ";
            var employees = Enumerable.Empty<Employee>();

            if (string.IsNullOrWhiteSpace(searchValue))
                employees =await _unitOfWork.Employees.GetAllWithDepartmentAsync();

            else employees = await _unitOfWork.Employees.GetAllAsync(searchValue);
            var employeeViewModel = _mapper.Map<IEnumerable<Employee> ,IEnumerable<EmployeeViewModel>>(employees);
                return View(employeeViewModel);
            
             

        }
        public async Task <IActionResult> Create()
        {
            var departments =await _unitOfWork.Departments.GetAllAsync();
            SelectList ListItems = new SelectList(departments , "Id" , "Name");
            ViewBag.Departments= ListItems;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(EmployeeViewModel employeeVM)
        {

            if (!ModelState.IsValid) return View(employeeVM);
            if (employeeVM.Image is not null)      
                employeeVM.ImageName =await DocumentSettings.UploadFileAsync(employeeVM.Image, "Images");

            var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
            await _unitOfWork.Employees.AddAsync(employee);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));  
        }


        private async Task<IActionResult> EmployeeControllerHandler(int? id, string viewname)
        {
            if(viewname == nameof(Edit))
            {
                var departments = await _unitOfWork.Departments.GetAllAsync();
                SelectList ListItems = new SelectList(departments, "Id", "Name");
                ViewBag.Departments = ListItems;

            }
            if (!id.HasValue) return BadRequest();
            var employee =await _unitOfWork.Employees.GetAsync(id.Value);
            if (employee is null) return NotFound();
            //var employeeVM = new EmployeeViewModel()
            //{
            //    Address = employee.Address,
            //    Department = employee.Department,
            //    Age = employee.Age,
            //    DepartmentId = employee.DepartmentId,
            //    Email = employee.Email,
            //    Id = employee.Id,
            //    IsActive = employee.IsActive,
            //    Name = employee.Name,
            //    Phone = employee.Phone,
            //    salary = employee.salary
            //};
            var employeeVM = _mapper.Map<EmployeeViewModel>(employee);

            return View(viewname, employeeVM);
        }
        public async Task<IActionResult> Details(int? id) =>await EmployeeControllerHandler(id, nameof(Details));
        public async Task<IActionResult> Edit(int? id) =>await EmployeeControllerHandler(id, nameof(Edit));
        public async Task<IActionResult> Delete(int? id) =>await EmployeeControllerHandler(id, nameof(Delete));


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id) { return BadRequest(); }
            if (ModelState.IsValid)
            {
                try
                {
                    if (employeeVM.Image is not null)
                        employeeVM.ImageName =await DocumentSettings.UploadFileAsync(employeeVM.Image, "Images");

                    var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    _unitOfWork.Employees.Update(employee);
                    if (await _unitOfWork.SaveChangesAsync() > 0)
                    {
                        TempData["Message"]= "Employee Updated successfully";
                        return RedirectToAction(nameof(Index));
                    }
                    

                }
                catch (Exception ex)
                {
                    {
                        // log Exception
                        ModelState.AddModelError("", ex.Message);
                    }
                }
            }
            return View(employeeVM);
        }



        [HttpPost]

        public async Task<IActionResult> Delete([FromRoute] int id, Employee Employee)
        {
            if (id != Employee.Id) { return BadRequest(); }
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Employees.Delete(Employee);
                    if(await _unitOfWork.SaveChangesAsync() >= 0 && Employee.ImageName is not null)
                    {
                        DocumentSettings.DeleteFile("Images",Employee.ImageName);
                    }
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
            return View(Employee);
        }
    }
}
