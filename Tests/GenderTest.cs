using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace TinderApp
{
    public class GenderTest : IDisposable
    {
        public GenderTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=tinder_user_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_DatabaseEmptyAtFirst()
        {
            //Arrange, Act
            int result = Gender.GetAll().Count;

            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_Equal_ReturnsTrueIfDescriptionsAreTheSame_true()
        {
            //Arrange, Act
            Gender firstGender = new Gender("Male");
            Gender secondGender = new Gender("Male");

            //Assert
            Assert.Equal(firstGender, secondGender);
        }


        [Fact]
        public void Test_Save_SavesToDatabase()
        {
            //Arrange
            Gender testGender = new Gender("Male");

            //Act
            testGender.Save();
            List<Gender> result = Gender.GetAll();
            List<Gender> testList = new List<Gender>{testGender};

            //Assert
            Assert.Equal(testList, result);
        }

        [Fact]
        public void Test_Save_AssignsIdToObject()
        {
            //Arrange
            Gender testGender = new Gender("Male");

            //Act
            testGender.Save();
            Gender savedGender = Gender.GetAll()[0];

            int result = savedGender.genderId;
            int testId = testGender.genderId;

            //Assert
            Assert.Equal(testId, result);
        }

        [Fact]
        public void Test_Find_FindGenderInDatabase()
        {
            //Arrange
            Gender testGender = new Gender("Male");
            testGender.Save();

            //Act
            Gender foundGender = Gender.Find(testGender.genderId);

            //Assert
            Assert.Equal(testGender, foundGender);
        }

        [Fact]
        public void Test_AddUser_AddsUserToGender()
        {
            //Arrange
            Gender testGender = new Gender("Male");
            testGender.Save();

            User testUser = new User("Riley", "Hello");
            testUser.Save();

            //Act
            testGender.AddUser(testUser);

            List<User> result = testGender.GetUsers();
            List<User> testList = new List<User>{testUser};

            //Assert
            Assert.Equal(testList, result);
        }


        [Fact]
        public void Test_GetUsers_ReturnsAllGenderUsers()
        {
            //Arrange
            Gender testGender = new Gender("Male");
            testGender.Save();

            User testUser1 = new User("Riley", "Hello");
            testUser1.Save();

            User testUser2 = new User("John", "I love Epicodus");
            testUser2.Save();

            //Act
            testGender.AddUser(testUser1);
            List<User> result = testGender.GetUsers();
            List<User> testList = new List<User> {testUser1};

            //Assert
            Assert.Equal(testList, result);
        }

        [Fact]
        public void Test_Delete_DeletesGenderAssociationsFromDatabase()
        {
          //Arrange
          User testUser = new User("John", "I love Epicodus");
          testUser.Save();

          Gender testGender = new Gender("Male");
          testGender.Save();

          //Act
          testGender.AddUser(testUser);
          testGender.Delete();

          List<Gender> resultUsersGenders = testUser.GetGenders();
          List<Gender> testUsersGenders = new List<Gender> {};

          //Assert
          Assert.Equal(testUsersGenders, resultUsersGenders);
        }

        public void Dispose()
        {
            Gender.DeleteAll();
            User.DeleteAll();
        }
    }
}
