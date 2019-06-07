export function Todo(id, name, like = 0, done = false) {
    this._id = id; 
    this.name = name;
    this.time = new Date();
    this.like = like;
    this.done = done;
}