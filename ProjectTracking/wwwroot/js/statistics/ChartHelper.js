//Chart.Legend.prototype.afterFit = function () {
//    this.height = this.height + 50;
//};

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
    lineOptions: (displayLegend = false) => {
        return {
            legend: {
                display: displayLegend
            },
            layout: {
                padding: {
                    top: 15,
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
    lineOptionsMultiDataSet: () => {
        return {
            legend: {
                display: true,
                position: 'bottom'
            },
            layout: {
                padding: {
                    top: 15,
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
    dynamicColors: () => {

        var r = Math.floor(Math.random() * 255);
        var g = Math.floor(Math.random() * 255);
        var b = Math.floor(Math.random() * 255);

        return "rgb(" + r + "," + g + "," + b + ")";
    },
    pallete: () => {
        return [
            'rgba(51, 0, 225, 0.75)',
            'rgba(214, 40, 40, 0.76)',
            'rgba(247, 127, 0, 0.71)',
            'rgba(252, 191, 73, 0.71)',
            'rgba(33, 158, 188, 0.80)',
            'rgba(116, 0, 184, 0.80)',
            'rgba(128, 255, 219, 0.77)',
            '#cdb4db',
            '#355070',
            '#212529',
            '#80b918',
            '#5a189a',
            '#e5383b',
            '#00bbf9',
        ]
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
        populateActivities: function (id, activitiesFrequency, type = 'line') {

            var ctx = document.getElementById(id).getContext('2d');

            const labels = activitiesFrequency.map(k => moment(k.key).format('D/MMM'))
            const data = activitiesFrequency.map((k, idx) => ({ x: idx, y: k.value }))

            const background = ctx.createLinearGradient(0, 0, 0, 600);
            background.addColorStop(0, colors.mainLightTransparent);
            background.addColorStop(1, colors.doneLight);


            return new Chart(ctx, {
                // The type of chart we want to create
                type,

                // The data for our dataset
                data: {
                    labels,
                    datasets: [{
                        label: 'Activities',
                        backgroundColor: background,
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

            const background = ctx.createLinearGradient(0, 0, 0, 600);
            background.addColorStop(0, colors.mainLightTransparent);
            background.addColorStop(1, colors.doneLight);

            return new Chart(ctx, {
                type: 'line',
                data: {
                    labels,
                    datasets: [{
                        label: 'Minutes',
                        backgroundColor: background,
                        borderColor: colors.mainDark,
                        data
                    }]
                },
                options: chartsHelper.lineOptions()
            });
        },
        /**
         * @param {Array<KeyValuePair<UserKeyValuePair, number>>} activitiesMinutes
         */
        populateUserActivities: function (id, activitiesMinuts, type = 'line') {

            var ctx = document.getElementById(id).getContext('2d');

            const labels = activitiesMinuts.map(k => k.key.name)
            const data = activitiesMinuts.map((k, idx) => ({ x: idx, y: k.value }))

            return new Chart(ctx, {
                type,
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

            const labels = metrics.map(k => `${k.code}: ${taskPerformance[k.fromProp]} (${(taskPerformance[k.fromProp] ? precisionRound((taskPerformance[k.fromProp]) / taskPerformance.totalCount * 100) : 0)}%:)`)
            const data = metrics.map(k => taskPerformance[k.fromProp])
            const pieColors = metrics.map(k => colors[k.code])

            //console.log({ labels, data, pieColors })


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
        },


        /**
         * 
         * @param {IProjectsPerformance} projectsPerformance
         */
        populateProjects: function (id, projectsPerformance) {

            var ctx = document.getElementById(id).getContext('2d');

            const metrics = initProjectsPerformanceProgress();

            const precisionRound = (number) => {
                var factor = Math.pow(10, 2);
                return Math.round(number * factor) / factor;
            }

            const labels = metrics.map(k => `${k.code}: ${projectsPerformance[k.fromProp]} (${(projectsPerformance[k.fromProp] ? precisionRound(projectsPerformance[k.fromProp] / projectsPerformance.totalCount * 100) : 0)}%)`)
            const data = metrics.map(k => projectsPerformance[k.fromProp])
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
        },

        /**
         * @param {Array<MemberActivitiesFrequency>} activitiesFrequency
         */
        populateMemberActivitiesFrequency: function (id, activitiesFrequency) {

            var ctx = document.getElementById(id).getContext('2d');

            const hasData = activitiesFrequency.length > 0

            const labels = hasData ? activitiesFrequency[0].activities.map(k => moment(k.key).format('D/MMM')) : []

            // colors
            const pallete = chartsHelper.pallete();
            const palletLength = pallete.length;
            let nextColor = 0

            const getNextColor = (idx) => {

                if (idx >= palletLength) {
                    nextColor = 0
                }

                return pallete[nextColor++]
            }


            // datasets

            const datasets = hasData ? activitiesFrequency.map((k, idxDs) => ({
                label: k.user.name,
                borderColor: getNextColor(),
                data: k.activities.map((a, idx) => ({ x: idx, y: a.value }))
            })) : []


            return new Chart(ctx, {
                type: 'line',
                data: {
                    labels,
                    datasets
                },
                options: chartsHelper.lineOptionsMultiDataSet()
            });
        },
        /**
         * @param {Array<TeamActivitiesFrequency>} activitiesFrequency
         */
        populateTeamActivitiesFrequency: function (id, activitiesFrequency) {

            var ctx = document.getElementById(id).getContext('2d');

            const hasData = activitiesFrequency.length > 0

            // labels
            const labels = hasData ? activitiesFrequency[0].activities.map(k => moment(k.key).format('D/MMM')) : []

            // colors
            const pallete = chartsHelper.pallete();
            const palletLength = pallete.length;
            let nextColor = 0

            const getNextColor = (idx) => {

                if (idx >= palletLength) {
                    nextColor = 0
                }

                return pallete[nextColor++]
            }

            // datasets
            const datasets = hasData ? activitiesFrequency.map(k => ({
                label: k.team.name,
                borderColor: getNextColor(),
                data: k.activities.map((k, idx) => ({ x: idx, y: k.value }))
            })) : []

            return new Chart(ctx, {
                type: 'line',
                data: {
                    labels,
                    datasets
                },
                options: chartsHelper.lineOptionsMultiDataSet()
            });
        },

    }
}

