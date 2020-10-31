const HOME_SERVICE_URI = (method) => `/home/${method}`;

const HomeService = {
    GetOverview: function () {

        const url = HOME_SERVICE_URI(`GetOverview`)

        return axios.get(url);
    },
}