(function () {    
    let todos = document.getElementById('elements');
    for (let todo = todos.firstElementChild; todo !== null; todo = todo.nextElementSibling) {
	
        let temp = null;
        let task = new Task(null, false, false);
        for (let element = todo.firstElementChild; element !== null; element = element.nextElementSibling) {
      
            if (element.localName === 'label' && element.innerHTML === 'Выполнена') {
                continue;
            }

            if (element.localName === 'label') {
                task._id = todo.id;
                task.name = element.innerHTML;
                temp = element;

            } else if (element.localName === 'input') {
                task.accomlished = element.checked;

                if (element.checked) {
                    temp.style.textDecoration = 'line-through';
                } 

                addEventListener(task, element, task.handleReplaceCheckboxTodo);

            } else if (element.localName === 'button' && element.innerHTML === 'Изменить задачу') {
                addEventListener(task, element, task.handleCloseReplaceInput);
                addEventListener(task, element, task.handleReplaceTodo);
                
            } else if (element.localName === 'button' && element.innerHTML === 'Удалить задачу') {
                addEventListener(task, element, task.handleRemoveTodo);
            }
        }
    }
}());


