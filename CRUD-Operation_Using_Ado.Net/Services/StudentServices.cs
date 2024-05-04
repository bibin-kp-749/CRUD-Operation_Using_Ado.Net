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
        //Variable hold the connection data
        private readonly string _strconnection = "data source = DESKTOP-QJT086F ; database = ADOTASKS ; integrated security = SSPI ; TrustServerCertificate = true";
       //Method for AddStudent
        public void AddStudent(Student student)
        {
            try
            {
                //Established the connection 
                using (SqlConnection _con = new SqlConnection(_strconnection))
                {
                    if (_con.State == ConnectionState.Closed) //opened the connection if it is closed
                        _con.Open();
                    //prepared an Sql command
                    using (SqlCommand cmd = new SqlCommand("insert into Students values(@fname,@lname,@age)", _con))
                    {
                        //defined parameter
                        cmd.Parameters.AddWithValue("@fname", student.FirstName);
                        cmd.Parameters.AddWithValue("@lname", student.LastName);
                        cmd.Parameters.AddWithValue("@age", student.Age);
                        cmd.ExecuteNonQuery(); //execute the query
                    }
                }
            }catch (SqlException ex) //Handled the Sql Exception
            {
                Log.Error(ex.ToString());
            }catch (Exception ex) //Handled the Exception
            {
                Log.Error(ex.ToString());
            }
        }
        //method for Deleting student by id
        public void DeleteStudent(int Id)
        {
            try
            {
                //established connection
                using (SqlConnection _con = new SqlConnection(_strconnection))
                {
                    if (_con.State == ConnectionState.Closed) //open the connection if it closed
                        _con.Open();
                    //prepared an sql command
                    using (SqlCommand cmd = new SqlCommand("delete from Students where StudentID=@id", _con))
                    {
                        //defined parameter
                        cmd.Parameters.AddWithValue("@id", Id);
                        cmd.ExecuteNonQuery(); //execute the Query
                    }
                }
            }catch(SqlException ex) //handled the sql exception
            {
                Log.Error("Sql Exception Occured "+ex.Message);
            }catch (Exception ex) //handled the exception
            {
                Log.Error("Exception Occured "+ex.Message);
            }
        }
        //method for Updating an existing student
        public void UpdateStudent(Student student)
        {
            try
            {
                //established connection
                using(SqlConnection _con=new SqlConnection(_strconnection))
                {
                    //prepared an sql command
                    using (SqlCommand cmd = new SqlCommand("update Students set FirstName=@fname,LastName=@lname,Age=@age where StudentID=@id", _con))
                    {
                        if (_con.State == ConnectionState.Closed) //opened the connection if it closed
                            _con.Open();
                        //defined parameter
                        cmd.Parameters.AddWithValue("id", student.Id);
                        cmd.Parameters.AddWithValue("@fname", student.FirstName);
                        cmd.Parameters.AddWithValue("@lname", student.LastName);
                        cmd.Parameters.AddWithValue("@age", student.Age);
                        cmd.ExecuteNonQuery();//executed the command
                    }
                }
            }catch (SqlException ex) //handled the sql exception
            {
                Log.Error("Sql Exception Occured " + ex.Message);
            }catch(Exception ex) //handled the exception
            {
                Log.Error("Exception Occured " + ex.Message);
            }
        }
        //method for View all student
        public IEnumerable<Student> ViewStudent()
        {
            //defined a list for returning data
            List<Student> students = new List<Student>();
            try
            {
                //established a connection
                using (SqlConnection _con = new SqlConnection(_strconnection))
                {
                    if (_con.State == ConnectionState.Closed) //opened the connection if it closed
                        _con.Open();
                    //prepared an sql command
                    using (SqlCommand cmd = new SqlCommand("select * from Students", _con))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();//execute the command
                        while (dr.Read())
                        {
                            //created an instance of Student
                            Student st = new Student();
                            //defined parameters
                            st.Id = Convert.ToInt32(dr["StudentID"]);
                            st.FirstName = dr["FirstName"].ToString();
                            st.LastName = dr["LastName"].ToString();
                            st.Age = Convert.ToInt32(dr["Age"]);
                            students.Add(st);//added the data to list
                        }
                    }
                }
                return students; //returned the list
            }
            catch (SqlException ex) //handled the sql Exception
            {
                Log.Error("Sql Exception Occured"+ex.Message);
                throw;
            }
            catch (Exception ex) //Handled the exception
            {
                Log.Error("Exception Occured "+ex.Message);
                throw;
            }
        }
    }
}
