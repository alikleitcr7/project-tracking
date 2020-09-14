var sidebar_app = new Vue({
    el: '#SideBar',
    data: {
        notificationsCount: 0,
        hasNewNotification: false
    },
    methods: {
        handleSideBarNotificationClick: function () {
            this.hasNewNotification = false

            $('#NotificationModal').modal('show')
        }
    },
    mounted: function () {

    }
})