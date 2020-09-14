Vue.component('date-picker', VueBootstrapDatetimePicker);


//const holidays = [
//    moment('2019-11-06T14:16:31+00:00'),
//    moment('2019-11-07T14:16:31+00:00'),
//]

//calculateBusinessDays(moment('2019-11-05T14:16:31+00:00'), moment('2019-11-12T14:16:31+00:00'),holidays)


const debugHolidays = true;

const Modals = {
    Holiday: {
        Show: function () {
            $('#Holiday').modal('show');
        },
        Hide: function () {
            $('#Holiday').modal('hide');
        }
    }
}

const holidayFormObject = () => {
    return {
        id: '',
        title: '',
        note: '',
        date: null,
    }
}


const holidayObject = () => {
    return {
        data: [],
        form: holidayFormObject(),
        backupEdit: holidayFormObject(),
        isLoading: false,
        message: ''
    }
}

var holidaysMethods = {
    openHolidaysModal: function () {
        Modals.Holiday.Show()
    },
    addHoliday: function () {

        this.holidays.message = '';

        /** @type {IHoliday} */
        const form = { ...this.holidays.form }

        // required fields validation

        if (!form.title || !form.date) {
            this.holidays.message = 'Fill required fields to continue.';
            return;
        }

        this.holidays.isLoading = true;

        HolidaysService.Create(form).then((r) => {

            /** @type {IClientResponseModel<IHoliday>} */
            const response = r.data

            if (debugHolidays) {
                console.log('add response', r)
            }

            /** @type {Array<IHoliday>} */
            let data = [...this.holidays.data]

            if (response.isSuccess) {
                data.unshift(response.record)
            }

            this.holidays.data = data
            this.holidays.form = holidayFormObject();

            this.holidays.message = 'Holiday Added!';

        }).catch((e) => {

            console.error('create catched', e)

            this.holidays.message = BASIC_ERROR_MESSAGE;

        }).then(() => {

            this.holidays.isLoading = false;
        });
    },
    deleteHoliday: function (idx) {

        if (!confirm('Confirm Deletion')) {
            return;
        }

        /** @type {Array<IHoliday>} */
        let data = [...this.holidays.data];

        let holiday = data[idx];

        HolidaysService.Delete(holiday.id).then((r) => {

            /** @type {IClientResponseModel<IHoliday>} */
            const response = r.data

            if (debugHolidays) {
                console.log('delete response', r)
            }

            if (response.isSuccess) {
                data.splice(idx, 1);

                this.holidays.data = data
                this.holidays.message = 'Deleted!';
            }
            else {
                this.holidays.message = response.message;
            }

        }).catch((e) => {

            console.error('delete catched', e)

            this.holidays.message = BASIC_ERROR_MESSAGE;

        }).then(() => {
            this.holidays.isLoading = false
        });
    },
    updateHoliday: function (idx) {

        if (!confirm('Confirm Save')) {
            return;
        }

        /** @type {Array<IHoliday>} */
        let data = [...this.holidays.data];

        let holiday = data[idx];

        // update validation
        if (!holiday.title || !holiday.date) {
            this.holidays.message = 'Fill required fields to continue'
            return;
        }
        // check if updated is changed

        /** @type {IHoliday} */
        const backupEdit = this.holidays.backupEdit

        const titleUpdated = holiday.title !== backupEdit.title
        const noteUpdated = holiday.note !== backupEdit.note
        const dateUpdated = holiday.date !== backupEdit.date

      
        // no change made
        if (!titleUpdated && !dateUpdated && !noteUpdated) {
            data[idx].isEdit = false;
            this.holidays.data = data;
            return;
        }

        console.log('updating')        


        HolidaysService.Update(holiday)
            .then((r) => {

                /** @type {IClientResponseModel<IHoliday>} */
                const response = r.data

                if (debugHolidays) {
                    console.log('update response', r)
                }

                if (response.isSuccess) {

                    data[idx] = { ...response.record }
                    data[idx].isEdit = false;

                    this.holidays.message = 'Holiday Saved!'

                    this.holidays.data = data;
                }
                else {

                    this.holidays.message = BASIC_ERROR_MESSAGE
                }

            }).catch((e) => {

                console.error('update catched', e)

                this.holidays.message = BASIC_ERROR_MESSAGE
            }).then(() => {

            });
    },
    editHoliday: function (idx) {

        let data = [...this.holidays.data];

        for (var i = 0; i < data.length; i++) {
            data[i].isEdit = i === idx;

            if (i === idx) {

                const { title, note, date } = data[i]

                let backupEdit = { ...this.holidays.backupEdit }

                backupEdit.title = title
                backupEdit.note = note
                backupEdit.date = date

                this.holidays.backupEdit = backupEdit
            }
        }

        this.holidays.data = data;
    },
    cancelHolidayEdit: function (idx) {
        let data = [...this.holidays.data];

        for (var i = 0; i < data.length; i++) {
            data[i].isEdit = false;

            if (i === idx) {

                //data[i].address = this.holidays.backupEdit.address;
                //data[i].title = this.holidays.backupEdit.title;

                const { title, note, date } = this.holidays.backupEdit

                let holiday = { ...data[i] }

                holiday.title = title
                holiday.note = note
                holiday.date = date

                data[i] = holiday
            }
        }

        this.holidays.data = data;
    }
}

// APPLICATION

new Vue({
    el: "#Holidays",
    data: {
        dateOptions,
        dateTimeOptions,
        holidays: holidayObject(),
    },
    methods: {
        ...holidaysMethods
    },
    mounted: function () {

        this.holidays.isLoading = true

        HolidaysService.GetAll()
            .then((r) => {

                /** @type {IClientResponseModel<IHoliday>} */
                const response = r.data

                if (debugHolidays) {
                    console.log('get holiday response', r)
                }

                if (response.isSuccess) {
                    this.holidays.data = response.record
                }
                else {
                    this.holidays.message = response.message
                }
            })
            .catch((e) => {
                console.error('getall catched', e)
            }).then(() => {
                this.holidays.isLoading = false
            })
    }
})