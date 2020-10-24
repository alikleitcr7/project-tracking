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
    lineOptions: () => {
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
            },
        }
    },
    pieOptions: () => {
        return {

            responsive: true,
            scaleBeginAtZero: true,

            layout: {
                padding: {
                    top: 5,
                    left: 5,
                    right: 5
                }
            },
        }
    },
}

/**
 * 
 * @param {Array<KeyValuePair<UserKeyValuePair,TasksWorkload>>} workload
 */
function populateWorkload(workload) {

    var ctx = document.getElementById('bar_workload').getContext('2d');

    const labels = workload.map(k => k.key.name)

    var chart = new Chart(ctx, {
        // The type of chart we want to create
        type: 'bar',

        // The data for our dataset
        data: {
            labels,
            datasets: [
                {
                    label: 'Pending',
                    backgroundColor: colors.pending,
                    borderColor: colors.pending,
                    data: workload.map(k => k.value.pendingCount)
                },
                {
                    label: 'Progress',
                    backgroundColor: colors.progress,
                    borderColor: colors.progress,
                    data: workload.map(k => k.value.progressCount)
                },
                {
                    label: 'Done',
                    backgroundColor: colors.done,
                    borderColor: colors.done,
                    data: workload.map(k => k.value.doneCount)
                },
            ]
        },
        options: chartsHelper.barOptions()
    });
}

/**
 * 
 * @param {Array<KeyValuePair<Date, number>>} activitiesFrequency
 */
function populateActivities(activitiesFrequency) {

    var ctx = document.getElementById('line_activities').getContext('2d');

    const labels = activitiesFrequency.map(k => moment(k.key).format('D/MMM'))
    const data = activitiesFrequency.map((k, idx) => ({ x: idx, y: k.value }))

    var chart = new Chart(ctx, {
        // The type of chart we want to create
        type: 'line',

        // The data for our dataset
        data: {
            labels,
            datasets: [{
                label: 'Activities',
                backgroundColor: colors.mainLight,
                borderColor: colors.main,
                data
            }]
        },

        // Configuration options go here
        options: chartsHelper.lineOptions()
    });
}

/**
 * 
 * @param {ITaskPerformance} taskPerformance
 */
function populateTasks(taskPerformance) {

    var ctx = document.getElementById('pie_tasks').getContext('2d');

    const metrics = initTasksPerformanceProgress();

    const labels = metrics.map(k => `${k.code}: ${taskPerformance[k.fromProp]} (${(taskPerformance[k.fromProp] / taskPerformance.totalCount * 100)}%)`)
    const data = metrics.map(k => taskPerformance[k.fromProp])
    const pieColors = metrics.map(k => colors[k.code])

    console.log({ labels, data, pieColors })


    var chart = new Chart(ctx, {
        // The type of chart we want to create
        type: 'pie',

        // The data for our dataset
        data: {
            labels,
            datasets: [{
                label: 'Task Performance',
                backgroundColor: pieColors,
                //borderColor: colors.main,
                data
            }]
        },
        // Configuration options go here
        options: chartsHelper.pieOptions()
    });
}

const Modals_Team = {
    Members: {
        Show: function () {
            $('#MembersModal').modal('show');
        },
        Hide: function () {
            $('#MembersModal').modal('hide');
        }
    },
    Broadcasts: {
        Show: function () {
            $('#BroadcastsModal').modal('show');
        },
        Hide: function () {
            $('#BroadcastsModal').modal('hide');
        }
    },
    Projects: {
        Show: function () {
            $('#ProjectsModal').modal('show');
        },
        Hide: function () {
            $('#ProjectsModal').modal('hide');
        }
    }
}

new Vue({
    el: '#Team',
    data: {
        teamId: null,
        team: null,
        message: '',
        broadcasts: {
            data: [],
            isLoading: false,
            isLoaded: false
        },
        projects: {
            data: [],
            isLoading: false,
            message: null,
            isLoaded: false
        },
        isLoading: true
    },
    methods: {
        getMemberNameById: function () {

        },
        showMembers: function () {
            Modals_Team.Members.Show()
        },
        openProjects: function () {

            Modals_Team.Projects.Show();

            if (this.projects.isLoaded) {
                return
            }

            this.projects.isLoading = true
            this.projects.message = null

            ProjectsService.GetByTeam(this.teamId)
                .then((r) => {
                    const record = r.data
                    this.projects.data = record
                    this.projects.isLoaded = true

                })
                .catch((e) => {
                    const errorMessage = getAxiosErrorMessage(e)
                    this.projects.message = errorMessage

                })
                .then(() => {
                    this.projects.isLoading = false

                })

        }
    },
    computed: {

    },
    mounted: function () {

        const teamId = parseInt($('#Team').attr('data-id'))

        this.teamId = parseInt(teamId)
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
                populateActivities(team.activitiesFrequency)
                populateTasks(team.tasksPerformance)
            })

    }
})