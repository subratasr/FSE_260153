using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using ProjectManager.Controllers;
using System.Collections.Generic;
using System.Web;
using ProjectManager.Models;
using System.Data.Entity;

namespace ProjectManager.Test
{
    class MockProjectManagerEntities : DAC.ProjectManagerEntities1
    {
        private DbSet<DAC.User> _users = null;
        private DbSet<DAC.Project> _projects = null;
        private DbSet<DAC.Task> _tasks = null;
        public override DbSet<DAC.User> Users
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
            }
        }

        public override DbSet<DAC.Project> Projects
        {
            get
            {
                return _projects;
            }
            set
            {
                _projects = value;
            }
        }

        public override DbSet<DAC.Task> Tasks
        {
            get
            {
                return _tasks;
            }
            set
            {
                _tasks = value;
            }
        }
    }

    [TestFixture]
    public class UserControllerTest
    {
        /// <summary>
        /// This Unit test methods test User Retrieval
        /// </summary>
        [Test]
        public void TestGetUser_Success()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "100000",
                First_Name = "User1FName",
                Last_Name = "User1LName",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "100001",
                First_Name = "User2FName",
                Last_Name = "User2LName",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.GetUser() as JSendResponse;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(List<User>),result.Data);
            Assert.AreEqual((result.Data as List<User>).Count, 2);
        }

       /* [Test]
        public void TestInsertUser_Success()
        {
            var context = new MockProjectManagerEntities();
            var user = new Models.User();
            user.FirstName = "User3FName";
            user.LastName = "User3Lname";
            user.EmployeeId = "100002";
            user.UserId = 123;
            var controller = new UserController(new BC.UserBC(context));
            var result = controller.InsertUserDetails(user) as JSendResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Data, 1);
        }*/

        [Test]
        public void TestUpdateUser_Success()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();

            users.Add(new DAC.User()
            {
                Employee_ID = "100001",
                First_Name = "User4FName",
                Last_Name = "User4LName",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.FirstName = "User5FName";
            user.LastName = "jaiUser5LNamen";
            user.EmployeeId = "100001";
            user.UserId = 503322;

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.UpdateUserDetails(user) as JSendResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual((context.Users.Local[0]).First_Name.ToUpper(), "User5FName".ToUpper());
        }

        [Test]
        public void TestDeleteUser_Success()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "100005",
                First_Name = "User5Fname",
                Last_Name = "User5Lname",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "100006",
                First_Name = "User6Fname",
                Last_Name = "User6Lname",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.FirstName = "User3Fname";
            user.LastName = "User3Lname";
            user.EmployeeId = "100002";
            user.UserId = 503322;

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.DeleteUserDetails(user) as JSendResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual(context.Users.Local.Count, 1);
        }

        [Test]       
        public void TestDeleteUser_UserNullException()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "100005",
                First_Name = "User5Fname",
                Last_Name = "User5Lname",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "100006",
                First_Name = "User6Fname",
                Last_Name = "User6Lname",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user = null;

            var controller = new UserController(new BC.UserBC(context));            
            Assert.That(() => controller.DeleteUserDetails(user),
               Throws.TypeOf<ArgumentNullException>());
        }

        [Test]       
        public void TestDeleteUser_InvalidEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "100005",
                First_Name = "User5Fname",
                Last_Name = "User5Lname",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "100006",
                First_Name = "User6Fname",
                Last_Name = "User6Lname",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            }); ;
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "TEST";

            var controller = new UserController(new BC.UserBC(context));           
            Assert.That(() => controller.DeleteUserDetails(user),
              Throws.TypeOf<FormatException>());
        }

        [Test]        
        public void TestDeleteUser_NegativeEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "100005",
                First_Name = "User5Fname",
                Last_Name = "User5Lname",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "100006",
                First_Name = "User6Fname",
                Last_Name = "User6Lname",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "-233";

            var controller = new UserController(new BC.UserBC(context));            
            Assert.That(() => controller.DeleteUserDetails(user),
             Throws.TypeOf<ArithmeticException>());
        }

        [Test]      
        public void TestDeleteUser_InvalidProjectIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "100005",
                First_Name = "User5Fname",
                Last_Name = "User5Lname",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "100006",
                First_Name = "User6Fname",
                Last_Name = "User6Lname",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.ProjectId = -1;

            var controller = new UserController(new BC.UserBC(context));           
            Assert.That(() => controller.DeleteUserDetails(user),
                Throws.TypeOf<ArithmeticException>());
        }

        [Test]       
        public void TestDeleteUser_NegativeUserIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "100005",
                First_Name = "User5Fname",
                Last_Name = "User5Lname",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "100006",
                First_Name = "User6Fname",
                Last_Name = "User6Lname",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.UserId = -1;

            var controller = new UserController(new BC.UserBC(context));          
            Assert.That(() => controller.DeleteUserDetails(user),
               Throws.TypeOf<ArithmeticException>());
        }

        [Test]        
        public void TestUpdateUser_UserNullException()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "100005",
                First_Name = "User5Fname",
                Last_Name = "User5Lname",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "100006",
                First_Name = "User6Fname",
                Last_Name = "User6Lname",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user = null;

            var controller = new UserController(new BC.UserBC(context));          
            Assert.That(() => controller.UpdateUserDetails(user),
             Throws.TypeOf<ArgumentNullException>());
        }

        [Test]       
        public void TestUpdateUser_InvalidEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "100005",
                First_Name = "User5Fname",
                Last_Name = "User5Lname",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "100006",
                First_Name = "User6Fname",
                Last_Name = "User6Lname",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "TEST";

            var controller = new UserController(new BC.UserBC(context));          
            Assert.That(() => controller.UpdateUserDetails(user),
             Throws.TypeOf<FormatException>());
        }

        [Test]       
        public void TestUpdateUser_NegativeEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "100005",
                First_Name = "User5Fname",
                Last_Name = "User5Lname",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "100006",
                First_Name = "User6Fname",
                Last_Name = "User6Lname",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "-233";

            var controller = new UserController(new BC.UserBC(context));          
            Assert.That(() => controller.UpdateUserDetails(user),
            Throws.TypeOf<ArithmeticException>());
        }

        [Test]     
        public void TestUpdateUser_InvalidProjectIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "100005",
                First_Name = "User5Fname",
                Last_Name = "User5Lname",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "100006",
                First_Name = "User6Fname",
                Last_Name = "User6Lname",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.ProjectId = -1;

            var controller = new UserController(new BC.UserBC(context));         
            Assert.That(() => controller.UpdateUserDetails(user),
                Throws.TypeOf<ArithmeticException>());
        }

        [Test]      
        public void TestUpdateUser_NegativeUserIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "100005",
                First_Name = "User5Fname",
                Last_Name = "User5Lname",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "100006",
                First_Name = "User6Fname",
                Last_Name = "User6Lname",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.UserId = -1;

            var controller = new UserController(new BC.UserBC(context));          
            Assert.That(() => controller.UpdateUserDetails(user),
              Throws.TypeOf<ArithmeticException>());
        }

        [Test]      
        public void TestInsertUser_UserNullException()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "100005",
                First_Name = "User5Fname",
                Last_Name = "User5Lname",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "100006",
                First_Name = "User6Fname",
                Last_Name = "User6Lname",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user = null;

            var controller = new UserController(new BC.UserBC(context));          
            Assert.That(() => controller.InsertUserDetails(user),
                     Throws.TypeOf<ArgumentNullException>());
        }

        [Test]       
        public void TestInsertUser_InvalidEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "100005",
                First_Name = "User5Fname",
                Last_Name = "User5Lname",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "100006",
                First_Name = "User6Fname",
                Last_Name = "User6Lname",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "TEST";

            var controller = new UserController(new BC.UserBC(context));           
            Assert.That(() => controller.InsertUserDetails(user),
                   Throws.TypeOf<FormatException>());
        }

        [Test]      
        public void TestInsertUser_NegativeEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "100005",
                First_Name = "User5Fname",
                Last_Name = "User5Lname",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "100006",
                First_Name = "User6Fname",
                Last_Name = "User6Lname",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "-233";

            var controller = new UserController(new BC.UserBC(context));       
            Assert.That(() => controller.InsertUserDetails(user),
                  Throws.TypeOf<ArithmeticException>());
        }

        [Test]      
        public void TestInsertUser_InvalidProjectIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "100005",
                First_Name = "User5Fname",
                Last_Name = "User5Lname",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "100006",
                First_Name = "User6Fname",
                Last_Name = "User6Lname",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.ProjectId = -1;

            var controller = new UserController(new BC.UserBC(context));           
            Assert.That(() => controller.InsertUserDetails(user),
                Throws.TypeOf<ArithmeticException>());
        }

    }
}

