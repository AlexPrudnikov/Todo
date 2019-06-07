import { Todo } from '../Project/model.js';

export function createElement(id, currentTarget, tag) {
    let currentElement = currentTarget.querySelector(id);
    let parentNode = currentElement.parentNode;
    let name = currentElement.innerHTML || currentElement.value;
    let checkbox = currentTarget.querySelector('#done');

    let newElement = document.createElement(tag);
    newElement.setAttribute('id', 'name');
    if (tag === 'input') {
        newElement.setAttribute('class', 'form-control');
        newElement.style.color = checkbox.checked ? 'darkgrey' : '#17a2b8';
        newElement.value = name;
    } else if (tag === 'p') {
        let checkbox = currentTarget.querySelector('#done');

        newElement.setAttribute('class', 'card-text');        
        newElement.innerHTML = name;
        styleTextDecorationAndColor(newElement, checkbox);
    }

    parentNode.replaceChild(newElement, currentElement);
};

export function createTodo(currentTarget) {
    let id = currentTarget.id;
    let name = currentTarget.querySelector('#name').innerHTML;
    let like = currentTarget.querySelector('#like').innerHTML;
    let done = currentTarget.querySelector('#done').checked ? true : false;

    return new Todo(id, name, like, done);
}

export function styleTextDecorationAndColor(element, { checked }){
    element.style.color = checked ? 'darkgrey' : '#17a2b8';
    element.style.textDecoration = checked ? 'line-through' : 'none';
}

export function querySelector(node, id) {
    return node.querySelector(id);
}
