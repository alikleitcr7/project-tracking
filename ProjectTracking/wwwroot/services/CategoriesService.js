
const CATEGORIES_SERVICE_URI = (method) => `/categories/${method}`;

const CategoriesService = {
    GetById: function (id) {
        const url = CATEGORIES_SERVICE_URI(`GetById?id=${id}`)

        return axios.get(url);
    },
    Add: function (category) {
        const url = CATEGORIES_SERVICE_URI(`Add`)

        return axios.post(url, category);
    },
    Update: function (category) {

        const url = CATEGORIES_SERVICE_URI(`Update`)

        return axios.put(url, category);
    },
    GetAll: function () {

        const url = CATEGORIES_SERVICE_URI(`GetAll`)

        return axios.get(url);
    },
    Delete: function (id) {

        const url = CATEGORIES_SERVICE_URI(`Delete?id=${id}`)

        return axios.delete(url);
    },
}