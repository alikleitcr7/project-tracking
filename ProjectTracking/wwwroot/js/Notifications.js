Vue.component('paginate', VuejsPaginate)

var notification_app = new Vue({
    el: '#NotificationModal',
    data: {
        notifications: [],
        isLoading: true,
        dataPaging: {
            page: 0,
            totalPages: 0,
            length: 5,
            totalCount: 0,
            pagination: 'custom-pagination',
            prev: 'Prev',
            next: 'Next',
        }
    },
    watch: {
        dataPaging: {
            handler: function (newVal, oldVal) {

                sidebar_app.notificationsCount = newVal.totalCount

                this.dataPaging.totalPages = Math.ceil(newVal.totalCount / newVal.length);

            },
            deep: true
        },
    },
    methods: {
        getNotifications: function (page) {


            this.isLoading = true;

            NotificationService.GetToCurrentUser(page, this.dataPaging.length)
                .then((r) => {

                    this.notifications = r.data.records ? r.data.records : []

                    this.dataPaging.totalCount = r.data.totalCount

                    sidebar_app.notificationsCount = this.dataPaging.totalCount
                })
                .catch((e) => {
                    console.error({ e })
                })
                .then(() => {
                    this.isLoading = false;
                })
        },
        clickCallback: function (pageNum) {
            this.getNotifications(pageNum - 1)
        },
        getNotificationClasses: function (notification) {
            //console.log('notification class',{notification})
            let classes = [`notification-${notification.notificationTypeDisplay.toLowerCase()}`]

            if (notification.isNew) {
                classes.push('isNew')
            }

            return classes
        },
        notificationClick: function (idx) {
            let notifications = [...this.notifications]

            notifications[idx].isNew = false

            this.notifications = notifications
        }
    },
    mounted: function () {

        this.getNotifications(0);

        //setTimeout(() => {
        //    console.log('sending')
        //    NotificationService
        //        .Send('a18be9c0-aa65-4af8-bd17-00bd9344e575', '478ad54b-4c7e-4263-b0d2-5d89941e4803', 'This is a test 3', 1)
        //        .then(r => console.log('send notification', r))
        //        .catch(e => console.error('send notification', e))
        //}, 5000)
    }
})