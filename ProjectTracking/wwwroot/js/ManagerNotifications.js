
Vue.component('paginate', VuejsPaginate)

const initSendModal = () => {
    return {
        message: '',
        type: 0,
        selectedEmployees: []
    }
}

var manager_notification_app = new Vue({
    el: '#ManagerNotifications',
    data: {
        notifications: [],
        isLoading: true,
        employeesLoading: true,
        employees: [],
        departments: [],
        selectedDepartment: null,
        form: initSendModal(),
        messageModal : '',
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

                //sidebar_app.notificationsCount = newVal.totalCount


                this.dataPaging.totalPages = Math.ceil(newVal.totalCount / newVal.length);
            },
            deep: true
        },
        selectedDepartment: {
            handler: function (newVal, oldVal) {
                this.employeesLoading = true;
                this.form.selectedEmployees = [];

                axios.get('/Employees/GetEmployeesByDepartment?departmentId=' + newVal).then(
                    response => {
                        this.employees = response.data;
                        this.employeesLoading = false;
                    }
                ).catch(e => {

                    this.employeesLoading = false;
                })
            }
        }
    },
    methods: {
        getNotifications: function (page) {
            this.isLoading = true;
            // 0 for current user
            NotificationService.GetFromUser(0, page, this.dataPaging.length)
                .then((r) => {
                    this.notifications = r.data.records
                    this.notifications = r.data.records
                    this.dataPaging.totalCount = r.data.totalCount
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
            console.log('notification class', { notification })
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
        openNewMessageModal: function () {
            this.messageModal = ''

            $('#NewMessageModal').modal('show')
        },
        sendMessage: function () {

            const { message, type, selectedEmployees } = this.form

            this.messageModal = ''

            if (selectedEmployees.length === 0 || !message) {
                this.messageModal = 'select employees, and enter a message';
                return
            }

            NotificationService.SendBroadCast(selectedEmployees, message, type).then(r => {
                console.log('sent', r)

                this.form = initSendModal()
                this.getNotifications(0);
                this.messageModal = 'Broadcast sent!';

                //alert('messages sent')
            }).catch(e => {

                this.messageModal = BASIC_INTERNAL_ERROR_MESSAGE;

                //alert(BASIC_INTERNAL_ERROR_MESSAGE)
                console.error('send msg', e)

            })
        }
    },
    mounted: function () {

        this.getNotifications(0);


        axios.get('/Employees/GetDepartments').then(
            response => {
                this.departments = response.data;
            }
        ).catch(e => {
        })

        //setTimeout(() => {
        //    console.log('sending')
        //    NotificationService
        //        .Send('a18be9c0-aa65-4af8-bd17-00bd9344e575', '478ad54b-4c7e-4263-b0d2-5d89941e4803', 'This is a test 3', 1)
        //        .then(r => console.log('send notification', r))
        //        .catch(e => console.error('send notification', e))
        //}, 5000)
    }
})