const NOTIFICATIONS_SERVICE_URI = (method) => `/Notifications/${method}`;

const NotificationService = {

    GetFromUser: function (fromUserId, page, countPerPage) {

        let query = serialize({ fromUserId, page, countPerPage });

        console.log({ query })

        let url = NOTIFICATIONS_SERVICE_URI(`GetFromUser?${query}`)

        return axios.get(url);
    },
    GetToUser: function (toUserId, page, countPerPage) {

        let query = serialize({ toUserId, page, countPerPage });

        //console.log({ query })

        let url = NOTIFICATIONS_SERVICE_URI(`GetToUser?${query}`)

        return axios.get(url);
    },
    Send: function (fromUserId, toUserId, message, notificationType) {

        let data = { fromUserId, toUserId, message, notificationType };

        let url = NOTIFICATIONS_SERVICE_URI(`Send`)

        return axios.post(url, data);
    },
    SendBroadCast: function (selectedEmployees, message, type) {

        let data = { selectedEmployees, message, type };

        let url = NOTIFICATIONS_SERVICE_URI(`SendBroadCast`)

        return axios.post(url, data);
    },
}