using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая1._0
{
    public class TaskForTeacher
    {
        public int id {  get; set; }
        public string descript { get; set; }
        public string issuence { get; set; }
        public string due {  get; set; }
        public string nameTeacher { get; set; }

        public TaskForTeacher(Tasks task)
        {
            this.id = task.IDTask;
            this.descript = task.Description;
            this.issuence = task.IssuanceDate.ToShortDateString();
            this.due = task.DueDate.ToShortDateString();
            this.nameTeacher = KBPClassBetaEntities1.GetContext().Teachers.FirstOrDefault(c =>c.IDTeacher == task.IDTeacher).Name;
        }
    }
}
