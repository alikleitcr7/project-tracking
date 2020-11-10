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
        },
        broadcasts: {
            data: [],
            isLoading: false,
            message: null,
            dataPaging: {
                page: 0,
                totalPages: 0,
                length: 5,
                totalCount: 0,
                pagination: 'custom-pagination',
                prev: 'Prev',
                next: 'Next',
            },
        },
        selectedIdx: 0,
        currentUserId: null,
        tabs: null
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
    computed: {
        broadcastsTotalPages: function () {
            const totalCount = this.broadcasts.dataPaging.totalCount
            const length = this.broadcasts.dataPaging.length

            const totalPages = Math.ceil(totalCount / length);

            console.log({ totalPages })

            return totalPages || 0
        },
        //hasNewBroadcast: function () {
        //    return this.broadcasts.data.findIndex(k => k.isNew) > -1
        //},
        //hasNewUserNotification: function () {
        //    return this.notifications.findIndex(k => k.isNew) > -1
        //},
    },
    methods: {
        getNotifications: function (page) {


            this.isLoading = true;
            this.notifications = []

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
        },
        changeTabIndex: function (idx) {
            this.selectedIdx = idx
        },
        broadcastClick: function (idx) {
            let data = [...this.broadcasts.data]

            data[idx].isNew = false

            this.broadcasts.data = data
        },

        broadcasts_setLoading: function (isLoading) {
            this.broadcasts.isLoading = isLoading
        },
        broadcasts_setMessage: function (message) {
            this.broadcasts.message = message
        },
        broadcasts_getAll: function (page = 0) {

            this.broadcasts_setLoading(true)
            this.broadcasts_setMessage('Loading...')

            //const { keyword } = this.broadcasts.filterBy
            const { length } = this.broadcasts.dataPaging

            this.broadcasts.data = []

            return BroadcastService.GetToTeam(this.teamId, page, length)
                .then((r) => {


                    const { records, totalCount } = r.data

                    //if (debugBroadcasts) {
                    //    console.log('getall broadcast response', r)
                    //}

                    if (records) {
                        this.broadcasts.data = [...records]
                        this.broadcasts.dataPaging.totalCount = totalCount
                    }
                    else {
                        this.broadcasts_setMessage(BASIC_ERROR_MESSAGE)
                    }
                })
                .catch((e) => {
                    console.error('broadcasts getall', e)
                    this.broadcasts_setMessage(BASIC_ERROR_MESSAGE)
                })
                .then(() => {
                    this.broadcasts_setLoading(false)
                })

        },
        broadcasts_pageClick: function (pageNum) {
            this.broadcasts_getAll(pageNum - 1)
        },
        getModalClasses: function () {
            const hasNewBroadcasts = this.broadcasts.data.findIndex(k => k.isNew) > -1
            const hasNewUserNotification = this.notifications.findIndex(k => k.isNew) > -1

            console.log({ hasNewBroadcasts, hasNewUserNotification})

            return {
                'new-broadcast': hasNewBroadcasts,
                'new-user-notification': hasNewUserNotification,
            }
        }
    },
    mounted: function () {

        this.teamId = currentUser.team()

        if (this.teamId) {
            this.tabs = ['Team', 'Other']
        }
        else {
            this.selectedIdx = 1
        }

        this.getNotifications(0);

        if (this.teamId) {

            this.broadcasts_getAll(0);
        }

        //setTimeout(() => {
        //    console.log('sending')
        //    NotificationService
        //        .Send('a18be9c0-aa65-4af8-bd17-00bd9344e575', '478ad54b-4c7e-4263-b0d2-5d89941e4803', 'This is a test 3', 1)
        //        .then(r => console.log('send notification', r))
        //        .catch(e => console.error('send notification', e))
        //}, 5000)
    }
})