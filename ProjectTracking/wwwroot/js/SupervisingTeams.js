
function renderCharts(teams) {
    //var ctx = document.getElementById('myChart').getContext('2d');

    var options = {
        legend: {
            display: false
        },
        layout: {
            padding: {
                top: 5,
                left: 5,
                right: 5
            }
        },
        scales: {
            yAxes: [{
                display: false,
                ticks: {
                    beginAtZero: true
                },
                gridLines: {
                    display: false
                },
            }],
            xAxes: [{
                //display: false,

                gridLines: {
                    display: false
                }
            }],
        },
    }

    for (var i = 0; i < teams.length; i++) {

        const team = teams[i]

        const ctx = $(`[data-team-activities=${team.id}] canvas`)[0].getContext('2d')

        const labels = team.activitiesFrequency.map(k => moment(k.key).format('D/MMM'))
        const data = team.activitiesFrequency.map((k, idx) => ({ x: idx, y: k.value }))

        console.log({ labels })

        new Chart(ctx, {
            type: 'line',
            data: {
                labels: labels,
                datasets: [{
                    label: '',
                    fill: false,
                    borderColor: "#2286c3",
                    data,
                    borderWidth: 1
                }]
            },
            options
        });

    }

    ////const WIDE_CLIP = { top: 2, bottom: 4 };

    ////Chart.canvasHelpers.clipArea = function (ctx, clipArea) {
    ////    ctx.save();
    ////    ctx.beginPath();
    ////    ctx.rect(
    ////        clipArea.left,
    ////        clipArea.top - WIDE_CLIP.top,
    ////        clipArea.right - clipArea.left,
    ////        clipArea.bottom - clipArea.top + WIDE_CLIP.bottom
    ////    );
    ////    ctx.clip();
    ////};

    //var myChart = new Chart(ctx, {
    //    type: 'bar',
    //    data: {
    //        labels: ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'],
    //        datasets: [{
    //            label: '# of Votes',
    //            data: [12, 19, 3, 5, 2, 3],
    //            backgroundColor: [
    //                'rgba(255, 99, 132, 0.2)',
    //                'rgba(54, 162, 235, 0.2)',
    //                'rgba(255, 206, 86, 0.2)',
    //                'rgba(75, 192, 192, 0.2)',
    //                'rgba(153, 102, 255, 0.2)',
    //                'rgba(255, 159, 64, 0.2)'
    //            ],
    //            borderColor: [
    //                'rgba(255, 99, 132, 1)',
    //                'rgba(54, 162, 235, 1)',
    //                'rgba(255, 206, 86, 1)',
    //                'rgba(75, 192, 192, 1)',
    //                'rgba(153, 102, 255, 1)',
    //                'rgba(255, 159, 64, 1)'
    //            ],
    //            borderWidth: 1
    //        }]
    //    },
    //      options: {
    //          scales: {
    //              yAxes: [{
    //                  ticks: {
    //                      beginAtZero: true
    //                  }
    //              }]
    //          }
    //      }
    //});

}

new Vue({
    el: '#SupervisingTeams',
    data: {
        userId: null,
        teams: {
            data: [],
            message: '',
            isLoading: true
        },
        tasksPerformanceProgress: initTasksPerformanceProgress()
    },
    computed: {

    },
    methods: {
        getMembersText: function (count) {

            if (!count) {
                return 'no members yet'
            }

            const label = `member${count > 0 ? 's' : ''}`

            return `${count} ${label}`
        },
        getProjectsText: function (count) {

            if (!count) {
                return 'no projects yet'
            }

            const label = `project${count > 0 ? 's' : ''}`

            return `${count} ${label}`
        },
        getPercentage: function (tasksPerformance, fromProp) {

            const total = tasksPerformance['totalCount']
            const value = tasksPerformance[fromProp]

            const percentage = `${value / total * 100}%`

            return percentage
        },
        getTeamPerformance: function (tasksPerformance) {

            let tasksPerformanceProgress = [...this.tasksPerformanceProgress]

            tasksPerformanceProgress =
                tasksPerformanceProgress.filter(k => tasksPerformance[k.fromProp] > 0)
                    .map(k => ({ ...k, name: tasksPerformance[k.fromProp] + ' ' + k.name }))

            return tasksPerformanceProgress
        }
    },
    mounted: function () {

        const userId = $('#SupervisingTeams').attr('data-user')
        this.userId = userId;


        TeamsService.GetSupervisingTeamsModel(userId)
            .then(r => {
                this.teams.data = r.data

                return r.data
            })
            .catch(e => {
                this.teams.message = getAxiosErrorMessage(e)
            })
            .then((r) => {

                console.log({ r })
                this.teams.isLoading = false

                if (Array.isArray(r)) {
                    renderCharts(r)
                }
            });
    }
});

