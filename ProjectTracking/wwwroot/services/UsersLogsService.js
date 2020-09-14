const USERSLOGS_SERVICE_URI = (method) => `/UsersLogs/${method}`;

const UsersLogs = {
 
    GetUsersLogs: function (page, countPerPage, fromDate, toDate) {
        console.log({page,countPerPage,fromDate,toDate})
        let url = USERSLOGS_SERVICE_URI(`GetUsersLogs?page=${page}&countPerPage=${countPerPage}&fromDate=${fromDate}&toDate=${toDate}`)
        return axios.get(url);
    },

}