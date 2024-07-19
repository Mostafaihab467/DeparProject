namespace DeparProject.Models
{
    public class Department
    {

        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public Guid? ParentDepartmentId { get; set; } = null;
        public Department ParentDepartment { get; set; }
        public ICollection<Department> SubDepartments { get; set; }
    }
}
