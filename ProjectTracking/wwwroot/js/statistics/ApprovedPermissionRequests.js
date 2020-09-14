Vue.component('paginate', VuejsPaginate)
new Vue({
    el: '#app',
    data: {
        permissionRequestTotals:'',
        errors: [], searchYears:'',
        searchMonths: '',
        requestedPermissions: [],
        requestedPermissionsLoading: true,
        yearOfTotals: '', monthOfTotals: '',
        dataPaging: {
            page: 0,
            totalPages: 0,
            length: 5,
            totalCount: 0,
            pagination: 'custom-pagination',
            prev: 'Prev',
            next: 'Next',
        },

    },
    computed: {
    },
    watch: {
        dataPaging: {
            handler: function (newVal, oldVal) {

                this.dataPaging.totalPages = Math.ceil(newVal.totalCount / newVal.length);
            },
            deep: true
        }

    },
    methods: {
        clearTotalProgressesFilters: function () {
            this.monthOfTotals = ''; this.dayOfTotals = '';
        },
        getPermissionRequests: function ( page, countPerPage, year, month) {
            this.requestedPermissionsLoading = [];
            PermissionRequests.GetApprovedRequestedPermission(this.userId, page, countPerPage, year, month).then(response => {
                console.log(response);

                const { data } = response

                this.requestedPermissions = data.records;
                this.dataPaging.totalCount = data.totalCount

                //if(resp)
                this.requestedPermissionsLoading = false;
            }).catch(error => { console.log(error) });
        },
        GetPermissionRequestsYears:function(userId) {
            PermissionRequests.GetPermissionRequestsYearsOrMonthsByUser(userId, null).then(response => {
                this.searchYears = response.data;
                this.searchMonths = [];
                this.monthOfTotals = '';
                console.log(' getting progress  years', response)
            }).catch(error => { console.log('getYearsError', error) })
                ;
        }
        ,
        GetPermissionRequestsMonths: function (year) {
            PermissionRequests.GetPermissionRequestsYearsOrMonthsByUser(this.userId, year).then(response => {
                this.monthOfTotals = '';

                this.searchMonths = response.data;
                console.log('get months reponse', response)
            }).catch(error => { console.log('getYearsError', error) })
                ;
        },
        GetPermissionRequestsTotals: function (year, month) {
            PermissionRequests.GetPermissionRequestsTotals(this.userId, year, month)
                .then(response => {
                this.permissionRequestTotals = response.data;
                console.log('get totals reponse', response)
            }).catch(error => { console.log('getYearsError', error) })
        }
        ,
        searchPermissionRequests: function () {

            this.getPermissionRequests(0, this.dataPaging.length, this.yearOfTotals, this.monthOfTotals);
            this.GetPermissionRequestsTotals(this.yearOfTotals, this.monthOfTotals);
        },
        clickCallback: function (pageNum) {
            this.getPermissionRequests(pageNum - 1, this.dataPaging.length,
                this.yearOfTotals, this.monthOfTotals);
        },

    },
    mounted: function () {
        var currentDate = new Date();
        var currentYear = currentDate.getFullYear();
        this.yearOfTotals = currentYear;
        this.userId = $("#UserIdField").val();
        this.getPermissionRequests(0, this.dataPaging.length,this.yearOfTotals, '');
        this.GetPermissionRequestsYears(this.userId);
        this.GetPermissionRequestsMonths(this.yearOfTotals);
        this.GetPermissionRequestsTotals(this.yearOfTotals, '');
    }
    ,
});