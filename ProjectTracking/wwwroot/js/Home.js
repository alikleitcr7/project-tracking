
const role = currentUser.role()

const getLoadingBoxesCountByRole = () => {

    switch (role) {
        case APP_USER_ROLES.admin.key:
            return 4;
        case APP_USER_ROLES.supervisor.key:
            return 4;
        default:
            return 5
    }
}

new Vue({
    el: '#Dashboard',
    data: {
        loadingBoxesCount: getLoadingBoxesCountByRole(),
        role: null,
        userId: null,
        overview: {
            data: null,
            isLoading: true,
            message: null
        },
    },
    methods: {
        populateDashboard: function (data) {

            console.log({role})
            switch (role) {
                case APP_USER_ROLES.admin.value:
                    this.populateAdminVisuals(data)
                    break;
                case APP_USER_ROLES.supervisor.value:
                    this.populateSupervisorVisuals(data)
                    break;
                case APP_USER_ROLES.teamMember.value:
                    this.populateTeamMemberVisuals(data)
                    break;
            }
        },
        /**
         * 
         * @param {AdminOverview} data
         */
        populateAdminVisuals: function (data) {

        },
        /**
         * 
         * @param {SupervisorOverview} data
         */
        populateSupervisorVisuals: function (data) {

        },
        /**
         * 
         * @param {TeamMemberOverview} data
         */
        populateTeamMemberVisuals: function (data) {

            console.log({ data })

            chartsHelper.charts.populateActivitiesMinuts('line_activities_minutes', data.activitiesMinuts)
            chartsHelper.charts.populateActivities('line_activities_frequency', data.activitiesFrequency)
        },
        getCardClassByTask: function (task) {
            const taskStatus = PROJECT_TASK_STATUS._toList().find(k => k.key === task.statusCode)

            if (!taskStatus) {
                console.error('task status not found in PROJECT_TASK_STATUS', task)
                return {}
            }

            const code = taskStatus.code

            return 'c-card--' + code
        }
    },
    computed: {
        loadingBoxes: function () {

            return new Array(this.loadingBoxesCount)
        }
    },
    mounted: function () {

        this.role = role;
        this.userId = currentUser.id()

        this.overview.isLoading = true

        HomeService.GetOverview()
            .then((r) => {
                const record = r.data

                if (!record) {
                    this.overview.errorMessage = BASIC_ERROR_MESSAGE
                    return
                }

                this.overview.data = record
                this.populateDashboard(record)
            })
            .catch((e) => {
                const errorMessage = getAxiosErrorMessage(e)

                this.overview.message = errorMessage
            })
            .then(() => {
                this.overview.isLoading = false

            })
    }
})