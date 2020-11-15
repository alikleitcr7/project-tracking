const BROADCASTS_SERVICE_URI = (method) => `/Broadcasts/${method}`;

const BroadcastService = {
    GetFromCurrentUser: function (page, countPerPage) {

        let query = serialize({  page, countPerPage });

        console.log({ query })

        let url = BROADCASTS_SERVICE_URI(`GetFromCurrentUser?${query}`)

        return axios.get(url);
    },
    GetToTeam: function (toTeamId, page, countPerPage) {

        let query = serialize({ toTeamId, page, countPerPage });

        //console.log({ query })

        let url = BROADCASTS_SERVICE_URI(`GetToTeam?${query}`)

        return axios.get(url);
    },
    Send: function (toTeamId, message, type) {

        let data = { toTeamId, message, type };

        let url = BROADCASTS_SERVICE_URI(`Send`)

        return axios.post(url, data);
    },
    MarkAsRead: function (id) {

        let query = serialize({ id })

        let url = BROADCASTS_SERVICE_URI(`MarkAsRead?${query}`)

        return axios.put(url);
    },
}