document.getElementById('create').addEventListener('click', addTodo);

let Task = (function () {
    let NewTask;

    NewTask = function (name, accomlished = false, ini = true) 
        this._id = 0;                                 
        this.name = name;
        this.accomlished = accomlished;
        this.main = null;
        this.in = ini;

        if (this.in) {
            this.init();
        }
    };

    // Сохраняем обработчики событий для их удяления
    NewTask.events = {};

    NewTask.prototype = {
        init,
        createElement,

        handleReplaceTodo,
        handleCloseReplaceInput,
        handleRemoveTodo,
        handleReplaceCheckboxTodo
    };

    NewTask.prototype.getId = function () {
        return this._id;
    };

    return NewTask;

}());

function init() {
    this.main = document.getElementById('elements');

    let task = this.createElement({ name: this.name, tag: 'label', attributes: { class: 'labelTask' } }),
        label = this.createElement({ name: 'Done', tag: 'label', attributes: {} }),
        checkbox = this.createElement({ name: '', tag: 'input', attributes: { type: 'checkbox' }, events: { replaceCheckbox: this.handleReplaceCheckboxTodo.bind(this, this) } }),
        buttonReplace = this.createElement({ name: 'Replace', tag: 'button', attributes: { type: 'submit', id: 'replaceAndSave' }, events: { closeReplaceInput: this.handleCloseReplaceInput.bind(this, this), replaceTodo: this.handleReplaceTodo.bind(this, this) } }),
        buttonRemove = this.createElement({ name: 'Delete', tag: 'button', attributes: { type: 'submit' }, events: { removeTodo: this.handleRemoveTodo.bind(this, this) } });

    let div = document.createElement('div');
    div.id = this.getId();

    let elements = [task, label, checkbox, buttonReplace, buttonRemove];
    elements.forEach((element) => div.appendChild(element));
    this.main.appendChild(div);
}

// Изменения задачи
function handleReplaceTodo(task, event) {
    event.preventDefault();

    let todo = document.getElementById(task.getId()),
        button = todo.getElementsByTagName('button')[0];

    if (button.innerHTML === 'Replace') {
        button.innerHTML = 'Сохранить задачу';
        replace.call(task, { todo: todo, class: '.labelTask', type: 'input', attributes: { class: 'inputReplace' } });
    } else if (button.innerHTML === 'Сохранить задачу') {
        button.innerHTML = 'Replace';
        replace.call(task, { todo: todo, class: '.inputReplace', type: 'label', attributes: { class: 'labelTask' } });

        getUrl('Edit', 'POST', task)
            .then(() => console.log('Задача отредоктирована'))
            .catch(error => console.error(`Error: handleReplaceTodo ${error}`));
    }
}

// Закрывает предыдущий input
function handleCloseReplaceInput(task, event) {
    event.preventDefault();

    let temp,
        elements = document.getElementById('elements'),
        elementId = 0;

    for (let todos = elements.firstElementChild; todos !== null; todos = todos.nextElementSibling) {
        elementId = todos.getAttribute('id');
        for (let element = todos.firstElementChild; element !== null; element = element.nextElementSibling) {

            // todos.id !== this_id - Если это не предыдущая задача, а текущая прерываем поиск
            if (element.localName === 'input' && element.type !== 'checkbox' && elementId !== task.getId()) {
                temp = element;
            } else if (element.localName === 'button' && element.innerHTML === 'Сохранить задачу' && elementId !== task.getId()) {
                element.innerHTML = 'Изменить задачу';

                // Конструируем label
                let label = document.createElement('label');
                label.setAttribute('class', 'labelTask');
                label.innerHTML = temp.value;
                temp.replaceWith(label);
            }
        }
    }
}

// Выполнена ли задача
function handleReplaceCheckboxTodo(task) {
    let main = document.getElementById(task.getId()),
        label = main.getElementsByTagName('label')[0],
        checkbox = main.getElementsByTagName('input')[0];

    // Выполнена ли задача
    label.style.textDecoration = checkbox.checked === true ? 'line-through' : 'none';

    task.accomlished = checkbox.checked;

    // Отправляем на сервер выполнена ли задача
    getUrl('Edit', 'POST', task)
        .then(() => console.log('Задача отредоктирована'))
        .catch(error => console.error(`Erorr handleReplaceCheckboxTodo: ${error}`));

}

// Удаление задачи
function handleRemoveTodo(task, event) {
    event.preventDefault();

    let todo = document.getElementById(task.getId());
    todo.parentNode.removeChild(todo);

    // Удаление задачи на сервере
    getUrl('Delete', 'POST', { _id: task.getId() })
        .then(() => console.log('Задача удалена'))
        .catch(error => console.error(`Error handleRemoveTodo: ${error}`));

}

// Создание элемента
function createElement(values) {
    let element = document.createElement(values.tag);
    element.innerHTML = values.name;

    // Атрибуты
    for (let att in values.attributes) {
        element.setAttribute(att, values.attributes[att]);
    }

    // События
    if (values.events) {
        for (let event in values.events) {
            element.addEventListener('click', values.events[event]);
        }
    }

    return element;
}

// Добавление задачи
function addTodo(event) {
    event.preventDefault();

    let input = document.getElementById('mainInput'),
        name = input.value;
    input.value = '';

    let task = new Task(name);
    // Сохраняем данные на сервере
    create('Create', task)
        .then(() => console.log('Задача успешно создана'))
        .catch(error => console.error(`Error: addTodo ${error}`));
}

// Изменение задачи
function replace(elements) {
    let element = elements.todo.querySelector(elements.class),
        newElement = document.createElement(elements.type);

    // Добавление атрибутов к елементу
    for (let att in elements.attributes) {
        newElement.setAttribute(att, elements.attributes[att]);
    }

    // В зависимости какой елемент input/label
    if (elements.type === 'input') {
        newElement.value = this.name;
    } else if (elements.type === 'label') {
        newElement.innerHTML = element.value;
        this.name = element.value;
    }

    element.replaceWith(newElement);
}