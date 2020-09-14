
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

        let { address, title } = { ...this.ipAddresses.form };


        if (!title) {
            alert('Title is Required')
            return;
        }


        this.ipAddresses.isLoading = true;

        IpAddressesService.Create(address, title).done((r) => {

            this.ipAddresses.form = ipAddressFormObject();

            const unlistedIpIdx = this.unlistedIps.findIndex(k => k === address)

            if (unlistedIpIdx > -1) {

                let unlitedIps = [...this.unlistedIps]

                unlitedIps.splice(unlistedIpIdx, 1)

                this.unlistedIps = unlitedIps
            }

            this.ipAddresses.data = [r, ...this.ipAddresses.data]
            this.ipAddresses.message = 'Added!';

        }).fail((e) => {

            this.ipAddresses.message = BASIC_ERROR_MESSAGE;

        }).always(() => {

            this.ipAddresses.form.address = ''
            this.ipAddresses.form.title = ''

            this.ipAddresses.isLoading = false;
        });
    },
    deleteIpAddress: function (idx) {

        if (!confirm('Confirm Delete')) {
            return;
        }

        let data = [...this.ipAddresses.data];

        let ipAddres = data[idx];

        IpAddressesService.Delete(ipAddres.id).done((r) => {

            if (r) {
                console.log({ data, idx })
                data.splice(idx, 1);

                this.getUnlistedIps();

                this.ipAddresses.data = data
                this.ipAddresses.message = 'Deleted!';

            }
            else {
                this.ipAddresses.message = BASIC_ERROR_MESSAGE;
            }

        }).fail((e) => {

            console.error({ e })
            this.ipAddresses.message = BASIC_ERROR_MESSAGE;

        }).always(() => {
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

        IpAddressesService.Update(id, address, title)
            .done((r) => {

                if (r) {

                    data[idx].isEdit = false;
                    this.ipAddresses.message = 'Saved!'

                    this.ipAddresses.data = data;
                }
                else {

                    this.ipAddresses.message = BASIC_ERROR_MESSAGE
                }

            }).fail((e) => {

                this.ipAddresses.message = BASIC_ERROR_MESSAGE

                console.error('save ipAddres', e)
            }).always(() => {

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
        unlistedIps: []
    },
    methods: {
        ...ipAddressesMethods,
        getUnlistedIps: function () {
            IpAddressesService.GetUnlistedIps()
                .done((r) => {
                    this.unlistedIps = r
                })
                .fail((e) => {
                    console.error({ e })
                }).always(() => {
                    this.ipAddresses.isLoading = false
                })
        }
    },
    mounted: function () {

        this.ipAddresses.isLoading = true

        IpAddressesService.GetAll()
            .done((r) => {
                console.log({ r })
                this.ipAddresses.data = r
            })
            .fail((e) => {
                console.error({ e })
            }).always(() => {
                this.ipAddresses.isLoading = false
            })

        this.getUnlistedIps()

    }
})