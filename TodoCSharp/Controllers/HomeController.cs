using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoCSharp.Models;
using TodoCSharp.TodoErrorPresentationService;
using TodoCSharp.TodoPresentationService;
using TodoCSharp.Infrastructure;

namespace TodoCSharp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ITodoPresentationService todoService;
        private readonly ITodoErrorPresentationService todoErrorService;
        public HomeController(ITodoPresentationService todoService, ITodoErrorPresentationService todoErrorService)
        {
            this.todoService = todoService;
            this.todoErrorService = todoErrorService;
        }


        /// <summary>
        /// Создать задачу:
        /// </summary>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Create(SortState sortOrder = SortState.NameAsc)
        {
            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["AccomlishedSort"] = sortOrder == SortState.AccomlishedAsc ? SortState.AccomlishedDesc : SortState.AccomlishedAsc;

            String currentUser = User.GetUserId();
            return View(await todoService.GetTodos(currentUser, sortOrder));
        }

        [HttpPost]
        public async Task Create(Todo todo)
        {
            String currentUser = User.GetUserId();
            await todoService.Create(currentUser, todo);
        }

        /// <summary>
        /// Ззапрос из JavaScript:
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> ReturnTodo(SortState sortOrder = SortState.NameAsc)
        {
            ModelFoJs temp = null;
            String currentUser = User.GetUserId();

            if (currentUser != null)
            {
                temp = new ModelFoJs
                {
                    NameSort = sortOrder == SortState.NameAsc ? nameof(SortState.NameDesc) : nameof(SortState.NameAsc),
                    AccomlishedSort = sortOrder == SortState.AccomlishedAsc ? nameof(SortState.AccomlishedDesc) : nameof(SortState.AccomlishedAsc),
                    Todos = await todoService.GetTodos(currentUser, sortOrder)
                };
            }
            
            return Json(temp);
        }


        /// <summary>
        /// Удалить задачу:
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Delete([FromHeader(Name = "_id")]Int32 id)
        {
            await todoService.Remove(id);
        }

        /// <summary>
        /// Редактировать задачу: 
        /// </summary>
        /// <param name="todo"></param> 
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> Edit(Todo todo)
        {
            await todoService.Edit(todo);
            return Json(todo);
        }

        /// <summary>
        /// Сохраняем ошибки которые произошли
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public async Task Error(TodoError error)
        {
            await todoErrorService.Create(error);
        }
    }
}