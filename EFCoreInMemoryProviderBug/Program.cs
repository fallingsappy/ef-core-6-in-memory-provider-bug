using System;
using System.Threading.Tasks;
using Dodo.Tools.Types;
using EFCoreInMemoryProviderBug.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreInMemoryProviderBug
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            RatingContext context = new();

            var department = new Department(UUId.NewUUId());

            var unit = new Unit(
                UUId.NewUUId(),
                department.Id,
                department
            );

            context.Add(department);
            context.Add(unit);
            await context.SaveChangesAsync();

            try
            {
                var retrievedUnit = await context.Units.FirstOrDefaultAsync(u => u.Id == unit.Id);

                if (retrievedUnit != null)
                {
                    Console.WriteLine(retrievedUnit.Id);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadLine();
        }
    }
}