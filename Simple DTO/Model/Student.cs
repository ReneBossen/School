using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Model {
	public class Student {
		public int Id {
			get; set;
		}
		public string Name {
			get; set;
		}
		public int Age {
			get; set;
		}
		public DateTime StartTime {
			get; set;
		}
		public StudentType LineType;

		public enum StudentType {
			MASTER,
			BACHELOR,
			SINGLE_SUBJECT,
			FURTHER_EDUCATION,
		}

		internal Student(int id, string name, int age, DateTime startTime, StudentType lineType) {
			Id = id;
			Name = name;
			Age = age;
			StartTime = startTime;
			LineType = lineType;
		}

		internal Student(string name, int age, DateTime startTime, StudentType lineType) {
			Name = name;
			Age = age;
			StartTime = startTime;
			LineType = lineType;
		}

		public Student() {
		}
	}
}
