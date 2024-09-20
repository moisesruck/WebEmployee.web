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
        public IActionResult Index(List<Employee> employees)
        {

            return View();
        }


        [HttpPost]
        public IActionResult Index(IFormFile file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;

            #region Upload CSV
            //string fileNamee = Path.Combine(wwwRootPath, @"files", file.FileName);
            string filepath = Path.Combine(wwwRootPath, @"files");

            if (!string.IsNullOrEmpty(file.FileName))
            {
                //in this case we need to delete old image
                //remove the backward slash, it has to backward slashes, one is to scape. 
                var oldImagePath = Path.Combine(filepath, file.FileName.TrimStart('\\'));

                //if anything exists in that image path, it will be deleted
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }


            //copy file to the location configured
            //updload
            using (var fileStream = new FileStream(Path.Combine(filepath, file.FileName), FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            #endregion

            var employees = this.GetEmployeeList(file.FileName);
            return Index(employees);
        }

        private List<Employee> GetEmployeeList(string filename)
        {
            List<Employee> employeess = new List<Employee>();

            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string filepath = Path.Combine(wwwRootPath, @"files");
            #region Read CSV
            var path = Path.Combine(filepath, filename);

            using (var reader = new StreamReader(path))
            using (var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var medicalcondition = csv.GetRecord<Employee>();
                    medicalcondition.Id = 0;
                    employeess.Add(medicalcondition);
                    _unitOfWork.Employee.Add(medicalcondition);
                }
            }
            _unitOfWork.Save();
            TempData["success"] = "Employee creado exitosamente";

            #endregion

            return employeess;
        }

        public IActionResult Upsert(int? id)
        {
            Employee employee = new();





            if (id == null || id == 0)
            {
                //Create

                return View(employee);
            }
            else
            {
                //update
                employee = _unitOfWork.Employee.Get(u => u.Id == id, includeProperties: "EmployeeImages");

                return View(employee);
            }

        }

        [HttpPost]
        public IActionResult Upsert(Employee employe, List<IFormFile> filess)
        {
            if (ModelState.IsValid)
            {
                employe.CreationTime = DateTime.Now;

                if (employe.Id == 0)
                {
                    _unitOfWork.Employee.Add(employe);
                }
                else
                {
                    _unitOfWork.Employee.Update(employe);
                }

                //_unitOfWork.Productt.Add(productVM.Product);
                _unitOfWork.Save();


                string wwwRootPathh = _webHostEnvironment.WebRootPath;

                if (filess != null)
                {
                    foreach (IFormFile file in filess)
                    {
                        //configure location
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string employeepath = @"images\employee\employee-" + employe.Id;
                        string finalPath = Path.Combine(wwwRootPathh, employeepath);

                        //create location if it does not exist
                        if (!Directory.Exists(finalPath))
                        {
                            Directory.CreateDirectory(finalPath);
                        }

                        //upload file to the location configured
                        using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        //create a ptientimage with the info of the image just uploaded
                        EmployeeImage EmployeeImage = new EmployeeImage()
                        {
                            ImageUrl = @"\" + employeepath + @"\" + fileName,
                            EmployeeId = employe.Id
                        };

                        ////Confirm Pacient images is not null, if it is, initialize it
                        //if (employe.EmployeeImages == null)
                        //{
                        //    employe.EmployeeImages = new List<EmployeeImage>();
                        //}
                        _unitOfWork.EmployeeImagee.Add(EmployeeImage);
                        _unitOfWork.Save();

                    }
                }
                TempData["success"] = "Empleado public creado exitosamente";
                return RedirectToAction("Index", "Employees");
            }
            else
            {
                return View(employe);
            }
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Employee> objCategoryList = _unitOfWork.Employee.GetAll().ToList();
            //List<Employee> employeeList = _unitOfWork.Employee.GetAll(includeProperties: "Position").ToList();
            return Json(new { data = objCategoryList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            var productToBeDeleted = _unitOfWork.Employee.Get(u => u.Id == id);

            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }


            _unitOfWork.Employee.Remove(productToBeDeleted);
            _unitOfWork.Save();

            List<Employee> objCategoryList = _unitOfWork.Employee.GetAll().ToList();
            return Json(new { success = true, message = "Delete Successful" });
        }
        [HttpDelete]
        public IActionResult DeleteImage(int? id)
        {

            var employeeImage = _unitOfWork.EmployeeImagee.Get(u => u.Id == id);

            if (employeeImage == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.EmployeeImagee.Remove(employeeImage);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
