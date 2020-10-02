const BROADCASTS_SERVICE_URI = (method) => `/Broadcasts/${method}`;

const BroadcastService = {
    GetFromUser: function (fromUserId, page, countPerPage) {

        let query = serialize({ fromUserId, page, countPerPage });

        console.log({ query })

        let url = BROADCASTS_SERVICE_URI(`GetFromUser?${query}`)

        return axios.get(url);
    },
    GetToTeam: function (toTeamId, page, countPerPage) {

        let query = serialize({ toTeamId, page, countPerPage });

        //console.log({ query })

        let url = BROADCASTS_SERVICE_URI(`GetToTeam?${query}`)

        return axios.get(url);
    },
    Send: function (fromUserId, toTeamId, message, notificationType) {

        let data = { fromUserId, toTeamId, message, notificationType };

        let url = BROADCASTS_SERVICE_URI(`Send`)

        return axios.post(url, data);
    },
}