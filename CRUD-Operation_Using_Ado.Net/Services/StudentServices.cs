using CRUD_Operation_Using_Ado.Net.Model;
using Microsoft.Data.SqlClient;
using Serilog;
using Serilog.Core;
using System.Data;
using System.Diagnostics.Eventing.Reader;
namespace CRUD_Operation_Using_Ado.Net.Services
{
    public class StudentServices:IStudentServices
    {
        private readonly string _strconnection = "data source = DESKTOP-QJT086F ; database = ADOTASKS ; integrated security = SSPI ; TrustServerCertificate = true";
        public void AddStudent(Student student)
        {
            try
            {
                using (SqlConnection _con = new SqlConnection(_strconnection))
                {
                    if (_con.State == ConnectionState.Closed)
                        _con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Students values(@fname,@lname,@age)",_con);
                    cmd.Parameters.AddWithValue("@fname", student.FirstName);
                    cmd.Parameters.AddWithValue("@lname", student.LastName);
                    cmd.Parameters.AddWithValue("@age", student.Age);
                    cmd.ExecuteNonQuery();    
                }
            }catch (SqlException ex)
            {
                Log.Error(ex.ToString());
            }catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }
        public void DeleteStudent(int Id)
        {
            try
            {
                using (SqlConnection _con = new SqlConnection(_strconnection))
                {
                    if (_con.State == ConnectionState.Closed)
                        _con.Open();
                    using (SqlCommand cmd = new SqlCommand("delete from Students where StudentID=@id", _con))
                    {
                        cmd.Parameters.AddWithValue("@id", Id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }catch(SqlException ex)
            {
                Log.Error("Sql Exception Occured "+ex.Message);
            }catch (Exception ex)
            {
                Log.Error("Exception Occured "+ex.Message);
            }
        }
        public void UpdateStudent(Student student)
        {
            try
            {
                using(SqlConnection _con=new SqlConnection(_strconnection))
                {
                    using (SqlCommand cmd = new SqlCommand("update Students set FirstName=@fname,LastName=@lname,Age=@age where StudentID=@id", _con))
                    {
                        if (_con.State == ConnectionState.Closed)
                            _con.Open();
                        cmd.Parameters.AddWithValue("id", student.Id);
                        cmd.Parameters.AddWithValue("@fname", student.FirstName);
                        cmd.Parameters.AddWithValue("@lname", student.LastName);
                        cmd.Parameters.AddWithValue("@age", student.Age);
                        cmd.ExecuteNonQuery();
                    }
                }
            }catch (SqlException ex)
            {
                Log.Error("Sql Exception Occured " + ex.Message);
            }catch(Exception ex)
            {
                Log.Error("Exception Occured " + ex.Message);
            }
        }
        public IEnumerable<Student> ViewStudent()
        {
            List<Student> students = new List<Student>();
            try
            {
                using (SqlConnection _con = new SqlConnection(_strconnection))
                {
                    if (_con.State == ConnectionState.Closed)
                        _con.Open();
                    using (SqlCommand cmd = new SqlCommand("select * from Students", _con))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            Student st = new Student();
                            st.Id = Convert.ToInt32(dr["StudentID"]);
                            st.FirstName = dr["FirstName"].ToString();
                            st.LastName = dr["LastName"].ToString();
                            st.Age = Convert.ToInt32(dr["Age"]);
                            students.Add(st);
                        }
                    }
                }
                return students;
            }
            catch (SqlException ex)
            {
                Log.Error("Sql Exception Occured"+ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Log.Error("Exception Occured "+ex.Message);
                throw;
            }
        }
    }
}
