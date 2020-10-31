
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

            switch (role) {
                case APP_USER_ROLES.admin.value:


                    break;
                case APP_USER_ROLES.supervisor.value:
                    break;
                case APP_USER_ROLES.teamMember.value:
                    break;
            }
        },
        /**
         * 
         * @param {AdminOverview} overview
         */
        populateAdminOverview: function (overview) {

        },
        /**
         * 
         * @param {SupervisorOverview} overview
         */
        populateSupervisorOverview: function (overview) {

        },
        /**
         * 
         * @param {TeamMemberOverview} overview
         */
        populateTeamMemberOverview: function (overview) {

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