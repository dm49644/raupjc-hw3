using System.Collections.Generic;
using Task_1;

namespace Task_2.Models
{
    public class CompletedViewModel
    {
        public List<TodoViewModel> TodoList { get; set; }

        public CompletedViewModel(List<TodoItem> list)
        {
            TodoList = new List<TodoViewModel>();
            foreach (TodoItem ti in list)
            {
                TodoViewModel tvm = new TodoViewModel();
                tvm.Title = ti.Text;
                tvm.DateDue = ti.DateDue;
                tvm.id = ti.Id;
                tvm.DateCompleted = ti.DateCompleted;
                TodoList.Add(tvm);
            }
        }
    }
}