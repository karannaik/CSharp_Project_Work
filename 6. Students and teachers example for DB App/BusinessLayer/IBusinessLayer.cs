using DataAccessLayer;
using System.Collections.Generic;

namespace BusinessLayer
{
    public interface IBusinessLayer
    {
        #region Standard
        IEnumerable<Standard> GetAllStandards();

        Standard GetStandardByIDWithStudents(int id);

        Standard GetStandardByName(string name);

        Standard GetStandardByID(int id);

        void AddStandard(Standard standard);

        void UpdateStandard(Standard standard);

        void RemoveStandard(Standard standard);
        #endregion

        #region Student
        IEnumerable<Student> GetAllStudents();
        Student GetStudentByID(int id);
        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void RemoveStudent(Student student);
        #endregion
    }
}