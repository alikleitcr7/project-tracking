Vue.component('paginate', VuejsPaginate)
Vue.component('date-picker', VueBootstrapDatetimePicker)

const debugProjectTasks = true;

const projectTaskFields = [
    {
        name: 'title',
        displayName: 'title',
        min: 0,
        max: 255,
        type: DATA_TYPES.TEXT,
        required: true,
    },
]

const Modals_ProjectTasks = {
    ProjectTask: {
        Show: function () {
            $('#ProjectTaskModal').modal('show');
        },
        Hide: function () {
            $('#ProjectTaskModal').modal('hide');
        }
    }
}

const getActiveProjectId = () => parseInt($('#ProjectTasks').attr('data-project'))

const projectTaskFormObject = (obj) => {

    //console.log({ obj })

    const projectId = getActiveProjectId();

    let record = obj ? {
        id: obj.id,
        title: obj.title,
        projectId,
        description: obj.description,
        startDate: obj.startDate,
        plannedEnd: obj.plannedEnd,
        actualEnd: obj.actualEnd,
        statusCode: obj.statusCode,
    } : {
            title: null,
            projectId,
            description: null,
            startDate: null,
            plannedEnd: null,
            actualEnd: null,
            statusCode: null,
        }


    return {
        record,
        message: '',
        isLoading: false,
        isSaving: false,
    }
}

const projectTaskObject = () => {
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
        form: projectTaskFormObject(),
        statuses: {
            data: [],
            isLoading: false
        }
    }
}

const projectTasksMethods = {
    projectTasks_validateForm: function (obj) {

        const form = obj || this.projectTasks.form.record

        console.log({ form })

        var isValid = true
        let finalMessage = '';

        for (var i = 0; i < projectTaskFields.length; i++) {

            const field = projectTaskFields[i]
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
            this.projectTasks_setFormMessage(finalMessage || 'Fill Required Fields to Continue')
        }

        return isValid;
    },
    projectTasks_setFormMessage: function (message) {

        this.projectTasks.form.message = message
    },
    projectTasks_setFormLoading: function (isLoading) {
        this.projectTasks.form.isLoading = isLoading
    },
    projectTasks_setFormSaving: function (isSaving) {
        this.projectTasks.form.isSaving = isSaving
    },
    projectTasks_setProcessing: function (isProcessing) {
        this.projectTasks.isProcessing = isProcessing
    },
    projectTasks_setLoading: function (isLoading) {
        this.projectTasks.isLoading = isLoading
    },
    projectTasks_setMessage: function (message) {
        this.projectTasks.message = message
    },
    projectTasks_openModal: function () {

        this.projectTasks.message = ''
        this.projectTasks.form = projectTaskFormObject()


        Modals_ProjectTasks.ProjectTask.Show()
    },
    projectTasks_edit: function (idx) {

        // INDEX VALIDATION if (idx < 0 || idx>

        if (idx > this.projectTasks.data.length - 1) {
            console.warn(`INVALID INDEX ${idx}, TOTAL RECORDS ARE ${this.projectTasks.data.length}`)
            return
        }

        // GET RECORD
        const record = this.projectTasks.data[idx]

        // OPEN MODAL
        Modals_ProjectTasks.ProjectTask.Show();
        this.projectTasks.form.isLoading = true

        this.projectTasks_setFormLoading(true)
        this.projectTasks_setFormMessage('Loading...');

        // GET REQUEST

        ProjectTasksService.GetById(record.id)
            .then(r => {

                const record = r.data

                if (!record) {
                    this.projectTasks_setFormMessage(BASIC_ERROR_MESSAGE);
                    return
                }

                this.projectTasks_setFormMessage('');

                this.projectTasks.form = projectTaskFormObject(record);
            })
            .catch(e => {

                console.error('get error', e)

                this.projectTasks_setFormMessage(BASIC_ERROR_MESSAGE);
            })
            .then(() => {
                this.projectTasks_setFormLoading(false)
            })
    },

    projectTasks_save: function () {

        this.projectTasks_setFormMessage('');

        let pendingRecord = { ...this.projectTasks.form.record }

        // required fields validation

        if (!this.projectTasks_validateForm()) {

            //this.projectTasks_setFormMessage('Fill the required fields to continue')
            return;
        }

        console.log('sending')

        const sendForm = () => {

            // START UPDATE/CREATE REQUEST
            this.projectTasks_setFormSaving(true)


            // EXISTING RECORD
            if (pendingRecord.id) {

                ProjectTasksService.Save(pendingRecord)
                    .then((r) => {

                        /** @type {IClientResponseModel<ISubject>} */
                        const record = r.data

                        if (debugProjectTasks) {
                            console.log('update response', r)
                        }

                        if (record) {

                            // feedback
                            this.projectTasks_setFormMessage('Updated!')

                            // update data array
                            let data = [...this.projectTasks.data]

                            const idx = data.findIndex(k => k.id === record.id)

                            if (idx !== -1) {

                                data[idx] = { ...record }
                                this.projectTasks.data = data;
                            }
                            else {
                                location.reload()
                            }
                        }
                        else {
                            this.projectTasks_setFormMessage(BASIC_ERROR_MESSAGE)
                        }
                    })
                    .catch((e) => {

                        console.error('Updated!', e)

                        this.projectTasks_setFormMessage(BASIC_ERROR_MESSAGE)

                    })
                    .then(() => {
                        this.projectTasks_setFormSaving(false)
                    });

                return
            }

            // NEW RECORD
            ProjectTasksService.Save(pendingRecord)
                .then((r) => {

                    const record = r.data

                    if (debugProjectTasks) {
                        console.log('add response', r)
                    }


                    if (record) {

                        let data = [...this.projectTasks.data]
                        data.unshift(record)

                        this.projectTasks.data = data
                        this.projectTasks.form = projectTaskFormObject();

                        this.projectTasks_setFormMessage('Added!')
                    }
                    else {
                        this.projectTasks_setFormMessage(BASIC_ERROR_MESSAGE)
                    }

                })
                .catch((e) => {
                    this.projectTasks_setFormMessage(getAxiosErrorMessage(e))
                })
                .then(() => {
                    this.projectTasks_setFormSaving(false)
                });

        }

        sendForm()
    },
    projectTasks_delete: function (idx) {

        // INDEX VALIDATION
        if (idx < 0 || idx > this.projectTasks.data.length - 1) {
            console.warn(`INVALID INDEX ${idx}, TOTAL RECORDS ARE ${this.projectTasks.data.length}`)
            return
        }

        // CONFIRMATION
        if (!confirm('Confirm Deletion')) {
            return;
        }

        /** @type {Array<ISubject>} */
        let data = [...this.projectTasks.data];

        // set deleting
        let record = data[idx];

        this.projectTasks_setProcessing(true)

        ProjectTasksService.Delete(record.id)
            .then((r) => {

                const record = r.data

                if (debugProjectTasks) {
                    console.log('delete response', r)
                }

                if (record) {

                    data.splice(idx, 1);
                    this.projectTasks.data = data
                    this.projectTasks_getAll(0)
                    this.projectTasks_setMessage('Subject deleted!')
                }
                else {
                    this.projectTasks_setMessage(BASIC_ERROR_MESSAGE)
                }
            })
            .catch((e) => {

                console.error('delete', e)

                this.projectTasks_setMessage(BASIC_ERROR_MESSAGE)

            })
            .then(() => {

                this.projectTasks_setProcessing(false)
            });
    },
    projectTasks_getAll: function (page = 0) {

        this.projectTasks_setLoading(true)
        this.projectTasks_setMessage('Loading...')

        const { keyword } = this.projectTasks.filterBy
        const { length } = this.projectTasks.dataPaging
        const projectId = getActiveProjectId();

        return ProjectTasksService.Search(keyword, projectId, page, length)
            .then((r) => {

                /** @type {IClientResponseModel<ISubject>} */
                const { record, totalCount } = r.data

                if (debugProjectTasks) {
                    console.log('getall projectTask response', r)
                }

                if (record) {
                    this.projectTasks.data = [...record]
                    this.projectTasks.dataPaging.totalCount = totalCount
                }
                else {
                    this.projectTasks_setMessage(BASIC_ERROR_MESSAGE)
                }
            })
            .catch((e) => {
                console.error('projectTasks getall', e)
                this.projectTasks_setMessage(BASIC_ERROR_MESSAGE)
            })
            .then(() => {
                this.projectTasks_setLoading(false)
            })

    },
    projectTasks_pageClick: function (pageNum) {
        this.projectTasks_getAll(pageNum - 1)
    },


    projectTasks_setStatusesLoading: function (isLoading) {
        this.projectTasks.statuses.isLoading = isLoading
    },
    projectTasks_getAllStatuses: function () {

        this.projectTasks_setStatusesLoading(true)

        return ProjectTasksService.GetProjectTaskStatuses()
            .then((r) => {

                /** @type {IClientResponseModel<ISubject>} */
                const record = r.data

                if (debugProjectTasks) {
                    console.log('getall projectTask response', r)
                }

                if (record) {
                    this.projectTasks.statuses.data = [...record]
                }
                else {
                    this.projectTasks_setMessage(BASIC_ERROR_MESSAGE)
                }
            })
            .catch((e) => {
                console.error('projectTasks getall', e)
                this.projectTasks_setMessage(getAxiosErrorMessage(e))
            })
            .then(() => {
                this.projectTasks_setStatusesLoading(false)
            })

    },
}

var projectTasks_app = new Vue({
    el: "#ProjectTasks",
    data: {
        dateOptions,
        dateTimeOptions,
        projectTasks: projectTaskObject(),
        projectTaskId: getActiveProjectId(),
        errors: '',
        oNull: null,
    },
    computed: {
        projectTasksTotalPages: function () {
            const totalCount = this.projectTasks.dataPaging.totalCount
            const length = this.projectTasks.dataPaging.length

            const totalPages = Math.ceil(totalCount / length);

            console.log({ totalPages })

            return totalPages || 0
        },
    },
    methods: {
        ...projectTasksMethods,
    },
    mounted: function () {
        this.projectTasks_getAll()
        this.projectTasks_getAllStatuses()
    }
})

