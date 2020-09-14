
const HOLIDAYS_SERVICE_URI = (method) => `/Holidays/${method}`;

const HolidaysService = {
    Create: function (holiday) {
        const url = HOLIDAYS_SERVICE_URI('Create')

        return axios.post(url, holiday);
    },
    Delete: function (id) {
        const url = HOLIDAYS_SERVICE_URI(`Delete?id=${id}`)

        return axios.delete(url);
    },
    Update: function (holiday) {

        const url = HOLIDAYS_SERVICE_URI('Update')

        return axios.put(url, holiday)
    },
    GetAll: function () {
        const url = HOLIDAYS_SERVICE_URI('GetAll')

        return axios.get(url);
    },
    GetAllPaged: function (page, countPerPage) {

        const query = serialize({ page, countPerPage })
        const url = HOLIDAYS_SERVICE_URI(`GetAllPaged?${query}`)

        return axios.get(url);
    },
}

