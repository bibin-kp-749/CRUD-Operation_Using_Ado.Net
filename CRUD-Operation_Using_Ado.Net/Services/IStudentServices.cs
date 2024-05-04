using CRUD_Operation_Using_Ado.Net.Model;

namespace CRUD_Operation_Using_Ado.Net.Services
{
    public interface IStudentServices
    {
        public void AddStudent(Student student);
        public void DeleteStudent(int Id);
        public void UpdateStudent(Student student);
        public IEnumerable<Student> ViewStudent();
    }
}
