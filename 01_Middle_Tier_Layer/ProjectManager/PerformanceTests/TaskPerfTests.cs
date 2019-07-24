using NUnit.Framework;
using NBench;
using ProjectManager.Controllers;

namespace PerformanceTests
{
    [TestFixture]
    public class TaskPerfTests
    {
        [PerfBenchmark(NumberOfIterations = 5, RunMode = RunMode.Throughput,
        TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 2000)]
        public void PerformanceTestRetrieveProjects()
        {
            // Set up Prerequisites   
            var controller = new ProjectController();
            // Act on Test  
            var response = controller.RetrieveProjects();
            // Assert the result  
            Assert.IsTrue(response != null);

        }

        [PerfBenchmark(NumberOfIterations = 5, RunMode = RunMode.Throughput,
        TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 2000)]
        public void PerformanceTestCreateProject()
        {
            // Set up Prerequisites   
            var controller = new ProjectController();
            // Act on Test  
            ProjectManager.Models.Project testProject = new ProjectManager.Models.Project()
            {
                
                NoOfCompletedTasks = 1,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = new System.DateTime(2019, 7, 12),
                ProjectStartDate = new System.DateTime(2019, 5, 9),
                ProjectName = "TestDevelopmentProject#2",
                User = new ProjectManager.Models.User()
                {
                    EmployeeId = "100001",
                    FirstName = "User2FName",
                    LastName = "User2LName",
                    ProjectId = 234,
                    UserId = 1
                }
            };
            var response = controller.InsertProjectDetails(testProject);
            // Assert the result  
            Assert.IsTrue(response != null);
        }
    }
}
