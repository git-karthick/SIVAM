using SIVAM.Models;
using System.Data;
using System.Data.SqlClient;

namespace SIVAM.DAL
{
    public class SivamDAL
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public static IConfiguration Configuration { get; set; }
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            return Configuration.GetConnectionString("DefaultConnection");
        }
        public List<Sivam> GetAll()
        {
            List<Sivam> schoolList = new List<Sivam>();
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "VIEWALL_SP";
                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();

                while (dr.Read())
                {
                    Sivam school = new Sivam();
                    school.Id = Convert.ToInt32(dr["ID"]);
                    school.RollNo = Convert.ToInt32(dr["ROLLNO"]);
                    school.Name = dr["NAME"].ToString();
                    school.Class = dr["CLASS"].ToString();
                    schoolList.Add(school);

                }
            }
            return schoolList;
        }
        public bool Insert(Sivam sivam)
        {
            int id = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "INSERT_SP";
                _command.Parameters.AddWithValue("@ROLLNO", sivam.RollNo);
                _command.Parameters.AddWithValue("@NAME", sivam.Name);
                _command.Parameters.AddWithValue("@CLASS", sivam.Class);
                _connection.Open();
              id =  _command.ExecuteNonQuery();
            }
            return id > 0 ? true : false;
        }

        public Sivam GetById(int id)
        {
            Sivam school = new Sivam();
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "VIEWBYID_SP";
                _command.Parameters.AddWithValue("@id", id);
                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();

                while (dr.Read())
                {
                    school.Id = Convert.ToInt32(dr["ID"]);
                    school.RollNo = Convert.ToInt32(dr["ROLLNO"]);
                    school.Name = dr["NAME"].ToString();
                    school.Class = dr["CLASS"].ToString();
                   

                }
            }
            return school;
        }
        public bool Update(Sivam sivam)
        {
            int id = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "UPDATE_SP";
                _command.Parameters.AddWithValue("@ID", sivam.Id);
                _command.Parameters.AddWithValue("@ROLLNO", sivam.RollNo);
                _command.Parameters.AddWithValue("@NAME", sivam.Name);
                _command.Parameters.AddWithValue("@CLASS", sivam.Class);
                _connection.Open();
                id = _command.ExecuteNonQuery();
            }
            return id > 0 ? true : false;
        }
        public bool Delete(int id)
        {
           
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "UPDATE1_SP";
                _command.Parameters.AddWithValue("@ID", id);
                _connection.Open();
                id = _command.ExecuteNonQuery();
            }
            return id > 0 ? true : false;
        }
    }
}
