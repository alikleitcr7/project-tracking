new Vue({

    el: '#Team',
    data: {
        team: null,
        message: '',
        isLoading: true
    },
    methods: {
        getMemberNameById: function () {

        }
    },
    computed: {

    },
    mounted: function () {

        const teamId = parseInt($('#Team').attr('data-id'))

        this.isLoading = true

        TeamsService.GetTeamViewModel(teamId)
            .then((r) => {
                const record = r.data

                this.team = record
            })
            .catch((e) => {
                const errorMessage = getAxiosErrorMessage(e)

                this.message = errorMessage
            })
            .then(() => {
                this.isLoading = false
            })

    }

})