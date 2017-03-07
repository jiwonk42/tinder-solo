using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace TinderApp
{
    public class Food
    {
        private int _id;
        private string _food;

        public Food(string food, int foodId = 0)
        {
            _id = foodId;
            _food = food;
        }

        public override bool Equals(System.Object otherFood)
        {
            if (!(otherFood is Food))
            {
                return false;
            }
            else
            {
                Food newFood = (Food) otherFood;
                return (this.foodId == newFood.foodId) && (this.food == newFood.food);
            }
        }

        /*public override int GetHashCode()
        {
            return this.GetName().GetHashCode();
        }*/

        public string food
        {
          get
          {
            return this._food;
          }
          set
          {
            this._food = value;
          }
        }

        public int foodId
        {
          get
          {
            return this._id;
          }
          set
          {
            this._id = value;
          }
        }

        public static List<Food> GetAll()
        {
            List<Food> AllFoods = new List<Food>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM foods;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int foodId = rdr.GetInt32(0);
                string foodName = rdr.GetString(1);
                Food newFood = new Food(foodName, foodId);
                AllFoods.Add(newFood);
            }

            DB.CloseSqlConnection(conn, rdr);

            return AllFoods;
        }


        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO foods (food) OUTPUT INSERTED.id VALUES (@FoodName);", conn);

            cmd.Parameters.Add(new SqlParameter("@FoodName", this.food);

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this.foodId = rdr.GetInt32(0);
            }

            DB.CloseSqlConnection(conn, rdr);
        }

        public static Food Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM foods WHERE id = @FoodId;", conn);

            cmd.Parameters.Add(new SqlParameter("@FoodId", id.ToString()));

            SqlDataReader rdr = cmd.ExecuteReader();

            int foundFoodId = 0;
            string foundFoodName = null;

            while(rdr.Read())
            {
                foundFoodId = rdr.GetInt32(0);
                foundFoodName = rdr.GetString(1);
            }

            Food foundFood = new Food(foundFoodName, foundFoodId);

            DB.CloseSqlConnection(conn, rdr);

            return foundFood;
        }

        public void AddUser(User newUser)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO users_foods (food_id, user_id) VALUES (@FoodId, @UserId);", conn);
            cmd.Parameters.Add(new SqlParameter("@FoodId", this.foodId.ToString());
            cmd.Parameters.Add(new SqlParameter("@UserId", newUser.ToString());

            DB.CloseSqlConnection(conn, rdr);
        }

        public List<User> GetUsers()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT users.* FROM foods JOIN users_foods ON (foods.id = users_foods.food_id) JOIN users ON (users_foods.user_id = users.id) WHERE foods.id = @FoodId;", conn);

            cmd.Parameters.Add(new SqlParameter("@FoodId", this.foodId.ToString()));

            SqlDataReader rdr = cmd.ExecuteReader();

            List<User> users = new List<User>{};

            while(rdr.Read())
            {
                int userId = rdr.GetInt32(0);
                string userFood = rdr.GetString(1);
                User newUser = new User(userFood, userId);
                users.Add(newUser);
            }

            DB.CloseSqlConnection(conn, rdr);

            return users;
        }


        public void Delete()
        {
          SqlConnection conn = DB.Connection();
          conn.Open();

          SqlCommand cmd = new SqlCommand("DELETE FROM foods WHERE id = @FoodId; DELETE FROM users_foods WHERE food_id = @FoodId;", conn);

          cmd.Parameters.Add(new SqlParameter("@FoodId", this.foodId.ToString());

          DB.CloseSqlConnection(conn, rdr);
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM foods;", conn);

            DB.CloseSqlConnection(conn, rdr);
        }
    }
}
