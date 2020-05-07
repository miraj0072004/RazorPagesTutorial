using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RazorPagesTutorial.Models;

namespace RazorPagesTutorial.Services
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;
        public IEnumerable<Employee> GetAllEmployees()
        {
            return this._employeeList;
        }

        public Employee GetEmployee(int id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == id);
        }

        public Employee Update(Employee updatedEmployee)
        {
            Employee employee = _employeeList
                .FirstOrDefault(e => e.Id == updatedEmployee.Id);
            if (employee != null)
            {
                employee.Name = updatedEmployee.Name;
                employee.Email = updatedEmployee.Email;
                employee.Department = updatedEmployee.Department;
                employee.PhotoPath = updatedEmployee.PhotoPath;
            }
            return employee;
        }

        public MockEmployeeRepository()
        {
            _employeeList= new List<Employee>()
            {
                new Employee(){Id=1,Name="Miraj",Email="miraj0072004@gmail.com",Department=Dept.HR,PhotoPath = "miraj.jpg"},
                new Employee(){Id=2,Name="Nadia",Email="nadia0072004@gmail.com",Department=Dept.IT,PhotoPath = "nadia.jpg"},
                new Employee(){Id=3,Name="Rizna",Email="rizna0072004@gmail.com",Department=Dept.Payroll,PhotoPath = "rizna.jpg"},
                new Employee(){Id=4,Name="Shifna",Email="shifna0072004@gmail.com",Department=Dept.None}
            };
        }

        public Employee Add(Employee newEmployee)
        {
            newEmployee.Id = _employeeList.Max(e => e.Id) + 1;
            _employeeList.Add(newEmployee);
            return newEmployee;
        }

        public Employee Delete(int id)
        {
            var employeeToDelete = _employeeList.FirstOrDefault(e => e.Id == id);

            if (employeeToDelete != null)
            {
                _employeeList.Remove(employeeToDelete);
            }

            return employeeToDelete;
        }

        public IEnumerable<DeptHeadCount> EmployeeCountByDept(Dept? dept)
        {
            IEnumerable<Employee> query = _employeeList;

            if (dept.HasValue)
            {
                query = query.Where(e => e.Department == dept.Value);
            }

            return query.GroupBy(e => e.Department)
                .Select(g => new DeptHeadCount()
                {
                    Department = g.Key.Value,
                    Count = g.Count()
                }).ToList();
        }

        public IEnumerable<Employee> Search(string searchTerm=null)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return _employeeList;

            return _employeeList.Where(e => e.Name.Contains(searchTerm) || e.Email.Contains(searchTerm)).ToList();
        }
    }
}
