using System.ComponentModel.DataAnnotations;

namespace CRUD_Operation_Using_Ado.Net.Model
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30,MinimumLength =3)]
        public string FirstName { get; set; }
        [StringLength(30, MinimumLength = 0)]
        public string LastName { get; set; } = null;
        [Required]
        [Range(18,70)]
        public int Age { get; set; }
    }
}
