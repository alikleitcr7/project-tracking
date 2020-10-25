new Vue({
    el: '#UserInsights',
    data: {
        insights: null,
        isLoading: true,
        message: null
    },
    methods: {

    },
    computed: {

    },
    mounted: function () {

        const userId = $('#UserInsights').attr('data-user')

        UsersService.GetUserInsights(userId)
            .then((r) => {

                const record = r.data

                this.insights = record

                return record
            })
            .catch((e) => {
                const errorMessage = getAxiosErrorMessage(e)

                this.message = errorMessage

                return null
            })
            .then((r) => {

                this.isLoading = false

                /** @type {UserInsights} */
                const insights = r

                //chartsHelper.charts.populateWorkload('bar_workload', insights.workload)
                chartsHelper.charts.populateActivitiesMinuts('line_activities_minutes', insights.activitiesMinuts)
                chartsHelper.charts.populateActivities('line_activities_frequency', insights.activitiesFrequency)
                chartsHelper.charts.populateTasks('pie_tasks', insights.tasksPerformance)
            })
    }
})