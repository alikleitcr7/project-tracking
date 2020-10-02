
const PROJECT_TASKS_SERVICE_URI = (method) => `/projectTasks/${method}`;

const ProjectTasksService = {
    GetById: function (id) {
        const url = PROJECT_TASKS_SERVICE_URI(`GetById?id=${id}`)

        return axios.get(url);
    },
    Save: function (projectTask) {
        const url = PROJECT_TASKS_SERVICE_URI(`Save`)

        return axios.post(url, projectTask);
    },
    Search: function (keyword, projectId, page, countPerPage) {

        const query = serialize({ keyword, projectId, page, countPerPage })

        const url = PROJECT_TASKS_SERVICE_URI(`Search?${query}`)

        return axios.get(url);
    },
    GetStatusModifications: function (taskId) {

        const query = serialize({ taskId })

        const url = PROJECT_TASKS_SERVICE_URI(`GetStatusModifications?${query}`)

        return axios.get(url);
    },
    Delete: function (id) {

        const url = PROJECT_TASKS_SERVICE_URI(`Delete?id=${id}`)

        return axios.delete(url);
    },
    GetProjectTaskStatuses: function () {

        const url = PROJECT_TASKS_SERVICE_URI(`GetProjectTaskStatuses`)

        return axios.get(url);
    },
    ChangeStatus: function (taskId, statusCode) {

        const query = serialize({ taskId, statusCode })

        const url = PROJECT_TASKS_SERVICE_URI(`GetProjectStatuses?${query}`)

        return axios.get(url);
    },
}