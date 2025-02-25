using DataAccessLayer;
using DataAccessLayer.Model;

namespace DTO
{
    public static class StudentDTO
    {
        public static DTO.Model.Student GetStudentDTO(int id)
        {
            DataAccessLayer.Model.Student student;
            DatabaseAccess databaseAccess = new DatabaseAccess();
			student = databaseAccess.GetStudent(id);

            return new DTO.Model.Student(
                id: student.Id,
				name: student.Name,
				age: student.Age,
				startTime: student.StartTime,
				lineType: (DTO.Model.Student.StudentType) student.LineType) {
            };
        }

		public static List<DTO.Model.Student> GetAllStudentsDTO() {
			List<DTO.Model.Student> students = new();
			DatabaseAccess databaseAccess = new DatabaseAccess();

			foreach (Student student in databaseAccess.GetAllStudents()) {
				students.Add(new DTO.Model.Student(
				id: student.Id,
				name: student.Name,
				age: student.Age,
				startTime: student.StartTime,
				lineType: (DTO.Model.Student.StudentType) student.LineType));
			}
			return students;
		}

		public static void AddStudent(string name, int age, DateTime startDate, DTO.Model.Student.StudentType line) {
			DatabaseAccess databaseAccess = new DatabaseAccess();

			databaseAccess.AddStudent(name, age, startDate, (DataAccessLayer.Model.Student.StudentType) line);
		}
		public static void DeleteStudent(int id) {
			DatabaseAccess databaseAccess = new DatabaseAccess();

			databaseAccess.DeleteStudent(id);
		}

		public static void UpdateStudent(int id, string name, int age, DateTime startDate, DTO.Model.Student.StudentType line) {
			DatabaseAccess databaseAccess = new DatabaseAccess();

			databaseAccess.UpdateStudent(id, name, age, startDate, (DataAccessLayer.Model.Student.StudentType) line);
		}
	}
}
