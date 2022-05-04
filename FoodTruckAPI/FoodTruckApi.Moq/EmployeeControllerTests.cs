using FoodTruckAPI.ClassLibrary.DataAccess;
using Microsoft.Build.Framework;
using Moq;
using Xunit;

namespace FoodTruckApi.Moq
{
    public class EmployeeControllerTests
    {
        [Fact]
        public async Task EmpController_GetAll_GetsAllEmployees()
        {
            //Arrange
            Mock < ILogger > = new();
            Mock < FoodTruckContext >= new();
        }
    }
}