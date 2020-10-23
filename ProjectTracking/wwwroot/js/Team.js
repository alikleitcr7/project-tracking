

Chart.defaults.global.responsive = true;

const chartsHelper = {
    barOptions: () => {
        return {
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
                    ticks: {
                        stepSize: 1,
                        beginAtZero: true
                    },
                }],
                xAxes: [{
                    ticks: {
                        callback: function (value) {
                            return value.substr(0, 7);//truncate
                        },
                    }
                }],
            },
            tooltips: {
                enabled: true,
                mode: 'label',
                callbacks: {
                    title: function (tooltipItems, data) {
                        var idx = tooltipItems[0].index;
                        return data.labels[idx];
                    },
                    //label: function (tooltipItems, data) {
                    //    var idx = tooltipItems.index;
                    //    console.log({ data })
                        
                    //    //return data.labels[idx] + ' €';
                    //    return tooltipItems.xLabel + ':' + data.datasets[].data[idx];
                    //}
                }
            },
        }
    },
    colors: {
        background: '#5bcbee',
        border: '#2286c3',
        pending: 'orange',
        done: 'green',
        progress: 'blue',
        failed: 'red',
    }
}

/**
 * 
 * @param {Array<KeyValuePair<UserKeyValuePair,TasksWorkload>>} workload
 */
function populateWorkload(workload) {

    var ctx = document.getElementById('bar_workload').getContext('2d');

    const labels = workload.map(k => k.key.name )

    console.log(labels)

    var chart = new Chart(ctx, {
        // The type of chart we want to create
        type: 'bar',

        // The data for our dataset
        data: {
            labels,
            datasets: [
                {
                    label: 'Pending',
                    backgroundColor: chartsHelper.colors.pending,
                    borderColor: chartsHelper.colors.pending,
                    data: workload.map(k => k.value.pendingCount)
                },
                {
                    label: 'Progress',
                    backgroundColor: chartsHelper.colors.progress,
                    borderColor: chartsHelper.colors.progress,
                    data: workload.map(k => k.value.progressCount)
                },
                {
                    label: 'Done',
                    backgroundColor: chartsHelper.colors.done,
                    borderColor: chartsHelper.colors.done,
                    data: workload.map(k => k.value.doneCount)
                },
            ]
        },
        options: chartsHelper.barOptions()
    });
}

/**
 * 
 * @param {IActiveActivity} activeActivities
 */
function populateActivities(activeActivities) {

    var ctx = document.getElementById('line_activities').getContext('2d');

    var chart = new Chart(ctx, {
        // The type of chart we want to create
        type: 'line',

        // The data for our dataset
        data: {
            labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
            datasets: [{
                label: 'My First dataset',
                backgroundColor: 'rgb(255, 99, 132)',
                borderColor: 'rgb(255, 99, 132)',
                data: [0, 10, 5, 2, 20, 30, 45]
            }]
        },

        // Configuration options go here
        options: {}
    });
}

/**
 * 
 * @param {ITaskPerformance} taskPerformance
 */
function populateTasks(taskPerformance) {

    var ctx = document.getElementById('pie_tasks').getContext('2d');

    var chart = new Chart(ctx, {
        // The type of chart we want to create
        type: 'line',

        // The data for our dataset
        data: {
            labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
            datasets: [{
                label: 'My First dataset',
                backgroundColor: 'rgb(255, 99, 132)',
                borderColor: 'rgb(255, 99, 132)',
                data: [0, 10, 5, 2, 20, 30, 45]
            }]
        },

        // Configuration options go here
        options: {}
    });
}


new Vue({

    el: '#Team',
    data: {
        team: null,
        message: '',
        isLoading: true
    },
    methods: {
        getMemberNameById: function () {

        }
    },
    computed: {

    },
    mounted: function () {

        const teamId = parseInt($('#Team').attr('data-id'))

        this.isLoading = true

        TeamsService.GetTeamViewModel(teamId)
            .then((r) => {
                const record = r.data

                this.team = record

                return record
            })
            .catch((e) => {
                const errorMessage = getAxiosErrorMessage(e)

                this.message = errorMessage

                return null
            })
            .then((r) => {
                this.isLoading = false

                /** @type {TeamViewModel} */
                const team = r

                populateWorkload(team.workload)
                populateActivities(team.activeActivities)
                populateTasks(team.tasksPerformance)
            })

    }

})