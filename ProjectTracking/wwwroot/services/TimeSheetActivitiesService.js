const TIMESHEET_ACTIVITIES_SERVICE_URI = (method) => `/TimeSheetActivities/${method}`;

const TimeSheetActivitiesService = {
    Update: function (model) {

        let url = TIMESHEET_ACTIVITIES_SERVICE_URI(`Update`)

        return axios.post(url, model)
    },
    Start: function (timeSheetTaskId) {

        const query = serialize({ timeSheetTaskId })

        let url = TIMESHEET_ACTIVITIES_SERVICE_URI(`Start?${query}`)

        return axios.post(url)
    },
    Stop: function (model) {

        let url = TIMESHEET_ACTIVITIES_SERVICE_URI(`Stop`)

        return axios.post(url, model)
    },
    GetUserActiveActivity: function (userId) {

        const query = serialize({ userId })

        let url = TIMESHEET_ACTIVITIES_SERVICE_URI(`GetUserActiveActivity?${query}`)

        return axios.get(url)
    },
    //Add: function (activity) {
    //    let url = TIMESHEET_ACTIVITIES_SERVICE_URI(`Add`)
    //    return axios.post(url, activity)
    //},
    Get: function (id) {

        let url = TIMESHEET_ACTIVITIES_SERVICE_URI(`Get?id=${id}`)

        return axios.get(url)
    },
    GetByTimeSheet: function (timesheetId) {

        let url = TIMESHEET_ACTIVITIES_SERVICE_URI(`GetByTimeSheet?timesheetId=${timesheetId}`)

        return axios.get(url)
    },
    //Update: function (activity) {
    //    let url = TIMESHEET_ACTIVITIES_SERVICE_URI(`Update`)
    //    return axios.post(url, activity)
    //},
    Delete: function (id) {

        let url = TIMESHEET_ACTIVITIES_SERVICE_URI(`Delete?id=${id}`)

        return axios.post(url)
    }
}