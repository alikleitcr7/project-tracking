
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
        loadingBoxesCount: getLoadingBoxesCountByRole()
    },
    methods: {

    },
    computed: {
        loadingBoxes: function () {

            return new Array(this.loadingBoxesCount)
        }
    },
    mounted: function () {

        this.role = role;
        this.userId = currentUser.id()


        console.log({ role })
        switch (role) {
            case APP_USER_ROLES.admin.value:
                break;
            case APP_USER_ROLES.supervisor.value:
                UsersService.GetSupervisorOverview(this.userId)
                    .then((r) => {
                        const record = r.data
                    })
                    .catch((e) => {
                        const errorMessage = getAxiosErrorMessage(e)
                    })
                    .then(() => {

                    })
                break;
            case APP_USER_ROLES.teamMember.value:
                break;
        }
    }
})