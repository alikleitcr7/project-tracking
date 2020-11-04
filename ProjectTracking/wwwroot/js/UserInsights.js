new Vue({
    el: '#UserInsights',
    data: {
        insights: null,
        hasTimeSheets: false,
        isLoading: true,
        message: null
    },
    methods: {

    },
    computed: {

    },
    mounted: function () {

        const userId = $('#UserInsights').attr('data-user')

        this.hasTimeSheets = $('#Profile').attr('data-has-timesheets') === 'True'

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


                $('#line_active_minutes').closest('.c-box').show();

                chartsHelper.charts.populateActivitiesMinuts('line_active_minutes', insights.activeMinuts)

                //chartsHelper.charts.populateWorkload('bar_workload', insights.workload)
                if (this.hasTimeSheets) {


                    $('#line_activities_minutes').closest('.c-box').show();
                    $('#line_activities_frequency').closest('.c-box').show();
                    $('#pie_tasks').closest('.c-box').show();

                    chartsHelper.charts.populateActivitiesMinuts('line_activities_minutes', insights.activitiesMinuts)
                    chartsHelper.charts.populateActivities('line_activities_frequency', insights.activitiesFrequency)
                    chartsHelper.charts.populateTasks('pie_tasks', insights.tasksPerformance)
                }
                else {

                }
            })
    }
})