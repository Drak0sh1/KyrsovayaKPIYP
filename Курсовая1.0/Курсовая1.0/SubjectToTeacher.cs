using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая1._0
{
    public class SubjectToTeacher
    {
        public int id {  get; set; }
        public string Name { get; set; }
        public string idTeacher {  get; set; }
        public int Hour {  get; set; }
        public SubjectToTeacher (Disciplines disciplines)
        {
            this.id = disciplines.IDDisciplines;
            this.Name = disciplines.Name;
            this.Hour = disciplines.Hours;
            this.idTeacher = KBPClassBetaEntities1.GetContext().Teachers.FirstOrDefault(c => c.IDTeacher == disciplines.IDTeacher).Name;
        }


    }
}
