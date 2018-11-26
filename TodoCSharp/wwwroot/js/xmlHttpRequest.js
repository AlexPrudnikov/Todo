function create(url, data) {
    return new Promise((resolve, reject) => {
        let xhr = new XMLHttpRequest();

        xhr.open('POST', url);
        xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
        xhr.onload = function () {
            if (xhr.status === 200 && xhr.readyState === 4) {
                if(xhr.response) {
                    let data = JSON.parse(xhr.response);
                    resolve(data);    
                }
            }
            else {
                reject(xhr.statusText);
            }
        };

        xhr.send( encodeFormData(data) );
    });
}

function encodeFormData(data) {
    let pairs = [];

    for (let name in data) {
        if (data[name]) {
            let value = data[name].toString();
            name = encodeURIComponent(name.replace('%20', '+'));
            value = encodeURIComponent(value.replace('%20', '+'));
            pairs.push(name + '=' + value);
        }
    }

    return pairs.join('&');
}

function getUrl(url, method, id) {
    return new Promise((resolve, reject) => {
        let xhr = new XMLHttpRequest();
        xhr.open(method, url);

        if (method === 'POST') {
            xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
        } 

        xhr.onload = function () {
            if (xhr.status === 200 && xhr.readyState === 4) {

                let data = JSON.parse(xhr.response);

                if (data) { 
                    resolve(data);
                } 
            }
            else {
                reject(xhr.statusText);
            }
        };

        //xhr.onerror = error;
        xhr.send( encodeFormData(id) );
    });
}