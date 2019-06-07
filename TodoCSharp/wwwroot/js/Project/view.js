import { querySelector } from '/js/CRUDFunctions/CRUD.js';
import { getElement } from './iterator.js';

export function View() {
    this.cloneDOM = document.getElementById('temp');
    this.main = document.getElementById('elements');
    this.form = document.getElementById('form');

    // Handles. Initialization in the controller
    this.onClick = null;
}

View.prototype = {
    createElement: createElementTodo,
    addTodo: addTodo,
    removeTodo: removeTodo,
    getFormData: getFormData,
    init: init
}

function init() {
    [this.form, this.main].forEach(value => {
        for (let elemnet of getElement(value)) {
            elemnet.addEventListener('click', this.onClick);
        }
    });
}

function addTodo(model) {
    this.createElement(model);
}

function removeTodo(element) {
    let id = element.getAttribute('id');
    let removeElement = document.getElementById(id);
    this.main.removeChild(removeElement);

    return id;
}

function createElementTodo(model) {
    let node,
        done,
        like,
        time,
        name;

    node = this.cloneDOM.firstElementChild.cloneNode(true);
    node.addEventListener('click', this.onClick);

    done = querySelector(node, '#done');
    like = querySelector(node, '#like');
    time = querySelector(node, '#time');
    name = querySelector(node, '#name');

    // settings clone
    node.setAttribute('id', model._id);
    done.checked = model.done ? 'checked' : '';

    like.innerHTML = model.like;
    time.innerHTML = model.time;
    name.innerHTML = model.name;
    this.main.appendChild(node);
}

function getFormData() {
    let data = {};
    let element = this.form.getElementsByTagName('textarea')[0];

    if(!element.value) {
        return alert('Необходимо ввести название задачи.');
    }

    data.name = element.value;
    element.value = '';

    return data;
}

