using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using WebEmployee.web.Data.Repository.IRepository;
using WebEmployee.web.Models;

namespace WebEmployee.web.Controllers
{
    public class EmployeesController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmployeesController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index(List<Employee> employees)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;

            #region Upload CSV
            string filepath = Path.Combine(wwwRootPath, @"files");

            if (!string.IsNullOrEmpty(file.FileName))
            {
                var oldImagePath = Path.Combine(filepath, file.FileName.TrimStart('\\'));

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            using (var fileStream = new FileStream(Path.Combine(filepath, file.FileName), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            #endregion

            var employees = await GetEmployeeListAsync(file.FileName);
            return await Index(employees);
        }

        private async Task<List<Employee>> GetEmployeeListAsync(string filename)
        {
            List<Employee> employees = new List<Employee>();
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string filepath = Path.Combine(wwwRootPath, @"files");
            var path = Path.Combine(filepath, filename);

            #region Read CSV
            using (var reader = new StreamReader(path))
            using (var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture))
            {
                await csv.ReadAsync();
                csv.ReadHeader();
                while (await csv.ReadAsync())
                {
                    var employee = csv.GetRecord<Employee>();
                    employee.Id = 0;
                    employees.Add(employee);
                    await _unitOfWork.Employee.AddAsync(employee);
                }
            }
            await _unitOfWork.SaveAsync();
            TempData["success"] = "Employee creado exitosamente";
            #endregion

            return employees;
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Employee employee = new();

            ViewBag.Employees = await _unitOfWork.Employee.GetAllAsync();
            ViewBag.EmployeeTypes = await _unitOfWork.EmployeeType.GetAllAsync();

            if (id == null || id == 0)
            {
                return View(employee);
            }
            else
            {
                employee = await _unitOfWork.Employee.GetAsync(u => u.Id == id, includeProperties: "EmployeeImages");
                return View(employee);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Employee employee, List<IFormFile> filess)
        {
            if (ModelState.IsValid)
            {
                employee.CreationTime = DateTime.Now;

                if (employee.BirthPlaceCountry != null)
                {
                    employee.BirthPlaceCountry = HelperModel.Countries.Where(x => x.iso2 == employee.BirthPlaceCountry).Select(x => x.name).FirstOrDefault();
                }
                if (employee.BirthPlaceState != null)
                {
                    employee.BirthPlaceState = HelperModel.States.Where(x => x.iso2 == employee.BirthPlaceState).Select(x => x.name).FirstOrDefault();
                }

                if (employee.Id == 0)
                {
                    await _unitOfWork.Employee.AddAsync(employee);
                }
                else
                {
                    await _unitOfWork.Employee.UpdateAsync(employee);
                }

                await _unitOfWork.SaveAsync();

                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (filess != null)
                {
                    foreach (var file in filess)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string employeePath = @"images\employee\employee-" + employee.Id;
                        string finalPath = Path.Combine(wwwRootPath, employeePath);

                        if (!Directory.Exists(finalPath))
                        {
                            Directory.CreateDirectory(finalPath);
                        }

                        using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        EmployeeImage employeeImage = new EmployeeImage()
                        {
                            ImageUrl = @"\" + employeePath + @"\" + fileName,
                            EmployeeId = employee.Id
                        };

                        await _unitOfWork.EmployeeImagee.AddAsync(employeeImage);
                    }

                    await _unitOfWork.SaveAsync();
                }

                TempData["success"] = "Empleado creado exitosamente";
                return RedirectToAction("Index", "Employees");
            }
            else
            {
                ViewBag.Employees = await _unitOfWork.Employee.GetAllAsync();
                ViewBag.EmployeeTypes = await _unitOfWork.EmployeeType.GetAllAsync();
                return View(employee);
            }
        }

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employeeList = (await _unitOfWork.Employee.GetAllAsync()).ToList();
            return Json(new { data = employeeList });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            var employeeToBeDeleted = await _unitOfWork.Employee.GetAsync(u => u.Id == id);

            if (employeeToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Employee.Remove(employeeToBeDeleted);
            await _unitOfWork.SaveAsync();
            return Json(new { success = true, message = "Delete Successful" });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteImage(int? id)
        {
            var employeeImage = await _unitOfWork.EmployeeImagee.GetAsync(u => u.Id == id);

            if (employeeImage == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.EmployeeImagee.Remove(employeeImage);
            await _unitOfWork.SaveAsync();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
