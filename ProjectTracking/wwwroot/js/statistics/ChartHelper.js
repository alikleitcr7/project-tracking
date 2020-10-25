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
                        precision: 0,
                        //stepSize: stepOne ? 1 : undefined,
                        beginAtZero: true
                    },
                }],
            },
        }
    },
    pieOptions: () => {
        return {
            legend: {
                //display:false,
                position: 'left'
            },
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
    charts: {
        /**
        * @param {Array<KeyValuePair<UserKeyValuePair,TasksWorkload>>} workload
        */
        populateWorkload: function populateWorkload(id, workload) {

            var ctx = document.getElementById(id).getContext('2d');

            const labels = workload.map(k => k.key.name)

            return new Chart(ctx, {
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
        },
        /**
         * @param {Array<KeyValuePair<Date, number>>} activitiesFrequency
         */
        populateActivities: function (id, activitiesFrequency) {

            var ctx = document.getElementById(id).getContext('2d');

            const labels = activitiesFrequency.map(k => moment(k.key).format('D/MMM'))
            const data = activitiesFrequency.map((k, idx) => ({ x: idx, y: k.value }))

            return new Chart(ctx, {
                // The type of chart we want to create
                type: 'line',

                // The data for our dataset
                data: {
                    labels,
                    datasets: [{
                        label: 'Activities',
                        backgroundColor: colors.mainLightTransparent,
                        borderColor: colors.main,
                        data
                    }]
                },

                // Configuration options go here
                options: chartsHelper.lineOptions()
            });
        },
        /**
         * @param {Array<KeyValuePair<Date, number>>} activitiesMinutes
         */
        populateActivitiesMinuts: function (id, activitiesMinuts) {

            var ctx = document.getElementById(id).getContext('2d');

            const labels = activitiesMinuts.map(k => moment(k.key).format('D/MMM'))
            const data = activitiesMinuts.map((k, idx) => ({ x: idx, y: k.value }))

            return new Chart(ctx, {
                type: 'line',
                data: {
                    labels,
                    datasets: [{
                        label: 'Minutes',
                        backgroundColor: colors.mainLightTransparent,
                        borderColor: colors.main,
                        data
                    }]
                },
                options: chartsHelper.lineOptions()
            });
        },

        /**
         * 
         * @param {ITaskPerformance} taskPerformance
         */
        populateTasks: function (id, taskPerformance) {

            var ctx = document.getElementById(id).getContext('2d');

            const metrics = initTasksPerformanceProgress();

            const precisionRound = (number) => {
                var factor = Math.pow(10, 2);
                return Math.round(number * factor) / factor;
            }

            const labels = metrics.map(k => `${k.code}: ${taskPerformance[k.fromProp]} (${(precisionRound(taskPerformance[k.fromProp] / taskPerformance.totalCount * 100))}%)`)
            const data = metrics.map(k => taskPerformance[k.fromProp])
            const pieColors = metrics.map(k => colors[k.code])

            console.log({ labels, data, pieColors })


            return new Chart(ctx, {
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
    }
}

