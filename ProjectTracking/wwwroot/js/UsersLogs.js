﻿
Vue.component('paginate', VuejsPaginate) 
Vue.component('date-picker', VueBootstrapDatetimePicker);
new Vue({
    el: '#app',
    data: {
        UsersLogsAreLoading: true,
        fromDate: '', dateOptions,toDate:'',
        dateTimeOptions,
        

        //array that contains targeted list :
        usersLogs: [],

        dataPaging: {
            page: 0,
            totalPages: 0,
            length: 10,
            totalCount: 0,
            pagination: 'custom-pagination',
            prev: 'Prev',
            next: 'Next',
        }

    },
    computed: {
    },

    methods: {
        resetLogsFilter: function () {
            this.fromDate = '', this.toDate = '';
        },
        searchUserLogs: function () {
            if (this.toDate < this.fromDate) {
                alert('to date can not be smaller than from date , please try again')
                return;
            }
            this.GetUsersLogs(0, this.dataPaging.length, this.fromDate, this.toDate);

        },
        clickCallback: function (pageNum) {
            this.GetUsersLogs(pageNum - 1, this.dataPaging.length,this.fromDate, this.toDate)

        },
        GetUsersLogs: function (page, countPerPage,fromDate,toDate) {
            this.usersLogs = [];
            UsersLogs.GetUsersLogs(page, countPerPage,fromDate,toDate).then(response => {
                const { data } = response
                this.usersLogs = data.records;
                this.dataPaging.totalCount = data.totalCount
                this.UsersLogsAreLoading = false;
            }).catch(error => { console.log(error) });
        },
    },
    mounted: function () {
        this.GetUsersLogs(0, this.dataPaging.length, this.fromDate, this.toDate);
    },
    watch: {
        dataPaging: {
            handler: function (newVal, oldVal) {

                this.dataPaging.totalPages = Math.ceil(newVal.totalCount / newVal.length);
            },
            deep: true
        }
    },

});
