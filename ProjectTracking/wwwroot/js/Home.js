
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

            console.log({ role })
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
            chartsHelper.charts.populateTeamActivitiesFrequency('teams_line_activities_minutes', data.teamsActivitiesMinutes)
            chartsHelper.charts.populateTeamActivitiesFrequency('teams_line_activities_frequency', data.teamsActivitiesFrequency)

            chartsHelper.charts.populateProjects('pie_projects', data.projectsPerformance)

        },
        /**
         * 
         * @param {SupervisorOverview} data
         */
        populateSupervisorVisuals: function (data) {



            $('#members_line_activities_minutes').closest('.c-box').show();
            $('#members_line_activities_frequency').closest('.c-box').show();
            $('#teams_line_activities_minutes').closest('.c-box').show();
            $('#teams_line_activities_frequency').closest('.c-box').show();

            chartsHelper.charts.populateMemberActivitiesFrequency('members_line_activities_minutes', data.membersActivitiesMinutes)
            chartsHelper.charts.populateMemberActivitiesFrequency('members_line_activities_frequency', data.membersActivitiesFrequency)

            chartsHelper.charts.populateTeamActivitiesFrequency('teams_line_activities_minutes', data.teamsActivitiesMinutes)
            chartsHelper.charts.populateTeamActivitiesFrequency('teams_line_activities_frequency', data.teamsActivitiesFrequency)
        },
        /**
         * 
         * @param {TeamMemberOverview} data
         */
        populateTeamMemberVisuals: function (data) {

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
        },
        getCardClassByProject: function (project) {
            const projectStatus = PROJECT_STATUS._toList().find(k => k.key === project.statusCode)

            if (!projectStatus) {
                console.error('task status not found in PROJECT_STATUS', projectStatus)
                return {}
            }

            const code = projectStatus.code

            return 'c-card--' + code
        },
        loggedUsersTitle: function (loggedInUsers) {

            if (!loggedInUsers) {
                return ''
            }

            const count = loggedInUsers.reduce((a, c) => a + c.value, 0)


            return `Total of ${count} User${(count > 1 ? 's' : '')}`
        }
    },
    computed: {
        loadingBoxes: function () {

            return new Array(this.loadingBoxesCount)
        },

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

                if (errorMessage === 'DONT_EXIST_OR_NOT_CLAIMED_ROLE') {
                    this.overview.message = "Your role might have changed!"
                    this.overview.showLogout = true

                }
                else if (errorMessage === 'NO_SUPERVISING_TEAMS') {
                    this.overview.message = 'No supervising team assigned yet!'
                }
                else {
                    this.overview.message = errorMessage
                }
            })
            .then(() => {
                this.overview.isLoading = false

            })
    }
})