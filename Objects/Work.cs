using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace TinderApp
{
    public class Work
    {
        private int _id;
        private string _work;

        public Work(string work, int workId = 0)
        {
            _id = workId;
            _work = work;
        }

        public override bool Equals(System.Object otherWork)
        {
            if (!(otherWork is Work))
            {
                return false;
            }
            else
            {
                Work newWork = (Work) otherWork;
                return (this.workId == newWork.workId) && (this.work == newWork.work);
            }
        }

        /*public override int GetHashCode()
        {
            return this.GetName().GetHashCode();
        }*/

        public string work
        {
          get
          {
            return this._work;
          }
          set
          {
            this._work = value;
          }
        }

        public int workId
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

        public static List<Work> GetAll()
        {
            List<Work> AllWorks = new List<Work>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM works;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int workId = rdr.GetInt32(0);
                string workName = rdr.GetString(1);
                Work newWork = new Work(workName, workId);
                AllWorks.Add(newWork);
            }

            DB.CloseSqlConnection(conn, rdr);

            return AllWorks;
        }


        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO works (work) OUTPUT INSERTED.id VALUES (@WorkName);", conn);

            cmd.Parameters.Add(new SqlParameter("@WorkName", this.work);

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this.workId = rdr.GetInt32(0);
            }

            DB.CloseSqlConnection(conn, rdr);
        }

        public static Work Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM works WHERE id = @WorkId;", conn);

            cmd.Parameters.Add(new SqlParameter("@WorkId", id.ToString()));

            SqlDataReader rdr = cmd.ExecuteReader();

            int foundWorkId = 0;
            string foundWorkName = null;

            while(rdr.Read())
            {
                foundWorkId = rdr.GetInt32(0);
                foundWorkName = rdr.GetString(1);
            }

            Work foundWork = new Work(foundWorkName, foundWorkId);

            DB.CloseSqlConnection(conn, rdr);

            return foundWork;
        }

        public void AddUser(User newUser)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO users_works (work_id, user_id) VALUES (@WorkId, @UserId);", conn);
            cmd.Parameters.Add(new SqlParameter("@WorkId", this.workId.ToString());
            cmd.Parameters.Add(new SqlParameter("@UserId", newUser.ToString());

            DB.CloseSqlConnection(conn, rdr);
        }

        public List<User> GetUsers()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT users.* FROM works JOIN users_works ON (works.id = users_works.work_id) JOIN users ON (users_works.user_id = users.id) WHERE works.id = @WorkId;", conn);

            cmd.Parameters.Add(new SqlParameter("@WorkId", this.workId.ToString()));

            SqlDataReader rdr = cmd.ExecuteReader();

            List<User> users = new List<User>{};

            while(rdr.Read())
            {
                int userId = rdr.GetInt32(0);
                string userWork = rdr.GetString(1);
                User newUser = new User(userWork, userId);
                users.Add(newUser);
            }

            DB.CloseSqlConnection(conn, rdr);

            return users;
        }


        public void Delete()
        {
          SqlConnection conn = DB.Connection();
          conn.Open();

          SqlCommand cmd = new SqlCommand("DELETE FROM works WHERE id = @WorkId; DELETE FROM users_works WHERE work_id = @WorkId;", conn);

          cmd.Parameters.Add(new SqlParameter("@WorkId", this.workId.ToString());

          DB.CloseSqlConnection(conn, rdr);
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM works;", conn);

            DB.CloseSqlConnection(conn, rdr);
        }
    }
}
