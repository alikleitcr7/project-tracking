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
        isTeamSupervisor: false,
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
        isLoading: true,
        tabs: null,
        selectedTab: 0,
        errors: '',
        oNull: null,
        teamNotifications: teamNotificationObject(),
        notificationTypes: {
            data: NOTIFICATION_TYPE._toList(),
            isLoading: false
        }
    },
    methods: {
        ...teamNotificationsMethods,
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
            //if (!teamNotifications_app) {
            //    console.error('team notification app undefined')
            //    return
            //}
            this.teamNotifications_openModal()
        },
        getNotificationClasses: function (notification) {

            let classes = [`notification-${notification.notificationTypeDisplay.toLowerCase()}`]

            return classes
        },
    },
    computed: {
        teamNotificationsTotalPages: function () {
            const totalCount = this.teamNotifications.dataPaging.totalCount
            const length = this.teamNotifications.dataPaging.length

            const totalPages = Math.ceil(totalCount / length);

            console.log({ totalPages })

            return totalPages || 0
        },
    },
    mounted: function () {

        const teamId = parseInt($('#Team').attr('data-id'))
        const secureMode = $('#Team').attr('data-secure-mode') === 'True'

        this.secureMode = secureMode

        const isTeamSupervisor = $('#Team').attr('data-is-team-supervisor') === 'True'

        this.isTeamSupervisor = isTeamSupervisor

        if (isTeamSupervisor) {
            this.tabs = [
                { title: 'Compose', icon: 'fa fa-pen' },
                { title: 'Sent', icon: 'fa fa-history' },
            ]
        }
        else {
            this.selectedTab = 1
        }

        this.teamId = parseInt(teamId)
        this.isTeamSupervisor = isTeamSupervisor
        this.isLoading = true

        // broadcasts
        this.teamNotifications_getAll()


        // insights
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

                const workloadTaskPerformance = team.workload
                    .map(k => k.value)
                    .reduce((acc, k) => ({
                        doneCount: (acc.doneCount + k.doneCount) || 0,
                        progressCount: (acc.progressCount + k.progressCount) || 0,
                        pendingCount: (acc.pendingCount + k.pendingCount) || 0,
                        failedOrTerminatedCount: (acc.failedOrTerminatedCount + k.failedOrTerminatedCount) || 0,
                    }), {
                        doneCount: 0,
                        progressCount: 0,
                        pendingCount: 0,
                        failedOrTerminatedCount: 0,
                    })


                workloadTaskPerformance.totalCount = workloadTaskPerformance.doneCount + workloadTaskPerformance.progressCount + workloadTaskPerformance.pendingCount + workloadTaskPerformance.failedOrTerminatedCount

                chartsHelper.charts.populateWorkload('bar_workload', team.workload)
                chartsHelper.charts.populateActivities('line_activities', team.activitiesFrequency)
                chartsHelper.charts.populateTasks('pie_tasks', team.tasksPerformance)
                chartsHelper.charts.populateTasks('pie_assigned_tasks', workloadTaskPerformance)
            })


    }
})