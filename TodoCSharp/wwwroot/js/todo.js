let elementButton = document.getElementById('create');
elementButton.addEventListener('click', addTodo);

function Task(name, accomlished = false, ini = true) {
    this._id = Date.now();
    this.name = name;
    this.accomlished = accomlished;
    this.main = null;
    this.in = ini;

    if (this.in) this.init();
}

Task.prototype = {
    init,
    createElement,

    handleReplaceTodo,
    handleCloseReplaceInput,
    handleRemoveTodo,
    handleReplaceCheckboxTodo
};

function init() {
    this.main = document.getElementById('elements');

    let task = this.createElement({ name: this.name, tag: 'label', attributes: { class: 'labelTask' } });
    let label = this.createElement({ name: 'Выполнена', tag: 'label', attributes: {} });
    let checkbox = this.createElement({ name: '', tag: 'input', attributes: { type: 'checkbox' }, events:{ preplaceCheckbox: this.handleReplaceCheckboxTodo.bind(this)} });
    let buttonReplace = this.createElement({ name: 'Изменить задачу', tag: 'button', attributes: { type: 'submit', id: 'replaceAndSave' }, events: { closeReplaceInput: this.handleCloseReplaceInput.bind(this), replaceTodo: this.handleReplaceTodo.bind(this)} });
    let buttonRemove = this.createElement({ name: 'Удалить задачу', tag: 'button', attributes: { type: 'submit' }, events: { removeTodo: this.handleRemoveTodo.bind(this)} });

    let div = document.createElement('div');
    div.id = this._id;

    let elements = [task, label, checkbox, buttonReplace, buttonRemove];
    elements.forEach((element) => div.appendChild(element));
    this.main.appendChild(div);
}

// Изменения задачи
function handleReplaceTodo(event) {
    event.preventDefault();
    let todo = document.getElementById(this._id),
        button = todo.getElementsByTagName('button')[0];

    if (button.innerHTML === 'Изменить задачу') {
        button.innerHTML = 'Сохранить задачу';
        replace.call(this, { todo: todo, class: '.labelTask', type: 'input', attributes: { class: 'inputReplace' } });
    } else if (button.innerHTML === 'Сохранить задачу') {
        button.innerHTML = 'Изменить задачу';
        replace.call(this, { todo: todo, class: '.inputReplace', type: 'label', attributes: { class: 'labelTask' } });

        getUrl('Home/Edit', 'POST', this)
            .then(() => console.log('Задача отредоктирована'))
            .catch(error => console.error(`Error: handleReplaceTodo ${error}`));
    }
}

// Закрывает предыдущий input
function handleCloseReplaceInput(event) {
    event.preventDefault();

    let temp,
        elements = document.getElementById('elements');
    for (let todos = elements.firstChild; todos !== null; todos = todos.nextSibling) {
        if (testNodeType(todos)) continue;
        for (let element = todos.firstChild; element !== null; element = element.nextSibling) {
            if (testNodeType(element)) continue;
        
            // todos.id !== this_id - Если это не предыдущая задача, а текущая прерываем поиск
            if (element.localName === 'input' && element.type !== 'checkbox' && todos.id !== this._id) {
                temp = element;
            } else if (element.localName === 'button' && element.innerHTML === 'Сохранить задачу' && todos.id !== this._id) {
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
function handleReplaceCheckboxTodo() {
    let main = document.getElementById(this._id),
        label = main.getElementsByTagName('label')[0],
        checkbox = main.getElementsByTagName('input')[0];

    // Выполнена ли задача
    if (!this.accomlished) {
        checkbox.checked = 'checked';
        label.style.textDecoration = 'line-through';
    } else if (this.accomlished) {
        checkbox.checked = '';
        label.style.textDecoration = 'none';
    }

    this.accomlished = checkbox.checked;

    // Отправляем на сервер выполнена ли задача
    getUrl('Home/Edit', 'POST', this)
        .then(() => console.log('Задача отредоктирована'))
        .catch(error => console.error(`Erorr handleReplaceCheckboxTodo: ${error}`));

}

// Удаление задачи
function handleRemoveTodo() {
    let todo = document.getElementById(this._id);
    todo.parentNode.removeChild(todo);

    // Удаление задачи на сервере
    getUrl('Home/Delete', 'POST', this)
        .then(() => console.log('Задача удалена'))
        .catch(error => console.error(`Error handleRemoveTodo: ${error}`));

}

// Создание элемента
function createElement(values) {
    let element = document.createElement(values.tag);
    element.innerHTML = values.name;

    // Атрибуты
    for (let attribute in values.attributes) {
        element.setAttribute(attribute, values.attributes[attribute]);
    }

    // События
    if(values.events) {
        for(let e in values.events) {
            element.addEventListener('click', values.events[e]);
            element.removeEventListener('click', values.events[e]); // ??? нужно ли удалять событие
        }
    }

    return element;
}

// Добавление задачи
function addTodo(event) {
    event.preventDefault();

    let input = document.getElementById('mainInput');
    let name = input.value;
    input.value = '';

    // Сохраняем данные на сервере
    create('Home/Create', new Task(name))
        .then(() => console.log('Задача успешно создана'))
        .catch(error => console.error(`Error: addTodo ${error}`));
}

// Изменение задачи
function replace(elements) {
    let element = elements.todo.querySelector(elements.class);
    let newElement = document.createElement(elements.type);

    // Добавление атрибутов к елементу
    for (let attribute in elements.attributes) {
        newElement.setAttribute(attribute, elements.attributes[attribute]);
    }

    // В зависимости какой елемент input/label
    if (newElement.getAttribute('class') === 'inputReplace') {
        newElement.value = this.name;
    } else if (newElement.getAttribute('class') === 'labelTask') {
        newElement.innerHTML = element.value;
        this.name = element.value;
    }

    // Заменяем элемент
    element.replaceWith(newElement);
}



