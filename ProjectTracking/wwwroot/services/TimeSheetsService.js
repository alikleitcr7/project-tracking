


const TIMESHEETS_SERVICE_URI = (method) => `/timesheets/${method}`;

const TimeSheetsService = {
    GetById: function (id) {

        let url = TIMESHEETS_SERVICE_URI(`GetById?id=${id}`)

        return axios.get(url)

    },
    Save: function (model) {

        const url = TIMESHEETS_SERVICE_URI(`Save`)

        return axios.post(url, model);

    },
    GetLatest: function (userId) {

        const query = serialize({ userId })

        const url = TIMESHEETS_SERVICE_URI(`GetLatest?${query}`)

        return axios.get(url);
    },
    GetTimeSheetTasks: function (timeSheetId) {

        const query = serialize({ timeSheetId })

        const url = TIMESHEETS_SERVICE_URI(`GetTimeSheetTasks?${query}`)

        return axios.get(url);
    },
    AddProjectToTimeSheet: function (model) {

        const url = TIMESHEETS_SERVICE_URI(`AddProjectToTimeSheet`)

        return axios.post(url, model);
    },
    RemoveProjectFromTimeSheet: function (model) {

        const url = TIMESHEETS_SERVICE_URI(`RemoveProjectFromTimeSheet`)

        return axios.post(url, model);
    },
    GetTimeSheetYears: function (userId) {
        const query = serialize({ userId })

        const url = TIMESHEETS_SERVICE_URI(`GetTimeSheetYears?${query}`)

        return axios.get(url);
    },
    GetTimeSheets: function (userId, year, includeTasks) {
        const query = serialize({ userId, year, includeTasks })

        const url = TIMESHEETS_SERVICE_URI(`GetTimeSheets?${query}`)

        return axios.get(url);
    },
    GetTimeSheetProjectsWithTasks: function (timeSheetId) {
        const query = serialize({ timeSheetId })

        const url = TIMESHEETS_SERVICE_URI(`GetTimeSheetProjectsWithTasks?${query}`)

        return axios.get(url);
    },
    GetActivities: function (timesheetId) {
        let url = TIMESHEETS_SERVICE_URI(`GetActivities?timesheetId=${timesheetId}`)
        return axios.get(url)
    },
    GetActivitiesByDate: function (timeSheetId, taskId, date) {

        const query = serialize({ timeSheetId, taskId, date })

        let url = TIMESHEETS_SERVICE_URI(`GetActivitiesByDate?${query}`)

        return axios.get(url)
    },
    GetTimeSheetProjects: function (timesheetId) {
        let url = TIMESHEETS_SERVICE_URI(`GetTimeSheetProjects?timesheetId=${timesheetId}`)
        return axios.get(url)
    },
    TimeSheetTasksWithActivityCheck: function (timesheetId) {
        let url = TIMESHEETS_SERVICE_URI(`TimeSheetTasksWithActivityCheck?timesheetId=${timesheetId}`)
        return axios.get(url)
    },
    GetTimeSheetProjectModel: function (timesheetId) {
        let url = TIMESHEETS_SERVICE_URI(`GetTimeSheetProjectModel?timesheetId=${timesheetId}`)
        return axios.get(url)
    },
    GetSubordinatesTimeSheets: function (page, countPerPage) {
        let url = TIMESHEETS_SERVICE_URI(`GetSubordinatesTimeSheets?page=${page}&countPerPage=${countPerPage}`)
        return axios.get(url)
    },
    PermitOfTimeSheet: function (permitModel) {
        let url = TIMESHEETS_SERVICE_URI('PermitTimeSheetStatus')
        return axios.put(url, permitModel);
    },
    GetUserTimeSheets: function (userId) {

        let url = TIMESHEETS_SERVICE_URI(`GetUserTimeSheets?userId=${userId}`)
        return axios.get(url);
    },
    Delete: function (id) {
        let url = TIMESHEETS_SERVICE_URI(`Delete?id=${id}`)
        return axios.delete(url);
    },
    SignTimeSheet: function (timeSheetId) {
        let url = TIMESHEETS_SERVICE_URI(`SignTimeSheet?timeSheetId=${timeSheetId}`)
        return axios.post(url);
    },
}