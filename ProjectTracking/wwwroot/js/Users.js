Vue.component('paginate', VuejsPaginate)
Vue.component('date-picker', VueBootstrapDatetimePicker)

const debugUsers = true;

const userFields = [
    {
        name: 'firstName',
        displayName: 'First Name',
        min: 0,
        max: 255,
        type: DATA_TYPES.TEXT,
        required: true,
    },
    {
        name: 'lastName',
        displayName: 'Last Name',
        min: 0,
        max: 255,
        type: DATA_TYPES.TEXT,
        required: true,
    },
    {
        name: 'email',
        displayName: 'Email',
        min: 1,
        type: DATA_TYPES.TEXT,
        required: true,
    },
]

const Modals_Users = {
    User: {
        Show: function () {
            $('#UserModal').modal('show');
        },
        Hide: function () {
            $('#UserModal').modal('hide');
        }
    },
    Supervisor: {
        Show: function () {
            $('#SupervisorsModal').modal('show');
        },
        Hide: function () {
            $('#SupervisorsModal').modal('hide');
        }

    },
    UserRole: {
        Show: function () {
            $('#UserRoleModal').modal('show');
        },
        Hide: function () {
            $('#UserRoleModal').modal('hide');
        }
    },
}

const userFormObject = (obj) => {

    let record = obj ? {
        id: obj.id,
        firstName: obj.firstName,
        lastName: obj.lastName,
        email: obj.email,
    } : {
            id: null,
            firstName: null,
            lastName: null,
            email: null,
        }

    return {
        record,
        message: '',
        isLoading: false,
        isSaving: false,
    }
}

const userObject = () => {
    return {
        data: [],
        filterBy: {
            keyword: '',
            categoryId: null,
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
        form: userFormObject(),
        statuses: {
            data: [],
            isLoading: false
        }
    }
}

const usersMethods = {
    users_validateForm: function (obj) {

        const form = obj || this.users.form.record

        console.log({ form })

        var isValid = true
        let finalMessage = '';

        for (var i = 0; i < userFields.length; i++) {

            const field = userFields[i]
            const fieldValue = form[field.name];

            console.log('field validation', { field, fieldValue })

            const validator = new CoreValidator(field.name, fieldValue, field.required, field.type, field.min, field.max)
            isValid = validator.validate()


            if (!isValid) {

                finalMessage = validator.message();
                console.log(' validation 3')

                break;
            }
        }

        console.log('end validation')


        if (!isValid) {
            this.users_setFormMessage(finalMessage || 'Fill Required Fields to Continue')
        }

        return isValid;
    },
    users_setFormMessage: function (message) {

        this.users.form.message = message
    },
    users_setFormLoading: function (isLoading) {
        this.users.form.isLoading = isLoading
    },
    users_setFormSaving: function (isSaving) {
        this.users.form.isSaving = isSaving
    },
    users_setProcessing: function (isProcessing) {
        this.users.isProcessing = isProcessing
    },
    users_setLoading: function (isLoading) {
        this.users.isLoading = isLoading
    },
    users_setMessage: function (message) {
        this.users.message = message
    },
    users_openModal: function () {

        this.users.message = ''
        this.users.form = userFormObject()


        Modals_Users.User.Show()
    },
    users_edit: function (idx) {

        // INDEX VALIDATION if (idx < 0 || idx>

        if (idx > this.users.data.length - 1) {
            console.warn(`INVALID INDEX ${idx}, TOTAL RECORDS ARE ${this.users.data.length}`)
            return
        }

        // GET RECORD
        const record = this.users.data[idx]

        // OPEN MODAL
        Modals_Users.User.Show();
        this.users.form.isLoading = true

        this.users_setFormLoading(true)
        this.users_setFormMessage('Loading...');

        // GET REQUEST

        UsersService.GetById(record.id)
            .then(r => {

                const record = r.data

                if (!record) {
                    this.users_setFormMessage(BASIC_ERROR_MESSAGE);
                    return
                }

                this.users_setFormMessage('');

                this.users.form = userFormObject(record);
            })
            .catch(e => {

                console.error('get error', e)

                this.users_setFormMessage(BASIC_ERROR_MESSAGE);
            })
            .then(() => {
                this.users_setFormLoading(false)
            })
    },

    users_isAdmin: function (user) {
        return user ? user.userName === 'admin' : false
    },
    users_save: function () {

        this.users_setFormMessage('');

        let pendingRecord = { ...this.users.form.record }

        // required fields validation

        if (!this.users_validateForm()) {

            //this.users_setFormMessage('Fill the required fields to continue')
            return;
        }

        console.log('sending')

        const sendForm = () => {

            // START UPDATE/CREATE REQUEST
            this.users_setFormSaving(true)


            // EXISTING RECORD
            if (pendingRecord.id) {

                UsersService.Save(pendingRecord)
                    .then((r) => {

                        /** @type {IClientResponseModel<ISubject>} */
                        const record = r.data

                        if (debugUsers) {
                            console.log('update response', r)
                        }

                        if (record) {

                            // feedback
                            this.users_setFormMessage('Updated!')

                            // update data array
                            let data = [...this.users.data]

                            const idx = data.findIndex(k => k.id === record.id)

                            if (idx !== -1) {

                                data[idx] = { ...record }
                                this.users.data = data;
                            }
                            else {
                                location.reload()
                            }
                        }
                        else {
                            this.users_setFormMessage(BASIC_ERROR_MESSAGE)
                        }
                    })
                    .catch((e) => {

                        console.error('Updated!', e)

                        this.users_setFormMessage(getAxiosErrorMessage(e))

                    })
                    .then(() => {
                        this.users_setFormSaving(false)
                    });

                return
            }

            // NEW RECORD
            UsersService.Save(pendingRecord)
                .then((r) => {

                    const record = r.data

                    if (debugUsers) {
                        console.log('add response', r)
                    }


                    if (record) {

                        let data = [...this.users.data]
                        data.unshift(record)

                        this.users.data = data
                        this.users.form = userFormObject();

                        this.users_setFormMessage('Added!')
                    }
                    else {
                        this.users_setFormMessage(BASIC_ERROR_MESSAGE)
                    }

                })
                .catch((e) => {
                    this.users_setFormMessage(getAxiosErrorMessage(e))
                })
                .then(() => {
                    this.users_setFormSaving(false)
                });

        }

        sendForm()
    },
    users_delete: function (idx) {

        // INDEX VALIDATION
        if (idx < 0 || idx > this.users.data.length - 1) {
            console.warn(`INVALID INDEX ${idx}, TOTAL RECORDS ARE ${this.users.data.length}`)
            return
        }

        // CONFIRMATION
        if (!confirm('Confirm Deletion')) {
            return;
        }

        /** @type {Array<ISubject>} */
        let data = [...this.users.data];

        // set deleting
        let record = data[idx];

        this.users_setProcessing(true)

        UsersService.Delete(record.id)
            .then((r) => {

                const record = r.data

                if (debugUsers) {
                    console.log('delete response', r)
                }

                if (record) {

                    data.splice(idx, 1);
                    this.users.data = data
                    this.users_getAll(0)
                    this.users_setMessage('Subject deleted!')
                }
                else {
                    this.users_setMessage(BASIC_ERROR_MESSAGE)
                }
            })
            .catch((e) => {

                console.error('delete', e)

                this.users_setMessage(BASIC_ERROR_MESSAGE)

            })
            .then(() => {

                this.users_setProcessing(false)
            });
    },
    users_getAll: function (page = 0) {

        this.users_setLoading(true)
        this.users_setMessage('Loading...')

        const { keyword } = this.users.filterBy
        const { length } = this.users.dataPaging

        return UsersService.Search(keyword, page, length)
            .then((r) => {

                /** @type {IClientResponseModel<ISubject>} */
                const { record, totalCount } = r.data

                if (debugUsers) {
                    console.log('getall user response', r)
                }

                if (record) {
                    this.users.data = [...record]
                    this.users.dataPaging.totalCount = totalCount
                }
                else {
                    this.users_setMessage(BASIC_ERROR_MESSAGE)
                }
            })
            .catch((e) => {
                console.error('users getall', e)
                this.users_setMessage(BASIC_ERROR_MESSAGE)
            })
            .then(() => {
                this.users_setLoading(false)
            })

    },
    users_pageClick: function (pageNum) {
        this.users_getAll(pageNum - 1)
    },


    users_setStatusesLoading: function (isLoading) {
        this.users.statuses.isLoading = isLoading
    },
    users_getAllStatuses: function () {

        this.users_setStatusesLoading(true)

        return UsersService.GetUserStatuses()
            .then((r) => {

                /** @type {IClientResponseModel<ISubject>} */
                const record = r.data

                if (debugUsers) {
                    console.log('getall user response', r)
                }

                if (record) {
                    this.users.statuses.data = [...record]
                }
                else {
                    this.users_setMessage(BASIC_ERROR_MESSAGE)
                }
            })
            .catch((e) => {
                console.error('users getall', e)
                this.users_setMessage(getAxiosErrorMessage(e))
            })
            .then(() => {
                this.users_setStatusesLoading(false)
            })

    },
}

const supervisorFormObject = (userId = null, teamIds = []) => {

    let record = {
        userId,
        teamIds
    }

    return {
        record,
        message: '',
        image: null,
        isLoading: false,
        isSaving: false,
    }
}

const supervisorObject = () => {
    return {
        data: [],
        dataPaging: {
            page: 0,
            totalPages: 0,
            length: 5,
            totalCount: 0,
            pagination: 'custom-pagination',
            prev: 'Prev',
            next: 'Next',
        },
        form: supervisorFormObject(),
        isLoading: false,
        isProcessing: false,
        message: '',
    }
}

const supervisorsMethods = {
    supervisors_setFormMessage: function (message) {

        this.supervisors.form.message = message
    },
    supervisors_setFormLoading: function (isLoading) {
        this.supervisors.form.isLoading = isLoading
    },
    supervisors_setFormSaving: function (isSaving) {
        this.supervisors.form.isSaving = isSaving
    },
    supervisors_edit: function (userId) {

        // OPEN MODAL
        Modals_Teams.Supervisors.Show();
        this.supervisors.form.isLoading = true

        this.supervisors_setFormLoading(true)
        this.supervisors_setFormMessage('Loading...');

        this.supervisors_getAll(userId)

        // GET REQUEST
        TeamsService.GetAllExecludingSupervising(userId)
            .then(r => {

                const record = r.data

                if (!record) {
                    this.supervisors_setFormMessage(BASIC_ERROR_MESSAGE);
                    return
                }

                this.supervisors_setFormMessage('');

                this.supervisors.form = supervisorFormObject(teamId, record);

            })
            .catch(e => {

                console.error('get error', e)

                this.supervisors_setFormMessage(BASIC_ERROR_MESSAGE);
            })
            .then(() => {
                this.supervisors_setFormLoading(false)
            })
    },
    supervisors_save: function () {

        this.supervisors_setFormMessage('');

        let pendingRecord = { ...this.supervisors.form.record }

        // START UPDATE/CREATE REQUEST
        this.supervisors_setFormSaving(true)

        // UPDATE
        TeamsService.AddRemoveTeamsUsers(pendingRecord)
            .then((r) => {

                const record = r.data

                if (record) {

                    // update members count (teams)
                    let data = [...this.teams.data]

                    let teamToUpdate = data.find(k => k.id === pendingRecord.teamId)

                    teamToUpdate.membersCount = pendingRecord.userIds.length

                    this.teams.data = data

                    // saved feeback (supervisors)
                    this.supervisors_setFormMessage('Saved!')
                }
                else {
                    this.supervisors_setFormMessage(BASIC_ERROR_MESSAGE)
                }

            })
            .catch((e) => {
                console.error('save team supervisors', e)
                this.supervisors_setFormMessage(getAxiosErrorMessage(e))
            })
            .then(() => {
                this.supervisors_setFormSaving(false)
            });
    },
    supervisors_setLoading: function (isLoading) {
        this.supervisors.isLoading = isLoading
    },
    supervisors_setMessage: function (message) {
        this.supervisors.message = message
    },
    supervisors_getAll: function (userId) {

        this.supervisors_setLoading(true)
        this.supervisors_setMessage('Loading...')
        this.supervisors.data = []

        return TeamsService.GetAllExecludingSupervising(userId)
            .then((r) => {

                /** @type {IClientResponseModel<ISubject>} */
                const record = r.data

                if (record) {
                    this.supervisors.data = [...record]
                }
                else {
                    this.supervisors_setMessage(BASIC_ERROR_MESSAGE)
                }
            })
            .catch((e) => {
                console.error('supervisors getall', e)
                this.supervisors_setMessage(BASIC_ERROR_MESSAGE)
            })
            .then(() => {
                this.supervisors_setLoading(false)
            })
    },
}


const userRoleFormObject = (userId = null, role = null) => {

    let record = {
        userId,
        role
    }

    return {
        record,
        message: '',
        image: null,
        isLoading: false,
        isSaving: false,
    }
}

const userRoleObject = () => {
    return {
        data: [],
        form: userRoleFormObject(),
        isLoading: false,
        isProcessing: false,
        message: '',
    }
}

const userRolesMethods = {
    userRoles_setFormMessage: function (message) {

        this.userRoles.form.message = message
    },
    userRoles_setFormLoading: function (isLoading) {
        this.userRoles.form.isLoading = isLoading
    },
    userRoles_setFormSaving: function (isSaving) {
        this.userRoles.form.isSaving = isSaving
    },
    userRoles_edit: function (userId) {

        // OPEN MODAL
        Modals_Teams.UserRole.Show();

        this.userRoles.form.isLoading = true

        this.userRoles_setFormLoading(true)
        this.userRoles_setFormMessage('Loading...');

        // GET REQUEST
        UsersService.GetUserRole(userId)
            .then(r => {

                const record = r.data

                if (!record) {
                    this.userRoles_setFormMessage(BASIC_ERROR_MESSAGE);
                    return
                }

                this.userRoles_setFormMessage('');

                this.userRoles.form = userRoleFormObject(userId, record);

            })
            .catch(e => {

                console.error('get error', e)

                this.userRoles_setFormMessage(BASIC_ERROR_MESSAGE);
            })
            .then(() => {
                this.userRoles_setFormLoading(false)
            })
    },
    userRoles_save: function () {

        this.userRoles_setFormMessage('');

        let pendingRecord = { ...this.userRoles.form.record }

        // START UPDATE/CREATE REQUEST
        this.userRoles_setFormSaving(true)

        const { userId, role } = pendingRecord

        // UPDATE
        UsersService.SetRole(userId, role)
            .then((r) => {

                const record = r.data

                if (record) {

                    // update members count (teams)
                    let data = [...this.users.data]

                    let recordToUpdate = data.find(k => k.id === pendingRecord.id)

                    recordToUpdate.role = role

                    this.users.data = data

                    // saved feeback (userRoles)
                    this.userRoles_setFormMessage('Saved!')
                }
                else {
                    this.userRoles_setFormMessage(BASIC_ERROR_MESSAGE)
                }

            })
            .catch((e) => {
                console.error('save team userRoles', e)
                this.userRoles_setFormMessage(getAxiosErrorMessage(e))
            })
            .then(() => {
                this.userRoles_setFormSaving(false)
            });
    },
    userRoles_setLoading: function (isLoading) {
        this.userRoles.isLoading = isLoading
    },
    userRoles_setMessage: function (message) {
        this.userRoles.message = message
    },
    userRoles_getAll: function () {

        this.userRoles_setLoading(true)
        this.userRoles_setMessage('Loading...')
        this.userRoles.data = []

        return UsersService.GetRoles()
            .then((r) => {

                /** @type {IClientResponseModel<ISubject>} */
                const record = r.data

                if (record) {
                    this.userRoles.data = [...record]
                }
                else {
                    this.userRoles_setMessage(BASIC_ERROR_MESSAGE)
                }
            })
            .catch((e) => {
                console.error('userRoles getall', e)
                this.userRoles_setMessage(getAxiosErrorMessage(e))
            })
            .then(() => {
                this.userRoles_setLoading(false)
            })
    },
}


var users_app = new Vue({
    el: "#Users",
    data: {
        dateOptions,
        dateTimeOptions,
        users: userObject(),
        supervisors: supervisorObject(),
        userRoles: userRoleObject(),
        errors: '',
        oNull: null,
    },
    computed: {
        usersTotalPages: function () {
            const totalCount = this.users.dataPaging.totalCount
            const length = this.users.dataPaging.length

            const totalPages = Math.ceil(totalCount / length);

            console.log({ totalPages })

            return totalPages || 0
        },
        categoriesToFilter: function () {

            if (this.categories.isLoading) {
                return []
            }

            return [{ id: null, name: 'all' }, ...this.categories.data]
        },
    },
    methods: {
        ...usersMethods,
        ...supervisorsMethods,
        ...userRolesMethods,
    },
    mounted: function () {
        this.users_getAll()
        this.userRoles_getAll()
        //this.supervisors_getAll()
    }
})

