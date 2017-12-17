using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Task_1;
using Task_2.Data;
using Task_2.Models;

namespace Task_2.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ITodoRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public TodoController(ITodoRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            PartialModel model = new PartialModel
            {
                IndexViewModel = new IndexViewModel(_repository.GetActive(new Guid(currentUser.Id)))
            };
            return View(model);
        }

        public async Task<IActionResult> Labels()
        {
            LabelViewModel lvm = new LabelViewModel(_repository.GetAllLabels());
            return View(lvm);
        }

        public async Task<IActionResult> Completed()
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            PartialModel model = new PartialModel
            {
                CompletedViewModel = new CompletedViewModel(_repository.GetCompleted(new Guid(currentUser.Id)))
            };
            return View(model);
        }

        public async Task<IActionResult> Add()
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);

            var labels = _repository.GetAllLabels();
            var model=new AddTodoViewModel();
            var selectList = new List<SelectListItem>();
            foreach (var element in labels)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.Id.ToString(),
                    Text = element.Value
                });
            }
            model.Options = selectList;
            
            return View(model);
        }

        public async Task<IActionResult> MarkAsCompleted(Guid id)
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            _repository.MarkAsCompleted(id, new Guid(currentUser.Id));
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> RemoveFromCompleted(Guid id)
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            _repository.Remove(id, new Guid(currentUser.Id));
            return RedirectToAction("Completed");
        }

        public async Task<IActionResult> RemoveLabel(Guid id)
        {
            _repository.RemoveLabel(id);
            return RedirectToAction("Labels");
        }

        public async Task<IActionResult> Update()
        {
            return View();
        }

        public async Task<IActionResult> UpdateLabel(Guid id)
        {
            _repository.RemoveLabel(id);
            return RedirectToAction("Update");
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTodoViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
                TodoItem ti=new TodoItem(model.text, new Guid(currentUser.Id));
                ti.DateDue = model.dateDue;
                _repository.Add(ti);
                foreach (String s in model.SelectedOptions)
                {
                    TodoItemLabel til=_repository.GetLabel(Guid.Parse(s));
                    Console.WriteLine(til.Value);
                    ti.Labels.Add(til);
                }

                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddLabel(AddLabelModel model)
        {
            if (ModelState.IsValid)
            {
                TodoItemLabel til = new TodoItemLabel(model.label);
                try
                {
                    _repository.AddLabel(til);
                }
                catch
                {
                    return RedirectToAction("Labels");
                }
                return RedirectToAction("Labels");
            }
            return View(model);
        }

        public async Task<IActionResult> AddLabel()
        {
            return View();
        }
    }
}