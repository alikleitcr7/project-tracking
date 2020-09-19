
const TEAMS_SERVICE_URI = (method) => `/teams/${method}`;

const TeamsService = {
    GetById: function (id) {
        const url = TEAMS_SERVICE_URI(`GetById?id=${id}`)

        return axios.get(url);
    },
    Add: function (team) {
        const url = TEAMS_SERVICE_URI(`Add`)

        return axios.post(url, team);
    },
    Update: function (team) {

        const url = TEAMS_SERVICE_URI(`Update`)

        return axios.put(url, team);
    },
    GetAll: function () {

        const url = TEAMS_SERVICE_URI(`GetAll`)

        return axios.get(url);
    },
    Delete: function (id) {

        const url = TEAMS_SERVICE_URI(`Delete?id=${id}`)

        return axios.delete(url);
    },
}