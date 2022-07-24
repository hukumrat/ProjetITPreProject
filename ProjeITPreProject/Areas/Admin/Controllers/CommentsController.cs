using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjeITPreProjectMvcUI.Areas.Admin.ViewModels;

namespace ProjeITPreProjectMvcUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Manager,Employee")]
    [Area("Admin")]
    public class CommentsController : Controller
    {
        private readonly IAppService _appService;
        private readonly ICommentService _commentService;
        private readonly ICompanyService _companyService;
        private readonly ITaskService _taskService;
        public CommentsController(IAppService appService, ICommentService commentService, ICompanyService companyService, ITaskService taskService)
        {
            _appService = appService;
            _commentService = commentService;
            _companyService = companyService;
            _taskService = taskService;
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            var comments = _commentService.ListAllByTaskIdWithIncludes(id);
            CommentsListViewModel model = new CommentsListViewModel
            {
                Comments = comments,
                TaskId = id
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult Create(int id)
        {
            var task = _taskService.GetByIdWithIncludes(id);

            if (task == null)
            {
                ViewBag.ErrorMessage = $"Task with Id={id} cannot be found";
                _appService.IziToast($"Task with Id={id} cannot be found", "Error", "error");
                return View("404");
            }
            CommentsCreateViewModel model = new CommentsCreateViewModel
            {
                TaskId = task.Id,
                Name = task.Name,
                Contents = task.Contents,
                StartDate = task.StartDate,
                FinishDate = task.FinishDate,
                IsImportant = task.IsImportant,
                IsUrgent = task.IsUrgent,
                Status = task.Status,
                CompanyId = task.CompanyId,
                Companies = _companyService.GetCompaniesForSelectList()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(CommentsCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var task = _taskService.GetByIdWithIncludes(model.TaskId);

                if (task == null)
                {
                    ViewBag.ErrorMessage = $"Task with Id={model.TaskId} cannot be found";
                    _appService.IziToast($"Task with Id={model.TaskId} cannot be found", "Error", "error");
                    return View("404");
                }
                task.Status = model.Status;
                _taskService.Update(task);
                Comment comment = new Comment
                {
                    TaskId = model.TaskId,
                    Description = model.Description,
                    CompanyId = model.CompanyId
                };
                _commentService.Add(comment);
                _appService.IziToast("", "", "");
                return RedirectToAction("Index", "Comments", new { area = "Admin", id = model.TaskId });
            }
            model.Companies = _companyService.GetCompaniesForSelectList(); ;
            _appService.IziToast("Please fill all required areas!", "Warning", "warning");
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id, int taskId)
        {
            if (ModelState.IsValid)
            {
                var comment = _commentService.GetById(id);
                if (comment == null)
                {
                    ViewBag.ErrorMessage = $"Comment with Id={id} cannot be found";
                    _appService.IziToast($"Comment with Id={id} cannot be found", "Error", "error");
                    return View("404");
                }
                _commentService.Delete(comment);
                _appService.IziToast("", "", "");
                return RedirectToAction("Index", "Comments", new { area = "Admin", id = taskId });
            }
            return RedirectToAction("Index", "Comments", new { area = "Admin", id = taskId });
        }
    }
}
