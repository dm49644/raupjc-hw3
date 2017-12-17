using System;
using System.Collections.Generic;
using System.Linq;

namespace Task_1
{
    public class TodoSqlRepository : ITodoRepository
    {
        private readonly TodoDbContext _context;
        public TodoSqlRepository(TodoDbContext context)
        {
            _context = context;
        }

        public List<TodoItemLabel> GetAllLabels()
        {
            return _context.TodoItemLabels.Where(t=>t.Value.Equals(t.Value)).ToList();
        }

        public TodoItemLabel GetLabel(Guid labelId)
        {
            TodoItemLabel til=_context.TodoItemLabels.Find(labelId);
            if (til == null) return null;
            return til;
        }

        public TodoItemLabel AddLabel(TodoItemLabel todoItemLabel)
        {
            if (_context.TodoItemLabels.Find(todoItemLabel.Id) != null)
                throw new DuplicateTodoItemLabelException
                    ($"Duplicate id: {todoItemLabel.Id}");

            if (_context.TodoItemLabels.Where(t=> t.Value.Equals(todoItemLabel.Value)).ToList().Capacity>0)
                throw new DuplicateTodoItemLabelException
                    ($"Duplicate id: {todoItemLabel.Id}");

            _context.TodoItemLabels.Add(todoItemLabel);
            _context.SaveChanges();
            return todoItemLabel;
        }

        public bool RemoveLabel(Guid labelId)
        {
            TodoItemLabel til= _context.TodoItemLabels.FirstOrDefault(t => t.Id.Equals(labelId));

            if (til == null)
            {
                return false;
            }

            _context.TodoItemLabels.Remove(til);
            _context.SaveChanges();
            return true;
        }

        public void UpdateLabel(TodoItemLabel til)
        {
            var item = _context.TodoItems.Find(til.Id);

            if (item == null) return;

            RemoveLabel(til.Id);
            AddLabel(til);
        }

        public TodoItem Get(Guid todoId, Guid userId)
        {
            var item = _context.TodoItems.Find(todoId);

            if (item == null) return null;

            if (!item.UserId.Equals(userId))
                throw new TodoAccessDeniedException
                    ("You do not have permission to access this item!");

            return item;
        }

        public TodoItem Add(TodoItem todoItem)
        {
            if (_context.TodoItems.Find(todoItem.Id) != null)
                throw new DuplicateTodoItemException
                    ($"Duplicate id: {todoItem.Id}");

            _context.TodoItems.Add(todoItem);
            _context.SaveChanges();
            return todoItem;
        }

        public bool Remove(Guid todoId, Guid userId)
        {
            TodoItem item = _context.TodoItems.FirstOrDefault(t=> t.Id.Equals(todoId));

            if (item == null)
            {
                return false;
            }

            if (!item.UserId.Equals(userId))
                throw new TodoAccessDeniedException
                    ("You do not have permission to remove this item!");

            _context.TodoItems.Remove(item);
            _context.SaveChanges();
            return true;
        }

        public void Update(TodoItem todoItem, Guid userId)
        {
            var item = _context.TodoItems.Find(todoItem.Id);

            if (item == null) return;

            if (item.UserId.Equals(userId))
                Remove(todoItem.Id, userId);
            else throw new TodoAccessDeniedException
                ("You do not have permission to update this item");

            Add(todoItem);
        }

        public bool MarkAsCompleted(Guid todoId, Guid userId)
        {
            var item = _context.TodoItems.Find(todoId);

            if (item == null) return false;

            if (item.UserId.Equals(userId))
            {
                bool a= item.MarkAsCompleted();
                _context.SaveChanges();
                return a;
            }
               
            throw new TodoAccessDeniedException
                ("You do not have permission to make changes to this item");
        }

        public List<TodoItem> GetAll(Guid userId)
        {
            return _context.TodoItems.Where(t => t.UserId.Equals(userId))
                .OrderByDescending(t => t.DateCreated).ToList();
        }

        public List<TodoItem> GetActive(Guid userId)
        {
            return _context.TodoItems.Where(t => !t.IsCompleted
                                            && t.UserId.Equals(userId))
                                            .ToList();
        }

        public List<TodoItem> GetCompleted(Guid userId)
        {
            return _context.TodoItems.Where(t => t.IsCompleted
                                            && t.UserId.Equals(userId))
                                            .ToList();
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction, Guid userId)
        {
            return _context.TodoItems.Where(filterFunction)
                .Where(t => t.UserId.Equals(userId)).ToList();
        }
    }
}
