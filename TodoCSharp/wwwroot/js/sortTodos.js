(function () {
    let links = document.getElementsByTagName('a');
    [].forEach.call(links, link => {
        link.addEventListener('click', removeEventListener.bind(link));
        link.addEventListener('click', sort.bind(link));
    });
}());


function sort(event) {
    event.preventDefault();

    let url = `${this.getAttribute('href')}`;
    getUrl(url, 'POST')
        .then(data => createLinks(data))
        .then(todos => createTodos(todos));
}

function addEventListener(task, element, callback) {
    let name = callback.name;
    let value = `${task._id}_${name}`;

    element.setAttribute(`data-${name}`, value);
    Task.events[value] = function (event) { callback(task, event); }
    element.addEventListener('click', Task.events[value]);
}

function removeEventListener() {
    let elements = document.getElementById('elements');

    remove(elements, 'input');
    remove(elements, 'button');   
}

function remove(elements, tag) {
    let temp = elements.getElementsByTagName(tag);

    [].forEach.call(temp, item => {
        for (let value in item.dataset) {
            let event = item.dataset[value];

            item.removeEventListener('click', Task.events[event]);
            delete Task.events[event];
        }
    });
}

// Конструирование ссылок сортировки:
function createLinks(data) {
    let a = document.getElementsByTagName('a');

    for (let i = 0; i < a.length; i++) {
        let temp = a[i].getAttribute('href'),
            href = temp.split('=')[0],
            argument = temp.split('=')[1];

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
    
    for (let todo = todos.firstElementChild; index < data.length; todo = todo.nextElementSibling) {

        let task = createTask(data, index);
        todo.setAttribute('id', data[index].id);

        for (let item = todo.firstElementChild; item !== null; item = item.nextElementSibling) {

            if (item.localName === 'label' && item.getAttribute('class') === 'labelTask') {
                item.innerHTML = data[index].name;

                item.style.textDecoration =
                    data[index].accomlished === true ? 'line-through' : '';

            } else if (item.localName === 'input') {
                item.checked = 
                    data[index].accomlished === true ? true : false;

                addEventListener(task, item, task.handleReplaceCheckboxTodo);

            } else if (item.localName === 'button' && item.innerHTML === 'Изменить задачу') {
                addEventListener(task, item, task.handleCloseReplaceInput);
                addEventListener(task, item, task.handleReplaceTodo);

            } else if (item.localName === 'button' && item.innerHTML === 'Удалить задачу') {
                addEventListener(task, item, task.handleRemoveTodo);
            }
        }

        index += 1;
    }
}

// return new Task
function createTask(todos, index) {
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