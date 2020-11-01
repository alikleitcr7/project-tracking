﻿const NOTIFICATIONS_SERVICE_URI = (method) => `/Notifications/${method}`;

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
    GetToCurrentUser: function (page, countPerPage) {

        let query = serialize({ page, countPerPage });

        let url = NOTIFICATIONS_SERVICE_URI(`GetToCurrentUser?${query}`)

        return axios.get(url);
    },
    GetFromCurrentUser: function (page, countPerPage) {

        let query = serialize({ page, countPerPage });

        let url = NOTIFICATIONS_SERVICE_URI(`GetFromCurrentUser?${query}`)

        return axios.get(url);
    },
    Send: function (fromUserId, toUserId, message, notificationType) {

        let data = { fromUserId, toUserId, message, notificationType };

        let url = NOTIFICATIONS_SERVICE_URI(`Send`)

        return axios.post(url, data);
    },
    SendBroadCast: function (selectedUserIds, message, type) {

        let data = { selectedUserIds, message, type };

        let url = NOTIFICATIONS_SERVICE_URI(`SendBroadCast`)

        return axios.post(url, data);
    },
}