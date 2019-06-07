import { create, getUrl } from './xmlHttpRequest.js';
import { Todo } from './model.js';
import { createElement, createTodo, styleTextDecorationAndColor } from '/js/CRUDFunctions/CRUD.js';
//import { createTodo } from '/js/CRUDFunctions/CRUD.js';

export function ControllerTodo(view) {
    this.view = view;
};

ControllerTodo.prototype.initialize = function initialize() {
    this.view.onClick = onClick.bind(this);
    this.view.init();
};

ControllerTodo.prototype.handles = {
    handleAddTodo: handleAddTodo,
    handleRemoveTodo: handleRemoveTodo,
    handleReplaceTodo: handleReplaceTodo,
    handleRemoveTodo: handleRemoveTodo,
    handleLikeTodo: handleLikeTodo,
    handleCheckboxTodo: handleCheckboxTodo
}

function onClick(event) {
    let target = event.target;
    let action = target.getAttribute('data-handle');

    if (action) {
        this.handles[action](event, this); // this - в данном случае это context Controller
    }
};

function handleAddTodo(event, self) {
    event.preventDefault();

    let data = self.view.getFormData();           
    let model = new Todo(0, data.name);

    create('Home/Create', model)
        .then((id) => self.view.addTodo(new Todo(id, model.name))) 
        .then(() => console.log('Add todo'))                       
        .catch(error => console.error(error));    
};

function handleRemoveTodo({ currentTarget }, self) {
    let targetElem = currentTarget;
    let id = self.view.removeTodo(targetElem);

    getUrl('Home/Delete', 'POST', { id: id })
        .then(() => console.log('Remove todo'))
        .catch(error => console.error(error));
};

function handleReplaceTodo(event) {
    let target = event.target;
    let currentTarget = event.currentTarget;

    if (target.innerHTML === 'Replace') {
        target.innerHTML = 'Save';
        createElement('#name', currentTarget, 'input');
    } else if (target.innerHTML === 'Save') {
        target.innerHTML = 'Replace';
        createElement('#name', currentTarget, 'p');

        let todo = createTodo(currentTarget);

        getUrl('Home/Edit', 'POST', todo)
            .then(() => console.log('Edit Todo'))
            .catch(error => console.error(error));
    }
};

function handleCheckboxTodo(event) {
    let target = event.target;
    let currentTarget = event.currentTarget;

    let todo = createTodo(currentTarget);
    getUrl('Home/Edit', 'POST', todo)
        .then(() => console.log('Edit Todo'))
        .catch(error => console.error(error));

    // TODO: Вынести в отдельную функцию 1.
    let p = currentTarget.querySelector('#name');
    styleTextDecorationAndColor(p, target);
}

function handleLikeTodo(event) {
    let currentTarget = event.currentTarget;

    let like = currentTarget.querySelector('#like');

    getUrl('Home/LikeTodo', 'POST', { id: currentTarget.id })
        .then((count) => { like.innerHTML = count })
        .then(() => console.log('AddLike'))
        .catch(error => console.error(error));
}