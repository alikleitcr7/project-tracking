
Vue.component('paginate', VuejsPaginate)
Vue.component('date-picker', VueBootstrapDatetimePicker);

new Vue({
    el: '#Requests',
    data: {
        dateTimeOptions: {
            ...dateTimeOptions,
            format: 'YYYY-MM-DD HH:mm'
        },
        dateOptions,
        EmployeeHasSubordinates: false,
        permissions: [],
        permissionsAreLoading: true,
        selectedPermission: '',
        permissionRequestModal: {
            Notes: "",
            FromDate: null,
            ToDate: null,
        },
        errors: [],
        requestedPermissions: [],
        requestedPermissionsLoading: true,
        areExpandAll: false,
        supervisingRequestedPermissions: [],
        supervisingRequestedPermissionsLoading: true,
        permitModal: {
            supervisingPermissionRequestStatusId: '',
            comment: '',
            status: 0,
            index: 0,

        },
        dataPaging: {
            page: 0,
            totalPages: 0,
            length: 7,
            totalCount: 0,
            pagination: 'custom-pagination',
            prev: 'Prev',
            next: 'Next',
        },
        holidays: [],
        supervisingDataPaging: {
            page: 0,
            totalPages: 0,
            length: 7,
            totalCount: 0,
            pagination: 'custom-pagination',
            prev: 'Prev',
            next: 'Next',
        }
    },
    computed: {
        fromToTimeSpan: function () {
            const { FromDate, ToDate } = this.permissionRequestModal
            console.log({ FromDate, ToDate })
            if (FromDate && ToDate) {

                var duration = moment.duration(moment(ToDate).diff(moment(FromDate)));

                var businessDays = calculateBusinessDays(moment(FromDate), moment(ToDate), this.holidays)
                const days = businessDays.workingDays;

                //console.log({ duration })

                var hours = Math.round(duration.asHours() * 100) / 100;

                if (hours <= 24) {

                    return hours + ' Hours'
                }

                const days = Math.round(duration.asDays() * 100) / 100;

                return days + ' Days'
            }

            return '-'
        }
    },
    watch: {
        dataPaging: {
            handler: function (newVal, oldVal) {

                this.dataPaging.totalPages = Math.ceil(newVal.totalCount / newVal.length);
            },
            deep: true
        }
        , supervisingDataPaging: {
            handler: function (newVal, oldVal) {

                this.supervisingDataPaging.totalPages = Math.ceil(newVal.totalCount / newVal.length);
            },
            deep: true
        }

    },
    methods: {
        setPermitModal: function (supervisingPermissionRequestStatusId, index) {
            this.permitModal.comment = '';
            this.permitModal.supervisingPermissionRequestStatusId = supervisingPermissionRequestStatusId;
            this.permitModal.index = index;

            console.log(this.permitModal);
        }
        ,
        expandPermission: function (permissionRequest, Index) {

            let togglePermissionRequest = { ...this.requestedPermissions.find(p => p.id == permissionRequest.id) };
            togglePermissionRequest.isExpanded = !togglePermissionRequest.isExpanded;
            this.$set(this.requestedPermissions, Index, togglePermissionRequest);
            console.log('permissionRequest.isExpanded', togglePermissionRequest.isExpanded);
        },
        expanddAll: function () {
            this.areExpandAll = !this.areExpandAll;
            var appExpanAll = this.areExpandAll;
            this.requestedPermissions.forEach(function (permissionRequest) {
                permissionRequest.isExpanded = appExpanAll;
            });
        },
        getPermissionRequests: function (page, countPerPage) {
            this.requestedPermissions = [];
            PermissionRequests.GetPermissionRequests(page, countPerPage).then(response => {
                console.log(response);

                const { data } = response

                this.requestedPermissions = data.records;
                this.dataPaging.totalCount = data.totalCount

                //if(resp)
                this.requestedPermissionsLoading = false;
            }).catch(error => { console.log(error) });
        },
        getSupervisingPermissionRequests: function (page, countPerPage) {
            PermissionRequests.getSupervisingPermissionRequests(page, countPerPage).then(response => {
                console.log(response);
                const { data } = response
                this.supervisingRequestedPermissions = data.records;
                this.supervisingDataPaging.totalCount = data.totalCount


                //if(resp)
                this.supervisingRequestedPermissionsLoading = false;
            }).catch(error => { console.log(error) });
        },
        CheckIfEmployeeHasSubordinates: function () {
            PermissionRequests.CheckIfEmployeeHasSubordinates().then(response => {
                console.log('CheckIfEmployeeHasSubordinates', response);
                const { data } = response;
                if (data.success)
                    this.EmployeeHasSubordinates = data.userHasSubordinates;


            }).catch(error => {
                console.log('CheckIfEmployeeHasSubordinates error', error);
            })
        }
        ,
        PermitRequestPermission: function () {
            PermissionRequests.PermitOfPermissionRequest(this.permitModal)
                .then(response => {
                    console.log(response)
                    if (response.data) {
                        console.log(response.data);
                        let togglePermissionRequest = { ...this.supervisingRequestedPermissions.find(p => p.id == this.permitModal.supervisingPermissionRequestStatusId) };
                        togglePermissionRequest.isApproved = this.permitModal.status;
                        this.$set(this.supervisingRequestedPermissions, this.permitModal.index, togglePermissionRequest);
                    }
                })
                .catch(error => {
                    console.log({ error })
                });
        }
        ,
        getPermissions: function () {
            axios.get("../permissions/get").then(
                response => {
                    let vrPermissions = [...this.permissions];
                    vrPermissions = response.data;
                    this.permissions = vrPermissions;
                    console.log(this.permissions);
                }
            ).catch(function (response) { console.log(response) }
            ).then(function (e) {
                this.permissionsAreLoading = false;
            });
        },
        submitRequestValidation: function () {

            const { FromDate, ToDate } = this.permissionRequestModal;

            let RequestDates = {
                from: FromDate,
                to: ToDate,
            }

            this.errors = []
            let isValid = true;

            if (!this.selectedPermission ||
                !RequestDates.from || !RequestDates.to
            ) {
                this.errors.push('please Insert Correct Information');
                isValid = false;

            }

            if (Date.parse(RequestDates.from) >= Date.parse(RequestDates.to)) {
                this.errors.push('Permission From date can not be greater or equal than Permission To date');
                isValid = false;

            }
            if (Date.parse(RequestDates.to) - Date.parse(RequestDates.from) <= 900000) {
                this.errors.push('Permission Request can not be less than 15 minutes ');
                isValid = false;

            }

            return isValid;
        }
        , clearRequestData: function () {
            //$('#R2equestPermissionFromDate').val('');
            //$('#R2equestPermissionToDate').val('');
            this.permissionRequestModal = {};
            this.selectedPermission = '';
        },

        addPermission: function () {

            if (!this.submitRequestValidation())
                return;

            //let RequestDates = {
            //    from: $('#RequestPermissionFromDate').val(),
            //    to: $('#RequestPermissionToDate').val(),
            //}
            //this.permissionRequestModal.FromDate = RequestDates.from;
            //this.permissionRequestModal.ToDate = RequestDates.to;

            this.permissionRequestModal.PermissionId = this.selectedPermission;

            PermissionRequests.AddPermissionRequest(this.permissionRequestModal).then(
                response => {
                    console.log(response);
                    this.clearRequestData();
                    var permissionsData = response.data;
                    if (permissionsData.success == false) {
                        console.log('Adding permition Request error ', permissionsData.error);
                        alert('something went wrong please try again');
                        return;
                    }
                    else {

                        const { added } = response.data

                        if (added === -1) {
                            alert('A previous request was made between the sent dates')
                        }
                        else {

                            alert('Your permission Request has been successfully submited !');
                            this.getPermissionRequests(0, this.dataPaging.length);
                        }


                    }

                })
                .catch(
                    function (response) {
                        alert('something went wrong');
                        console.log(response);
                    });
        },


        clickCallback: function (pageNum) {
            this.getPermissionRequests(pageNum - 1, this.dataPaging.length)
        },
        clickCallbackSupervising: function (pageNum) {
            this.getSupervisingPermissionRequests(pageNum - 1, this.supervisingDataPaging.length)
        },
        getFinalStatus: function (permission) {
            //console.log({ permission })
            let approvedPermissionsLength = permission.requestedPermissionsStatuses.filter(k => k.isApproved === 1).length


            if (approvedPermissionsLength === permission.requestedPermissionsStatuses.length) {

                return 'All Approved'
            }

            if (approvedPermissionsLength > 0) {

                return `Pending, Approved By ${approvedPermissionsLength} of ${permission.requestedPermissionsStatuses.length}`
            }

            return 'Pending'

        }
    },
    mounted: function () {
        this.getPermissions();
        this.getPermissionRequests(0, this.dataPaging.length);
        this.getSupervisingPermissionRequests(0, this.supervisingDataPaging.length);
        this.CheckIfEmployeeHasSubordinates();
    }
});