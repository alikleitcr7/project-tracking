
$(document).ready(function () {

    $("#TimeSheetTable").clone(true).appendTo('#table-scroll').addClass('clone');

})
new Vue({
    el: '#TimeSheet',
    data: {
        timeSheet: null,
        timeSheetLoading: true
    },
    methods: {
        getTotalHoursForProject: function (monthActivities) {
            console.log({ monthActivities })

            return Math.round( (monthActivities.map(k => parseFloat(k.numberOfHours)).reduce((a, b) => a + b, 0))*100) / 100
        },
        getTotalTimeSheetHours: function () {
            return Math.round((this.totalHoursPerDay.reduce((a, b) => a + b, 0) ) * 100) / 100
        }
    },
    computed: {
        totalHoursPerDay: function () {
            if (!this.timeSheet) {
                return []
            }

            let totalHours = [];

            for (var i = 0; i <= this.timeSheet.numberOfDays; i++) {

                let total = 0.0;

                for (var p = 0; p < this.timeSheet.projects.length; p++) {

                    const project = this.timeSheet.projects[p]

                    for (var j = 0; j < project.activities.length; j++) {

                        const activity = project.activities[j]

                        total += parseFloat(activity.monthActivities[i].numberOfHours)
                    }
                }

                totalHours.push(Math.round(total*100)/100)
            }

            return totalHours;
        }
    },
    watch: {
    },
    mounted: function () {

        let timeSheetId = $('input[name=id]').val();

        console.log('timeSheetId', timeSheetId);

        axios.get('/TimeSheets/GetTimeSheetModel?timeSheetId=' + timeSheetId).then(
            response => {

                console.log('response timesheets', response.data);

                this.timeSheet = response.data;

            }).catch(e => {
                console.error('err', e);

            }).then(() => {
                this.timeSheetLoading = false;
            })
    }
});


