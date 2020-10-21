new Vue({
    el: '#SupervisingTeams',
    data: {
        userId: null,
        teams: {
            data: [],
            message: '',
            isLoading: true
        },
        tasksPerformanceProgress: [
            {
                'fromProp': 'doneCount',
                'code': 'done',
                'name': 'Done'
            },
            {
                'fromProp': 'progressCount',
                'code': 'progress',
                'name': 'In Progress'
            },
            {
                'fromProp': 'pendingCount',
                'code': 'pending',
                'name': 'Pending'
            },
            {
                'fromProp': 'failedOrTerminatedCount',
                'code': 'failed',
                'name': 'Failed/Terminated'
            }
        ]
    },
    computed: {

    },
    methods: {
        getMembersText: function (count) {

            if (!count) {
                return 'no members yet'
            }

            const label = `member${count > 0 ? 's' : ''}`

            return `${count} ${label}`
        },
        getProjectsText: function (count) {

            if (!count) {
                return 'no projects yet'
            }

            const label = `project${count > 0 ? 's' : ''}`

            return `${count} ${label}`
        },
        getPercentage: function (tasksPerformance, fromProp) {

            const total = tasksPerformance['totalCount']
            const value = tasksPerformance[fromProp]

            const percentage = `${value / total * 100}%`

            return percentage
        },
        getTeamPerformance: function (tasksPerformance) {

            let tasksPerformanceProgress = [...this.tasksPerformanceProgress]

            tasksPerformanceProgress =
                tasksPerformanceProgress.filter(k => tasksPerformance[k.fromProp] > 0)
                    .map(k => ({ ...k, name: tasksPerformance[k.fromProp] + ' ' + k.name }))

            return tasksPerformanceProgress
        }
    },
    mounted: function () {

        const userId = $('#SupervisingTeams').attr('data-user')
        this.userId = userId;


        TeamsService.GetSupervisingTeamsModel(userId)
            .then(r => {
                this.teams.data = r.data
            })
            .catch(e => {
                this.teams.message = getAxiosErrorMessage(e)
            })
            .then(() => {
                this.teams.isLoading = false
            });
    }
});

