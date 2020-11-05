const debugTeamNotifications = true;

const teamNotificationFields = [
    {
        name: 'message',
        displayName: 'Message',
        max: 255,
        type: DATA_TYPES.TEXT,
        required: true,
    },
]

const Modals_TeamNotifications = {
    TeamNotification: {
        Show: function () {
            $('#TeamNotificationModal').modal('show');
        },
        Hide: function () {
            $('#TeamNotificationModal').modal('hide');
        },
        SetTeam: function (id) {
            $('#TeamNotificationModal').attr('data-id', id);
        }
    },
}

const teamNotificationFormObject = (obj) => {

    const teamId = parseInt($('#TeamNotificationModal').attr('data-id'))

    let record = {
        toTeamId: teamId,
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

const teamNotificationObject = () => {
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
        form: teamNotificationFormObject(),
    }
}

const teamNotificationsMethods = {
    teamNotifications_validateForm: function (obj) {

        const form = obj || this.teamNotifications.form.record

        console.log({ form })

        var isValid = true
        let finalMessage = '';

        for (var i = 0; i < teamNotificationFields.length; i++) {

            const field = teamNotificationFields[i]
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
            this.teamNotifications_setFormMessage(finalMessage || 'Fill Required Fields to Continue')
        }

        return isValid;
    },
    teamNotifications_setFormMessage: function (message) {

        this.teamNotifications.form.message = message
    },
    teamNotifications_setFormLoading: function (isLoading) {
        this.teamNotifications.form.isLoading = isLoading
    },
    teamNotifications_setFormSaving: function (isSaving) {
        this.teamNotifications.form.isSaving = isSaving
    },
    teamNotifications_setProcessing: function (isProcessing) {
        this.teamNotifications.isProcessing = isProcessing
    },
    teamNotifications_setLoading: function (isLoading) {
        this.teamNotifications.isLoading = isLoading
    },
    teamNotifications_setMessage: function (message) {
        this.teamNotifications.message = message
    },
    teamNotifications_openModal: function () {

        this.teamNotifications.message = ''
        this.teamNotifications.form = teamNotificationFormObject()

        Modals_TeamNotifications.TeamNotification.Show()
    },
    teamNotifications_save: function () {

        this.teamNotifications_setFormMessage('');

        let pendingRecord = { ...this.teamNotifications.form.record }

        // required fields validation

        if (!this.teamNotifications_validateForm()) {

            //this.teamNotifications_setFormMessage('Fill the required fields to continue')
            return;
        }

        console.log('sending')
        // START UPDATE/CREATE REQUEST
        this.teamNotifications_setFormSaving(true)

        const { toTeamId, message, type } = pendingRecord


        console.log({ toTeamId, message, type })

        NotificationService.SendToTeam(toTeamId, message, type)
            .then((r) => {

                const record = r.data

                if (debugTeamNotifications) {
                    console.log('update response', r)
                }

                if (record) {

                    // update data array


                    let data = [record, ...this.teamNotifications.data]

                    //data.unshift(record)

                    this.teamNotifications.data = data
                    this.teamNotifications.form = teamNotificationFormObject();


                    // feedback
                    this.teamNotifications_setFormMessage('Sent!')

                }
                else {
                    this.teamNotifications_setFormMessage(BASIC_ERROR_MESSAGE)
                }
            })
            .catch((e) => {

                console.error('Send', e)

                this.teamNotifications_setFormMessage(getAxiosErrorMessage(e))
            })
            .then(() => {
                this.teamNotifications_setFormSaving(false)
            });
    },
    teamNotifications_getAll: function (page = 0) {

        this.teamNotifications_setLoading(true)
        this.teamNotifications_setMessage('Loading...')

        const { keyword } = this.teamNotifications.filterBy
        const { length } = this.teamNotifications.dataPaging

        return BroadcastService.GetToTeam(this.teamId, page, length)
            .then((r) => {


                const { records, totalCount } = r.data

                if (debugTeamNotifications) {
                    console.log('getall teamNotification response', r)
                }

                if (records) {
                    this.teamNotifications.data = [...records]
                    this.teamNotifications.dataPaging.totalCount = totalCount
                }
                else {
                    this.teamNotifications_setMessage(BASIC_ERROR_MESSAGE)
                }
            })
            .catch((e) => {
                console.error('teamNotifications getall', e)
                this.teamNotifications_setMessage(BASIC_ERROR_MESSAGE)
            })
            .then(() => {
                this.teamNotifications_setLoading(false)
            })

    },
    teamNotifications_pageClick: function (pageNum) {
        this.teamNotifications_getAll(pageNum - 1)
    },
    teamNotifications_changeTab: function (idx) {
        this.selectedTab = idx
    },
}

//var teamNotifications_app = new Vue({
//    el: "#TeamNotificationModal",
//    data: {
//        dateOptions,
//        dateTimeOptions,
//        teamNotifications: teamNotificationObject(),
//        errors: '',
//        oNull: null,
//        teamId: null,
//        notificationTypes: {
//            data: NOTIFICATION_TYPE._toList(),
//            isLoading: false
//        }
//    },
//    computed: {
//        teamNotificationsTotalPages: function () {
//            const totalCount = this.teamNotifications.dataPaging.totalCount
//            const length = this.teamNotifications.dataPaging.length

//            const totalPages = Math.ceil(totalCount / length);

//            console.log({ totalPages })

//            return totalPages || 0
//        },
//    },
//    methods: {
//        ...teamNotificationsMethods,
//        getNotificationClasses: function (notification) {
//            //console.log('notification class',{notification})
//            let classes = [`notification-${notification.notificationTypeDisplay.toLowerCase()}`]

//            //if (notification.isNew) {
//            //    classes.push('isNew')
//            //}

//            return classes
//        },
//    },
//    mounted: function () {

//        const teamId = parseInt($('#TeamNotificationModal').attr('data-id'))

//        this.teamId = teamId

//        this.teamNotifications_getAll()
//    }
//})

