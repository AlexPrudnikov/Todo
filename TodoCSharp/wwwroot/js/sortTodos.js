(function () {
    let a = document.getElementsByTagName('a');
    [].forEach.call(a, element => element.addEventListener('click', sort.bind(element)));
}());


function sort(event) {
    event.preventDefault();

    let url = `${this.getAttribute('href')}`;
    getUrl(url, 'POST')
        .then(data => createLinks(data))
        .then(todos => createTodos(todos));
}

// Конструирование ссылок сортировки:
function createLinks(data) {
    console.log(data);
    let a = document.getElementsByTagName('a');

    for (let i = 0; i < a.length; i++) {
        let temp = a[i].getAttribute('href');
        let href = temp.split('=')[0];
        let argument = temp.split('=')[1];

        let result;
        if (argument === 'NameAsc' || argument === 'NameDesc') {
            result = `${href}=${data.nameSort}`;
        } else if (argument === 'AccomlishedAsc' || argument === 'AccomlishedDesc') {
            result = `${href}=${data.accomlishedSort}`;
        }

        a[i].setAttribute('href', result);
    }

    return data.todos;
}

// Создание задач:
function createTodos(data) {
    let index = 0;
    let todos = document.getElementById('elements');
    console.log(data);

    for (let todo = todos.firstChild; index < data.length; todo = todo.nextSibling) {
        if (testNodeType(todo)) continue;

        let task = returnNewTask(data, index);
        for (let item = todo.firstChild; item !== null; item = item.nextSibling) {
            if (testNodeType(item)) continue;

            item.parentNode.setAttribute('id', data[index].id);

            if (item.localName === 'label' && item.getAttribute('class') === 'labelTask') {
                item.innerHTML = data[index].name;
                if (data[index].accomlished) {
                    item.style.textDecoration = 'line-through';
                } else {
                    item.style.textDecoration = '';
                }
            } else if (item.localName === 'label') {
                continue;
            } else if (item.localName === 'input') {
                if (data[index].accomlished) {
                    item.checked = 'checked';
                } else {
                    item.checked = '';
                }

            } else if (item.localName === 'button' && item.innerHTML === 'Изменить задачу') {
                item.addEventListener('click', task.handleReplaceTodo.bind(task));
            } else if (item.localName === 'button' && item.innerHTML === 'Удалить задачу') {
                item.addEventListener('click', task.handleRemoveTodo.bind(task));
            }

        }

        index += 1;
    }
}

// nodeType
function testNodeType({ nodeType }) {
    return nodeType === 3 ? true : false;
}

// return new Task
function returnNewTask(todos, index) {
    let task = new Task(null, false, false);
    task._id = todos[index].id;
    task.name = todos[index].name;
    task.accomlished = todos[index].accomlished;

    return task;
}

// Error
function error(error) {
    console.error(error);
}