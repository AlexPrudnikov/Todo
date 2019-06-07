export function getElement(elements) {
    let current = elements.firstElementChild;

    return {
        [Symbol.iterator]() {
            return {
                next() {
                    let result = { value: undefined, done: true }

                    if (current !== null) {
                        result.value = current;
                        result.done = false;
                        current = current.nextElementSibling;
                    }

                    return result;
                }
            };
        }
    };
}

export function* getElement2(elements) {
    let current = elements.firstElementChild;

    while (current !== null) {
        yield current;
        current = current.nextElementSibling;
    }
}
