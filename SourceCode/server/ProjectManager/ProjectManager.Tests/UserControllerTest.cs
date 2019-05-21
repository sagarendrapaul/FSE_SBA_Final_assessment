using System;

using ProjectManager.Controllers;
using System.Collections.Generic;
using System.Web;
using ProjectManager.Models;
using System.Data.Entity;
using NUnit.Framework;

namespace ProjectManager.Test
{
    class MockProjectManagerEntities : DAC.ProjectManagerContainer
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
        [Test]
        public void TestGetUser_Success()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "418220",
                First_Name = "Sagar",
                Last_Name = "Paul",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "503322",
                First_Name = "Raj",
                Last_Name = "Aryan",
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

        
        public void TestInsertUser_Success()
        {
            var context = new MockProjectManagerEntities();
            var user = new Models.User();
            user.FirstName = "Sagar";
            user.LastName = "Paul";
            user.EmployeeId = "123456";
            user.UserId = 123;
            var controller = new UserController(new BC.UserBC(context));
            var result = controller.InsertUserDetails(user) as JSendResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Data, 1);
        }

        [Test]
        public void TestUpdateUser_Success()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();

            users.Add(new DAC.User()
            {
                Employee_ID = "503322",
                First_Name = "Rajan",
                Last_Name = "Aryan",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.FirstName = "Raj";
            user.LastName = "Aryan";
            user.EmployeeId = "123";
            user.UserId = 503322;

            var controller = new UserController(new BC.UserBC(context));
            var result = controller.UpdateUserDetails(user) as JSendResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual((context.Users.Local[0]).First_Name.ToUpper(), "RAJ");
        }

        [Test]
        public void TestDeleteUser_Success()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "418220",
                First_Name = "Sagar",
                Last_Name = "Paul",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "503322",
                First_Name = "ABC",
                Last_Name = "XYZ",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.FirstName = "ABC";
            user.LastName = "XYZ";
            user.EmployeeId = "503322";
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
                Employee_ID = "418229",
                First_Name = "Sagar",
                Last_Name = "Paul",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "503328",
                First_Name = "Raj",
                Last_Name = "Aryan",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user = null;

            var controller = new UserController(new BC.UserBC(context));
            var ex = Assert.Throws<ArgumentNullException>(() => controller.DeleteUserDetails(user));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestDeleteUser_InvalidEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "418220",
                First_Name = "Sagar",
                Last_Name = "Paul",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "503322",
                First_Name = "Raj",
                Last_Name = "Aryan",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "TEST";

            var controller = new UserController(new BC.UserBC(context));
            var ex = Assert.Throws<FormatException>(() => controller.DeleteUserDetails(user));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestDeleteUser_NegativeEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "418220",
                First_Name = "Sagar",
                Last_Name = "Paul",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "503322",
                First_Name = "Raj",
                Last_Name = "Aryan",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "-233";

            var controller = new UserController(new BC.UserBC(context));
            var ex = Assert.Throws<ArithmeticException>(() => controller.DeleteUserDetails(user));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestDeleteUser_InvalidProjectIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "418220",
                First_Name = "Sagar",
                Last_Name = "Paul",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "503322",
                First_Name = "Raj",
                Last_Name = "Aryan",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.ProjectId = -1;

            var controller = new UserController(new BC.UserBC(context));
            var ex = Assert.Throws<ArithmeticException>(() => controller.DeleteUserDetails(user));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestDeleteUser_NegativeUserIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "418220",
                First_Name = "Sagar",
                Last_Name = "Paul",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "503322",
                First_Name = "Raj",
                Last_Name = "Aryan",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.UserId = -1;

            var controller = new UserController(new BC.UserBC(context));
            var ex = Assert.Throws<ArithmeticException>(() => controller.DeleteUserDetails(user));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestUpdateUser_UserNullException()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "418220",
                First_Name = "Sagar",
                Last_Name = "Paul",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "503322",
                First_Name = "Raj",
                Last_Name = "Aryan",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user = null;

            var controller = new UserController(new BC.UserBC(context));
            var ex = Assert.Throws<ArgumentNullException>(() => controller.UpdateUserDetails(user));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestUpdateUser_InvalidEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "418220",
                First_Name = "Sagar",
                Last_Name = "Paul",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "503322",
                First_Name = "Raj",
                Last_Name = "Aryan",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "TEST";

            var controller = new UserController(new BC.UserBC(context));
            var ex = Assert.Throws<FormatException>(() => controller.UpdateUserDetails(user));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestUpdateUser_NegativeEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "418220",
                First_Name = "Sagar",
                Last_Name = "Paul",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "503322",
                First_Name = "Raj",
                Last_Name = "Aryan",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "-233";

            var controller = new UserController(new BC.UserBC(context));
            var ex = Assert.Throws<ArithmeticException>(() => controller.UpdateUserDetails(user));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestUpdateUser_InvalidProjectIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "418220",
                First_Name = "Sagar",
                Last_Name = "Paul",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "503322",
                First_Name = "Raj",
                Last_Name = "Aryan",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.ProjectId = -1;

            var controller = new UserController(new BC.UserBC(context));
            var ex = Assert.Throws<ArithmeticException>(() => controller.UpdateUserDetails(user));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestUpdateUser_NegativeUserIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "418220",
                First_Name = "Sagar",
                Last_Name = "Paul",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "503322",
                First_Name = "Raj",
                Last_Name = "Aryan",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.UserId = -1;

            var controller = new UserController(new BC.UserBC(context));
            var ex = Assert.Throws<ArithmeticException>(() => controller.UpdateUserDetails(user));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestInsertUser_UserNullException()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "418220",
                First_Name = "Sagar",
                Last_Name = "Paul",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "503322",
                First_Name = "Raj",
                Last_Name = "Aryan",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user = null;

            var controller = new UserController(new BC.UserBC(context));
            var ex = Assert.Throws<ArgumentNullException>(() => controller.InsertUserDetails(user));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestInsertUser_InvalidEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "418220",
                First_Name = "Sagar",
                Last_Name = "Paul",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "503322",
                First_Name = "Raj",
                Last_Name = "Aryan",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "TEST";

            var controller = new UserController(new BC.UserBC(context));
            var ex = Assert.Throws<FormatException>(() => controller.InsertUserDetails(user));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestInsertUser_NegativeEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "418220",
                First_Name = "Sagar",
                Last_Name = "Paul",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "503322",
                First_Name = "Raj",
                Last_Name = "Aryan",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.EmployeeId = "-233";

            var controller = new UserController(new BC.UserBC(context));
            var ex = Assert.Throws<ArithmeticException>(() => controller.InsertUserDetails(user));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestInsertUser_InvalidProjectIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "418220",
                First_Name = "Sagar",
                Last_Name = "Paul",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 418220
            });
            users.Add(new DAC.User()
            {
                Employee_ID = "503322",
                First_Name = "Raj",
                Last_Name = "Aryan",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 503322
            });
            context.Users = users;

            var user = new Models.User();
            user.ProjectId = -1;

            var controller = new UserController(new BC.UserBC(context));
            var ex = Assert.Throws<ArithmeticException>(() => controller.InsertUserDetails(user));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

    }
}

