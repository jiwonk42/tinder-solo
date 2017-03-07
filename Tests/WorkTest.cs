using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace TinderApp
{
    public class WorkTest : IDisposable
    {
        public WorkTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=tinder_user_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_DatabaseEmptyAtFirst()
        {
            //Arrange, Act
            int result = Work.GetAll().Count;

            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_Equal_ReturnsTrueIfDescriptionsAreTheSame_true()
        {
            //Arrange, Act
            Work firstWork = new Work("Epicodus");
            Work secondWork = new Work("Epicodus");

            //Assert
            Assert.Equal(firstWork, secondWork);
        }


        [Fact]
        public void Test_Save_SavesToDatabase()
        {
            //Arrange
            Work testWork = new Work("Epicodus");

            //Act
            testWork.Save();
            List<Work> result = Work.GetAll();
            List<Work> testList = new List<Work>{testWork};

            //Assert
            Assert.Equal(testList, result);
        }

        [Fact]
        public void Test_Save_AssignsIdToObject()
        {
            //Arrange
            Work testWork = new Work("Epicodus");

            //Act
            testWork.Save();
            Work savedWork = Work.GetAll()[0];

            int result = savedWork.workId;
            int testId = testWork.workId;

            //Assert
            Assert.Equal(testId, result);
        }

        [Fact]
        public void Test_Find_FindWorkInDatabase()
        {
            //Arrange
            Work testWork = new Work("Epicodus");
            testWork.Save();

            //Act
            Work foundWork = Work.Find(testWork.workId);

            //Assert
            Assert.Equal(testWork, foundWork);
        }

        [Fact]
        public void Test_AddUser_AddsUserToWork()
        {
            //Arrange
            Work testWork = new Work("Epicodus");
            testWork.Save();

            User testUser = new User("Riley", "Hello");
            testUser.Save();

            //Act
            testWork.AddUser(testUser);

            List<User> result = testWork.GetUsers();
            List<User> testList = new List<User>{testUser};

            //Assert
            Assert.Equal(testList, result);
        }


        [Fact]
        public void Test_GetUsers_ReturnsAllWorkUsers()
        {
            //Arrange
            Work testWork = new Work("Epicodus");
            testWork.Save();

            User testUser1 = new User("Riley", "Hello");
            testUser1.Save();

            User testUser2 = new User("John", "I love Epicodus");
            testUser2.Save();

            //Act
            testWork.AddUser(testUser1);
            List<User> result = testWork.GetUsers();
            List<User> testList = new List<User> {testUser1};

            //Assert
            Assert.Equal(testList, result);
        }

        [Fact]
        public void Test_Delete_DeletesWorkAssociationsFromDatabase()
        {
          //Arrange
          User testUser = new User("John", "I love Epicodus");
          testUser.Save();

          Work testWork = new Work("Epicodus");
          testWork.Save();

          //Act
          testWork.AddUser(testUser);
          testWork.Delete();

          List<Work> resultUsersWorks = testUser.GetWorks();
          List<Work> testUsersWorks = new List<Work> {};

          //Assert
          Assert.Equal(testUsersWorks, resultUsersWorks);
        }

        public void Dispose()
        {
            Work.DeleteAll();
            User.DeleteAll();
        }
    }
}
