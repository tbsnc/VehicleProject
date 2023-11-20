using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleProject.Common.DTOs;
using VehicleProject.Model;

namespace VehicleProject.UnitTest.ClassData
{
    public class VehicleMakeClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new VehicleMakeDTO
                {
                    Name = "Test",
                    Abrv = "Test"
                },
              

               
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
