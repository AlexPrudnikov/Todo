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

        public async Task<IEnumerable<TodoViewModel>> GetUserTodos(String id, SortState sortOrder = SortState.NameAsc)
        {
            // Если нужно будет подтянуть данные из другой таблицы, используем метод 'Include(x => x.Из-другой-таблицы)'
            //IQueryable<Todo> todos = db.Todos.Include(x => x.Style);
            //IQueryable<Todo> todos = db.Todos.Include(x => x);
            //IEnumerable<Todo> todos = await db.Todos.ToListAsync();
            IEnumerable<TodoViewModel> todos = GetTodoViewModels(await todoDao.GetUserTodosAsync(id));
            var sort = new Dictionary<SortState, Action>
            {
                { SortState.NameAsc, () => todos = todos.OrderBy(s => s.Name) },
                { SortState.NameDesc, () => todos = todos.OrderByDescending(s => s.Name) },
                { SortState.DoneAsc, () => todos = todos.OrderBy(s => s.Done) },
                { SortState.DoneDesc, () => todos = todos.OrderByDescending(s => s.Done) }
            };

            sort[sortOrder]();

            return todos;
        }

        // TODO: new method
        public async Task<Int32> GetTodo(String userId, Int32 todoId)
        {
            Todo todo = await todoDao.GetTodoAsync(todoId);
            Like like = todo.Likes.FirstOrDefault(l => l.ApplicationUserId == userId);
            List<Like> likes = todo.Likes.ToList();

            if (like != null)
            {
                likes.Remove(like);
            }
            else
            {
                likes.Add(new Like() { ApplicationUserId = userId });
            }

            todo.Likes = likes;
            await todoDao.EditAsync(todo);

            return todo.Likes.Count();
        }

        public async Task<IEnumerable<TodoViewModel>> GetPublicTodos()
        {
            var todos = await todoDao.GetAllTodosAsync();
            var todosList = todos.Where(todo => todo.Public == true)
                                 .Select(todoPublic => new TodoViewModel()
                                 {
                                     Id = todoPublic.TodoId,
                                     Name = todoPublic.Name,
                                     Done = todoPublic.Done,
                                     Public = todoPublic.Public,
                                     Time = todoPublic.Time,
                                     Likes = todoPublic.Likes,
                                     ApplicationUserId = todoPublic.ApplicationUserId
                                 });

            return todosList;
        }

        public async Task<Int32> Create(String id, Todo todo)
        {
            todo.Time = DateTime.Now;
            return await todoDao.CreateAsync(id, todo);
        }

        public async Task Remove(Int32? id) =>
            await todoDao.RemoveAsync(id);

        public async Task RemoveAll(String id) =>
            await todoDao.RemoveAllAsync(id);

        public async Task Edit(Todo todo)
        {
            todo.Time = DateTime.Now;
            await todoDao.EditAsync(todo);
        }
        

        private IEnumerable<TodoViewModel> GetTodoViewModels(IEnumerable<Todo> todos) =>
            todos.Select(obj => new TodoViewModel()
            {
                Id = obj.TodoId,
                Name = obj.Name,
                Done = obj.Done,
                Public = obj.Public,
                ApplicationUserId = obj.ApplicationUserId,
                Time = obj.Time,
                Likes = obj.Likes
            });

    }
}
