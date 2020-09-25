
const ADMINS_SERVICE_URI = (method) => `/admin/${method}`;

const AdminsService = {

    GetAllUsersKeyValues: function () {

        const url = ADMINS_SERVICE_URI(`GetAllUsersKeyValues`)

        return axios.get(url);
    },
    GetAllUsersExecludeTeamSupervisors: function (teamId) {

        const query = serialize({ teamId })

        const url = ADMINS_SERVICE_URI(`GetAllUsersExecludeTeamSupervisors?${query}`)

        return axios.get(url);
    },
}