const ipAddressFormObject = () => {
    return {
        address: '',
        title: ''
    }
}

const ipAddressDataContract = (ipAddress) => {
    return {
        Address: ipAddress.address,
        Title: ipAddress.title
    }
}

const ipAddressObject = () => {
    return {
        data: [],
        form: ipAddressFormObject(),
        backupEdit: ipAddressFormObject(),
        isLoading: false,
        message: ''
    }
}

var ipAddressesMethods = {
    openIpAddressesModal: function () {
        Modals.IpAddresses.Show()
    },
    addIpAddress: function () {

        let pendingRecord = { ...this.ipAddresses.form };

        console.log({ pendingRecord })

        if (!pendingRecord.title || !pendingRecord.address) {
            bootbox.alert('Title and IP Address are required')
            return;
        }

        this.ipAddresses.isLoading = true;

        IpAddressesService.Save(pendingRecord)
            .then((r) => {


                const record = r.data
                const address = pendingRecord.address.trim()
                // reset form
                this.ipAddresses.form = ipAddressFormObject();

                // remove from unlisted ips
                const unlistedIpIdx = this.unlistedIps.findIndex(k => k === address)

                if (unlistedIpIdx > -1) {

                    let unlistedIps = [...this.unlistedIps]

                    unlistedIps.splice(unlistedIpIdx, 1)

                    this.unlistedIps = unlistedIps
                }

                // append to listed ips

                let data = [...this.ipAddresses.data]

                const idx = data.findIndex(k => k.address === address)

                // update existing
                if (idx > -1) {
                    data[idx] = record
                }
                // append
                else {
                    data = [record, ...data]
                }

                this.ipAddresses.data = data

                this.ipAddresses.message = 'Saved!';

            })
            .catch((e) => {

                const errorMessage = getAxiosErrorMessage(e)

                this.ipAddresses.message = errorMessage;

                //bootbox.alert(errorMessage)

                //this.ipAddresses.message = e.responseJSON && e.responseJSON.message ? e.responseJSON.message : BASIC_ERROR_MESSAGE;

            })
            .then(() => {

                //this.ipAddresses.form.address = ''
                //this.ipAddresses.form.title = ''

                this.ipAddresses.isLoading = false;
            });
    },
    deleteIpAddress: function (idx) {

        if (!confirm('Confirm Delete')) {
            return;
        }

        //const idx = this.ipAddresses.data.findIndex;

        let data = [...this.ipAddresses.data];

        let ipAddres = data[idx];

        IpAddressesService.Delete(ipAddres.address)
            .then((r) => {

                const record = r.data

                if (record) {

                    data.splice(idx, 1);

                    this.getUnlistedIps();

                    this.ipAddresses.data = data
                    this.ipAddresses.message = 'Deleted!';
                }
                else {
                    this.ipAddresses.message = BASIC_ERROR_MESSAGE;
                }

            })
            .catch((e) => {

                const errorMessage = getAxiosErrorMessage(e)
                console.error(errorMessage)

                this.ipAddresses.message = errorMessage;
            })
            .then(() => {
                this.ipAddresses.isLoading = false
            });
    },
    updateIpAddress: function (idx) {

        let data = [...this.ipAddresses.data];
        let ipAddres = data[idx];

        let { id, address, title } = ipAddres;

        if (!title) {
            alert('Title is Required')
            return;
        }

        if (!confirm('Confirm Save')) {
            return;
        }

        if (title === this.ipAddresses.backupEdit.title) {
            data[idx].isEdit = false;
            this.ipAddresses.data = data;

            return;
        }

        IpAddressesService.Save({ address, title })
            .then((r) => {

                const record = r.data

                if (record) {

                    data[idx].isEdit = false;

                    this.ipAddresses.message = 'Saved!'
                    this.ipAddresses.data = data;

                }
                else {

                    this.ipAddresses.message = BASIC_ERROR_MESSAGE
                }

            })
            .catch((e) => {
                const errorMessage = getAxiosErrorMessage(e)
                console.error(errorMessage)

                this.ipAddresses.message = errorMessage

            })
            .then(() => {

            });
    },
    editIpAddress: function (idx) {

        let data = [...this.ipAddresses.data];

        for (var i = 0; i < data.length; i++) {
            data[i].isEdit = i === idx;

            if (i === idx) {
                this.ipAddresses.backupEdit.address = data[i].address;
                this.ipAddresses.backupEdit.title = data[i].title;
            }
        }

        this.ipAddresses.data = data;
    },
    cancelIpAddressEdit: function (idx) {
        let data = [...this.ipAddresses.data];

        for (var i = 0; i < data.length; i++) {
            data[i].isEdit = false;

            if (i === idx) {
                data[i].address = this.ipAddresses.backupEdit.address;
                data[i].title = this.ipAddresses.backupEdit.title;
            }
        }

        this.ipAddresses.data = data;
    },
    defineIp: function (address) {

        this.ipAddresses.form.address = address;
        this.ipAddresses.form.title = '';
    }
}

// APPLICATION

new Vue({
    el: "#IpAddresses",
    data: {
        dateOptions,
        dateTimeOptions,
        ipAddresses: ipAddressObject(),
        unlistedIps: [],
        unlistedIsLoading: true
    },
    methods: {
        ...ipAddressesMethods,
        getUnlistedIps: function () {

            this.unlistedIsLoading = true

            IpAddressesService.GetUnlistedIps()
                .then((r) => {

                    const data = r.data

                    this.unlistedIps = data
                })
                .catch((e) => {
                    const errorMessage = getAxiosErrorMessage(e)
                    console.error(errorMessage)
                })
                .then(() => {
                    this.unlistedIsLoading = false
                })
        }
    },
    mounted: function () {

        this.ipAddresses.isLoading = true

        IpAddressesService.GetListed()
            .then((r) => {

                this.ipAddresses.data = r.data
            })
            .catch((e) => {

                const errorMessage = getAxiosErrorMessage(e)
                console.error(errorMessage)
            })
            .then(() => {
                this.ipAddresses.isLoading = false
            })

        this.getUnlistedIps()

    }
})