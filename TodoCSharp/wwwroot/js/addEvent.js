(function () {
    function Task() {
        this._id;
        this.name;
        this.accomlished;
    }

    // Мжно запиисать так: 
    Task.prototype = {
        handleRemoveTodo,
        handleReplaceTodo,
        handleCloseReplaceInput,
        handleReplaceCheckboxTodo
    };
    
    let todos = document.getElementById('elements');
    for (let todo = todos.firstChild; todo !== null; todo = todo.nextSibling) {
        if (testNodeType(todo)) continue;

        let temp = null;
        let task = new Task();
        for (let element = todo.firstChild; element !== null; element = element.nextSibling) {
            if (element.localName === 'label' &&
                element.innerHTML === 'Выполнена' ||
                testNodeType(element)) continue;

            if (element.localName === 'label') {
                task._id = todo.id;
                task.name = element.innerHTML;

                temp = element;
            } else if (element.localName === 'input') {
                task.accomlished = element.checked;
                if (element.checked) temp.style.textDecoration = 'line-through';
                element.addEventListener('click', task.handleReplaceCheckboxTodo.bind(task));
            } else if (element.localName === 'button' && element.innerHTML === 'Изменить задачу') {
                element.addEventListener('click', task.handleCloseReplaceInput.bind(task));
                element.addEventListener('click', task.handleReplaceTodo.bind(task));
            } else if (element.localName === 'button' && element.innerHTML === 'Удалить задачу') {
                element.addEventListener('click', task.handleRemoveTodo.bind(task));
            }
        }
    }
}());


