const UserProfile_SERVICE_URI = (method) => `/Employees/${method}`;
const UserProfileRequests = {
    GetLatestTimeSheet: function (userId) {
        let url = UserProfile_SERVICE_URI(`GetLatest?userId=${userId}`)
        return axios.get(url);
    },
    AddTimeSheet: function (timeSheet) {
        return axios.post('/TimeSheets/Add', timeSheet);
    },
    GetUser: function (userId) {
        let url = UserProfile_SERVICE_URI(`GetUser?userId=${userId}`)
        return axios.get(url);
    },
    GetCurrentUserId: function () {
        let url = UserProfile_SERVICE_URI(`GetCurrentUserId`)
        return axios.get(url);
    },
    ValidateCurrentUserPassword: function (password) {
        let url = UserProfile_SERVICE_URI(`ValidateCurrentUserPassword?password=${password}`)
        return axios.post(url);
    },
}