const debugCategories = true;

const categoryFields = [
    {
        name: 'name',
        displayName: 'name',
        min: 0,
        max: 255,
        type: DATA_TYPES.TEXT,
        required: false,
    },
]

const Modals_Categories = {
    Category: {
        Show: function () {
            $('#CategoryModal').modal('show');
        },
        Hide: function () {
            $('#CategoryModal').modal('hide');
        }
    }
}

const categoryFormObject = (obj) => {

    let record = obj || {
        title: null,
    }

    return {
        record,
        message: '',
        image: null,
        isLoading: false,
        isSaving: false,
    }
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
        form: categoryFormObject(),
    }
}

const categoriesMethods = {
    categories_validateForm: function (obj) {

        const form = obj || this.categories.form.record

        console.log({ form })

        var isValid = true
        let finalMessage = '';

        for (var i = 0; i < categoryFields.length; i++) {

            const field = categoryFields[i]
            const fieldValue = form[field.name];
            const validator = new CoreValidator(field.name, fieldValue, field.required, field.type, field.min, field.max)
            isValid = validator.validate()

            if (!isValid) {
                finalMessage = validator.message();
                break;
            }
        }

        if (!isValid) {
            this.categories_setFormMessage(finalMessage || 'Fill Required Fields to Continue')
        }

        return isValid;
    },
    categories_setFormMessage: function (message) {

        this.categories.form.message = message
    },
    categories_setFormLoading: function (isLoading) {
        this.categories.form.isLoading = isLoading
    },
    categories_setFormSaving: function (isSaving) {
        this.categories.form.isSaving = isSaving
    },
    categories_setProcessing: function (isProcessing) {
        this.categories.isProcessing = isProcessing
    },
    categories_setLoading: function (isLoading) {
        this.categories.isLoading = isLoading
    },
    categories_setMessage: function (message) {
        this.categories.message = message
    },
    categories_openModal: function () {

        this.categories.message = ''
        this.categories.form = categoryFormObject()


        Modals_Categories.Category.Show()
    },
    categories_edit: function (idx) {

        // INDEX VALIDATION if (idx < 0 || idx>

        if (idx > this.categories.data.length - 1) {
            console.warn(`INVALID INDEX ${idx}, TOTAL RECORDS ARE ${this.categories.data.length}`)
            return
        }

        // GET RECORD
        const record = this.categories.data[idx]



        // OPEN MODAL
        Modals_Categories.Category.Show();
        this.categories.form.isLoading = true

        this.categories_setFormLoading(true)
        this.categories_setFormMessage('Loading...');


        // GET REQUEST

        CategoriesService.GetById(record.id)
            .then(r => {

                const record = r.data

                if (!record) {
                    this.categories_setFormMessage(BASIC_ERROR_MESSAGE);
                    return
                }

                this.categories_setFormMessage('');

                this.categories.form = categoryFormObject(record);
            })
            .catch(e => {

                console.error('get error', e)

                this.categories_setFormMessage(BASIC_ERROR_MESSAGE);
            })
            .then(() => {
                this.categories_setFormLoading(false)
            })
    },
    categories_save: function () {

        this.categories_setFormMessage('');

        let pendingRecord = { ...this.categories.form.record }

        // required fields validation

        if (!this.categories_validateForm()) {

            //this.categories_setFormMessage('Fill the required fields to continue')
            return;
        }


        const sendForm = () => {

            // START UPDATE/CREATE REQUEST
            this.categories_setFormSaving(true)


            // EXISTING RECORD
            if (pendingRecord.id) {

                CategoriesService.Update(pendingRecord)
                    .then((r) => {

                        /** @type {IClientResponseModel<ISubject>} */
                        const record = r.data

                        if (debugCategories) {
                            console.log('update response', r)
                        }

                        if (record) {

                            // feedback
                            this.categories_setFormMessage('Updated!')

                            // update data array
                            let data = [...this.categories.data]

                            const idx = data.findIndex(k => k.id === record.id)

                            if (idx !== -1) {

                                data[idx] = { ...record }
                                this.categories.data = data;
                            }
                            else {
                                location.reload()
                            }
                        }
                        else {
                            this.categories_setFormMessage(BASIC_ERROR_MESSAGE)
                        }
                    })
                    .catch((e) => {

                        console.error('Updated!', e)

                        this.categories_setFormMessage(BASIC_ERROR_MESSAGE)

                    })
                    .then(() => {
                        this.categories_setFormSaving(false)
                    });

                return
            }

            // NEW RECORD
            CategoriesService.Add(pendingRecord)
                .then((r) => {

                    const record = r.data

                    if (debugCategories) {
                        console.log('add response', r)
                    }


                    if (record) {

                        let data = [...this.categories.data]
                        data.unshift(record)

                        this.categories.data = data
                        this.categories.form = categoryFormObject();

                        this.categories_setFormMessage('Added!')
                    }
                    else {
                        this.categories_setFormMessage(BASIC_ERROR_MESSAGE)
                    }

                })
                .catch((e) => {

                    console.error('create', e)

                    this.categories_setFormMessage(BASIC_ERROR_MESSAGE)
                })
                .then(() => {
                    this.categories_setFormSaving(false)
                });

        }

        sendForm()
    },
    categories_delete: function (idx) {

        // INDEX VALIDATION
        if (idx < 0 || idx > this.categories.data.length - 1) {
            console.warn(`INVALID INDEX ${idx}, TOTAL RECORDS ARE ${this.categories.data.length}`)
            return
        }

        // CONFIRMATION
        if (!confirm('Confirm Deletion')) {
            return;
        }

        /** @type {Array<ISubject>} */
        let data = [...this.categories.data];

        // set deleting
        let record = data[idx];

        this.categories_setProcessing(true)

        CategoriesService.Delete(record.id)
            .then((r) => {

                const record = r.data

                if (debugCategories) {
                    console.log('delete response', r)
                }

                if (record) {

                    data.splice(idx, 1);
                    this.categories.data = data
                    this.categories_getAll(0)
                    this.categories_setMessage('Subject deleted!')
                }
                else {
                    this.categories_setMessage(BASIC_ERROR_MESSAGE)
                }
            })
            .catch((e) => {

                //console.error('delete', e)
                let errorMessage = getAxiosErrorMessage(e);

                if (errorMessage === 'HAS_PROJECTS') {
                    errorMessage = 'Category is set to some projects and cannot be deleted'
                }

                bootbox.alert(errorMessage)

                this.categories_getAll(0)

                this.categories_setMessage(BASIC_ERROR_MESSAGE)

            })
            .then(() => {

                this.categories_setProcessing(false)
            });
    },
    categories_getAll: function (page = 0) {

        this.categories_setLoading(true)
        this.categories_setMessage('Loading...')

        return CategoriesService.GetAll()
            .then((r) => {

                /** @type {IClientResponseModel<ISubject>} */
                const record = r.data

                if (debugCategories) {
                    console.log('getall category response', r)
                }

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
    categories_pageClick: function (pageNum) {
        this.categories_getAll(pageNum - 1)
    },
}

var categories_app = new Vue({
    el: "#Categories",
    data: {
        dateOptions,
        dateTimeOptions,

        categories: categoryObject(),
    },
    computed: {
        categoriesTotalPages: function () {
            const totalCount = this.categories.dataPaging.totalCount
            const length = this.categories.dataPaging.length

            const totalPages = Math.ceil(totalCount / length);

            console.log({ totalPages })


            return totalPages || 0
        }
    },
    methods: {
        ...categoriesMethods
    },
    mounted: function () {

        this.categories_getAll()



    }
})

