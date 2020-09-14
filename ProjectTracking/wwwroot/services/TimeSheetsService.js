


const TIMESHEETS_SERVICE_URI = (method) => `/timesheets/${method}`;

const TimeSheetsService = {
    GetActivities: function (timesheetId) {
        let url = TIMESHEETS_SERVICE_URI(`GetActivities?timesheetId=${timesheetId}`)
        return axios.get(url)
    },
    GetActivitiesByDate: function (timesheetId,date) {
        let url = TIMESHEETS_SERVICE_URI(`GetActivitiesByDate?timesheetId=${timesheetId}&date=${date}`)
        return axios.get(url)
    },
    GetTimeSheetProjects: function (timesheetId) {
        let url = TIMESHEETS_SERVICE_URI(`GetTimeSheetProjects?timesheetId=${timesheetId}`)
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
    GetUserTimeSheets: function (userId, timeSheetId) {

        let url = TIMESHEETS_SERVICE_URI(`GetUserTimeSheets?userId=${userId}&timeSheetId=${timeSheetId}`)
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