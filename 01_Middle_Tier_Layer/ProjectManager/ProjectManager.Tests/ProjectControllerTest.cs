﻿using ProjectManager.Controllers;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ProjectManager.Test
{

    [TestFixture]
    public class ProjectControllerTest
    {
       /* [Test]
        public void TestGetProjects_Success()
        {
            var context = new MockProjectManagerEntities();
            var projects = new TestDbSet<DAC.Project>();
            projects.Add(new DAC.Project()
            {
                Project_ID = 1234,
                Project_Name = "MyProject",
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(5),
                Priority = 3
            });
            projects.Add(new DAC.Project()
            {
                Project_ID = 12345,
                Project_Name = "MyProject",
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(5),
                Priority = 3
            });
            context.Projects = projects;

            var controller = new ProjectController(new BC.ProjectBC(context));
            var result = controller.RetrieveProjects() as JSendResponse;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(List<Project>),result.Data);
            Assert.AreEqual((result.Data as List<Project>).Count, 2);
        }*/

        /*[Test]
        public void TestInsertProjects_Success()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "414942",
                First_Name = "Suvam",
                Last_Name = "Chowdhury",
                User_ID = 123,
                Task_ID = 123
            });
            context.Users = users;
            var testProject = new Models.Project()
            {
                ProjectId = 12345,
                ProjectName = "MyProject",
                ProjectStartDate = DateTime.Now,
                ProjectEndDate = DateTime.Now.AddDays(5),
                Priority = 3,
                NoOfCompletedTasks = 3,
                NoOfTasks = 5,
                User = new User()
                {
                    FirstName = "User4FName",
                    LastName = "User4LName",
                    EmployeeId = "100001",
                    UserId = 123
                }
            };
            var controller = new ProjectController(new BC.ProjectBC(context));
            var result = controller.InsertProjectDetails(testProject) as JSendResponse;

            Assert.IsNotNull(result);
            Assert.IsNotNull((context.Users.Local[0]).Project_ID);
        }*/

        [Test]
        public void TestUpdateProjects_Success()
        {
            var context = new MockProjectManagerEntities();
            var projects = new TestDbSet<DAC.Project>();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = 418220.ToString(),
                First_Name = "TEST",
                Last_Name = "TEST2",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 123
            });
            projects.Add(new DAC.Project()
            {
                Project_Name = "MyTestProject",
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(5),
                Priority = 2,
                Project_ID = 123
            });
            context.Projects = projects;
            context.Users = users;
            var testProject = new Models.Project()
            {
                ProjectId = 123,
                Priority = 3,
                NoOfCompletedTasks = 2,
                NoOfTasks = 5,
                ProjectName = "ProjectTest",
                ProjectStartDate = DateTime.Now,
                ProjectEndDate = DateTime.Now.AddDays(10),
                User = new User()
                {
                    EmployeeId = "100001",
                    FirstName = "User2FName",
                    LastName = "User2LName",
                    ProjectId = 123,
                    UserId = 123
                }
            };

            var controller = new ProjectController(new BC.ProjectBC(context));
            var result = controller.UpdateProjectDetails(testProject) as JSendResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual((context.Projects.Local[0]).Project_Name.ToUpper(), "PROJECTTEST");
        }

        [Test]
        public void TestDeleteProjects_Success()
        {
            var context = new MockProjectManagerEntities();
            var projects = new TestDbSet<DAC.Project>();
            projects.Add(new DAC.Project()
            {
                Project_ID = 123,
                Project_Name = "TEST",
                Priority = 1,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(5)
            });
            projects.Add(new DAC.Project()
            {
                Project_ID = 234,
                Project_Name = "TEST2",
                Priority = 2,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(10)
            });
            context.Projects = projects;
            var controller = new ProjectController(new BC.ProjectBC(context));

            var testProject = new Models.Project()
            {
                ProjectId = 123,
                Priority = 3,
                NoOfCompletedTasks = 2,
                NoOfTasks = 5,
                ProjectName = "ProjectTest",
                ProjectStartDate = DateTime.Now,
                ProjectEndDate = DateTime.Now.AddDays(10),
                User = new User()
                {
                    EmployeeId = "100001",
                    FirstName = "User2FName",
                    LastName = "User2LName",
                    ProjectId = 123,
                    UserId = 123
                }
            };

            var result = controller.DeleteProjectDetails(testProject) as JSendResponse;
            Assert.IsNotNull(result);
            Assert.AreEqual(context.Projects.Local.Count, 1);
        }

        [Test]        
        public void TestInsertProject_NoProjectAsParameter()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "100001",
                First_Name = "User4FName",
                Last_Name = "User4LName",
                User_ID = 123,
                Task_ID = 123
            });
            context.Users = users;
            Models.Project testProject = null;
            var controller = new ProjectController(new BC.ProjectBC(context));           
            Assert.That(() => controller.InsertProjectDetails(testProject),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]        
        public void TestInsertProject_NegativeProjectId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "100001",
                First_Name = "User4FName",
                Last_Name = "User4LName",
                User_ID = 123,
                Task_ID = 123
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = -234,
                NoOfCompletedTasks = 4,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "TEST",
                User = new User()
                {
                    EmployeeId = "100001",
                    FirstName = "User2FName",
                    LastName = "User2LName",
                    ProjectId = -234,
                    UserId = 123
                }
            };
            var controller = new ProjectController(new BC.ProjectBC(context));           
            Assert.That(() => controller.InsertProjectDetails(testProject),
                Throws.TypeOf<ArithmeticException>());
        }

        [Test]        
        public void TestInsertProject_UserNullInProject()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "100001",
                First_Name = "User4FName",
                Last_Name = "User4LName",
                User_ID = 123,
                Task_ID = 123
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = 222,
                NoOfCompletedTasks = 4,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "TEST",
                User = null
            };
            var controller = new ProjectController(new BC.ProjectBC(context));           
            Assert.That(() => controller.InsertProjectDetails(testProject),
               Throws.TypeOf<ArgumentNullException>());
        }

        [Test]      
        public void TestInsertProject_NegativeProjectIdInUser()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "100001",
                First_Name = "User4FName",
                Last_Name = "User4LName",
                User_ID = 123,
                Task_ID = 123
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = 234,
                NoOfCompletedTasks = 4,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "TEST",
                User = new User()
                {
                    EmployeeId = "100001",
                    FirstName = "User2FName",
                    LastName = "User2LName",
                    ProjectId = -234,
                    UserId = 123
                }
            };
            var controller = new ProjectController(new BC.ProjectBC(context));           
            Assert.That(() => controller.InsertProjectDetails(testProject),
              Throws.TypeOf<ArithmeticException>());
        }

        [Test]       
        public void TestInsertProject_CompletedTasksGreater()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                First_Name = "User4FName",
                Last_Name = "User4LName",
                Employee_ID = "100001",
                User_ID = 123,
                Task_ID = 123
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = 234,
                NoOfCompletedTasks = 10,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "TEST",
                User = new User()
                {
                    EmployeeId = "100001",
                    FirstName = "User2FName",
                    LastName = "User2LName",
                    ProjectId = 234,
                    UserId = 123
                }
            };
            var controller = new ProjectController(new BC.ProjectBC(context));           
            Assert.That(() => controller.InsertProjectDetails(testProject),
              Throws.TypeOf<ArgumentException>());
        }

        [Test]       
        public void TestUpdateProject_NoProjectAsParameter()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                First_Name = "User4FName",
                Last_Name = "User4LName",
                Employee_ID = "100001",
                User_ID = 123,
                Task_ID = 123
            });
            context.Users = users;
            Models.Project testProject = null;
            var controller = new ProjectController(new BC.ProjectBC(context));           
            Assert.That(() => controller.UpdateProjectDetails(testProject),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]       
        public void TestUpdateProject_NegativeProjectId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                First_Name = "User4FName",
                Last_Name = "User4LName",
                Employee_ID = "100001",
                User_ID = 123,
                Task_ID = 123
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = -234,
                NoOfCompletedTasks = 4,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "TEST",
                User = new User()
                {
                    EmployeeId = "100001",
                    FirstName = "User2FName",
                    LastName = "User2LName",
                    ProjectId = -234,
                    UserId = 123
                }
            };
            var controller = new ProjectController(new BC.ProjectBC(context));           
            Assert.That(() => controller.UpdateProjectDetails(testProject),
               Throws.TypeOf<ArithmeticException>());
        }

        [Test]        
        public void TestUpdateProject_UserNullInProject()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                First_Name = "User4FName",
                Last_Name = "User4LName",
                Employee_ID = "100001",
                User_ID = 123,
                Task_ID = 123
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = 222,
                NoOfCompletedTasks = 4,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "TEST",
                User = null
            };
            var controller = new ProjectController(new BC.ProjectBC(context));           
            Assert.That(() => controller.UpdateProjectDetails(testProject),
               Throws.TypeOf<ArgumentNullException>());
        }

        [Test]       
        public void TestUpdateProject_NegativeProjectIdInUser()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                First_Name = "User4FName",
                Last_Name = "User4LName",
                Employee_ID= "100001",
                User_ID = 123,
                Task_ID = 123
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = 234,
                NoOfCompletedTasks = 4,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "TEST",
                User = new User()
                {
                    EmployeeId = "100001",
                    FirstName = "User2FName",
                    LastName = "User2LName",
                    ProjectId = -234,
                    UserId = 123
                }
            };
            var controller = new ProjectController(new BC.ProjectBC(context));          
            Assert.That(() => controller.UpdateProjectDetails(testProject),
              Throws.TypeOf<ArithmeticException>());
        }

        [Test]       
        public void TestUpdateProject_CompletedTasksGreater()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                First_Name = "User4FName",
                Last_Name = "User4LName",
                Employee_ID = "100001",
                User_ID = 123,
                Task_ID = 123
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = 234,
                NoOfCompletedTasks = 10,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "TEST",
                User = new User()
                {
                    EmployeeId = "100001",
                    FirstName = "User2FName",
                    LastName = "User2LName",
                    ProjectId = 234,
                    UserId = 123
                }
            };
            var controller = new ProjectController(new BC.ProjectBC(context));          
            Assert.That(() => controller.UpdateProjectDetails(testProject),
            Throws.TypeOf<ArgumentException>());
        }

        [Test]       
        public void TestDeleteProject_NoProjectAsParameter()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                First_Name = "User4FName",
                Last_Name = "User4LName",
                Employee_ID = "100001",
                User_ID = 123,
                Task_ID = 123
            });
            context.Users = users;
            Models.Project testProject = null;
            var controller = new ProjectController(new BC.ProjectBC(context));           
            Assert.That(() => controller.DeleteProjectDetails(testProject),
                    Throws.TypeOf<ArgumentNullException>());
        }

        [Test]       
        public void TestDeleteProject_NegativeProjectId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                First_Name = "User4FName",
                Last_Name = "User4LName",
                Employee_ID = "100001",
                User_ID = 123,
                Task_ID = 123
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = -234,
                NoOfCompletedTasks = 4,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "TEST",
                User = new User()
                {
                    EmployeeId = "100001",
                    FirstName = "User2FName",
                    LastName = "User2LName",
                    ProjectId = -234,
                    UserId = 123
                }
            };
            var controller = new ProjectController(new BC.ProjectBC(context));          
            Assert.That(() => controller.DeleteProjectDetails(testProject),
                   Throws.TypeOf<ArithmeticException>());
        }

        [Test]     
        public void TestDeleteProject_UserNullInProject()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                First_Name = "User4FName",
                Last_Name = "User4LName",
                Employee_ID = "100001",
                User_ID = 123,
                Task_ID = 123
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = 222,
                NoOfCompletedTasks = 4,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "TEST",
                User = null
            };
            var controller = new ProjectController(new BC.ProjectBC(context));           
            Assert.That(() => controller.DeleteProjectDetails(testProject),
                  Throws.TypeOf<ArgumentNullException>());
        }

        [Test]       
        public void TestDeleteProject_NegativeProjectIdInUser()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                First_Name = "User4FName",
                Last_Name = "User4LName",
                Employee_ID = "100001",
                User_ID = 123,
                Task_ID = 123
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = 234,
                NoOfCompletedTasks = 4,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "TEST",
                User = new User()
                {
                    EmployeeId = "100001",
                    FirstName = "User2FName",
                    LastName = "User2LName",
                    ProjectId = -234,
                    UserId = 123
                }
            };
            var controller = new ProjectController(new BC.ProjectBC(context));         
            Assert.That(() => controller.DeleteProjectDetails(testProject),
                Throws.TypeOf<ArithmeticException>());
        }

        [Test]       
        public void TestDeleteProject_CompletedTasksGreater()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                First_Name = "User4FName",
                Last_Name = "User4LName",
                Employee_ID = "100001",
                User_ID = 123,
                Task_ID = 123
            });
            context.Users = users;
            Models.Project testProject = new Models.Project()
            {
                ProjectId = 234,
                NoOfCompletedTasks = 10,
                NoOfTasks = 5,
                Priority = 1,
                ProjectEndDate = DateTime.Now.AddDays(10),
                ProjectStartDate = DateTime.Now,
                ProjectName = "TEST",
                User = new User()
                {
                    EmployeeId = "100001",
                    FirstName = "User2FName",
                    LastName = "User2LName",
                    ProjectId = 234,
                    UserId = 123
                }
            };
            var controller = new ProjectController(new BC.ProjectBC(context));         
            Assert.That(() => controller.DeleteProjectDetails(testProject),
                Throws.TypeOf<ArgumentException>());
        }
    }
}
