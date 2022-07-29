import axios from 'axios';

const BASE_URL = 'https://localhost:7137';

const getById = (resource, id) => {
    return axios
        .get(`${BASE_URL}/${resource}/${id}`)
        .then(handleResponse);
};

const post = (resource, data) => {
    return axios
        .post(`${BASE_URL}/${resource}`, data)
        .then(handleResponse);
};

const handleResponse = (response) => {
    if (response.data) {
        return response.data;
    }

    return response;
}

export const apiClient = {
    getById,
    post
};