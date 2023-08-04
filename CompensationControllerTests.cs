
using System;
using System.Net;
using System.Net.Http;
using System.Text;

using CodeChallenge.Models;

using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeChallenge.Tests.Integration
{

    [TestClass]
    public class CompensationControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void CreateCompensationByEmployeeID_ExistingEmployee_Returns_Created()
        {
            // Arrange
            var compensation = new Compensation()
            {
                employee = new Employee()
                {
                    EmployeeId = "03aa1462-ffa9-4978-901b-7c001562cf6f",
                    Department = "Engineering",
                    FirstName = "Pete",
                    LastName = "Best",
                    Position = "Developer VI"
                },
                salary = 1.23,
                effectiveDate = DateTime.Now                
            };

            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newComp = response.DeserializeContent<Compensation>();
            Assert.IsNotNull(newComp);
        }

        [TestMethod]
        public void GetCompensationByEmployeeID_Returns_Ok()
        {
            var compensation = new Compensation()
            {
                employee = new Employee()
                {
                    EmployeeId = "03aa1462-ffa9-4978-901b-7c001562cf6f",
                    Department = "Engineering",
                    FirstName = "Pete",
                    LastName = "Best",
                    Position = "Developer VI"
                },
                salary = 1.23,
                effectiveDate = DateTime.Now
            };

            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            // Arrange
            var employeeId = "03aa1462-ffa9-4978-901b-7c001562cf6f";

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{employeeId}");
            response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var comp = response.DeserializeContent<Compensation>();
            Assert.AreEqual(comp.employee.EmployeeId, employeeId);
        }
    }
}
