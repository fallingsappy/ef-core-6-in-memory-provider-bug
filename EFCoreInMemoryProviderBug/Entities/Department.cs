using Dodo.Tools.Types;

namespace EFCoreInMemoryProviderBug.Entities
{
    public class Department
    {
        private Department()
        {
            
        }
        
        public Department(UUId id)
        {
            Id = id;
        }

        public UUId Id { get; }
    }
}