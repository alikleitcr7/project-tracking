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
    GetRoles: function () {

        const url = USERS_SERVICE_URI(`GetRoles`)

        return axios.get(url);
    },
    GetUserRole: function (userId) {

        const query = serialize({ userId })

        const url = USERS_SERVICE_URI(`GetUserRole?${query}`)

        return axios.get(url);
    },
    GetEmploymentTypes : function () {

        const url = USERS_SERVICE_URI(`GetEmploymentTypes`)

        return axios.get(url);
    },
    AddRemoveTeamsFromSupervisor: function (model) {

        const url = USERS_SERVICE_URI(`AddRemoveTeamsFromSupervisor`)

        return axios.post(url, model);
    },
    GetUsersByRole: function (roleCode) {

        const query = serialize({ roleCode })

        const url = USERS_SERVICE_URI(`GetUsersByRole?${query}`)

        return axios.get(url);
    },
    GetUsersByRoleKeyValue: function (roleCode) {

        const query = serialize({ roleCode })

        const url = USERS_SERVICE_URI(`GetUsersByRoleKeyValue?${query}`)

        return axios.get(url);
    },
    SetRole: function (userId, role) {

        const query = serialize({ userId, role })

        const url = USERS_SERVICE_URI(`SetRole?${query}`)

        return axios.put(url);
    },
    Delete: function (id) {

        const url = USERS_SERVICE_URI(`Delete?id=${id}`)

        return axios.delete(url);
    },
}