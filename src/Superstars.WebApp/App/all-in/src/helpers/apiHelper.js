import AuthService from '../services/UserApiService';

async function checkErrors(resp) {
    if(resp.ok) return resp;

    let errorMsg = `ERROR ${resp.status} (${resp.statusText})`;
    let serverText = await resp.text();
    if(serverText) errorMsg = `${errorMsg}: ${serverText}`;

    var error = new Error(errorMsg);
    error.response = resp;
    throw error;
}

async function toJSON(resp) {
    const result = await resp.text();
    if(result) return JSON.parse(result);
}

async function toString(resp) {
     return await resp.text();
}


export async function postAsync(url, data) {
    return await fetch(url, {
        method: 'POST',
        body: JSON.stringify(data),
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${AuthService.accessToken}`
        }
    })
    .then(checkErrors);
}

export async function putAsync(url, data) {
    return await fetch(url, {
        method: 'PUT',
        body: JSON.stringify(data),
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${AuthService.accessToken}`
        }
    })
    .then(checkErrors)
    .then(toJSON);
}

export async function getAsync(url) {
    return await fetch(url, {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${AuthService.accessToken}`
        }
    })
    .then(checkErrors)
    .then(toJSON);
}

export async function getboolasync(url) {
    return await fetch(url, {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${AuthService.accessToken}`
        }
    })
    .then(checkErrors);
}


export async function getAsyncNoJSON(url) {
    return await fetch(url, {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${AuthService.accessToken}`
        }
    })
    .then(checkErrors)
    .then(toString);
}

export async function deleteAsync(url) {
    return await fetch(url, {
        method: 'DELETE',
        headers: {
            'Authorization': `Bearer ${AuthService.accessToken}`
        }
    })
    .then(checkErrors)
    .then(toJSON);
}