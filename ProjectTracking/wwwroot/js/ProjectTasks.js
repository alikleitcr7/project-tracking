Vue.component('paginate', VuejsPaginate)
Vue.component('date-picker', VueBootstrapDatetimePicker)

const debugProjectTasks = true;

const projectTaskFields = [
    {
        name: 'title',
        displayName: 'title',
        min: 0,
        max: 255,
        type: DATA_TYPES.TEXT,
        required: true,
    },
    {
        name: 'statusCode',
        displayName: 'Status',
        errorMessage: 'Status is Required',
        min: 0,
        type: DATA_TYPES.NUMBER,
        required: true,
    },
]

const Modals_ProjectTasks = {
    ProjectTask: {
        Show: function () {
            $('#ProjectTaskModal').modal('show');
        },
        Hide: function () {
            $('#ProjectTaskModal').modal('hide');
        }
    },
    ProjectTaskStatusModifications: {
        Show: function () {
            $('#ProjectTaskStatusModificationsModal').modal('show');
        },
        Hide: function () {
            $('#ProjectTaskStatusModificationsModal').modal('hide');
        }
    },
    Members: {
        Show: function () {
            $('#MembersModal').modal('show');
        },
        Hide: function () {
            $('#MembersModal').modal('hide');
        }
    },
    Teams: {
        Show: function () {
            $('#TeamsModal').modal('show');
        },
        Hide: function () {
            $('#TeamsModal').modal('hide');
        }
    }
}

const getActiveProjectId = () => parseInt($('#ProjectTasks').attr('data-project'))
const isProjectSupervisor = () => $('#ProjectTasks').attr('data-is-project-supervisor') === 'True'

const projectTaskFormObject = (obj) => {

    const projectId = getActiveProjectId();

    let record = obj ? {
        id: obj.id,
        title: obj.title,
        projectId,
        description: obj.description,
        startDate: obj.startDate,
        plannedEnd: obj.plannedEnd,
        actualEnd: obj.actualEnd,
        statusCode: obj.statusCode,
        startDateDisplay: obj.startDateDisplay,
        plannedEndDisplay: obj.plannedEndDisplay,
        actualEndDisplay: obj.actualEndDisplay,
        statusDisplay: obj.statusDisplay,
    } : {
            title: null,
            projectId,
            description: null,
            startDate: null,
            plannedEnd: null,
            actualEnd: null,
            statusCode: 0,
            startDateDisplay: null,
            plannedEndDisplay: null,
            actualEndDisplay: null,
            statusDisplay: null,
        }

    return {
        record,
        message: '',
        isLoading: false,
        isSaving: false,
    }
}

const projectTaskObject = () => {
    return {
        data: [],
        filterBy: {
            keyword: '',
            categoryId: null,
        },
        dataPaging: {
            page: 0,
            totalPages: 0,
            length: 5,
            totalCount: 0,
            pagination: 'custom-pagination',
            prev: 'Prev',
            next: 'Next',
        },
        isLoading: false,
        isProcessing: false,
        message: '',
        form: projectTaskFormObject(),
        statuses: {
            data: [],
            isLoading: false
        },
        activeProjectTask: null,
        statusModifications: {
            data: [],
            isLoading: false
        },
        statuses: {
            data: [],
            isLoading: false
        }
    }
}

const projectTasksMethods = {
    projectTasks_validateForm: function (obj) {

        const form = obj || this.projectTasks.form.record

        console.log({ form })

        var isValid = true
        let finalMessage = '';

        for (var i = 0; i < projectTaskFields.length; i++) {

            const field = projectTaskFields[i]
            const fieldValue = form[field.name];

            console.log('field validation', { field, fieldValue })

            const validator = new CoreValidator(field.name, fieldValue, field.required, field.type, field.min, field.max, field.displayName)
            isValid = validator.validate()


            if (!isValid) {

                finalMessage = field.errorMessage || validator.message();
                console.log(' validation 3')

                break;
            }
        }

        console.log('end validation')


        if (!isValid) {
            this.projectTasks_setFormMessage(finalMessage || 'Fill Required Fields to Continue')
        }
        else {

            // check dates 

            const { startDate, plannedEnd, actualEnd } = form

            // validate start/planned/actual date
            // from must be less than to/actual date
            if (startDate) {

                const startDate_moment = moment(startDate)

                const invalidPlanned = plannedEnd && moment(plannedEnd) <= startDate_moment
                const invalidActual = actualEnd && moment(actualEnd) <= startDate_moment

                if (invalidPlanned && invalidActual) {
                    finalMessage = 'Planned and Actual date must be greater than Start date'
                    isValid = false
                }
                else if (invalidPlanned) {
                    finalMessage = 'Planned date must be greater than Start date'
                    isValid = false
                }
                else if (invalidActual) {
                    finalMessage = 'Actual date must be greater than Start date'
                    isValid = false
                }
            }
            else if (plannedEnd || actualEnd) {

                finalMessage = 'Cannot add Planned or Actual end date without start date'
                isValid = false
            }


            if (!isValid) {
                this.projectTasks_setFormMessage(finalMessage)
            }


        }

        return isValid;
    },
    projectTasks_setFormMessage: function (message) {

        this.projectTasks.form.message = message
    },
    projectTasks_setFormLoading: function (isLoading) {
        this.projectTasks.form.isLoading = isLoading
    },
    projectTasks_setFormSaving: function (isSaving) {
        this.projectTasks.form.isSaving = isSaving
    },
    projectTasks_setProcessing: function (isProcessing) {
        this.projectTasks.isProcessing = isProcessing
    },
    projectTasks_setLoading: function (isLoading) {
        this.projectTasks.isLoading = isLoading
    },
    projectTasks_setMessage: function (message) {
        this.projectTasks.message = message
    },
    projectTasks_openModal: function () {

        this.projectTasks.message = ''
        this.projectTasks.form = projectTaskFormObject()


        Modals_ProjectTasks.ProjectTask.Show()
    },
    projectTasks_edit: function (id) {

        // INDEX VALIDATION if (idx < 0 || idx>

        //if (idx > this.projectTasks.data.length - 1) {
        //    console.warn(`INVALID INDEX ${idx}, TOTAL RECORDS ARE ${this.projectTasks.data.length}`)
        //    return
        //}

        // GET RECORD
        const record = this.projectTasks.data.find(k => k.id === id)

        // OPEN MODAL
        Modals_ProjectTasks.ProjectTask.Show();
        this.projectTasks.form.isLoading = true

        this.projectTasks_setFormLoading(true)
        this.projectTasks_setFormMessage('Loading...');

        // GET REQUEST

        ProjectTasksService.GetById(record.id)
            .then(r => {

                const record = r.data

                if (!record) {
                    this.projectTasks_setFormMessage(BASIC_ERROR_MESSAGE);
                    return
                }

                this.projectTasks_setFormMessage('');

                this.projectTasks.form = projectTaskFormObject(record);
            })
            .catch(e => {

                console.error('get error', e)

                this.projectTasks_setFormMessage(BASIC_ERROR_MESSAGE);
            })
            .then(() => {
                this.projectTasks_setFormLoading(false)
            })
    },

    projectTasks_save: function () {

        this.projectTasks_setFormMessage('');

        let pendingRecord = { ...this.projectTasks.form.record }

        // required fields validation

        if (!this.projectTasks_validateForm()) {

            //this.projectTasks_setFormMessage('Fill the required fields to continue')
            return;
        }

        console.log('sending')

        const sendForm = () => {

            // START UPDATE/CREATE REQUEST
            this.projectTasks_setFormSaving(true)


            // EXISTING RECORD
            if (pendingRecord.id) {

                ProjectTasksService.Save(pendingRecord)
                    .then((r) => {

                        /** @type {IClientResponseModel<ISubject>} */
                        const record = r.data

                        if (debugProjectTasks) {
                            console.log('update response', r)
                        }

                        if (record) {

                            // feedback
                            this.projectTasks_setFormMessage('Updated!')

                            // update data array
                            let data = [...this.projectTasks.data]

                            const idx = data.findIndex(k => k.id === record.id)

                            if (idx !== -1) {

                                data[idx] = record
                                this.projectTasks.data = data;

                                // update gantt if loaded
                                if (this.ganttChart.isLoaded) {

                                    this.ganttChart.data = this.ganttChart.data.map(k => k.id === record.id ? record : k)

                                    this.drawGantt(this.ganttChart.data)
                                }

                            }
                            else {
                                location.reload()
                            }
                        }
                        else {
                            this.projectTasks_setFormMessage(BASIC_ERROR_MESSAGE)
                        }
                    })
                    .catch((e) => {

                        console.error('Updated!', e)

                        this.projectTasks_setFormMessage(BASIC_ERROR_MESSAGE)

                    })
                    .then(() => {
                        this.projectTasks_setFormSaving(false)
                    });

                return
            }

            // NEW RECORD
            ProjectTasksService.Save(pendingRecord)
                .then((r) => {

                    const record = r.data

                    if (debugProjectTasks) {
                        console.log('add response', r)
                    }

                    if (record) {

                        let data = [...this.projectTasks.data]

                        data.unshift(record)

                        this.projectTasks.data = data
                        this.projectTasks.form = projectTaskFormObject();

                        // append to gantt if loaded

                        if (this.ganttChart.isLoaded) {

                            this.ganttChart.data = [...this.ganttChart.data, record]

                            this.drawGantt(this.ganttChart.data)
                        }

                        this.projectTasks_setFormMessage('Added!')
                    }
                    else {
                        this.projectTasks_setFormMessage(BASIC_ERROR_MESSAGE)
                    }

                })
                .catch((e) => {
                    this.projectTasks_setFormMessage(getAxiosErrorMessage(e))
                })
                .then(() => {
                    this.projectTasks_setFormSaving(false)
                });

        }

        sendForm()
    },
    projectTasks_delete: function (idx) {

        // INDEX VALIDATION
        if (idx < 0 || idx > this.projectTasks.data.length - 1) {
            console.warn(`INVALID INDEX ${idx}, TOTAL RECORDS ARE ${this.projectTasks.data.length}`)
            return
        }

        // CONFIRMATION
        if (!confirm('Confirm Deletion')) {
            return;
        }

        /** @type {Array<ISubject>} */
        let data = [...this.projectTasks.data];

        // set deleting
        let record = data[idx];

        this.projectTasks_setProcessing(true)

        ProjectTasksService.Delete(record.id)
            .then((r) => {

                const record = r.data

                if (debugProjectTasks) {
                    console.log('delete response', r)
                }

                if (record) {

                    data.splice(idx, 1);
                    this.projectTasks.data = data
                    this.projectTasks_getAll(0)
                    this.projectTasks_setMessage('Subject deleted!')
                }
                else {
                    this.projectTasks_setMessage(BASIC_ERROR_MESSAGE)
                }
            })
            .catch((e) => {

                let errorMessage = getAxiosErrorMessage(e);

                console.error('delete', errorMessage)

                if (errorMessage === 'IS_ASSIGNED_TO_TIMESHEET') {

                    errorMessage = 'Task is assigned to a timesheet and cannot be deleted'


                    this.projectTasks_getAll(0)
                }

                bootbox.alert(errorMessage)

                this.projectTasks_setMessage(errorMessage)
            })
            .then(() => {

                this.projectTasks_setProcessing(false)
            });
    },
    projectTasks_getAll: function (page = 0) {

        this.projectTasks_setLoading(true)
        this.projectTasks_setMessage('Loading...')

        const { keyword } = this.projectTasks.filterBy
        const { length } = this.projectTasks.dataPaging
        const projectId = getActiveProjectId();

        return ProjectTasksService.Search(keyword, projectId, page, length)
            .then((r) => {

                /** @type {IClientResponseModel<ISubject>} */
                const { record, totalCount } = r.data

                if (debugProjectTasks) {
                    console.log('getall projectTask response', r)
                }

                if (record) {
                    this.projectTasks.data = [...record]
                    this.projectTasks.dataPaging.totalCount = totalCount
                }
                else {
                    this.projectTasks_setMessage(BASIC_ERROR_MESSAGE)
                }
            })
            .catch((e) => {
                console.error('projectTasks getall', e)
                this.projectTasks_setMessage(BASIC_ERROR_MESSAGE)
            })
            .then(() => {
                this.projectTasks_setLoading(false)
            })

    },
    projectTasks_pageClick: function (pageNum) {
        this.projectTasks_getAll(pageNum - 1)
    },


    projectTasks_setStatusesLoading: function (isLoading) {
        this.projectTasks.statuses.isLoading = isLoading
    },
    projectTasks_getAllStatuses: function () {

        this.projectTasks_setStatusesLoading(true)

        return ProjectTasksService.GetProjectTaskStatuses()
            .then((r) => {

                /** @type {IClientResponseModel<ISubject>} */
                const record = r.data

                if (debugProjectTasks) {
                    console.log('getall projectTask response', r)
                }

                if (record) {
                    this.projectTasks.statuses.data = [...record]
                }
                else {
                    this.projectTasks_setMessage(BASIC_ERROR_MESSAGE)
                }
            })
            .catch((e) => {
                console.error('projectTasks getall', e)
                this.projectTasks_setMessage(getAxiosErrorMessage(e))
            })
            .then(() => {
                this.projectTasks_setStatusesLoading(false)
            })

    },


    projectTasks_setStatusesModificationLoading: function (isLoading) {
        this.projectTasks.statusModifications.isLoading = isLoading
    },
    projectTasks_viewStatusesModification: function (projectTask) {

        this.projectTasks_setStatusesModificationLoading(true)

        this.projectTasks.activeProjectTask = null;
        this.projectTasks.activeProjectTask = { ...projectTask }

        Modals_ProjectTasks.ProjectTaskStatusModifications.Show();

        return ProjectTasksService.GetStatusModifications(projectTask.id)
            .then((r) => {

                /** @type {IClientResponseModel<ISubject>} */
                const record = r.data

                if (record) {
                    this.projectTasks.statusModifications.data = [...record]
                }
                else {
                    this.projectTasks_setMessage(BASIC_ERROR_MESSAGE)
                }
            })
            .catch((e) => {
                console.error('projectTasks getall', e)
                this.projectTasks_setMessage(getAxiosErrorMessage(e))
            })
            .then(() => {
                this.projectTasks_setStatusesModificationLoading(false)
            })
    },
}


function handleGanntTabClick() {
    projectTasks_app.populateGannt()
}


var projectTasks_app = new Vue({
    el: "#ProjectTasks",
    data: {
        dateOptions,
        dateTimeOptions,
        projectTasks: projectTaskObject(),
        projectId: getActiveProjectId(),
        errors: null,
        oNull: null,
        isProjectSupervisor: false,
        isAdmin: false,
        isMember: false,
        overview: {
            data: null,
            isLoading: true,
            message: null
        },
        ganttChart: {
            isLoaded: false,
            isLoading: false,
            data: []
        }
    },
    computed: {
        projectTasksTotalPages: function () {
            const totalCount = this.projectTasks.dataPaging.totalCount
            const length = this.projectTasks.dataPaging.length

            const totalPages = Math.ceil(totalCount / length);

            console.log({ totalPages })

            return totalPages || 0
        },
        teamsUsers: function () {
            const data = this.overview.data

            if (!data) {
                return []
            }


            return data.teams.map(k => ({ ...k, users: data.members.filter(u => u.teamId === k.id) }))
        }
    },
    methods: {
        ...projectTasksMethods,
        showMembers: function () {
            Modals_ProjectTasks.Members.Show()
        },
        populateGannt: function () {


            if (this.ganttChart.isLoaded) {
                return
            }

            this.errors = null

            this.ganttChart.isLoaded = false
            this.ganttChart.isLoading = true
            //this.ganttChart.data = []

            ProjectTasksService.GetByProject(this.projectId)
                .then((r) => {
                    const record = r.data

                    this.ganttChart.data = record

                    return record
                })
                .catch((e) => {
                    const errorMessage = getAxiosErrorMessage(e)
                    this.errors = errorMessage

                    return null
                })
                .then((r) => {

                    this.ganttChart.isLoaded = r !== null

                    this.drawGantt(r)
                    //  var tasks = [
                    //      {
                    //          id: 'Task 1',
                    //          name: 'Redesign website',
                    //          start: '2016-12-28',
                    //          end: '2016-12-31',
                    //          //progress: 20,
                    //          //dependencies: 'Task 2, Task 3',
                    //          custom_class: 'bar-milestone' // optional
                    //      },
                    //      {
                    //          id: 'Task 2',
                    //          name: 'Dev website',
                    //          start: '2016-12-31',
                    //          end: '2017-02-01',
                    //          //progress: 20,
                    //          //dependencies: 'Task 2, Task 3',
                    //          custom_class: 'bar-milestone' // optional
                    //      },
                    //]
                    //  var gantt = new Gantt("#gantt-chart", tasks, {
                    //      header_height: 50,
                    //      column_width: 30,
                    //      step: 24,
                    //      view_modes: ['Quarter Day', 'Half Day', 'Day', 'Week', 'Month'],
                    //      bar_height: 20,
                    //      bar_corner_radius: 3,
                    //      arrow_curve: 5,
                    //      padding: 18,
                    //      view_mode: 'Day',
                    //      date_format: 'YYYY-MM-DD',
                    //      custom_popup_html: null
                    //  });


                    //google.charts.load('current', { 'packages': ['gantt'] });
                    //google.charts.setOnLoadCallback(drawChart);

                    //function daysToMilliseconds(days) {
                    //    return days * 24 * 60 * 60 * 1000;
                    //}

                    //function drawChart() {
                    //    var data = new google.visualization.DataTable();
                    //    data.addColumn('string', 'Task ID');
                    //    data.addColumn('string', 'Task Name');
                    //    data.addColumn('string', 'Resource');
                    //    data.addColumn('date', 'Start Date');
                    //    data.addColumn('date', 'End Date');
                    //    data.addColumn('number', 'Duration');
                    //    data.addColumn('number', 'Percent Complete');
                    //    data.addColumn('string', 'Dependencies');

                    //    data.addRows([
                    //        ['2014Spring', 'Spring 2014', 'spring',
                    //            new Date(2014, 2, 22), new Date(2014, 5, 20), null, 100, null],
                    //        ['2014Summer', 'Summer 2014', 'summer',
                    //            new Date(2014, 5, 21), new Date(2014, 8, 20), null, 100, null],
                    //        ['2014Autumn', 'Autumn 2014', 'autumn',
                    //            new Date(2014, 8, 21), new Date(2014, 11, 20), null, 100, null],
                    //        ['2014Winter', 'Winter 2014', 'winter',
                    //            new Date(2014, 11, 21), new Date(2015, 2, 21), null, 100, null],
                    //        ['2015Spring', 'Spring 2015', 'spring',
                    //            new Date(2015, 2, 22), new Date(2015, 5, 20), null, 100, null],
                    //        ['2015Summer', 'Summer 2015', 'summer',
                    //            new Date(2015, 5, 21), new Date(2015, 8, 20), null, 100, null],
                    //        ['2015Autumn', 'Autumn 2015', 'autumn',
                    //            new Date(2015, 8, 21), new Date(2015, 11, 20), null, 100, null],
                    //        ['2015Winter', 'Winter 2015', 'winter',
                    //            new Date(2015, 11, 21), new Date(2016, 2, 21), null, 100, null],
                    //        ['Football', 'Football Season', 'sports',
                    //            new Date(2014, 8, 4), new Date(2015, 1, 1), null, 100, null],
                    //        ['Baseball', 'Baseball Season', 'sports',
                    //            new Date(2015, 2, 31), new Date(2015, 9, 20), null, 100, null],
                    //        ['Basketball', 'Basketball Season', 'sports',
                    //            new Date(2014, 9, 28), new Date(2015, 5, 20), null, 100, null],
                    //        ['Hockey', 'Hockey Season', 'sports',
                    //            new Date(2014, 9, 8), new Date(2015, 5, 21), null, 100, null]
                    //    ]);

                    //    const trackHeight = 30
                    //    var paddingHeight = 50;
                    //    var rowHeight = data.getNumberOfRows() * trackHeight;
                    //    var chartHeight = rowHeight + paddingHeight;


                    //    var options = {
                    //        height: chartHeight,
                    //        //width: 2000,
                    //        gantt: {
                    //            trackHeight: trackHeight,
                    //            barHeight: 20,
                    //            innerGridDarkTrack: {
                    //                fill: colors.progress,
                    //            },
                    //            palette: [
                    //                {
                    //                    "color": colors.main,
                    //                    "dark": colors.main,
                    //                    "light": colors.mainLight
                    //                }
                    //            ]
                    //        }
                    //    };

                    //    var ganttChart = new google.visualization.Gantt(document.getElementById('gantt-chart'));

                    //    ganttChart.draw(data, options);

                    //    function resizeCharts() {
                    //        ganttChart.draw(data, options);
                    //    }

                    //    if (window.addEventListener) {
                    //        window.addEventListener("resize", resizeCharts);
                    //    } else if (window.attachEvent) {
                    //        window.attachEvent("onresize", resizeCharts);
                    //    } else {
                    //        window.onresize = resizeCharts;
                    //    }
                    //}

                    this.ganttChart.isLoading = false
                })
        },
        /**
         * @param {Array<IProjectTask>} tasks
         */
        drawGantt: function (tasks) {
            //const tasks = r
            const statuses = PROJECT_TASK_STATUS._toList()

            const chartDateFormat = 'M/D/YYYY HH:mm'
            const formatTaskDate = (taskDate) => {
                return moment(taskDate).format(chartDateFormat)
            }

            const ganttTasksPoints = tasks
                .filter(k => k.startDate && k.plannedEnd)
                .map(k => ({
                    color: colors[statuses.find(s => s.key === k.statusCode).code],
                    name: k.title,
                    id: k.id,
                    y: [formatTaskDate(k.startDate), formatTaskDate(k.plannedEnd)]
                }));

            const chartId = 'gantt-chart'


            const app = this

            var chart = JSC.chart(chartId, {
                debug: false,
                type: 'horizontal column',
                zAxisScaleType: 'stacked',
                yAxis_scale_type: 'time',
                xAxis_visible: false,
                yAxis: {
                    markers: [
                        {
                            value: moment().format(chartDateFormat),
                            color: colors.mainDark,
                            label_text: 'Now'
                        }
                    ]
                },
                defaultPoint_events_click: function () {
                    app.projectTasks_edit(this.id)
                },
                //title_label_text: 'Project Alpha ',
                legend_visible: false,
                legend_defaultEntry_value: "{hours(%yRangeSums*1):number d2}hr",
                defaultPoint: {
                    label_text: '%name',
                    tooltip: '<b>%name</b> <br/>%low - %high<br/>{days(%high-%low)}days'
                },
                series: [
                    {
                        name: 'one',
                        points: ganttTasksPoints
                    }
                ]
            });

            console.log({ chart })
        }
        //showTeams: function () {
        //    Modals_ProjectTasks.Teams.Show()
        //},
    },
    mounted: function () {

        this.projectTasks_getAll()
        this.projectTasks_getAllStatuses()

        this.isProjectSupervisor = isProjectSupervisor()
        this.isAdmin = currentUser.role() === APP_USER_ROLES.admin.value
        this.isMember = currentUser.role() === APP_USER_ROLES.teamMember.value

        ProjectsService.GetOverview(this.projectId)
            .then((r) => {
                const record = r.data
                this.overview.data = record

                return record
            })
            .catch((e) => {
                const errorMessage = getAxiosErrorMessage(e)
                this.overview.message = errorMessage

                return null
            })
            .then((r) => {
                this.overview.isLoading = false

                /** @type {ProjectOverview} */
                const overview = r

                chartsHelper.charts.populateWorkload('bar_workload', overview.workload)
                chartsHelper.charts.populateActivities('line_activities', overview.activitiesFrequency)
                chartsHelper.charts.populateTasks('pie_tasks', overview.tasksPerformance)
            })
    }
})

