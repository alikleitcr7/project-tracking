
const TEAMS_SERVICE_URI = (method) => `/teams/${method}`;

const TeamsService = {
    GetById: function (id, includeMembers = true) {

        const query = serialize({ id, includeMembers })

        const url = TEAMS_SERVICE_URI(`GetById?${query}`)

        return axios.get(url);
    },
    Add: function (team) {
        const url = TEAMS_SERVICE_URI(`Add`)

        return axios.post(url, team);
    },
    Save: function (team) {
        const url = TEAMS_SERVICE_URI(`Save`)

        return axios.post(url, team);
    },
    GetSupervisorLog: function (teamId) {

        const query = serialize({ teamId })

        const url = TEAMS_SERVICE_URI(`GetSupervisorLog?${query}`)

        return axios.get(url);
    },
    Update: function (team) {

        const url = TEAMS_SERVICE_URI(`Update`)

        return axios.put(url, team);
    },
    GetAll: function (includeMembersCount) {

        const query = serialize({ includeMembersCount })

        const url = TEAMS_SERVICE_URI(`GetAll?${query}`)

        return axios.get(url);
    },
    GetSupervisorTeamsModel: function (userId) {

        const query = serialize({ userId })

        const url = TEAMS_SERVICE_URI(`GetSupervisorTeamsModel?${query}`)

        return axios.get(url);
    },
    GetSupervisingTeamsModel: function (userId) {

        const query = serialize({ userId })

        const url = TEAMS_SERVICE_URI(`GetSupervisingTeamsModel?${query}`)

        return axios.get(url);
    },
    GetCurrentUserTeamsViewModel: function (userId) {

        const query = serialize({ userId })

        const url = TEAMS_SERVICE_URI(`GetCurrentUserTeamsViewModel?${query}`)

        return axios.get(url);
    },
    GetTeamViewModel: function (teamId) {

        const query = serialize({ teamId })

        const url = TEAMS_SERVICE_URI(`GetTeamViewModel?${query}`)

        return axios.get(url);
    },
    //GetAllExecludingSupervising: function (userId) {

    //    const query = serialize({ userId })

    //    const url = TEAMS_SERVICE_URI(`GetAllExecludingSupervising?${query}`)

    //    return axios.get(url);
    //},
    GetAllSupervisableTeams: function (userId) {

        const query = serialize({ userId })

        const url = TEAMS_SERVICE_URI(`GetAllSupervisableTeams?${query}`)

        return axios.get(url);
    },
    GetAllSupervisingTeamIds: function (userId) {

        const query = serialize({ userId })

        const url = TEAMS_SERVICE_URI(`GetAllSupervisingTeamIds?${query}`)

        return axios.get(url);
    },
    GetSupervisingTeamId: function (userId) {

        const query = serialize({ userId })

        const url = TEAMS_SERVICE_URI(`GetSupervisingTeamId?${query}`)

        return axios.get(url);
    },
    GetTeamUsers: function (teamId) {

        const url = TEAMS_SERVICE_URI(`GetTeamUsers?teamId=${teamId}`)

        return axios.get(url);
    },
    AddRemoveTeamsUsers: function (model) {

        const url = TEAMS_SERVICE_URI(`AddRemoveTeamsUsers`)

        return axios.post(url, model);
    },
    Delete: function (id) {

        const url = TEAMS_SERVICE_URI(`Delete?id=${id}`)

        return axios.delete(url);
    },
}