Vue.component('paginate', VuejsPaginate)
Vue.component('date-picker', VueBootstrapDatetimePicker)

const debugProjects = true;

const projectFields = [
    {
        name: 'title',
        displayName: 'title',
        min: 0,
        max: 255,
        type: DATA_TYPES.TEXT,
        required: true,
    },
    {
        name: 'categoryId',
        displayName: 'Category',
        min: 1,
        type: DATA_TYPES.NUMBER,
        required: true,
    },
]

const Modals_Projects = {
    Project: {
        Show: function () {
            $('#ProjectModal').modal('show');
        },
        Hide: function () {
            $('#ProjectModal').modal('hide');
        }
    }
}

const projectFormObject = (obj) => {
    console.log({ obj })
    let record = obj ? {
        id: obj.id,
        title: obj.title,
        categoryId: obj.categoryId,
        description: obj.description,
        startDate: obj.startDate,
        plannedEnd: obj.plannedEnd,
        actualEnd: obj.actualEnd,
        statusCode: obj.statusCode,
        teamsIds: obj.teamsProjects ? obj.teamsProjects.map(k => k.teamId) : []
    } : {
            title: null,
            categoryId: null,
            description: null,
            startDate: null,
            plannedEnd: null,
            actualEnd: null,
            statusCode: null,
            teamsIds: [],
        }


    return {
        record,
        message: '',
        isLoading: false,
        isSaving: false,
    }
}

const projectObject = () => {
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
        form: projectFormObject(),
        statuses: {
            data: [],
            isLoading: false
        }
    }
}

const projectsMethods = {
    projects_validateForm: function (obj) {

        const form = obj || this.projects.form.record

        console.log({ form })

        var isValid = true
        let finalMessage = '';

        for (var i = 0; i < projectFields.length; i++) {

            const field = projectFields[i]
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
            this.projects_setFormMessage(finalMessage || 'Fill Required Fields to Continue')
        }

        return isValid;
    },
    projects_setFormMessage: function (message) {

        this.projects.form.message = message
    },
    projects_setFormLoading: function (isLoading) {
        this.projects.form.isLoading = isLoading
    },
    projects_setFormSaving: function (isSaving) {
        this.projects.form.isSaving = isSaving
    },
    projects_setProcessing: function (isProcessing) {
        this.projects.isProcessing = isProcessing
    },
    projects_setLoading: function (isLoading) {
        this.projects.isLoading = isLoading
    },
    projects_setMessage: function (message) {
        this.projects.message = message
    },
    projects_openModal: function () {

        this.projects.message = ''
        this.projects.form = projectFormObject()


        Modals_Projects.Project.Show()
    },
    projects_edit: function (idx) {

        // INDEX VALIDATION if (idx < 0 || idx>

        if (idx > this.projects.data.length - 1) {
            console.warn(`INVALID INDEX ${idx}, TOTAL RECORDS ARE ${this.projects.data.length}`)
            return
        }

        // GET RECORD
        const record = this.projects.data[idx]

        // OPEN MODAL
        Modals_Projects.Project.Show();
        this.projects.form.isLoading = true

        this.projects_setFormLoading(true)
        this.projects_setFormMessage('Loading...');

        // GET REQUEST

        ProjectsService.GetById(record.id)
            .then(r => {

                const record = r.data

                if (!record) {
                    this.projects_setFormMessage(BASIC_ERROR_MESSAGE);
                    return
                }

                this.projects_setFormMessage('');

                this.projects.form = projectFormObject(record);
            })
            .catch(e => {

                console.error('get error', e)

                this.projects_setFormMessage(BASIC_ERROR_MESSAGE);
            })
            .then(() => {
                this.projects_setFormLoading(false)
            })
    },

    projects_save: function () {

        this.projects_setFormMessage('');

        let pendingRecord = { ...this.projects.form.record }

        // required fields validation

        if (!this.projects_validateForm()) {

            //this.projects_setFormMessage('Fill the required fields to continue')
            return;
        }

        console.log('sending')

        const sendForm = () => {

            // START UPDATE/CREATE REQUEST
            this.projects_setFormSaving(true)


            // EXISTING RECORD
            if (pendingRecord.id) {

                ProjectsService.Save(pendingRecord)
                    .then((r) => {

                        /** @type {IClientResponseModel<ISubject>} */
                        const record = r.data

                        if (debugProjects) {
                            console.log('update response', r)
                        }

                        if (record) {

                            // feedback
                            this.projects_setFormMessage('Updated!')

                            // update data array
                            let data = [...this.projects.data]

                            const idx = data.findIndex(k => k.id === record.id)

                            if (idx !== -1) {

                                data[idx] = { ...record }
                                this.projects.data = data;
                            }
                            else {
                                location.reload()
                            }
                        }
                        else {
                            this.projects_setFormMessage(BASIC_ERROR_MESSAGE)
                        }
                    })
                    .catch((e) => {

                        console.error('Updated!', e)

                        this.projects_setFormMessage(getAxiosErrorMessage(e))

                    })
                    .then(() => {
                        this.projects_setFormSaving(false)
                    });

                return
            }

            // NEW RECORD
            ProjectsService.Save(pendingRecord)
                .then((r) => {

                    const record = r.data

                    if (debugProjects) {
                        console.log('add response', r)
                    }


                    if (record) {

                        let data = [...this.projects.data]
                        data.unshift(record)

                        this.projects.data = data
                        this.projects.form = projectFormObject();

                        this.projects_setFormMessage('Added!')
                    }
                    else {
                        this.projects_setFormMessage(BASIC_ERROR_MESSAGE)
                    }

                })
                .catch((e) => {
                    this.projects_setFormMessage(getAxiosErrorMessage(e))
                })
                .then(() => {
                    this.projects_setFormSaving(false)
                });

        }

        sendForm()
    },
    projects_delete: function (idx) {

        // INDEX VALIDATION
        if (idx < 0 || idx > this.projects.data.length - 1) {
            console.warn(`INVALID INDEX ${idx}, TOTAL RECORDS ARE ${this.projects.data.length}`)
            return
        }

        // CONFIRMATION
        if (!confirm('Confirm Deletion')) {
            return;
        }

        /** @type {Array<ISubject>} */
        let data = [...this.projects.data];

        // set deleting
        let record = data[idx];

        this.projects_setProcessing(true)

        ProjectsService.Delete(record.id)
            .then((r) => {

                const record = r.data

                if (debugProjects) {
                    console.log('delete response', r)
                }

                if (record) {

                    data.splice(idx, 1);
                    this.projects.data = data
                    this.projects_getAll(0)
                    this.projects_setMessage('Subject deleted!')
                }
                else {
                    this.projects_setMessage(BASIC_ERROR_MESSAGE)
                }
            })
            .catch((e) => {

                console.error('delete', e)

                this.projects_setMessage(BASIC_ERROR_MESSAGE)

            })
            .then(() => {

                this.projects_setProcessing(false)
            });
    },
    projects_getAll: function (page = 0) {

        this.projects_setLoading(true)
        this.projects_setMessage('Loading...')

        const { keyword, categoryId } = this.projects.filterBy
        const { length } = this.projects.dataPaging

        return ProjectsService.Search(categoryId, keyword, page, length)
            .then((r) => {

                /** @type {IClientResponseModel<ISubject>} */
                const { record, totalCount } = r.data

                if (debugProjects) {
                    console.log('getall project response', r)
                }

                if (record) {
                    this.projects.data = [...record]
                    this.projects.dataPaging.totalCount = totalCount
                }
                else {
                    this.projects_setMessage(BASIC_ERROR_MESSAGE)
                }
            })
            .catch((e) => {
                console.error('projects getall', e)
                this.projects_setMessage(BASIC_ERROR_MESSAGE)
            })
            .then(() => {
                this.projects_setLoading(false)
            })

    },
    projects_pageClick: function (pageNum) {
        this.projects_getAll(pageNum - 1)
    },


    projects_setStatusesLoading: function (isLoading) {
        this.projects.statuses.isLoading = isLoading
    },
    projects_getAllStatuses: function () {

        this.projects_setStatusesLoading(true)

        return ProjectsService.GetProjectStatuses()
            .then((r) => {

                /** @type {IClientResponseModel<ISubject>} */
                const record = r.data

                if (debugProjects) {
                    console.log('getall project response', r)
                }

                if (record) {
                    this.projects.statuses.data = [...record]
                }
                else {
                    this.projects_setMessage(BASIC_ERROR_MESSAGE)
                }
            })
            .catch((e) => {
                console.error('projects getall', e)
                this.projects_setMessage(getAxiosErrorMessage(e))
            })
            .then(() => {
                this.projects_setStatusesLoading(false)
            })

    },
}

const categoryObject = () => {
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
        isLoading: false,
        isProcessing: false,
        message: '',
    }
}

const categoriesMethods = {
    categories_setLoading: function (isLoading) {
        this.categories.isLoading = isLoading
    },
    categories_setMessage: function (message) {
        this.categories.message = message
    },
    categories_getAll: function (page = 0) {

        this.categories_setLoading(true)
        this.categories_setMessage('Loading...')

        return CategoriesService.GetAll()
            .then((r) => {

                /** @type {IClientResponseModel<ISubject>} */
                const record = r.data

                if (record) {
                    this.categories.data = [...record]
                    //this.categories.dataPaging.totalCount = extraData.totalCount
                }
                else {
                    this.categories_setMessage(BASIC_ERROR_MESSAGE)
                }
            })
            .catch((e) => {
                console.error('categories getall', e)
                this.categories_setMessage(BASIC_ERROR_MESSAGE)
            })
            .then(() => {
                this.categories_setLoading(false)
            })
    },
}

const teamObject = () => {
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
        isLoading: false,
        isProcessing: false,
        message: '',
    }
}

const teamsMethods = {
    teams_setLoading: function (isLoading) {
        this.teams.isLoading = isLoading
    },
    teams_setMessage: function (message) {
        this.teams.message = message
    },
    teams_getAll: function (teamId) {

        this.teams_setLoading(true)
        this.teams_setMessage('Loading...')

        return TeamsService.GetAll()
            .then((r) => {

                /** @type {IClientResponseModel<ISubject>} */
                const record = r.data

                //if (debugTeams) {
                //    console.log('getall team response', r)
                //}

                if (record) {
                    this.teams.data = [...record]
                    //this.teams.dataPaging.totalCount = extraData.totalCount
                }
                else {
                    this.teams_setMessage(BASIC_ERROR_MESSAGE)
                }
            })
            .catch((e) => {
                console.error('teams getall', e)
                this.teams_setMessage(BASIC_ERROR_MESSAGE)
            })
            .then(() => {
                this.teams_setLoading(false)
            })
    },
}

var projects_app = new Vue({
    el: "#Projects",
    data: {
        dateOptions,
        dateTimeOptions,
        categories: categoryObject(),
        projects: projectObject(),
        teams: teamObject(),
        errors: '',
        oNull: null,
    },
    computed: {
        projectsTotalPages: function () {
            const totalCount = this.projects.dataPaging.totalCount
            const length = this.projects.dataPaging.length

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
        ...projectsMethods,
        ...categoriesMethods,
        ...teamsMethods,
    },
    mounted: function () {
        this.projects_getAll()
        this.categories_getAll()
        this.teams_getAll()
        this.projects_getAllStatuses()
    }
})

