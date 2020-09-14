const PermissionRequests_SERVICE_URI = (method) => `/PendingRequests/${method}`;



const PermissionRequests = {
    Create: function (name) {

    },
    AddPermissionRequest: function (request) {

        let url = PermissionRequests_SERVICE_URI('AddRequestStatus')
        return axios.post(url, request);
    },
    GetPermissionRequests: function (page, countPerPage) {

        let url = PermissionRequests_SERVICE_URI(`GetPermissionRequests?page=${page}&countPerPage=${countPerPage}`)
        return axios.get(url);
    },
    getSupervisingPermissionRequests: function (page, countPerPage) {

        let url = PermissionRequests_SERVICE_URI(`GetSuperVisingPermissionRequests?page=${page}&countPerPage=${countPerPage}`)
        return axios.get(url);
    },
    PermitOfPermissionRequest: function (permitModel) {

        let url = PermissionRequests_SERVICE_URI('PermitRequestedPermission')
        return axios.put(url, permitModel);
    },
    CheckIfEmployeeHasSubordinates: function () {
        let url = PermissionRequests_SERVICE_URI(`CheckIfEmployeeHasSubordinates`)
        return axios.get(url);
    },
    GetPermissionRequestsYearsOrMonthsByUser(userId, year) {
        let url = PermissionRequests_SERVICE_URI(`GetPermissionRequestsYearsOrMonthsByUser?userId=${userId}&year=${year}`)
        return axios.get(url);
    },
    GetApprovedRequestedPermission(userId, page, countPerPage, year, month) {
        let url = PermissionRequests_SERVICE_URI(`GetApprovedRequestedPermission?userId=${userId}&page=${page}&countPerPage=${countPerPage}&year=${year}&month=${month}`)
        return axios.get(url);
    },
    GetPermissionRequestsTotals(userId, year, month) {
        let url = PermissionRequests_SERVICE_URI(`GetPermissionRequestsTotals?userId=${userId}&year=${year}&month=${month}`)
        return axios.get(url);
    }
}

