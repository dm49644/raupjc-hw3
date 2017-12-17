using System;
using System.Collections.Generic;
using System.Web.Security;
using Task_1;

namespace Task_2.Models
{
    public class IndexViewModel
    {
        public List<TodoViewModel> TodoList { get; set; }

        public IndexViewModel(List<TodoItem> list)
        {
            TodoList=new List<TodoViewModel>();
            foreach (TodoItem ti in list)
            {
                TodoViewModel tvm = new TodoViewModel();
                tvm.Title = ti.Text;
                tvm.DateDue = ti.DateDue;
                tvm.id = ti.Id;
                TodoList.Add(tvm);
            }
        }
    }
}