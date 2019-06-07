using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoCSharp.Models;
using TodoCSharp.TodoErrorPresentationService;
using TodoCSharp.TodoPresentationService;
using TodoCSharp.Infrastructure;
using System.Diagnostics;

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

        [HttpGet]
        public async Task<IActionResult> GetPublicTodoList()
        {
            var todos = await todoService.GetPublicTodos();
            return View("~/Views/Home/GetPublicTodoList.cshtml", todos);
        }

        /// <summary>
        /// Создать задачу:
        /// </summary>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Create(SortState sortOrder = SortState.NameAsc)
        {
            ViewData["NameSort"] = (sortOrder == SortState.NameAsc)
                ? SortState.NameDesc
                : SortState.NameAsc;

            ViewData["DoneSort"] = (sortOrder == SortState.DoneAsc)
                ? SortState.DoneDesc
                : SortState.DoneAsc;

            String currentUser = User.GetUserId();
            return View("~/Views/Home/Create.cshtml", await todoService.GetUserTodos(currentUser, sortOrder));
        }

        [HttpPost]
        public async Task<Int32> Create(Todo todo)
        {
            String currentUser = User.GetUserId();
            return await todoService.Create(currentUser, todo);
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

            temp = new ModelFoJs
            {
                // Сортировка по 'имени задачи'
                NameSort = (sortOrder == SortState.NameAsc)
                ? nameof(SortState.NameDesc)
                : nameof(SortState.NameAsc),

                // Сортировака по 'выолнена ли задача'
                AccomlishedSort = (sortOrder == SortState.DoneAsc)
                ? nameof(SortState.DoneDesc)
                : nameof(SortState.DoneAsc),

                Todos = await todoService.GetUserTodos(currentUser, sortOrder)
            };

            return Json(temp);
        }

        [HttpPost]
        public async Task<JsonResult> LikeTodo([FromHeader(Name = "id")]Int32 id)
        {
            String currentUser = User.GetUserId();
            Int32 count = await todoService.GetTodo(currentUser, id);
            return Json(count);
        }

        /// <summary>
        /// Удалить задачу:
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> Delete([FromHeader(Name = "id")]Int32 id)
        {
            Debug.WriteLine($"Id {id}");
            await todoService.Remove(id);
            Debug.WriteLine("return"); 
            return Json(1);
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
        public async Task Error(TodoError error) =>
            await todoErrorService.Create(error);
    }
}