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
    public class ReportingStructureControllerTests
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
        public void GetReportingStructure_Returns_NotFound()
        {
            // Arrange
            var employeeID = "Invalid_Id";
            var requestContent = new JsonSerialization().ToJson(employeeID);

            // Execute
            var postRequestTask = _httpClient.GetAsync($"api/reportingstructure/{employeeID}");
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetReportingStructure_Returns_Ok()
        {
            // Arrange
            var employeeID = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            var requestContent = new JsonSerialization().ToJson(employeeID);

            // Execute
            var postRequestTask = _httpClient.GetAsync($"api/reportingstructure/{employeeID}");
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetReportingStructure_Returns_CorrectStructure()
        {
            // Arrange
            var employeeID = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            var requestContent = new JsonSerialization().ToJson(employeeID);

            // Execute
            var postRequestTask = _httpClient.GetAsync($"api/reportingstructure/{employeeID}");
            var response = postRequestTask.Result;

            // Assert
            var reportingStructure = response.DeserializeContent<ReportingStructure>();

            Assert.AreEqual(reportingStructure.employee.EmployeeId, employeeID);
        }

        [TestMethod]
        public void GetReportingStructure_Returns_CorrectReportsCount()
        {
            // Arrange
            var employeeID = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            var expectedReportsCount = 4;

            var requestContent = new JsonSerialization().ToJson(employeeID);

            // Execute
            var postRequestTask = _httpClient.GetAsync($"api/reportingstructure/{employeeID}");
            var response = postRequestTask.Result;

            // Assert
            var reportingStructure = response.DeserializeContent<ReportingStructure>();

            Assert.AreEqual(reportingStructure.numberOfReports, expectedReportsCount);
        }
    }
}
