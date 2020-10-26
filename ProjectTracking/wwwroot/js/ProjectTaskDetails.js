var projectTasks_app = new Vue({
    el: "#ProjectTask",
    data: {
        taskId: null,
        oNull: null,
        overview: {
            data: null,
            isLoading: true,
            message: null
        }
    },
    computed: {
    },
    methods: {
    },
    mounted: function () {

        this.taskId = parseInt($('#ProjectTask').attr('data-task'))

        ProjectTasksService.GetOverview(this.taskId)
            .then((r) => {

                const record = r.data
                this.overview.data = record

                return record
            })
            .catch((e) => {
                const errorMessage = getAxiosErrorMessage(e)
                this.overview.message = errorMessage

                return null
            })
            .then((r) => {
                this.overview.isLoading = false

                /** @type {ProjectOverview} */
                const overview = r

                chartsHelper.charts.populateActivities('bar_activitiesMinutes', overview.activitiesMinutes, 'bar')
                chartsHelper.charts.populateActivities('line_activitiesFrequency', overview.activitiesFrequency, 'line')
                chartsHelper.charts.populateUserActivities('bar_userActivitiesMinutes', overview.userActivitiesMinutes, 'bar')
                chartsHelper.charts.populateUserActivities('line_userActivitiesFrequency', overview.userActivitiesFrequency, 'line')
            })
    }
})

