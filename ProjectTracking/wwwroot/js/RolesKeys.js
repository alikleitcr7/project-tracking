var rolesKeys_app = new Vue({

    el: "#RolesKeys",
    data: {
        keys: [],
        isLoading: true,
        isGenerating: false,
        message: null
    },
    methods: {
        regenerate: function (role) {

            bootboxExtension.confirm('Are you sure?', `You are about to regenarate the key for "${role}" Role`, undefined, () => {


                this.isGenerating = true

                UsersService.RegenerateKey(role)
                    .then((r) => {
                        const record = r.data

                        this.keys = this.keys.map(k => k.key === role ? ({ ...k, value: record }) : k)
                    })
                    .catch((e) => {
                        const errorMessage = getAxiosErrorMessage(e)

                        bootbox.alert(errorMessage)
                    })
                    .then(() => {
                        this.isGenerating = false

                    })
            })

        }
    },
    mounted: function () {
        UsersService.GetRoleKeys()
            .then((r) => {
                const record = r.data

                this.keys = record
            })
            .catch((e) => {
                const errorMessage = getAxiosErrorMessage(e)

                bootbox.alert(errorMessage)
            })
            .then(() => {
                this.isLoading = false
            })
    }
});


