using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoCSharp.Models;
using TodoCSharp.TodoDao;
using TodoCSharp.TodoStyleDao;
using TodoCSharp.ViewModels;

namespace TodoCSharp.TodoPresentationService
{
    public class TodoPresentationService : ITodoPresentationService
    {
        private readonly ITodoDao todoDao;
        private readonly ITodoStyleDao todoStyleDao;
        public TodoPresentationService(ITodoDao todoDao, ITodoStyleDao todoStyleDao)
        {
            this.todoDao = todoDao;
            this.todoStyleDao = todoStyleDao;
        }

        public async Task<IEnumerable<TodoViewModel>> GetTodos(String id, SortState sortOrder = SortState.NameAsc)
        {
            // Если нужно будет подтянуть данные из другой таблицы, используем метод 'Include(x => x.Из-другой-таблицы)'
            //IQueryable<Todo> todos = db.Todos.Include(x => x.Style);
            //IQueryable<Todo> todos = db.Todos.Include(x => x);
            //IEnumerable<Todo> todos = await db.Todos.ToListAsync();
            IEnumerable<TodoViewModel> todos = GetTodoViewModels(await todoDao.GetTodosAsync(id));
            var sort = new Dictionary<SortState, Action>
            {
                { SortState.NameAsc, () => todos = todos.OrderBy(s => s.Name) },
                { SortState.NameDesc, () => todos = todos.OrderByDescending(s => s.Name) },
                { SortState.AccomlishedAsc, () => todos = todos.OrderBy(s => s.Accomlished) },
                { SortState.AccomlishedDesc, () => todos = todos.OrderByDescending(s => s.Accomlished) }
            };

            sort[sortOrder]();

            return todos;
        }

        public async Task Create(String id, Todo todo) =>
            await todoDao.CreateAsync(id, todo);

        public async Task Remove(Int32? id) =>
            await todoDao.RemoveAsync(id);

        public async Task RemoveAll(String id) =>
            await todoDao.RemoveAllAsync(id);

        public async Task Edit(Todo todo) =>
            await todoDao.EditAsync(todo);

        private IEnumerable<TodoViewModel> GetTodoViewModels(IEnumerable<Todo> todos) =>
            todos.Select(obj => new TodoViewModel() { Id = obj.TodoId, Name = obj.Name, Accomlished = obj.Accomlished });
    }
}
