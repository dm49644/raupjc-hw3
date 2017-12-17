using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1.Tests
{
    [TestClass()]
    public class TodoSqlRepositoryTests
    {
        private readonly TodoDbContext _context=new TodoDbContext(Configuration.GetConnectionString("DefaultConnection");

        [TestMethod()]
        public void RemoveTest()
        {
            Guid g=new Guid();
            TodoItem ti = new TodoItem("Naziv", g);
            _context.TodoItems.Add(ti);
            TodoItem tii=_context.TodoItems.Find(g);
            Assert.AreEqual(ti.Text, tii.Text);
            _context.TodoItems.Remove(ti);
           
        }
    }
}