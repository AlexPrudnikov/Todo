import { ControllerTodo } from './/controller.js';
import { View } from './view.js';

let viewTodo = new View();
let controller = new ControllerTodo(viewTodo);
controller.initialize();