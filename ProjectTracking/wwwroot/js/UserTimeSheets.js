
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
        }
    }

    return {
        id: null,
        timeSheetTaskId: null,
        fromDate: null,
        toDate: null,
        message: '',
        ipAddressDisplay: '',
    }
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
        isLoading: false,
        isDeleting: false,
        isSaving: false,
        isDismissing: false,
        isLocked: false,
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

/**
 * @param {UserTimeSheetsApp} app
 * @param {TimeSheetActivity} activities
 */
function SetTimeSheetProjetsActivities(app, activities) {

    //console.log({ activities })
    //app.activeActivities = [];

    let filteredProjects = [...app.activeTimeSheet.projects];

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
                if (app.activeActivities && app.activeActivities.length) {
                    activityExist = app.activeActivities.findIndex(k => k.projectIndex === activeActivityParsed.projectIndex && k.subProjectIndex === activeActivityParsed.subProjectIndex) > -1
                }


                if (!activityExist) {

                    app.activeActivities.push(activeActivityParsed)
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

    app.filteredProjects = filteredProjects
    //app.timesheetsAreLoading = false
}

var activityMethods = {
    openNewActivityModal: function (fromDate, timesheetProjectId, data) {

        this.endActivityData = data


        /** @type {ActivityModalObject} */
        let activityModal = { ...this.activityModal }

        // reset form
        activityModal.form = activityModalForm()
        activityModal.form.timeSheetProjectId = timesheetProjectId;
        //activityModal.form.fromDate = fromDate;
        //activityModal.form.toDate = moment().format(dateTimeOptions.format);

        activityModal.isDeleting = false
        activityModal.isSaving = false
        activityModal.isDismissing = false

        this.activityModal = activityModal

        Modals_TimeSheets.ActivityModal.Show()
    },
    openEditActivityModal: function (activity, tagIdx, subProjectIdx, projectIdx, locked = false) {

        /** @type {ActivityModalObject} */
        let activityModal = { ...this.activityModal }

        // set form
        activityModal.form = activityModalForm(activity)
        console.log({ form: activityModal.form })
        activityModal.isDeleting = false
        activityModal.isLoading = false
        activityModal.isLocked = locked
        activityModal.message = ''
        activityModal.isDismissing = false

        this.activeSubProject = this.filteredProjects[projectIdx].tasks[subProjectIdx]
        this.activeActivityData = { activity, tagIdx, subProjectIdx, projectIdx }
        console.log(activity, tagIdx, subProjectIdx, projectIdx)

        this.activityModal = activityModal

        Modals_TimeSheets.ActivityModal.Show()
    },
    addActivity: function () {

        /** @type {UserTimeSheetsApp} */
        const app = this;

        /** @type {ActivityModalObject} */
        let activityModal = { ...this.activityModal }

        // validate
        if (activityModal.form.number !== null && activityModal.form.number !== '' && !isNumber(activityModal.form.number)) {

            this.activityModal.message = 'Number field should be a valid number';

            return;
        }

        /** @type {TimeSheetActivity} */
        const activity = activityDataContractObject(activityModal.form)

        this.activityModal.message = '';

        // set loading
        activityModal.isLoading = true;
        this.activityModal = activityModal

        // send add request
        TimeSheetActivitiesService.Add(activity)
            .then((r) => {
                if (r && r.data) {

                    const { projectIndex, subProjectIndex } = this.endActivityData

                    let subProject = { ...app.filteredProjects[projectIndex].tasks[subProjectIndex] }
                    subProject.isActive = false
                    subProject.activities.push(r.data);

                    this.filteredProjects[projectIndex].tasks[subProjectIndex] = subProject;

                    let activeActivityIdx = this.activeActivities.findIndex(k => k.projectIndex === projectIndex && k.subProjectIndex === subProjectIndex)

                    if (activeActivityIdx !== -1) {
                        console.log('removing from active activities')
                        this.activeActivities.splice(activeActivityIdx, 1)
                    }

                    Modals_TimeSheets.ActivityModal.Hide()

                    console.log('activity add', r.data)
                }
                else {
                    activityModal.message = BASIC_ERROR_MESSAGE;
                }
            })
            .catch((e) => {
                console.log(e)
                activityModal.message = BASIC_ERROR_MESSAGE;
            })
            .then(() => {

                activityModal.isLoading = false;
                this.activityModal = activityModal
            });
    },
    saveActivity: function () {

        /** @type {UserTimeSheetsApp} */
        const app = this;

        /** @type {ActivityModalObject} */
        let activityModal = { ...this.activityModal }

        // validate
        if (activityModal.form.number !== null && activityModal.form.number !== '' && !isNumber(activityModal.form.number)) {

            this.activityModal.message = 'Number field should be a valid number';

            return;
        }

        /** @type {TimeSheetActivity} */
        const activity = activityDataContractObject(activityModal.form)

        this.activityModal.message = '';

        // set loading
        this.activityModal = activityModal

        const tsFromDate = app.activeTimeSheet.fromDate
        const tsToDateLimit = moment(app.activeTimeSheet.toDate).add(5, 'days');
        //console.log({tsToDateLimit})
        if (!moment(activity.FromDate).isBefore(activity.ToDate)) {
            alert("from date must be before to date")
            return
        }

        const fromDateValid = moment(activity.FromDate).isBetween(tsFromDate, tsToDateLimit)
        const toDateValid = moment(activity.ToDate).isBetween(tsFromDate, tsToDateLimit)

        if (!fromDateValid || !toDateValid) {
            alert("from/to date must fall between timesheet's from/to date")
            return;
        }


        activityModal.isSaving = true;
        this.activityModal = activityModal

        // send add request
        TimeSheetActivitiesService.Update(activity)
            .then((r) => {
                if (r && r.data) {

                    const { tagIdx, subProjectIdx, projectIdx } = this.activeActivityData;

                    let subProject = { ...app.filteredProjects[projectIdx].tasks[subProjectIdx] }

                    subProject.isLoading = false

                    console.log({ activeActivities: this.activeActivities })

                    const activeActivity = this.activeActivities.find(k => k.activity.id === r.data.id);
                    console.log({ activeActivity })
                    if (activeActivity && subProject.isActive) {

                        console.log('is active')
                        subProject.isActive = false

                        /**@type {Array<ActiveActivity>} */
                        let activeActivities = [...this.activeActivities]

                        let activeTagIdx = activeActivities.findIndex(k => k.activity.id === r.data.id)

                        if (activeTagIdx > -1) {
                            activeActivities.splice(activeTagIdx, 1)

                            this.activeActivities = activeActivities
                        }

                    }


                    subProject.activities[tagIdx] = r.data;

                    this.filteredProjects[projectIdx].tasks[subProjectIdx] = subProject;

                    activityModal.message = 'Saved!';

                    //Modals_TimeSheets.ActivityModal.Hide()

                    console.log('activity add', r.data)
                }
                else {
                    activityModal.message = BASIC_ERROR_MESSAGE;
                }
            })
            .catch((e) => {
                console.error(e)
                activityModal.message = BASIC_ERROR_MESSAGE;
            })
            .then(() => {

                activityModal.isSaving = false;
                this.activityModal = activityModal
            });
    },
    dismissActivity: function () {

    },
    deleteActivity: function () {

        if (!confirm('Confirm Deletion')) {
            return;
        }

        /**@type {ActivityModalObject} */
        let activityModal = this.activityModal

        activityModal.isDeleting = true

        this.activityModal = activityModal

        const { tagIdx, subProjectIdx, projectIdx } = this.activeActivityData;

        /**@type {TimeSheetActivity} */
        const activity = this.activeActivityData.activity

        TimeSheetActivitiesService.Delete(activity.id)
            .then(r => {
                console.log({ r })
                if (r) {

                    let filteredProjects = { ...this.filteredProjects }

                    let subProject = filteredProjects[projectIdx].tasks[subProjectIdx]

                    if (subProject.isActive) {
                        subProject.isActive = false

                        /**@type {Array<ActiveActivity>} */
                        let activeActivities = [...this.activeActivities]

                        let activeTagIdx = activeActivities.findIndex(k => k.activity.id === activity.id)

                        if (activeTagIdx > -1) {
                            activeActivities.splice(activeTagIdx, 1)

                            this.activeActivities = activeActivities
                        }

                    }





                    let activities = [...subProject.activities]

                    activities.splice(tagIdx, 1)

                    subProject.activities = activities

                    filteredProjects[projectIdx].tasks[subProjectIdx] = subProject

                    this.filteredProjects = filteredProjects

                    alert('Deleted');
                    Modals_TimeSheets.ActivityModal.Hide();
                }
                else {
                    activityModal.message = BASIC_ERROR_MESSAGE
                }
            })
            .catch(e => {

                activityModal.message = BASIC_INTERNAL_ERROR_MESSAGE
                console.error(e)
            })
            .then(() => {

                activityModal.isDeleting = false
                this.activityModal = activityModal
            })

        ///** @type {ActivityModalObject} */
        //let activityModal = { ...this.activityModal }

        //let data = [...this.projects.data];

        //let project = data[idx];

        //InventoryProjectsService.Delete(project.id).done((r) => {

        //    if (r) {
        //        console.log({ data, idx })
        //        data.splice(idx, 1);
        //        this.projects.data = data
        //        this.projects.message = 'Deleted!';
        //    }
        //    else {
        //        this.projects.message = BASIC_ERROR_MESSAGE;
        //    }

        //}).fail((e) => {

        //    console.error({ e })
        //    this.projects.message = BASIC_ERROR_MESSAGE;

        //}).always(() => {
        //    this.projects.isLoading = false
        //});
    },
    handleFileFocus: function () {

        let files = { ...this.activityModal.files };

        let fileName = this.activityModal.form.fileName

        files.isVisible = fileName && fileName.length > 0
        files.isFocused = true

        this.activityModal.files = files

    },
    handleFileBlur: function (e) {


        let targetClassName = e.explicitOriginalTarget &&
            e.explicitOriginalTarget.parentElement &&
            e.explicitOriginalTarget.parentElement.className


        if (targetClassName === undefined && e.target) {
            targetClassName = e.target.name
        }

        const hintListClasses = ['hint-list', 'hint-list__item', 'fileName']
        const isHintList = targetClassName ? hintListClasses.includes(targetClassName) : false

        if (!isHintList) {
            this.closeFilesHints()
        }

    },
    handleFileClick: function (file) {


        this.activityModal.form.fileId = file.id
        this.activityModal.form.fileName = file.name

        this.closeFilesHints()
    },
    closeFilesHints: function () {

        let files = { ...this.activityModal.files }

        files.isFocused = false
        //files.data = []
        files.isVisible = false

        this.activityModal.files = files
    },
    handleFileChange: function (e) {

        const { value } = e.target;
        console.log({ value });


        this.activityModal.form.fileId = null

        if (delayTimer) {
            clearTimeout(delayTimer);
        }

        let files = { ...this.activityModal.files }

        files.data = []
        files.hasResults = true
        files.isDirty = true

        if (!value) {
            this.closeFilesHints()
            return;
        }

        this.activityModal.files = files

        /** @type {TimeSheetProjectModelSubProject} */
        const activeSubProject = this.activeSubProject;

        //console.log({ activeSubProject })

        delayTimer = setTimeout(() => {

            this.activityModal.files.isVisible = true
            this.activityModal.files.areLoading = true;

            ProjectFilesService.Search(value, activeSubProject.parentId)
                .done(r => {
                    const data = r;
                    //console.log({ data });

                    files.data = data

                    files.hasResults = files.data.length > 0
                    this.activityModal.files = files

                })
                .fail(e => console.error(e))
                .always(() => {
                    files.areLoading = false
                })

        }, 500);
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
        typeOfWorkData: [],
        measurementUnitData: [],
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
            isLoading: true
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

            if (!id) {
                return ''
            }

            return toDate ? (isLoading ? 'Saving...' : 'Save') : (isLoading ? 'Commit...' : 'Commit')
        }
    },
    methods: {
        ...activityMethods,
        startActivity: function () {

            const task = this.selectedTask

            if (!task) {
                console.error('no task selected')
                return
            }

            if (this.activeTask) {
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

            /** @type {ActivityModalObject} */
            let activityModal = { ...this.activityModal }

            // set form
            activityModal.form = activityModalForm(activity)

            //activityModal.form.id = activity.id

            activityModal.isDeleting = false
            activityModal.isLoading = false
            activityModal.isLocked = false
            activityModal.message = ''
            activityModal.isDismissing = false

            this.activityModal = activityModal

            Modals_TimeSheets.ActivityModal.Show()
        },
        commitActivity: function () {

            //const task = this.activeTask
            const activity = this.activeActivity

            if (!activity) {
                console.error('no active activity')
                return
            }

            /** @type {ActivityModalObject} */
            let activityModal = { ...this.activityModal }

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

            const isUpdate = activityModal.form.toDate != null

            activityModal.isLoading = true

            if (isUpdate) {

                model = {
                    ...model,
                    id: activityModal.form.id,
                    fromDate: activityModal.form.fromDate,
                    toDate: activityModal.form.toDate
                }
            }
            else {
                model = {
                    ...model,
                    activityId: activityModal.form.id,
                }
            }
            if (isUpdate) {
                TimeSheetActivitiesService.Update(model)
                    .then(r => {

                        const record = r.data

                        if (!record) {
                            this.taskActivities.message = BASIC_ERROR_MESSAGE
                            return
                        }

                        //this.activeTask = null
                        //this.activeActivity = null

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
            else {
                TimeSheetActivitiesService.Stop(model)
                    .then(r => {

                        const record = r.data

                        if (!record) {
                            this.taskActivities.message = BASIC_ERROR_MESSAGE
                            return
                        }

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
        startActiveActivity: function (projectIndex, subProjectIndex, subProject) {

            console.log('starting activity', { projectIndex, subProjectIndex })

            let activeActivities = [...this.activeActivities]

            let activeActivityIdx = activeActivities.findIndex(k => k.projectIndex === projectIndex && k.subProjectIndex === subProjectIndex)

            if (activeActivityIdx > -1) {
                alert('Already Active')
                return;
            }

            // update filtered project : set > active
            let filteredProjects = { ...this.filteredProjects }

            filteredProjects[projectIndex].tasks[subProjectIndex].isActive = true

            //activityId
            console.log({ subProject })

            let fromDate = moment().format(dateTimeOptions.format)
            let timesheetProjectId = filteredProjects[projectIndex].tasks[subProjectIndex].id


            let activity = activityDataContractStartObject(timesheetProjectId, fromDate)

            TimeSheetActivitiesService.Save(activity)
                .then((r) => {
                    if (r && r.data) {


                        const activeTagIndex = filteredProjects[projectIndex].tasks[subProjectIndex].activities.length
                        const activeActivityParsed = getActiveActivity(r.data, activeTagIndex, projectIndex, subProjectIndex)

                        // add active activity
                        activeActivities.push(activeActivityParsed)

                        filteredProjects[projectIndex].tasks[subProjectIndex].activities.push(r.data)
                        //let activeActivityIdx = this.activeActivities.findIndex(k => k.projectIndex === projectIndex && k.subProjectIndex === subProjectIndex)
                    }
                    else {
                        alert(BASIC_ERROR_MESSAGE)
                    }
                })
                .catch((e) => {
                    console.log(e)
                    alert(BASIC_INTERNAL_ERROR_MESSAGE)
                })
                .then(() => {
                    this.activeActivities = activeActivities
                    this.filteredProjects = filteredProjects
                });

            // set app data
        },
        endActiveActivity: function (projectIndex, subProjectIndex, subProject) {

            let activeActivities = [...this.activeActivities]

            projectIndex = parseInt(projectIndex)
            subProjectIndex = parseInt(subProjectIndex)

            let activeActivityIdx = activeActivities.findIndex(k => k.projectIndex === projectIndex && k.subProjectIndex === subProjectIndex)

            if (activeActivityIdx === -1) {
                alert('Project Did Not Start')
                return;
            }

            /** @type {ActiveActivity} */
            let activity = activeActivities[activeActivityIdx]

            //const endActivityData = { projectIndex, subProjectIndex, subProject }
            console.log({ activity })


            activity.activity.toDate = moment().format(dateTimeOptions.format);

            this.openEditActivityModal(activity.activity, activity.tagIndex, activity.subProjectIndex, activity.projectIndex, true)

            //this.openNewActivityModal(activity.fromDate, activity.timesheetProjectId, endActivityData)
            this.activeSubProject = subProject
            //activeActivities.splice(activeActivityIdx, 1)
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
            const itemClass = `task-filter--${status.code}`

            return {
                [itemClass]: true,
                'active': this.tasksFilter.selectedStatusKey === status.key
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

            this.selectedTask = { ...task }

            TimeSheetsService.GetActivitiesByDate(activeTimeSheet.id, task.id, date)
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
    },
    mounted: function () {

        var url_string = window.location.href
        var url = new URL(url_string);

        var userId = $('#UserTimeSheets').attr('data-user');
        var activeRole = $('#UserTimeSheets').attr('data-active-role');

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

        const timeSheetId = url.searchParams.get("timeSheetId");

        TimeSheetsService.GetUserTimeSheets(userId)
            .then((r) => {
                //this.readOnly = true
                this.timesheets = r.data

                for (var i = 0; i < this.timesheets.length; i++) {
                    const timesheet = this.timesheets[i]

                    if (timesheet.id == timeSheetId) {
                        this.openTimeSheet(timesheet)
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