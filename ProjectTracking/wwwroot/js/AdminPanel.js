

const Modals = {
    TypeOfWork: {
        Show: () => {
            $('#TypeOfWorkModal').modal('show')
        },
        Hide: () => {
            $('#TypeOfWorkModal').hide('show')
        }
    },
    MeasurementUnits: {

    },
}

var measurementUnitsMethods = {

    openMeasurementUnitsModal: function () {
        Modals.MeasurementUnits.Show()
    },
    addMeasurementUnit: function () {
        console.log('fired add')
        let { name } = { ...this.measurementUnits };

        this.measurementUnits.isLoading = true;

        MeasurementUnitsService.Create(name).done((r) => {

            this.measurementUnits.data = [r, ...this.measurementUnits.data]
            this.measurementUnits.message = 'Added!';

        }).fail((e) => {

            this.measurementUnits.message = BASIC_ERROR_MESSAGE;

        }).always(() => {

            this.measurementUnits.name = ''
            this.measurementUnits.isLoading = false;
        });
    },
    deleteMeasurementUnit: function (idx) {

        if (!confirm('Confirm Delete')) {
            return;
        }

        let data = [...this.measurementUnits.data];

        let measurementUnit = data[idx];

        MeasurementUnitsService.Delete(measurementUnit.id).done((r) => {

            if (r) {
                console.log({ data, idx })
                data.splice(idx, 1);
                this.measurementUnits.data = data
                this.measurementUnits.message = 'Deleted!';
            }
            else {
                this.measurementUnits.message = BASIC_ERROR_MESSAGE;
            }

        }).fail((e) => {

            console.error({ e })
            this.measurementUnits.message = BASIC_ERROR_MESSAGE;

        }).always(() => {
            this.measurementUnits.isLoading = false
        });
    },
    updateMeasurementUnit: function (idx) {

        if (!confirm('Confirm Save')) {
            return;
        }

        let data = [...this.measurementUnits.data];

        let measurementUnit = data[idx];

        MeasurementUnitsService.Update(measurementUnit.id, measurementUnit.name)
            .done((r) => {

                if (r) {

                    data[idx].isEdit = false;
                    this.measurementUnits.message = 'Saved!'

                    this.measurementUnits.data = data;
                }
                else {

                    this.measurementUnits.message = BASIC_ERROR_MESSAGE
                }

            }).fail((e) => {

                this.measurementUnits.message = BASIC_ERROR_MESSAGE

                console.error('save measurementUnit', e)
            }).always(() => {

            });
    },
    editMeasurementUnit: function (idx) {

        let data = [...this.measurementUnits.data];

        for (var i = 0; i < data.length; i++) {
            data[i].isEdit = i === idx;

            if (i === idx) {
                this.measurementUnits.backupEdit.name = data[i].name;
            }
        }

        this.measurementUnits.data = data;
    },
    cancelMeasurementUnitEdit: function (idx) {
        let data = [...this.measurementUnits.data];

        for (var i = 0; i < data.length; i++) {
            data[i].isEdit = false;

            if (i === idx) {
                data[i].name = this.measurementUnits.backupEdit.name;
            }
        }

        this.measurementUnits.data = data;
    }
}

var typeOfWorkMethods = {

    openTypeOfWorkModal: function () {
        Modals.TypeOfWork.Show()
    },
    addTypeOfWork: function () {
        console.log('fired add')
        let { name } = { ...this.typeOfWork };

        this.typeOfWork.isLoading = true;

        TypeOfWorkService.Create(name).done((r) => {

            this.typeOfWork.data = [r, ...this.typeOfWork.data]
            this.typeOfWork.message = 'Added!';

        }).fail((e) => {

            this.typeOfWork.message = BASIC_ERROR_MESSAGE;

        }).always(() => {

            this.typeOfWork.name = ''
            this.typeOfWork.isLoading = false;
        });
    },
    deleteTypeOfWork: function (idx) {

        if (!confirm('Confirm Delete')) {
            return;
        }

        let data = [...this.typeOfWork.data];

        let typeOfWork = data[idx];

        TypeOfWorkService.Delete(typeOfWork.id).done((r) => {

            if (r) {
                console.log({ data, idx })
                data.splice(idx, 1);
                this.typeOfWork.data = data
                this.typeOfWork.message = 'Deleted!';
            }
            else {
                this.typeOfWork.message = BASIC_ERROR_MESSAGE;
            }

        }).fail((e) => {

            console.error({ e })
            this.typeOfWork.message = BASIC_ERROR_MESSAGE;

        }).always(() => {
            this.typeOfWork.isLoading = false
        });
    },
    updateTypeOfWork: function (idx) {

        if (!confirm('Confirm Save')) {
            return;
        }

        let data = [...this.typeOfWork.data];

        let typeOfWork = data[idx];

        TypeOfWorkService.Update(typeOfWork.id, typeOfWork.name)
            .done((r) => {

                if (r) {

                    data[idx].isEdit = false;
                    this.typeOfWork.message = 'Saved!'

                    this.typeOfWork.data = data;
                }
                else {

                    this.typeOfWork.message = BASIC_ERROR_MESSAGE
                }

            }).fail((e) => {

                this.typeOfWork.message = BASIC_ERROR_MESSAGE

                console.error('save typeOfWork', e)
            }).always(() => {

            });
    },
    editTypeOfWork: function (idx) {

        let data = [...this.typeOfWork.data];

        for (var i = 0; i < data.length; i++) {
            data[i].isEdit = i === idx;

            if (i === idx) {
                this.typeOfWork.backupEdit.name = data[i].name;
            }
        }

        this.typeOfWork.data = data;
    },
    cancelTypeOfWorkEdit: function (idx) {
        let data = [...this.typeOfWork.data];

        for (var i = 0; i < data.length; i++) {
            data[i].isEdit = false;

            if (i === idx) {
                data[i].name = this.typeOfWork.backupEdit.name;
            }
        }

        this.typeOfWork.data = data;
    }
}

new Vue({
    el: '#AdminPanel',
    data: {
        typeOfWork: basicModalObject(),
        measurementUnits: basicModalObject()
    },
    methods: {
        ...typeOfWorkMethods,
        ...measurementUnitsMethods,
    },
    mounted: function () {

        TypeOfWorkService.GetAll().then(response => {

            let typeOfWorkData = [...response]

            TypeOfWorkService
                .GetActiveIds()
                .done(activeIds => {
                    console.log('GetActiveIds', activeIds)

                    for (var i = 0; i < typeOfWorkData.length; i++) {

                        typeOfWorkData[i].isLocked = activeIds.includes(typeOfWorkData[i].id)

                    }
                })
                .fail(e => {
                    console.error('GetActiveIds', e)


                }).always(() => {

                    this.typeOfWork.data = typeOfWorkData;
                })

        })
        MeasurementUnitsService.GetAll().then(response => {

            let measurementUnitsData = [...response]

            MeasurementUnitsService
                .GetActiveIds()
                .done(activeIds => {
                    console.log('GetActiveIds', activeIds)

                    for (var i = 0; i < measurementUnitsData.length; i++) {

                        measurementUnitsData[i].isLocked = activeIds.includes(measurementUnitsData[i].id)

                    }
                })
                .fail(e => {
                    console.error('GetActiveIds', e)


                })
                .always(() => {
                    this.measurementUnits.data = measurementUnitsData;

                })

            //this.measurementUnits.data = response;
        })

    }
})

