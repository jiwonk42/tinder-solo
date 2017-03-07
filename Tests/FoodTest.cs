using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace TinderApp
{
    public class FoodTest : IDisposable
    {
        public FoodTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=tinder_user_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_DatabaseEmptyAtFirst()
        {
            //Arrange, Act
            int result = Food.GetAll().Count;

            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_Equal_ReturnsTrueIfDescriptionsAreTheSame_true()
        {
            //Arrange, Act
            Food firstFood = new Food("Burger");
            Food secondFood = new Food("Burger");

            //Assert
            Assert.Equal(firstFood, secondFood);
        }


        [Fact]
        public void Test_Save_SavesToDatabase()
        {
            //Arrange
            Food testFood = new Food("Burger");

            //Act
            testFood.Save();
            List<Food> result = Food.GetAll();
            List<Food> testList = new List<Food>{testFood};

            //Assert
            Assert.Equal(testList, result);
        }

        [Fact]
        public void Test_Save_AssignsIdToObject()
        {
            //Arrange
            Food testFood = new Food("Burger");

            //Act
            testFood.Save();
            Food savedFood = Food.GetAll()[0];

            int result = savedFood.foodId;
            int testId = testFood.foodId;

            //Assert
            Assert.Equal(testId, result);
        }

        [Fact]
        public void Test_Find_FindFoodInDatabase()
        {
            //Arrange
            Food testFood = new Food("Burger");
            testFood.Save();

            //Act
            Food foundFood = Food.Find(testFood.foodId);

            //Assert
            Assert.Equal(testFood, foundFood);
        }

        [Fact]
        public void Test_AddUser_AddsUserToFood()
        {
            //Arrange
            Food testFood = new Food("Burger");
            testFood.Save();

            User testUser = new User("Riley", "Hello");
            testUser.Save();

            //Act
            testFood.AddUser(testUser);

            List<User> result = testFood.GetUsers();
            List<User> testList = new List<User>{testUser};

            //Assert
            Assert.Equal(testList, result);
        }


        [Fact]
        public void Test_GetUsers_ReturnsAllFoodUsers()
        {
            //Arrange
            Food testFood = new Food("Burger");
            testFood.Save();

            User testUser1 = new User("Riley", "Hello");
            testUser1.Save();

            User testUser2 = new User("John", "I love Epicodus");
            testUser2.Save();

            //Act
            testFood.AddUser(testUser1);
            List<User> result = testFood.GetUsers();
            List<User> testList = new List<User> {testUser1};

            //Assert
            Assert.Equal(testList, result);
        }

        [Fact]
        public void Test_Delete_DeletesFoodAssociationsFromDatabase()
        {
          //Arrange
          User testUser = new User("John", "I love Epicodus");
          testUser.Save();

          Food testFood = new Food("Burger");
          testFood.Save();

          //Act
          testFood.AddUser(testUser);
          testFood.Delete();

          List<Food> resultUsersFoods = testUser.GetFoods();
          List<Food> testUsersFoods = new List<Food> {};

          //Assert
          Assert.Equal(testUsersFoods, resultUsersFoods);
        }

        public void Dispose()
        {
            Food.DeleteAll();
            User.DeleteAll();
        }
    }
}
