const HOME_SERVICE_URI = (method) => `/home/${method}`;

const HomeService = {
    GetOverview: function (logsAndCountersOnly) {

        const query = serialize({ logsAndCountersOnly})

        const url = HOME_SERVICE_URI(`GetOverview?${query}`)

        return axios.get(url);
    },
}