var sidebar_app = new Vue({
    el: '#SideBar',
    data: {
        notificationsCount: 0,
        hasNewNotification: false,
        endSessionMessage: null,
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
        endSession: function (message) {

            $('#SessionEndModal').modal({ backdrop: 'static', keyboard: false })

            this.endSessionMessage = message
        }
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

        const pathname = window.location.pathname
        const items = $('.side-bar-item')

        for (var i = 0; i < items.length; i++) {
            const item = items.eq(i)
            const href = item.attr('href')
            const altPaths = item.attr('data-alt-paths')

            if (!href) {
                continue
            }

            if ((pathname === href) || (altPaths && altPaths.split(',').includes(pathname))) {
                item.addClass('active')
                break
            }

        }

    }
})