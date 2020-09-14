
const init_permission_form = () => {
    return {
        id: '',
        title: '',
        description: ''
    }
}

new Vue({
    el: '#permissionManager',
    data: {
        permissions: [],
        permission: init_permission_form(),
        isLoading: true
        , errors: []
    }
    ,
    mounted: function () {
        getPermissions(this);
    },
    methods: {
        addpermission: function () {

            let model = {
                Title: this.permission.title,
                description: this.permission.description
            }

            this.errors = [];


            axios.post("permissions/Add", model)
                .then(response => {

                    const { message, success, record } = response.data;

                    if (!success || !record) {
                        console.log('failed' )


                        this.errors = [message]
                        return;
                    }

                    console.log({ record })
                    let newProj = { ...record };
                    console.log({ newProj })

                    newProj.editMode = {
                        ...newProj,
                        active: false
                    }


                    let permissions = [...this.permissions];

                    permissions.unshift(newProj)

                    this.permissions = permissions;
                    this.permission = init_permission_form();
                })
                .catch(
                    function (response) {
                        console.error('permission add', response);
                        alert(BASIC_ERROR_MESSAGE);
                    });
        }
        ,
        deletepermission: function (id, index) {

            if (!confirm('Delete this permission?')) {
                return;
            }

            deletePermission(this, id, index);
        }
        ,
        toggleEditMode: function (permission, index) {
            let vitrualPermission = { ...permission };
            vitrualPermission.editMode.active = !vitrualPermission.editMode.active;
            this.$set(this.permissions, index, vitrualPermission);
        },
        editPermission: function (permission, index) {
            EditPermission(this, permission, index);
        }
    }
});

function getPermissions(application) {

    application.isLoading = true

    axios.get("permissions/get").then(
        function (response) {
            application.permissions = response.data;
            application.permissions = addEditModeToPermissions(application.permissions);
            //console.log(application.permissions);
        }
    ).catch(function (response) { console.log(response) }
    ).then(function (e) {
        application.isLoading = false;
    });
}
function addEditModeToPermissions(Permissions) {
    Permissions.forEach(function (project) {
        project.editMode = {
            id: project.id,
            title: project.title,
            description: project.description
            , active: false
        }
    }
    );
    return Permissions;
}

function EditPermission(application, Permission, Index) {
    axios.put("permissions/Update", Permission.editMode).then(
        function (response) {
            //console.log(response);

            const { error, success} = response.data

            if (!success) {
                alert('Record was not updated: ' + error);
                return;
            }

            let vrPermission = { ...Permission };
            vrPermission.title = vrPermission.editMode.title;
            vrPermission.description = vrPermission.editMode.description;
            vrPermission.editMode.active = false;

            application.$set(application.permissions, Index, vrPermission);
        }
    ).catch(function (response) {
        console.log(response);
    })
}
function deletePermission(application, id, index) {
    axios.delete(`permissions/delete?id=${id}`).then(
        function (response) {
            let vrPersmissions = [...application.permissions];
            vrPersmissions.splice(index, 1);
            //console.log(index);

            //console.log(vrPersmissions);
            application.permissions = vrPersmissions;
        }
    )
        .catch(
            function (response) {
                console.log(response);
            }
        )
}