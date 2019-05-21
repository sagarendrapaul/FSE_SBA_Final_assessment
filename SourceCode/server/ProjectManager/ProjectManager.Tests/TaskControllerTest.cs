
using NUnit.Framework;
using ProjectManager.Controllers;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Test
{

    [TestFixture]
    public class TaskControllerTest
    {
        [Test]
        public void TestRetrieveTasks_Success()
        {
            var context = new MockProjectManagerEntities();
            var tasks = new TestDbSet<DAC.Task>();
            var users = new TestDbSet<DAC.User>();
            var parentTasks = new TestDbSet<DAC.ParentTask>();

            parentTasks.Add(new DAC.ParentTask()
            {
                Parent_ID = 123456,
                Parent_Task_Name = "PNB"

            });
            context.ParentTasks = parentTasks;
            users.Add(new DAC.User()
            {
                Employee_ID = "485088",
                First_Name = "Raj",
                Last_Name = "Aryan",
                User_ID = 123,
                Task_ID = 1
            });
            context.Users = users;
            int projectid = 1234;
            tasks.Add(new DAC.Task()
            {
                Task_ID = 1,
                Task_Name = "MYTASK",
                Parent_ID = 123456,
                Project_ID = 1234,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(2),
                Priority = 10,
                Status = 0

            });
            context.Tasks = tasks;
            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.RetrieveTaskByProjectId(projectid) as JSendResponse;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(List<ProjectManager.Models.Task>),result.Data);
        }

        [Test]
        public void TestRetrieveParentTasks_Success()
        {
            var context = new MockProjectManagerEntities();
            var parentTasks = new TestDbSet<DAC.ParentTask>();
            parentTasks.Add(new DAC.ParentTask()
            {
                Parent_ID = 12345,
                Parent_Task_Name = "PTASK"

            });
            parentTasks.Add(new DAC.ParentTask()
            {
                Parent_ID = 123456,
                Parent_Task_Name = "MYTASK"

            });
            context.ParentTasks = parentTasks;

            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.RetrieveParentTasks() as JSendResponse;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(List<ProjectManager.Models.ParentTask>),result.Data);
            Assert.AreEqual((result.Data as List<ParentTask>).Count, 2);
        }

        
        public void TestInsertTasks_Success()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<DAC.User>();
            users.Add(new DAC.User()
            {
                Employee_ID = "485088",
                First_Name = "Raj",
                Last_Name = "Aryan",
                User_ID = 123,
                Task_ID = 123
            });
            context.Users = users;
            var task = new ProjectManager.Models.Task()
            {

                Task_Name = "ASDQW",
                Parent_ID = 123674,
                Project_ID = 34856,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(2),
                Priority = 10,
                Status = 0,
                User = new User()
                {
                    FirstName = "Sagar",
                    LastName = "ChowdPaulhury",
                    EmployeeId = "123456",
                    UserId = 123
                }
            };

            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.InsertTaskDetails(task) as JSendResponse;


            Assert.IsNotNull(result);

            Assert.IsNotNull((context.Users.Local[0]).Task_ID);
        }

        [Test]
        public void TestUpdateProjects_Success()
        {
            var context = new MockProjectManagerEntities();
            var tasks = new TestDbSet<DAC.Task>();
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
            tasks.Add(new DAC.Task()
            {
                Task_ID = 1,
                Task_Name = "ASDQW",
                Parent_ID = 123674,
                Project_ID = 34856,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(2),
                Priority = 10,
                Status = 0
            });
            context.Tasks = tasks;
            context.Users = users;
            var testTask = new Models.Task()
            {
                TaskId = 1,
                Task_Name = "task1",
                Parent_ID = 123674,
                Project_ID = 34856,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(2),
                Priority = 30,
                Status = 0,
                User = new User()
                {
                    FirstName = "Raj",
                    LastName = "Aryan",
                    EmployeeId = "123456",
                    UserId = 123
                }
            };

            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.UpdateTaskDetails(testTask) as JSendResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual((context.Tasks.Local[0]).Priority, 30);
        }

        [Test]
        public void TestDeleteProjects_Success()
        {
            var context = new MockProjectManagerEntities();
            var tasks = new TestDbSet<DAC.Task>();

            tasks.Add(new DAC.Task()
            {
                Task_ID = 1,
                Task_Name = "task1",
                Parent_ID = 123674,
                Project_ID = 34856,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(2),
                Priority = 10,
                Status = 0
            });
            context.Tasks = tasks;
            var testTask = new Models.Task()
            {
                TaskId = 1,
                Task_Name = "task1",
                Parent_ID = 123674,
                Project_ID = 34856,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(2),
                Priority = 10,
                Status = 0
            };

            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.DeleteTaskDetails(testTask) as JSendResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual((context.Tasks.Local[0]).Status, 1);
        }

        [Test]
        public void TestRetrieveTaskByProjectId_Success()
        {
            var context = new MockProjectManagerEntities();
            var tasks = new TestDbSet<DAC.Task>();
            var users = new TestDbSet<DAC.User>();
            var parentTasks = new TestDbSet<DAC.ParentTask>();
            parentTasks.Add(new DAC.ParentTask()
            {
                Parent_ID = 12345,
                Parent_Task_Name = "ANB"

            });
            context.ParentTasks = parentTasks;
            users.Add(new DAC.User()
            {
                Employee_ID = "414843",
                First_Name = "Raj",
                Last_Name = "Aryan",
                User_ID = 123,
                Task_ID = 12345,
                Project_ID = 1234
            });
            context.Users = users;
            tasks.Add(new DAC.Task()
            {
                Project_ID = 12345,
                Parent_ID = 12345,
                Task_ID = 12345,
                Task_Name = "TEST",
                Priority = 1,
                Status = 1,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(5)
            });
            tasks.Add(new DAC.Task()
            {
                Project_ID = 123,
                Parent_ID = 123,
                Task_ID = 123,
                Task_Name = "TEST",
                Priority = 1,
                Status = 1,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(5)
            });
            context.Tasks = tasks;

            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.RetrieveTaskByProjectId(12345) as JSendResponse;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(List<ProjectManager.Models.Task>),result.Data);
            Assert.AreEqual((result.Data as List<ProjectManager.Models.Task>).Count, 1);
            Assert.AreEqual((result.Data as List<ProjectManager.Models.Task>)[0].Task_Name, "TEST");
        }

        [Test]
        
        public void TestRetrieveTaskByProjectId_NegativeTaskId()
        {
            int projectId = -12345;
            var context = new MockProjectManagerEntities();

            var controller = new TaskController(new BC.TaskBC(context));
            var ex = Assert.Throws<ArithmeticException>(() => controller.RetrieveTaskByProjectId(projectId));
            Assert.That(ex.Message, Is.Not.Null);
            
        }


        [Test]
        
        public void TestInsertTask_NullTaskObject()
        {
            Models.Task taskDetails = null;
            var context = new MockProjectManagerEntities();

            var controller = new TaskController(new BC.TaskBC(context));
            var ex = Assert.Throws<ArgumentNullException>(() => controller.InsertTaskDetails(taskDetails));
            Assert.That(ex.Message, Is.Not.Null);
            
        }


        [Test]
        
        public void TestInsertTask_NegativeTaskParentId()
        {
            var context = new MockProjectManagerEntities();
            ProjectManager.Models.Task task = new Models.Task();
            task.Parent_ID = -236;
            var controller = new TaskController(new BC.TaskBC(context));
            var ex = Assert.Throws<ArithmeticException>(() => controller.InsertTaskDetails(task));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestInsertTask_NegativeProjectId()
        {
            var context = new MockProjectManagerEntities();
            ProjectManager.Models.Task task = new Models.Task();
            task.Project_ID = -234;
            var controller = new TaskController(new BC.TaskBC(context));
            var ex = Assert.Throws<ArithmeticException>(() => controller.InsertTaskDetails(task));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestInsertTask_NegativeTaskId()
        {
            var context = new MockProjectManagerEntities();
            ProjectManager.Models.Task task = new Models.Task();
            task.TaskId = -234;
            var controller = new TaskController(new BC.TaskBC(context));
            var ex = Assert.Throws<ArithmeticException>(() => controller.InsertTaskDetails(task));
            Assert.That(ex.Message, Is.Not.Null);
            
        }




        [Test]
        
        public void TestUpdateTask_NullTaskObject()
        {
            var context = new MockProjectManagerEntities();
            Models.Task taskDetails = null;
            var controller = new TaskController(new BC.TaskBC(context));
            var ex = Assert.Throws<ArgumentNullException>(() => controller.UpdateTaskDetails(taskDetails));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestUpdateTask_NegativeTaskParentId()
        {
            var context = new MockProjectManagerEntities();
            ProjectManager.Models.Task task = new Models.Task();
            task.Parent_ID = -234;
            var controller = new TaskController(new BC.TaskBC(context));
            var ex = Assert.Throws<ArithmeticException>(() => controller.UpdateTaskDetails(task));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestUpdateTask_NegativeProjectId()
        {
            var context = new MockProjectManagerEntities();
            ProjectManager.Models.Task task = new Models.Task();
            task.Project_ID = -234;
            var controller = new TaskController(new BC.TaskBC(context));
            var ex = Assert.Throws<ArithmeticException>(() => controller.UpdateTaskDetails(task));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestUpdateTask_NegativeTaskId()
        {
            var context = new MockProjectManagerEntities();
            ProjectManager.Models.Task task = new Models.Task();
            task.TaskId = -234;
            var controller = new TaskController(new BC.TaskBC(context));
            var ex = Assert.Throws<ArithmeticException>(() => controller.UpdateTaskDetails(task));
            Assert.That(ex.Message, Is.Not.Null);
            
        }




        [Test]
        
        public void TestDeleteTask_NullTaskObject()
        {
            var context = new MockProjectManagerEntities();
            Models.Task task = null;
            var controller = new TaskController(new BC.TaskBC(context));
            var ex = Assert.Throws<ArgumentNullException>(() => controller.DeleteTaskDetails(task));
            Assert.That(ex.Message, Is.Not.Null);
            
        }



        [Test]
        
        public void TestDeleteTask_NegativeTaskParentId()
        {
            var context = new MockProjectManagerEntities();
            ProjectManager.Models.Task task = new Models.Task();
            task.Parent_ID = -234;
            var controller = new TaskController(new BC.TaskBC(context));
            var ex = Assert.Throws<ArithmeticException>(() => controller.DeleteTaskDetails(task));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestDeleteTask_NegativeProjectId()
        {
            var context = new MockProjectManagerEntities();
            ProjectManager.Models.Task task = new Models.Task();
            task.Project_ID = -234;
            var controller = new TaskController(new BC.TaskBC(context));
            var ex = Assert.Throws<ArithmeticException>(() => controller.DeleteTaskDetails(task));
            Assert.That(ex.Message, Is.Not.Null);
            
        }

        [Test]
        
        public void TestDeleteTask_NegativeTaskId()
        {
            var context = new MockProjectManagerEntities();
            ProjectManager.Models.Task task = new Models.Task();
            task.TaskId = -234;
            var controller = new TaskController(new BC.TaskBC(context));
            var ex = Assert.Throws<ArithmeticException>(() => controller.DeleteTaskDetails(task));
            Assert.That(ex.Message, Is.Not.Null);
            
        }
    }
}
