const debugUsersNotifications = true;

const usersNotificationFields = [
    {
        name: 'message',
        displayName: 'Message',
        max: 1,
        max: 255,
        type: DATA_TYPES.TEXT,
        required: true,
    },
    {
        name: 'selectedUserId',
        //name: 'selectedUserIds',
        displayName: 'User',
        errorMessage: 'User is required',
        min: 1,
        type: DATA_TYPES.TEXT,
        required: true,
    },
]

const Modals_UsersNotifications = {
    UsersNotification: {
        Show: function () {
            $('#UsersNotificationModal').modal('show');
        },
        Hide: function () {
            $('#UsersNotificationModal').modal('hide');
        }
    },
}

const usersNotificationFormObject = (obj) => {

    let record = {
        selectedUserIds: obj ? obj.selectedUserIds : [],
        message: obj ? obj.message : null,
        type: obj ? obj.type : NOTIFICATION_TYPE.default.key,
    }

    return {
        record,
        message: '',
        isLoading: false,
        isSaving: false,
    }
}

const usersNotificationObject = () => {
    return {
        data: [],
        filterBy: {
            //keyword: '',
            //categoryId: null,
        },
        dataPaging: {
            page: 0,
            totalPages: 0,
            length: 5,
            totalCount: 0,
            pagination: 'custom-pagination',
            prev: 'Prev',
            next: 'Next',
        },
        isLoading: false,
        isProcessing: false,
        message: '',
        form: usersNotificationFormObject(),
    }
}

const usersNotificationsMethods = {
    usersNotifications_validateForm: function (obj) {

        const form = obj || this.usersNotifications.form.record

        console.log({ form })

        var isValid = true
        let finalMessage = '';

        for (var i = 0; i < usersNotificationFields.length; i++) {

            const field = usersNotificationFields[i]
            const fieldValue = form[field.name];

            console.log('field validation', { field, fieldValue })

            const validator = new CoreValidator(field.name, fieldValue, field.required, field.type, field.min, field.max, field.displayName)
            isValid = validator.validate()


            if (!isValid) {

                finalMessage = validator.message();
                console.log(' validation 3')

                break;
            }
        }

        console.log('end validation')


        if (!isValid) {
            this.usersNotifications_setFormMessage(finalMessage || 'Fill Required Fields to Continue')
        }

        return isValid;
    },
    usersNotifications_setFormMessage: function (message) {

        this.usersNotifications.form.message = message
    },
    usersNotifications_setFormLoading: function (isLoading) {
        this.usersNotifications.form.isLoading = isLoading
    },
    usersNotifications_setFormSaving: function (isSaving) {
        this.usersNotifications.form.isSaving = isSaving
    },
    usersNotifications_setProcessing: function (isProcessing) {
        this.usersNotifications.isProcessing = isProcessing
    },
    usersNotifications_setLoading: function (isLoading) {
        this.usersNotifications.isLoading = isLoading
    },
    usersNotifications_setMessage: function (message) {
        this.usersNotifications.message = message
    },
    usersNotifications_openModal: function () {

        this.usersNotifications.message = ''
        this.usersNotifications.form = usersNotificationFormObject()

        this.populateNotificationUsers();

        Modals_UsersNotifications.UsersNotification.Show()
    },

    usersNotifications_save: function () {

        this.usersNotifications_setFormMessage('');

        let pendingRecord = { ...this.usersNotifications.form.record }

        // required fields validation

        if (!this.usersNotifications_validateForm()) {

            //this.usersNotifications_setFormMessage('Fill the required fields to continue')
            return;
        }

        console.log('sending')
        // START UPDATE/CREATE REQUEST
        this.usersNotifications_setFormSaving(true)

        const { selectedUserId, message, type } = pendingRecord

        NotificationService.Send(selectedUserId, message, type)
            .then((r) => {

                const record = r.data

                if (debugUsersNotifications) {
                    console.log('update response', r)
                }

                if (record) {

                    // update data array
                    let data = [record, ...this.usersNotifications.data]

                    //data.unshift(record)

                    this.usersNotifications.data = data
                    this.usersNotifications.form = usersNotificationFormObject();


                    // feedback
                    this.usersNotifications_setFormMessage('Sent!')

                }
                else {
                    this.usersNotifications_setFormMessage(BASIC_ERROR_MESSAGE)
                }
            })
            .catch((e) => {

                console.error('Updated!', e)

                this.usersNotifications_setFormMessage(getAxiosErrorMessage(e))
            })
            .then(() => {
                this.usersNotifications_setFormSaving(false)
            });
    },

    usersNotifications_getAll: function (page = 0) {

        this.usersNotifications_setLoading(true)
        this.usersNotifications_setMessage('Loading...')

        const { keyword } = this.usersNotifications.filterBy
        const { length } = this.usersNotifications.dataPaging

        return NotificationService.GetFromCurrentUser(page, length)
            .then((r) => {


                const { records, totalCount } = r.data

                if (debugUsersNotifications) {
                    console.log('getall usersNotification response', r)
                }

                if (records) {
                    this.usersNotifications.data = [...records]
                    this.usersNotifications.dataPaging.totalCount = totalCount
                }
                else {
                    this.usersNotifications_setMessage(BASIC_ERROR_MESSAGE)
                }
            })
            .catch((e) => {
                console.error('usersNotifications getall', e)
                this.usersNotifications_setMessage(BASIC_ERROR_MESSAGE)
            })
            .then(() => {
                this.usersNotifications_setLoading(false)
            })

    },
    usersNotifications_pageClick: function (pageNum) {
        this.usersNotifications_getAll(pageNum - 1)
    },
}

var usersNotifications_app = new Vue({
    el: "#UsersNotifications",
    data: {
        dateOptions,
        dateTimeOptions,
        usersNotifications: usersNotificationObject(),
        errors: '',
        oNull: null,
        teamMembers: {
            data: [],
            isLoading: false,
            isLoaded: false,
            message: null
        },
        supervisors: {
            data: [],
            isLoading: false,
            isLoaded: false,
            message: null
        },
        isAdmin: false,
        notificationTypes: {
            data: NOTIFICATION_TYPE._toList(),
            isLoading: false
        }
    },
    computed: {
        usersNotificationsTotalPages: function () {
            const totalCount = this.usersNotifications.dataPaging.totalCount
            const length = this.usersNotifications.dataPaging.length

            const totalPages = Math.ceil(totalCount / length);

            console.log({ totalPages })

            return totalPages || 0
        },
    },
    methods: {
        ...usersNotificationsMethods,
        populateNotificationUsers: function () {

            // supervisors

            if (this.isAdmin && !this.supervisors.isLoaded) {

                UsersService.GetUsersByRoleKeyValue(APP_USER_ROLES.supervisor.key)
                    .then((r) => {
                        const record = r.data

                        if (record) {
                            this.supervisors.data = record
                            this.supervisors.isLoaded = true
                        }
                    })
                    .catch((e) => {

                        const errorMessage = getAxiosErrorMessage(e)

                        this.supervisors.message = errorMessage
                    })
                    .then(() => {
                        this.supervisors.isLoading = false
                    })
            }

            // team members

            if (!this.teamMembers.isLoaded) {

                this.teamMembers.isLoading = true

                UsersService.GetUsersByRoleKeyValue(APP_USER_ROLES.teamMember.key)
                    .then((r) => {
                        const record = r.data

                        if (record) {
                            this.teamMembers.data = record
                            this.teamMembers.isLoaded = true
                        }
                    })
                    .catch((e) => {
                        const errorMessage = getAxiosErrorMessage(e)

                        this.teamMembers.message = errorMessage

                    })
                    .then(() => {
                        this.teamMembers.isLoading = false
                    })
            }
        }
    },
    mounted: function () {

        this.isAdmin = currentUser.role() === APP_USER_ROLES.admin.value

        this.usersNotifications_getAll()
    }
})

