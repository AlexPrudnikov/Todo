(function () {
    let todos = document.getElementsByTagName('tbody')[0];
    for (let todo = todos.firstElementChild; todo !== null; todo = todo.nextElementSibling) {
	
        let temp = null;
        let task = new Task(null, false, false);
        for (let element = todo.firstElementChild; element !== null; element = element.nextElementSibling) {

            if (element.firstElementChild.localName === 'label') {
                task._id = todo.id;
                task.name = element.firstElementChild.innerHTML;
                temp = element.firstElementChild;

            } else if (element.firstElementChild.localName === 'input') {
                task.accomlished = element.firstElementChild.checked;

                if (element.firstElementChild.checked) {
                    temp.style.textDecoration = 'line-through';
                } 

                addEventListener(task, element.firstElementChild, task.handleReplaceCheckboxTodo);

            } else if (element.firstElementChild.localName === 'button' && element.firstElementChild.innerHTML === 'Replace') {
                addEventListener(task, element.firstElementChild, task.handleCloseReplaceInput);
                addEventListener(task, element.firstElementChild, task.handleReplaceTodo);

            } else if (element.firstElementChild.localName === 'button' && element.firstElementChild.innerHTML === 'Delete') {
                addEventListener(task, element.firstElementChild, task.handleRemoveTodo);
            }
        }
    }
}());


