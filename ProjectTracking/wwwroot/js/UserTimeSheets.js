Vue.component('date-picker', VueBootstrapDatetimePicker);

const Modals_TimeSheets = {
    ActivityModal: {
        Show: () => { $('#ActivityModal').modal('show') },
        Hide: () => { $('#ActivityModal').modal('hide') },
    }
}

//const CurrentUserTimeSheets = () => {
//    return axios.get('/TimeSheets/CurrentUserTimeSheets');
//}

var enumerateDaysBetweenDates = function (startDate, endDate) {
    var dates = [];
    console.log({ startDate, endDate })

    var currDate = moment(startDate, 'MM/DD/YYYY').startOf('day');
    var lastDate = moment(endDate, 'MM/DD/YYYY').startOf('day');

    dates.push(currDate.clone().toDate());

    while (currDate.add(1, 'days').diff(lastDate) < 0) {
        dates.push(currDate.clone().toDate());
    }

    dates.push(lastDate.clone().toDate());
    return dates;
};

var delayTimer;

// ACTIVITY MODAL DATA
const activityModalForm = (activity) => {

    if (activity) {
        return {
            id: activity.id,
            timeSheetTaskId: activity.timeSheetTaskId,
            fromDate: activity.fromDate ? moment(activity.fromDate) : null,
            toDate: activity.toDate ? moment(activity.toDate) : null,
            message: activity.message,
            ipAddressDisplay: activity.ipAddressDisplay,
            deletedAtDisplay: activity.deletedAtDisplay,
        }
    }

    return {
        id: null,
        timeSheetTaskId: null,
        fromDate: null,
        toDate: null,
        message: '',
        ipAddressDisplay: '',
        deletedAtDisplay: null,
    }
}


function getTaskFilterClass(status) {
    return `task-filter--${status.code}`
}

//const activityDataContractStartObject = (timeSheetProjectId, fromDate) => {

//    return {
//        FromDate: fromDate,
//        TimeSheetProjectId: timeSheetProjectId
//    }
//}

//const activityDataContractObject = (activity) => {

//    let ProjectFile = null;

//    if (activity.fileId) {
//        ProjectFile = {
//            ID: activity.fileId,
//            Name: activity.fileName
//        }
//    }
//    else if (activity.fileName) {
//        ProjectFile = {
//            ID: 0,
//            Name: activity.fileName
//        }
//    }

//    return {
//        ID: activity.id,
//        TypeOfWorkId: activity.typeOfWorkId,
//        MeasurementUnitId: activity.measurementUnitId,
//        Number: activity.number,
//        FromDate: activity.fromDate,
//        ToDate: activity.toDate,
//        Comment: activity.comment,
//        ProjectFile,
//        ProjectFileId: ProjectFile ? ProjectFile.ID : null,
//        TimeSheetProjectId: activity.timeSheetProjectId
//    }
//}

const activityModalObject = () => {
    return {
        title: '',
        //data: [],
        form: activityModalForm(),
        backupEdit: activityModalForm(),
        isUpdate: false,
        isLoading: false,
        isDeleting: false,
        isSaving: false,
        isDismissing: false,
        isLocked: false,
        isDeleted: false,
        isSaved: false,
        message: ''
    }
}

const getActiveActivity = (activity, tagIndex, projectIndex, subProjectIndex) => {
    return {
        activity,
        tagIndex: parseInt(tagIndex),
        projectIndex: parseInt(projectIndex),
        subProjectIndex: parseInt(subProjectIndex)
    }
}

const TASK_STATUSES_KEYS = {
    PENDING: 0,
    PROGRESS: 1,
    DONE: 2,
    FAILED_OR_TERMINATED: 3,
}

var user_timesheet_app = new Vue({
    el: '#UserTimeSheets',
    data: {
        timesheets: [],
        timesheetsAreLoading: true,
        activeTimeSheet: null,
        activeTimeSheetLoading: false,
        filteredProjects: [],
        filteredProjectsLoading: false,
        activeDate: null,
        activeDateIdx: -1,
        activeDateIndexIsCurrentDay: -1,
        activityModal: activityModalObject(),
        dateOptions,
        dateTimeOptions,
        activeSubProject: null,
        activeActivityData: null,
        endActivityData: null,
        activeActivities: [],
        tasksFilter: {
            statuses: [
                {
                    key: null,
                    code: 'all',
                    name: 'All',
                    icon: 'fa fa-stream',

                },
                {
                    key: TASK_STATUSES_KEYS.PENDING,
                    code: 'pending',
                    name: 'Pending',
                    icon: 'far fa-clock',

                },
                {
                    key: TASK_STATUSES_KEYS.PROGRESS,
                    code: 'progress',
                    name: 'In Progress',
                    icon: 'fa fa-spinner',

                },
                {
                    key: TASK_STATUSES_KEYS.DONE,
                    code: 'done',
                    name: 'Done',
                    icon: 'fa fa-check',

                },
                {
                    key: TASK_STATUSES_KEYS.FAILED_OR_TERMINATED,
                    code: 'failed',
                    name: 'F/T',
                    title: 'Failed or Terminated',
                    icon: 'fa fa-times',

                },
            ],
            selectedStatusKey: null
        },
        readOnly: false,
        currentUserRoles: [],
        selectedTimeSheetId: null,
        oNull: null,
        selectedTask: null,
        activeTask: null,
        activeActivity: null,
        activeActivityIsLoading: true,
        taskActivities: {
            data: [],
            message: '',
            isStarting: false,
            isLoading: true,
            showDeleted: false
        }
    },
    computed: {
        activeTimeSheetDisplay: function () {
            return this.activeTimeSheetLoading ? 'Loading...' : this.activeTimeSheet ? `` : '';
            //return this.activeTimeSheetLoading ? 'Loading...' : this.activeTimeSheet ? `${this.activeTimeSheet.fromDateDisplay} - ${this.activeTimeSheet.toDateDisplay}` : '';
        },
        activeTimeSheetDateIsLocked: function () {

            const activeDate = this.activeDate

            if (activeDate) {

                return !activeDate.isSame(new Date(), "day");
            }

            return true;
        },
        canUnlock: function () {
            const currentUserRoles = this.currentUserRoles
            var userId = getParam("userId");

            return userId && (currentUserRoles.includes('Admin') || currentUserRoles.includes('Manager'))
        },
        filteredTimeSheetProjects: function () {

            const timesheetProjects = this.activeTimeSheet.projects

            if (!timesheetProjects || !timesheetProjects.length)
                return []

            let filteredProjects = [...timesheetProjects];

            const status = this.tasksFilter.selectedStatusKey

            // map defines a new ref for that object (VDOM)
            return filteredProjects.map(k => ({ ...k })).filter(k => {

                console.log({ status, tasks: k.tasks.length })

                if (status === null) {
                    return true
                }

                // filter status
                k.tasks = k.tasks.filter(t => status === TASK_STATUSES_KEYS.FAILED_OR_TERMINATED ? t.statusCode >= status : t.statusCode === status)

                // dont show project if no tasks under it
                return k.tasks.length
            })
        },
        filteredTimeSheetProjectsTasksCount: function () {

            return this.filteredTimeSheetProjects
                .map(k => k.tasks.length).reduce((a, b) => a + b, 0)
        },
        activeDateIndexIsToday: function () {
            const { activeDateIdx, activeDateIndexIsCurrentDay } = this

            return activeDateIdx === activeDateIndexIsCurrentDay
        },
        activityModalSaveButtonLabel: function () {
            const { id, fromDate, toDate } = this.activityModal.form
            const isLoading = this.activityModal.isLoading
            const isUpdate = this.activityModal.isUpdate

            if (!id) {
                return ''
            }

            return isUpdate ? (isLoading ? 'Saving...' : 'Save') : (isLoading ? 'Commit...' : 'Commit')
        },
        activityModalDeleteButtonLabel: function () {

            const { id, fromDate, toDate } = this.activityModal.form

            const isDeleting = this.activityModal.isDeleting
            const isUpdate = this.activityModal.isUpdate

            if (!id) {
                return ''
            }

            return isUpdate ? (isDeleting ? 'Deleting...' : 'Delete') : (isDeleting ? 'Dismiss...' : 'Dismiss')
        },
        activityModalFeedbackLabel: function () {

            const { isSaved, isDeleted, isUpdate, form: { toDate } } = this.activityModal


            if (isSaved) {
                return isUpdate ? 'Saved!' : 'Done!'
            }

            if (isDeleted) {
                return isUpdate ? 'Deleted!' : 'Dismissed!'
            }

            return null
        },
        markAsStatuses: function () {

            const markAsAllowed = [TASK_STATUSES_KEYS.PENDING, TASK_STATUSES_KEYS.PROGRESS, TASK_STATUSES_KEYS.DONE]

            return this.tasksFilter.statuses.filter(k => markAsAllowed.includes(k.key))
        },
        taskActivitiesHasDeleted: function () {
            return this.taskActivities.data.findIndex(k => k.deletedAt !== null) > -1
        },
        taskActivitiesView: function () {

            const showDeleted = this.taskActivities.showDeleted

            if (showDeleted) {
                return this.taskActivities.data
            }

            return this.taskActivities.data.filter(k => k.deletedAt === null)
        }
    },
    methods: {
        deleteActivity: function () {

            if (!confirm('Confirm Deletion')) {
                return;
            }

            /**@type {ActivityModalObject} */
            let activityModal = { ...this.activityModal }

            activityModal.isDeleted = false
            activityModal.isDeleting = true

            this.activityModal = activityModal


            //const { tagIdx, subProjectIdx, projectIdx } = this.activeActivityData;

            /**@type {TimeSheetActivity} */
            //const activity = this.activeActivityData.activity

            const id = activityModal.form.id;

            TimeSheetActivitiesService.Delete(id)
                .then(r => {

                    const deleted = r.data

                    if (!deleted) {
                        activityModal.message = BASIC_ERROR_MESSAGE
                        return
                    }

                    let activities = [...this.taskActivities.data]

                    let activityToDeleteIdx = activities.findIndex(k => k.id === id)

                    // remove from view actvities
                    if (activityToDeleteIdx !== -1) {
                        activities.splice(activityToDeleteIdx, 1)
                        this.taskActivities.data = activities
                    }


                    // if it is an active activity -> clear

                    if (this.activeActivity && this.activeActivity.id === id) {
                        this.activeActivity = null
                    }

                    activityModal.isDeleted = true
                })
                .catch(e => {

                    console.error('delete', e)
                    activityModal.message = getAxiosErrorMessage(e)
                })
                .then(() => {

                    activityModal.isDeleting = false
                    this.activityModal = activityModal
                })

        },
        startActivity: function () {

            const task = this.selectedTask

            if (!task) {
                console.error('no task selected')
                return
            }

            if (this.activeActivity) {
                alert(`There is already an active activity on task "${task.title}"`)
                return
            }

            this.taskActivities.isStarting = true
            this.taskActivities.message = ''

            TimeSheetActivitiesService.Start(task.timeSheetTaskId)
                .then(r => {

                    const record = r.data

                    if (!record) {
                        this.taskActivities.message = BASIC_ERROR_MESSAGE
                        return
                    }

                    this.activeTask = { ...task }
                    this.activeActivity = record
                    this.taskActivities.data = [...this.taskActivities.data, record]
                })
                .catch(e => {
                    this.taskActivities.message = getAxiosErrorMessage(e)
                })
                .then(() => {
                    this.taskActivities.isStarting = false
                })
        },
        openActivityCommitModal: function (activity) {

            //const task = this.activeTask

            /** @type {ActivityModalForm} */
            activity = activity || this.activeActivity

            if (!activity) {
                console.error('no active activity')
                return
            }

            if (!activity.toDate && this.readOnly) {
                return
            }

            /** @type {ActivityModalObject} */
            let activityModal = { ...this.activityModal }

            // set form
            activityModal.form = activityModalForm(activity)
            console.log({ activityModal })

            //activityModal.form.id = activity.id

            activityModal.isUpdate = activity.fromDate && activity.toDate
            activityModal.isDeleted = false
            activityModal.isSaved = false
            activityModal.isDeleting = false
            activityModal.isLoading = false
            activityModal.isLocked = false
            activityModal.message = ''
            //activityModal.isDismissing = false

            this.activityModal = activityModal

            Modals_TimeSheets.ActivityModal.Show()
        },
        saveActivity: function () {

            //const task = this.activeTask
            //const activity = this.activeActivity

            /** @type {ActivityModalObject} */
            let activityModal = { ...this.activityModal }
            let form = activityModal.form


            if (!form.id) {
                console.error('no active activity in the form')
                return
            }

            activityModal.message = ''

            if (!activityModal.form.message) {
                activityModal.message = 'message is required'
                this.activityModal = activityModal
                return
            }

            this.activityModal = activityModal

            let model = {
                message: activityModal.form.message,
            }

            const isUpdate = activityModal.isUpdate


            if (isUpdate) {

                model = {
                    ...model,
                    id: activityModal.form.id,
                    fromDate: activityModal.form.fromDate,
                    toDate: activityModal.form.toDate
                }

                if (!(model.fromDate && model.toDate)) {
                    activityModal.message = 'From/To Dates are required'
                    this.activityModal = activityModal

                    return
                }

                if (moment(model.toDate) <= moment(model.fromDate)) {
                    activityModal.message = 'To Date should be greater than From Date'
                    this.activityModal = activityModal

                    return
                }

            }
            else {
                model = {
                    ...model,
                    activityId: activityModal.form.id,
                }
            }

            activityModal.isLoading = true

            if (isUpdate) {


                TimeSheetActivitiesService.Update(model)
                    .then(r => {

                        const record = r.data

                        if (!record) {
                            this.taskActivities.message = BASIC_ERROR_MESSAGE
                            return
                        }

                        activityModal.isSaved = true
                        //this.activeTask = null
                        //this.activeActivity = null

                        this.taskActivities.data = this.taskActivities.data
                            .map(k => k.id === model.id ? record : k)
                    })
                    .catch(e => {

                        console.error('stop', e)

                        activityModal.message = getAxiosErrorMessage(e)
                    })
                    .then(() => {
                        activityModal.isLoading = false
                        this.activityModal = activityModal
                    })
            }
            else {
                TimeSheetActivitiesService.Stop(model)
                    .then(r => {

                        const record = r.data

                        if (!record) {
                            this.taskActivities.message = BASIC_ERROR_MESSAGE
                            return
                        }

                        activityModal.isSaved = true

                        this.activeTask = null
                        this.activeActivity = null

                        this.taskActivities.data = this.taskActivities.data
                            .map(k => k.id === model.activityId ? record : k)
                    })
                    .catch(e => {

                        console.error('stop', e)

                        activityModal.message = getAxiosErrorMessage(e)
                    })
                    .then(() => {
                        activityModal.isLoading = false
                        this.activityModal = activityModal
                    })
            }

        },
        getActivityTitle: function (activity) {
            return activity.toDate ? activity.message : 'Active'
        },
        openTimeSheet: function (timesheet) {

            this.selectedTask = null

            if (!timesheet) {
                this.activeTimeSheet = null
                return
            }

            this.activeTimeSheet = {}
            this.activeTimeSheet.datesList = []
            this.activeTimeSheet.projects = []
            this.activeDateIndexIsCurrentDay = -1;
            this.activeDateIdx = -1;

            this.activeTimeSheetLoading = true

            //if (!this.readOnly) {
            //    this.readOnly = timesheet.isSigned
            //}

            TimeSheetsService.GetTimeSheetProjectsWithTasks(timesheet.id)
                .then(r => {

                    this.activeTimeSheet = { ...timesheet }

                    //id,title,description,[tasks]
                    // > id,title,description, [activities]
                    // > id,fromDate,toDate,comment,ipAddress,(activityId),(timeSheetId)

                    this.activeTimeSheet.projects = r.data

                    //.projects.map(k => ({
                    //    ...k,
                    //    tasks: k.tasks.map(sp => ({
                    //        ...sp
                    //    }))
                    //}))

                    // DATES LIST
                    let datesList = enumerateDaysBetweenDates(timesheet.fromDateDisplay, timesheet.toDateDisplay).map(k => ({ oDate: moment(k), date: moment(k).format("MM/DD/YYYY"), display: moment(k).format("ddd, DD/M") }))

                    for (var i = 0; i < datesList.length; i++) {
                        var date = datesList[i]

                        let index = date.oDate.isSame(new Date(), "day") ? i : -1;

                        if (index !== -1) {
                            this.activeDateIndexIsCurrentDay = index
                        }
                    }

                    this.activeTimeSheet.datesList = datesList

                    if (this.activeDateIndexIsCurrentDay > -1) {


                        this.openTimeSheetDate(this.activeDateIndexIsCurrentDay)


                        setTimeout(() => {

                            const el = $('.user-timesheets-content__days .list-group-item.is-current-day')

                            $('.user-timesheets-content__days').animate({
                                scrollTop: $(el).offset().top / 2
                            }, 800);

                        }, 100)
                    }

                    //,
                    //activities: sp.activities.map(a => ({
                    //    ...a,
                    //    oFromDate: moment(a.fromDate),
                    //    oToDate: moment(a.toDate),
                    //}))

                    this.filteredProjects = [];

                    //console.log('filteredProjects init')
                    //this.activeTimeSheet.filteredProjectsLoading = false;
                    //console.log({ projects: this.activeTimeSheet.projects })
                })
                .catch(e => console.warn({ e }))
                .then(() => {
                    this.activeTimeSheetLoading = false
                })

        },
        openTimeSheetDate: function (idx) {

            this.activeDate = moment(this.activeTimeSheet.datesList[idx].date, 'MM/DD/YYYY')
            this.activeDateIdx = idx

            return

            this.filteredProjectsLoading = true

            // get activities
            TimeSheetsService.GetActivitiesByDate(this.activeTimeSheet.id, this.activeTimeSheet.datesList[idx].date)
                .then(r => {

                    const activities = r.data

                    let filteredProjects = [...this.activeTimeSheet.projects];

                    for (var i = 0; i < filteredProjects.length; i++) {
                        for (var j = 0; j < filteredProjects[i].tasks.length; j++) {

                            //let subProject = filteredProjects[i].tasks[j]

                            let subProjectActivities = activities.filter(k => k.timeSheetProjectId === filteredProjects[i].tasks[j].id)

                            filteredProjects[i].tasks[j].activities = subProjectActivities

                            let activeTagIndex = subProjectActivities.findIndex(k => !k.toDate);

                            if (activeTagIndex > -1) {

                                let subProjectActiveActivity = subProjectActivities[activeTagIndex]

                                const activeActivityParsed = getActiveActivity(subProjectActiveActivity, activeTagIndex, i, j)

                                let activityExist = false
                                if (this.activeActivities && this.activeActivities.length) {
                                    activityExist = this.activeActivities.findIndex(k => k.projectIndex === activeActivityParsed.projectIndex && k.subProjectIndex === activeActivityParsed.subProjectIndex) > -1
                                }


                                if (!activityExist) {

                                    this.activeActivities.push(activeActivityParsed)
                                }

                                filteredProjects[i].tasks[j].isActive = true
                            }
                            else {

                                filteredProjects[i].tasks[j].isActive = false
                            }


                            //filteredProjects[i].tasks[j].activities = filteredProjects[i].tasks[j].activities.map(a => ({
                            //    ...a,
                            //    oFromDate: moment(a.fromDate),
                            //    oToDate: a.toDate ? moment(a.toDate) : null,
                            //}))
                        }
                    }

                    //console.log({filteredProjects})

                    this.filteredProjects = filteredProjects

                    //SetTimeSheetProjetsActivities(this, r.data)
                })
                .catch(e => {
                    console.warn({ e })
                })
                .then(() => {
                    this.filteredProjectsLoading = false
                })
        },
        toggleReadOnly: function () {
            this.readOnly = !this.readOnly
        },
        handleTimeSheetChange: function () {

            const id = this.selectedTimeSheetId

            if (id == null) {
                this.openTimeSheet(null)
            }

            const timesheet = this.timesheets.find(k => k.id === id)

            this.openTimeSheet(timesheet)
        },
        taskFilter: function (key) {
            this.tasksFilter.selectedStatusKey = key
        },
        taskStatusClass: function (status) {
            const itemClass = getTaskFilterClass(status)

            return {
                [itemClass]: true,
                'active': this.tasksFilter.selectedStatusKey === status.key
            }
        },
        markAsStatusClass: function (status, activeStatus) {
            const itemClass = getTaskFilterClass(status)

            return {
                [itemClass]: true,
                'active': activeStatus === status.key
            }
        },
        openTaskActivities: function (task) {

            if (!task) {
                this.selectedTask = null
                return
            }

            const activeTimeSheet = this.activeTimeSheet

            if (!activeTimeSheet) {
                console.error('no active timesheet')
                return
            }


            const activeDateIdx = this.activeDateIdx

            if (activeDateIdx === -1) {
                console.error('no active date')
                return
            }

            const date = this.activeTimeSheet.datesList[activeDateIdx].date;

            let taskActivities = { ...this.taskActivities }

            taskActivities.message = ''
            taskActivities.isLoading = true
            taskActivities.data = []

            this.taskActivities = taskActivities

            console.log({ task })
            this.selectedTask = { ...task }

            TimeSheetsService.GetActivitiesByDate(activeTimeSheet.id, task.id, date, this.readOnly)
                .then(r => {
                    const data = r.data

                    taskActivities.data = data || []
                })
                .catch(e => {
                    taskActivities.message = getAxiosErrorMessage(e)
                })
                .then(() => {

                    taskActivities.isLoading = false
                    this.taskActivities = taskActivities
                })
        },
        markTaskAs: function (status) {

            const selectedTask = { ...this.selectedTask }

            if (!selectedTask) {
                console.error('no selected task')
                return
            }


            if (selectedTask.statusCode === status.key) {
                return
            }

            bootboxExtension.confirm('Status Change', `You are about to change the current task status`, null, () => {


                let selectedTask = { ...this.selectedTask }

                selectedTask.statusIsChanging = true
                this.selectedTask = selectedTask


                ProjectTasksService.ChangeStatus(selectedTask.id, status.key)
                    .then((r) => {

                        const record = r.data

                        if (!record) {
                            bootbox.alert(BASIC_ERROR_MESSAGE)
                            return
                        }

                        //const timesheetProjects = this.activeTimeSheet.projects

                        const taskId = selectedTask.id
                        const projectId = selectedTask.projectId

                        let activeTimeSheet = { ...this.activeTimeSheet }
                        let projects = [...activeTimeSheet.projects]

                        activeTimeSheet.projects = projects
                            .map(k => k.id !== projectId ? k : ({
                                ...k,
                                tasks: k.tasks.map(t => t.id !== taskId ? t : ({ ...t, statusCode: status.key }))
                            }))

                        // update task in projects list
                        this.activeTimeSheet = activeTimeSheet


                        console.log('projects', this.activeTimeSheet.projects)

                        // update selected task
                        selectedTask.statusCode = status.key


                        //const projectIdx = timesheetProjects.findIndex(k => k.id === projectId)
                        //if (projectId > -1) {
                        //    const taskIdx = 
                        //}
                    })
                    .catch((e) => {
                        const errorMessage = getAxiosErrorMessage(e)
                        bootbox.alert(errorMessage)
                    })
                    .then(() => {
                        selectedTask.statusIsChanging = false
                        this.selectedTask = selectedTask
                    })
            })
        },
        getMarkAsIcon: function () {
            const task = this.selectedTask

            if (!task) {
                return null
            }

            const statusCode = task.statusCode
            console.log({ statusCode })
            const filterStatus = this.tasksFilter.statuses.find(k => k.key === statusCode)

            if (!filterStatus) {
                return null
            }

            return filterStatus.icon + ' ' + getTaskFilterClass(filterStatus)
        }
    },
    mounted: function () {

        var url_string = window.location.href
        var url = new URL(url_string);

        var userId = $('#UserTimeSheets').attr('data-user');
        var timeSheetId = $('#UserTimeSheets').attr('data-timesheet');
        var activeRole = $('#UserTimeSheets').attr('data-active-role');
        var activeUserId = $('#UserTimeSheets').attr('data-active-user');
        var isActiveSupervisor = $('#UserTimeSheets').attr('data-active-supervisor') === 'True';

        console.log({ userId, activeUserId })

        this.readOnly = userId !== activeUserId

        //if (userId) {

        //    //GetRoles()
        //    //    .then(r => {
        //    //        this.currentUserRoles = r.data
        //    //        //console.log('roles', r.data)
        //    //    })
        //    //    .catch(e => {
        //    //        console.error('roles', e)
        //    //    })
        //}

        //const timeSheetId = url.searchParams.get("timeSheetId");

        TimeSheetsService.GetUserTimeSheets(userId)
            .then((r) => {
                //this.readOnly = true
                this.timesheets = r.data || []

                if (timeSheetId) {

                    for (var i = 0; i < this.timesheets.length; i++) {
                        const timesheet = this.timesheets[i]

                        if (timesheet.id == timeSheetId) {
                            this.selectedTimeSheetId = timeSheetId
                            this.openTimeSheet(timesheet)
                        }
                    }
                }
                else {

                    if (this.timesheets.length) {
                        this.selectedTimeSheetId = this.timesheets[0].id
                        this.openTimeSheet(this.timesheets[0])
                    }
                }
            })
            .catch((e) => {
                console.error({ e })
            })
            .then(() => {
                this.timesheetsAreLoading = false
            })

        TimeSheetActivitiesService.GetUserActiveActivity(userId)
            .then((r) => {
                const record = r.data

                this.activeActivity = record
            })
            .catch((e) => {
                const errorMessage = getAxiosErrorMessage(e)
                console.error(errorMessage)
            })
            .then(() => {
                this.activeActivityIsLoading = false
            })

        //else {
        //    CurrentUserTimeSheets()
        //        .then((r) => {
        //            //console.log(r.data)
        //            this.timesheets = r.data

        //            if (this.timesheets.length > 0) {
        //                this.openTimeSheet(this.timesheets[0])
        //            }


        //        })
        //        .catch((e) => {
        //            console.error({ e })
        //        })
        //        .then(() => {
        //            this.timesheetsAreLoading = false
        //        })
        //}
    }
})
