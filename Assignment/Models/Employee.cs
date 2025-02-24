namespace Assignment.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        public Employee(string employeeName)
        {
            EmployeeName = employeeName;
        }
    }
}
