using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace TinderApp
{
    public class Gender
    {
        private int _id;
        private string _gender;

        public Gender(string gender, int genderId = 0)
        {
            _id = genderId;
            _gender = gender;
        }

        public override bool Equals(System.Object otherGender)
        {
            if (!(otherGender is Gender))
            {
                return false;
            }
            else
            {
                Gender newGender = (Gender) otherGender;
                return (this.genderId == newGender.genderId()) && (this.gender == newGender.gender);
            }
        }

        /*public override int GetHashCode()
        {
            return this.GetName().GetHashCode();
        }*/

        public string gender
        {
          get
          {
            return this._gender;
          }
          set
          {
            this._gender = value;
          }
        }

        public int genderId
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

        public static List<Gender> GetAll()
        {
            List<Gender> AllGenders = new List<Gender>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM genders;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int genderId = rdr.GetInt32(0);
                string genderName = rdr.GetString(1);
                Gender newGender = new Gender(genderName, genderId);
                AllGenders.Add(newGender);
            }

            DB.CloseSqlConnection(conn, rdr);

            return AllGenders;
        }


        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO genders (gender) OUTPUT INSERTED.id VALUES (@GenderName);", conn);

            cmd.Parameters.Add(new SqlParameter("@GenderName", this.gender);

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this.genderId = rdr.GetInt32(0);
            }

            DB.CloseSqlConnection(conn, rdr);
        }

        public static Gender Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM genders WHERE id = @GenderId;", conn);

            cmd.Parameters.Add(new SqlParameter("@GenderId", id.ToString()));

            SqlDataReader rdr = cmd.ExecuteReader();

            int foundGenderId = 0;
            string foundGenderName = null;

            while(rdr.Read())
            {
                foundGenderId = rdr.GetInt32(0);
                foundGenderName = rdr.GetString(1);
            }

            Gender foundGender = new Gender(foundGenderName, foundGenderId);

            DB.CloseSqlConnection(conn, rdr);

            return foundGender;
        }

        public void AddUser(User newUser)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO users_genders (gender_id, user_id) VALUES (@GenderId, @UserId);", conn);
            cmd.Parameters.Add(new SqlParameter("@GenderId", this.genderId.ToString());
            cmd.Parameters.Add(new SqlParameter("@UserId", newUser.ToString());

            DB.CloseSqlConnection(conn, rdr);
        }

        public List<User> GetUsers()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT users.* FROM genders JOIN users_genders ON (genders.id = users_genders.gender_id) JOIN users ON (users_genders.user_id = users.id) WHERE genders.id = @GenderId;", conn);

            cmd.Parameters.Add(new SqlParameter("@GenderId", this.genderId.ToString()));

            SqlDataReader rdr = cmd.ExecuteReader();

            List<User> users = new List<User>{};

            while(rdr.Read())
            {
                int userId = rdr.GetInt32(0);
                string userGender = rdr.GetString(1);
                User newUser = new User(userGender, userId);
                users.Add(newUser);
            }

            DB.CloseSqlConnection(conn, rdr);

            return users;
        }


        public void Delete()
        {
          SqlConnection conn = DB.Connection();
          conn.Open();

          SqlCommand cmd = new SqlCommand("DELETE FROM genders WHERE id = @GenderId; DELETE FROM users_genders WHERE gender_id = @GenderId;", conn);

          cmd.Parameters.Add(new SqlParameter("@GenderId", this.genderId.ToString());

          DB.CloseSqlConnection(conn, rdr);
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM genders;", conn);

            DB.CloseSqlConnection(conn, rdr);
        }
    }
}
