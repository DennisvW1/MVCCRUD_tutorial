using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCCRUD_tutorial.Data;
using MVCCRUD_tutorial.Models;
using MVCCRUD_tutorial.Models.Domain;

namespace MVCCRUD_tutorial.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCCRUDDbContext mvc;

        public EmployeesController(MVCCRUDDbContext mvc)
        {
            this.mvc = mvc;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await mvc.Employees.ToListAsync();

            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
                Department = addEmployeeRequest.Department
            };

            await mvc.Employees.AddAsync(employee);
            await mvc.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid Id)
        { 
            var employee = await mvc.Employees.FirstOrDefaultAsync(x => x.Id == Id);

            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    DateOfBirth = employee.DateOfBirth,
                    Department = employee.Department
                };

                return await Task.Run(() => View("View", viewModel));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await mvc.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Department = model.Department;

                await mvc.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return await Task.Run(() => View("View", model));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await mvc.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                mvc.Employees.Remove(employee);
                await mvc.SaveChangesAsync();

                return RedirectToAction("Index");
            }
                
            return RedirectToAction("Index");
        }
    }
}
