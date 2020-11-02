Chart.defaults.global.responsive = true;

const Modals_Team = {
    Members: {
        Show: function () {
            $('#MembersModal').modal('show');
        },
        Hide: function () {
            $('#MembersModal').modal('hide');
        }
    },
    Broadcasts: {
        Show: function () {
            $('#BroadcastsModal').modal('show');
        },
        Hide: function () {
            $('#BroadcastsModal').modal('hide');
        }
    },
    Projects: {
        Show: function () {
            $('#ProjectsModal').modal('show');
        },
        Hide: function () {
            $('#ProjectsModal').modal('hide');
        }
    }
}

new Vue({
    el: '#Team',
    data: {
        teamId: null,
        team: null,
        message: '',
        broadcasts: {
            data: [],
            isLoading: false,
            isLoaded: false
        },
        projects: {
            data: [],
            isLoading: false,
            message: null,
            isLoaded: false
        },
        isLoading: true
    },
    methods: {
        getMemberNameById: function () {

        },
        showMembers: function () {
            Modals_Team.Members.Show()
        },
        openProjects: function () {

            Modals_Team.Projects.Show();

            if (this.projects.isLoaded) {
                return
            }

            this.projects.isLoading = true
            this.projects.message = null

            ProjectsService.GetByTeam(this.teamId)
                .then((r) => {
                    const record = r.data
                    this.projects.data = record
                    this.projects.isLoaded = true

                })
                .catch((e) => {
                    const errorMessage = getAxiosErrorMessage(e)
                    this.projects.message = errorMessage

                })
                .then(() => {
                    this.projects.isLoading = false

                })

        },
        openBroadcasts: function () {
            if (!teamNotifications_app) {
                console.error('team notification app undefined')
                return
            }
            teamNotifications_app.teamNotifications_openModal()
        }
    },
    computed: {

    },
    mounted: function () {

        const teamId = parseInt($('#Team').attr('data-id'))

        this.teamId = parseInt(teamId)
        this.isLoading = true

        TeamsService.GetTeamViewModel(teamId)
            .then((r) => {
                const record = r.data

                this.team = record

                return record
            })
            .catch((e) => {
                const errorMessage = getAxiosErrorMessage(e)

                this.message = errorMessage

                return null
            })
            .then((r) => {
                this.isLoading = false

                /** @type {TeamViewModel} */
                const team = r

                chartsHelper.charts.populateWorkload('bar_workload', team.workload)
                chartsHelper.charts.populateActivities('line_activities', team.activitiesFrequency)
                chartsHelper.charts.populateTasks('pie_tasks', team.tasksPerformance)
            })

    }
})