const USER_INSIGHTS_SERVICE_URI = (method) => `/userinsights/${method}`;

const UserInsightsService = {
    UserMonthlyActivities: function (userId, monthly,daily, year, month) {

        let url = USER_INSIGHTS_SERVICE_URI(`UserMonthlyActivities?userId=${userId}&year=${year}&monthly=${monthly}&daily=${daily}&month=${month}`)

        console.log(url)
        let payload = BASIC_AJAX_PAYLOAD(url, 'get');

        return $.ajax(payload);
    },
}