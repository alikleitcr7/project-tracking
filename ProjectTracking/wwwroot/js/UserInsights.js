//UserInsightsService

const monthsDisplay = ["", "January", "February", "March", "April", "May", "June",
    "July", "August", "September", "October", "November", "December"];

const currentYear = new Date().getFullYear()
const currentMonth = new Date().getMonth() + 1
const mainColor = '#c80000'

new Vue({
    el: '#UserInsights',
    data: {
        monthlyActivitiesLoading: true,
        monthlyActivities: [],
        years: [],
        months: [],
        month: null,
        year: null,
        userId: null
    },
    watch: {
    },
    methods: {
        displayMonth: function (month) {
            return monthsDisplay[month]
        },
        handleYearChange: function (event) {
            const value = event.target.value

            this.month = null
            this.months = []
            this.getUserMonthlyInsights(true, false, value, this.month)
        },
        handleMonthChange: function (event) {
            const value = event.target.value

            if (!value) {

                this.month = null
                //this.months = []

                return;
            }

            this.getUserMonthlyInsights(true, true, this.year, value)
        },
        getUserMonthlyInsights: function (monthly, daily, year, month) {

            UserInsightsService.UserMonthlyActivities(this.userId, monthly, daily, year, month)
                .done(r => {
                    console.log({ r })

                    this.monthlyActivities = r.insights

                    if (!daily) {
                        this.years = r.years
                        this.month = null
                        this.months = r.months
                    }

                    //this.months = r.insights.map(k => k.month).filter(onlyUnique)

                    if (this.months.length > 0) {
                        //this.month = this.months[0]

                    }

                    if (this.years.length === 0) {
                        this.years = [year];
                    }

                    this.year = year;
                    this.month = month;

                    var ctx = document.getElementById(daily ? 'UserDailyInsights' : 'UserMonthlyInsights').getContext('2d');

                    const insights = this.monthlyActivities

                    let data = {
                        labels: insights.map(k => (daily ? k.day : monthsDisplay[k.month])),
                        datasets: [{
                            label: `User ${(daily ? 'Daily' : 'Monthly')} Hours`,
                            borderColor: mainColor,
                            data: insights.map(k => k.totalHours)
                        }]
                    }

                    var myLineChart = new Chart(ctx, {
                        type: 'line',
                        data,
                        options: {}
                    });
                })
                .catch(e => console.error('user monthly ac.', e))
                .always(() => {
                    this.monthlyActivitiesLoading = false
                });
        },
    },
    mounted: function () {

        var url_string = window.location.href
        var url = new URL(url_string);

        var userId = url.searchParams.get("userId");

        this.userId = userId
        //this.userId = 
        this.getUserMonthlyInsights(true, false, currentYear, '')

    }
})