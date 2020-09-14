Vue.component('paginate', VuejsPaginate)

var app = new Vue({
    el: '#app',
    data: {
        permitModal: {
            supervisingPermissionRequestStatusId: '',
            comment: '',
            status: 0,
            index: 0,
        },
       supervisingDataPaging: {
            page: 0,
            totalPages: 0,
            length: 10,
            totalCount: 0,
            pagination: 'custom-pagination',
            prev: 'Prev',
            next: 'Next',
        }
        , TimeSheetsAreLoading: true,
        SubordinatesTimeSheetStatuses: [],
    },
    methods: {
        PermitTimeSheet: function () {
            TimeSheetsService.PermitOfTimeSheet(this.permitModal)
                .then(response => {
                    console.log(response)
                    if (response.data) {
                        console.log(response.data);
                        let togglePermissionRequest = {
                            ...this.SubordinatesTimeSheetStatuses
                                .find(p => p.id == this.permitModal.supervisingPermissionRequestStatusId)
                        };
                        togglePermissionRequest.isApproved = this.permitModal.status;
                        this.$set(this.SubordinatesTimeSheetStatuses, this.permitModal.index, togglePermissionRequest);
                    }
                })
                .catch(error => {
                    console.log({ error })
                });
        },
        clickCallbackSupervising: function (pageNum) {
            this.getSubordinatesTimeSheets(pageNum - 1, this.supervisingDataPaging.length)
        },
        setPermitModal: function (timeSheetStatusId, index) {
            this.permitModal.comment = '';
            this.permitModal.supervisingPermissionRequestStatusId = timeSheetStatusId;
            this.permitModal.index = index;

            console.log(this.permitModal);
        },
        getSubordinatesTimeSheets: function (page, countPerPage) {
            TimeSheetsService.GetSubordinatesTimeSheets(page, countPerPage).then(response => {
                console.log(response);
                const { data } = response
                this.SubordinatesTimeSheetStatuses = data.records;
                this.supervisingDataPaging.totalCount = data.totalPages
                this.TimeSheetsAreLoading= false
            }).catch(error => {
                console.log(error);
            })
        }
    },
    mounted: function () {
        this.getSubordinatesTimeSheets(0, this.supervisingDataPaging.length);
    },
    watch: {
          supervisingDataPaging: {
        handler: function (newVal, oldVal) {

            this.supervisingDataPaging.totalPages = Math.ceil(newVal.totalCount / newVal.length);
        },
        deep: true
    }
    }
})