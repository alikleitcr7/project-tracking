
const USERS_SERVICE_URI = (method) => `/users/${method}`;

const UsersService = {
    GetById: function (id) {
        const url = USERS_SERVICE_URI(`GetById?id=${id}`)

        return axios.get(url);
    },
    Save: function (user) {
        const url = USERS_SERVICE_URI(`Save`)

        return axios.post(url, user);
    },
    Search: function (keyword, page, countPerPage) {

        const query = serialize({ keyword, page, countPerPage })

        const url = USERS_SERVICE_URI(`Search?${query}`)

        return axios.get(url);
    },
    Delete: function (id) {

        const url = USERS_SERVICE_URI(`Delete?id=${id}`)

        return axios.delete(url);
    },
}