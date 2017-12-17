using System;

namespace Task_2.Models
{
    public class TodoViewModel
    {
        public String Title { get; set; }
        public DateTime? DateDue { get; set; }
        public Guid id { get; set; }
        public DateTime? DateCompleted { get; set; }

        public String getDateDueText()
        {
            if (DateDue.HasValue)
            {
                if ((DateDue - DateTime.Now).Value.Days >= 0)
                {
                    String dani = (DateDue - DateTime.Now).Value.Days.ToString();
                    if (dani.EndsWith("1")) return "(za " + dani + " dan)";
                    return "(za " + dani + " dana)";
                }
                else return "(Propušten rok!)";
            }
            return "";
        }
        public String getDateDueOther()
        {
            if (DateDue != null) return DateDue.GetValueOrDefault().ToLongDateString();
            return "";
        }
    }
}