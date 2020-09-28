
const PROJECTS_SERVICE_URI = (method) => `/projects/${method}`;

const ProjectsService = {
    GetById: function (id) {
        const url = PROJECTS_SERVICE_URI(`GetById?id=${id}`)

        return axios.get(url);
    },
    Save: function (project) {
        const url = PROJECTS_SERVICE_URI(`Save`)

        return axios.post(url, project);
    },
    Search: function (categoryId, keyword, page, countPerPage) {

        const query = serialize({ categoryId, keyword, page, countPerPage})

        const url = PROJECTS_SERVICE_URI(`Search?${query}`)

        return axios.get(url);
    },
    GetByTeam: function (teamId) {

        const query = serialize({ teamId})

        const url = PROJECTS_SERVICE_URI(`GetByTeam?${query}`)

        return axios.get(url);
    },
    Delete: function (id) {

        const url = PROJECTS_SERVICE_URI(`Delete?id=${id}`)

        return axios.delete(url);
    },
    GetProjectStatuses: function () {

        const url = PROJECTS_SERVICE_URI(`GetProjectStatuses`)

        return axios.get(url);
    },
}