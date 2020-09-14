const TIMESHEET_ACTIVITY_LOGS_SERVICE_URI = (method) => `/TimeSheetActivityLogs/${method}`;

const TimeSheetActivitiesService = {
    Add: function (activityLog) {
        let url = TIMESHEET_ACTIVITY_LOGS_SERVICE_URI(`Add`)
        return axios.post(url, activityLog)
    },
    Get: function (id) {
        let url = TIMESHEET_ACTIVITY_LOGS_SERVICE_URI(`Get?id=${id}`)
        return axios.get(url)
    },
    GetByActivity: function (activityId) {
        let url = TIMESHEET_ACTIVITY_LOGS_SERVICE_URI(`GetByActivity?activityId=${activityId}`)
        return axios.get(url)
    },
    Clear: function (activityId) {
        let url = TIMESHEET_ACTIVITY_LOGS_SERVICE_URI(`Clear?activityId=${activityId}`)
        return axios.post(url, activity)
    },
    Delete: function (id) {
        let url = TIMESHEET_ACTIVITY_LOGS_SERVICE_URI(`Delete?id=${id}`)
        return axios.post(url)
    }
}