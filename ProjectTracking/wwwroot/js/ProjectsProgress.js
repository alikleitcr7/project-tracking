
new Vue({
    el: '#app',
    data: {
        projectsProgresses: [], measurementTotals: [],
        filterProgress: 0, userId: null
        , projectProgressYears: [], projectProgressMonths: [], projectProgressDays:[]
        , yearOfTotals: '', monthOfTotals: '', dayOfTotals: ''
    },
    computed: {
      
    },
    methods: {
        clearTotalProgressesFilters: function () {
           this.monthOfTotals = ''; this.dayOfTotals = '';
        },
        GetTotalProjectsProgress: function () {
            filterTotalProgressData= {
                Year: this.yearOfTotals, Month: this.monthOfTotals, day: this.dayOfTotals
            }
            this.GetMeasurementUnitsTotalProgress(this.userId, filterTotalProgressData.Year, filterTotalProgressData.Month, filterTotalProgressData.day);
            console.log({ filterTotalProgressData });
        }
        ,
        getFiltered: function (filterBy) {
            switch (filterBy) {
                case 0:
                    this.GetProjectsProgress(false, false, this.userId );
                    break;
                case 1:
                    this.GetProjectsProgress(true, false, this.userId );
                    break;
                case 2:
                    this.GetProjectsProgress(true, true, this.userId );
                    break;
            }
        },
        GetProjectsProgress: function (filterByYear, filterByYearAndMonth,userId) {
            this.projectsProgresses = [];
            ProjectsStatisticsService.GetProjectsProgress(filterByYear, filterByYearAndMonth, userId).then(response => {
                this.projectsProgresses = response.data;
                console.log(response)
            }).catch(error => {
                console.log(error);
            })
        },
        GetMeasurementUnitsTotalProgress: function (userId,year, month, day) {
            this.measurementTotals = [];
            ProjectsStatisticsService.GetMeasurementUnitsTotalProgress(userId, year, month, day).then(response => {
                this.measurementTotals = response.data;
                console.log(response)
            }).catch(error => {
                console.log(error);
            })
        },
        GetProjectProgressYears: function (userId) {
            ProjectsStatisticsService.GetProjectProgressYears(userId).then(response => {
                this.projectProgressYears = response.data;
                this.projectProgressMonths = [];

                console.log(' getting progress  years',response)
            }).catch(error => {
                console.log('error getting progress  months', error);

            })
        },
        GetProjectProgressMonthsByYear: function (year) {
            this.clearTotalProgressesFilters();

            ProjectsStatisticsService.GetProjectProgressMonthsByYear(this.userId, year).then(response => {
                this.projectProgressMonths = response.data;
                this.projectProgressDays = [];
                console.log('get months reponse',response)
            }).catch(error => {
                console.log('error getting progress  months',error);
            })
        },
        getProjectProgressDatesByMonthAndYear: function (year, month) {
            this.dayOfTotals = '';
            ProjectsStatisticsService.GetProjectProgressDaysByMonthAndYear(this.userId,year,month).then(response => {
                this.projectProgressDays = response.data;
                console.log(response)
            }).catch(error => {
                console.log('error getting total days',error);
            })
        },
    },
    mounted: function () {
        console.log('user id field', $("#UserIdField").val());
        this.userId = $("#UserIdField").val();
        console.log('user id vue', this.userId);
        this.GetProjectsProgress(false, false, this.userId );
        this.GetMeasurementUnitsTotalProgress(this.userId,'','','');
        this.GetProjectProgressYears(this.userId);

    }

});
