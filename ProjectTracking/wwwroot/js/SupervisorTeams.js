new Vue({
    el: '#SupervisorTeams',
    data: {
        userId: null,
        teams: {
            data: null,
            message: '',
            isLoading: true
        }
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
        }
    },
    mounted: function () {

        const userId = $('#SupervisorTeams').attr('data-user')
        this.userId = userId;

        console.log({ userId })

        TeamsService.GetSupervisorTeamsModel(userId)
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

