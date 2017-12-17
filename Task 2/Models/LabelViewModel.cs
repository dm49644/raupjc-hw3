using System.Collections.Generic;
using Task_1;

namespace Task_2.Models
{
    public class LabelViewModel
    {
        public List<AddLabelModel> LabelList { get; set; }

        public LabelViewModel(List<TodoItemLabel> list)
        {
            LabelList = new List<AddLabelModel>();
            foreach (TodoItemLabel til in list)
            {
                AddLabelModel alm = new AddLabelModel();
                alm.label = til.Value;
                alm.id = til.Id;
                LabelList.Add(alm);
            }
        }
    }
}