using Dodo.Tools.Types;

namespace EFCoreInMemoryProviderBug.Entities
{
    public class Unit
    {
        private Unit()
        {
            
        }
        
        public Unit(UUId id, UUId departmentId, Department department)
        {
            Id = id;
            DepartmentId = departmentId;
            Department = department;
        }

        public UUId Id { get; }

        public UUId DepartmentId { get; }
        public Department Department { get; init; }
    }
}