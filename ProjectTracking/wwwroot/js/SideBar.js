var sidebar_app = new Vue({
    el: '#SideBar',
    data: {
        notificationsCount: 0,
        hasNewNotification: false,
    },
    methods: {
        handleSideBarNotificationClick: function () {

            this.hasNewNotification = false

            NotificationService.SetHasNotificationFlag(false)
                .then((r) => {
                    const record = r.data
                })
                .catch((e) => {
                    const errorMessage = getAxiosErrorMessage(e)
                    console.error(errorMessage)
                })

            $('#NotificationModal').modal('show')
        },
    },
    mounted: function () {

        NotificationService.GetHasNotificationFlag()
            .then((r) => {
                const record = r.data

                this.hasNewNotification = record
            })
            .catch((e) => {
                const errorMessage = getAxiosErrorMessage(e)
                console.error(errorMessage)
            })
    }
})