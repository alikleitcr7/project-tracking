Vue.component('date-picker', VueBootstrapDatetimePicker);

var projectComponent = Vue.extend({
    props: ['records'],
    template: `
          <div class="group-content">

              <template v-for="project in records" v-if="!records.isLoading && project.tasks.length">
                  <div class="section-item" v-show="!project.isHidden">
                      <div class="item-container">
         
                          <p class="flex-right-left">
                              <span class="left-side text-bold">{{project.id}}. {{project.title}}</span>
                              <span class="right-side small-date">{{project.displayDate}}</span>
                          </p>
         
                          <small>{{project.description}}</small>
                      </div>
         
                      <div class="item-child-items">
                          <small v-show="!project.tasks.length">project does not contain any tasks</small>
                          <table class="shared-child-table table" v-show="project.tasks.length">
                              <tr v-for="activity in project.tasks" v-show="!activity.isHidden" v-bind:class="{'activeActivity':activity.isLocked > 0}">
         
                                  <td style="width:40px">
         
                                      <input type="checkbox" :disabled="activity.isLocked" v-model="activity.isChecked" />
                                  </td>
                                  <td>
                                      <p class="flex-right-left">
                                          <span class="left-side "> {{activity.id}}. {{activity.title}}</span>
                                          <span class="right-side small-date">{{activity.displayDate}}</span>
                                      </p>
                                  </td>
                              </tr>
                          </table>
                      </div>
                  </div>
              </template>
         
          </div>
    `
})

Vue.component('projects', projectComponent)

const GetTimeSheetProjects = (timeSheetId) => {
    return axios.get('/Employees/GetTimeSheetProjects?timeSheetId=' + timeSheetId);
}

const GetTimeSheets = (userId, year) => {
    return axios.get(`/Employees/GetTimeSheets?userId=${userId}&year=${year}`);
}

const GetTimeSheetYears = (userId) => {
    return axios.get('/Employees/GetTimeSheetYears?userId=' + userId);
}

function filterProjects(val, filteredProjects) {

    val = val.toLowerCase();

    if (!val) {
        for (var i = 0; i < filteredProjects.length; i++) {
            filteredProjects[i].isHidden = false;
            for (var j = 0; j < filteredProjects[i].tasks.length; j++) {
                filteredProjects[i].tasks[j].isHidden = false;
            }
        }

        return;
    }

    for (var i = 0; i < filteredProjects.length; i++) {

        let project = filteredProjects[i];

        let showProject = project.title.toLowerCase().indexOf(val) > -1 || (project.description ? project.description.toLowerCase().indexOf(val) > -1 : false);

        // show project if some tasks met the search criteria
        project.isHidden = !project.tasks.some(k => k.title.toLowerCase().indexOf(val) > -1);

        // criteria met the parent but not the tasks
        // so discard the activity filtering 
        // and show all the tasks under the parent project
        if (showProject && project.isHidden) {

            project.isHidden = false;

            for (var j = 0; j < project.tasks.length; j++) {
                project.tasks[j].isHidden = false;
            }

            continue;
        }

        // some tasks met the criteria
        // filter tasks under the project
        if (!project.isHidden) {

            for (var j = 0; j < project.tasks.length; j++) {

                let activity = project.tasks[j];
                activity.isHidden = activity.title.toLowerCase().indexOf(val) == -1;
            }
        }
    }

}

function extractSelectedIdsFromProjects(projects) {

    let selectedIds = [];

    projects.forEach(p => {
        selectedIds.pushArray(p.tasks.filter(a => a.isChecked).map(a => a.id));
    });

    return selectedIds;
}

function getTimeSheetFormDates() {

    let currentDate = moment()

    currentDate = currentDate.set('date', 1)

    let prevDate = currentDate.clone().subtract(1, 'months')
    let nextDate = currentDate.clone().add(1, 'months')

    return [prevDate, currentDate, nextDate]
}

const userProfileData = {
    user: () => {
        return $('#Profile').attr('data-user')
    },
    currentUser: () => {
        return $('#Profile').attr('data-current-user')
    },
    team: () => {
        return $('#Profile').attr('data-team')
    }
}

const timeSheetFormObject = (obj) => {

    let record = {
        id: obj ? obj.id : null,
        userId: obj ? obj.userId : userProfileData.user(),
        fromDate: obj ? obj.fromDate : null,
        toDate: obj ? obj.toDate : null,
    }

    return {
        record,
        message: '',
        isLoading: false,
        isSaving: false,
    }
}

new Vue({
    el: '#Schedules',
    data: {
        isInAdministration: false,
        //timeSheetStatuses: [],

        timeSheetForm: timeSheetFormObject(),
        //timeSheetForm: {
        //    id: null,
        //    userId: userProfileData.user(),
        //    fromDate: '',
        //    toDate: '',
        //    timeSheetError: '',
        //    dateList: []
        //},
        dateOptions,
        dateList: getTimeSheetFormDates(),
        dateTimeOptions,
        timeSheets: [],
        timeSheetsLoading: true,
        projects: [],
        projectsLoading: true,
        userId: null,
        timeSheetProjects: {
            timeSheetId: null,
            availableProjects: [],
            assignedProjects: [],
            isLoading: true,
            title: '',
            isFailed: false
        },
        teamId: null,
        searchKeys: {
            timeSheets: ''
        },
        availableProjectsChecked: true,
        assignedProjectsChecked: true,
        availableProjectsSearchKey: '',
        assignedProjectsSearchKey: '',
        assignProjectsLoading: false,
        currentUserId: null,
        timesheetDeleteModal: {
            activeIdx: null,
            message: '',
            isDeleting: false,
        },
        timeSheetYears: [],
        selectedTimeSheetYear: new Date().getFullYear()
    },
    computed: {
        isCurrentUser: function () {

            const currentUserId = this.currentUserId
            const userId = this.userId

            console.log({currentUserId,userId})
            return currentUserId && userId && currentUserId === userId
        }
    },
    watch: {
        availableProjectsSearchKey: {
            handler: function (val) {
                let filteredProjects = [...this.timeSheetProjects.availableProjects];

                filterProjects(val, filteredProjects);

                this.timeSheetProjects.availableProjects = filteredProjects;
            }
        },
        assignedProjectsSearchKey: {
            handler: function (val) {

                let filteredProjects = [...this.timeSheetProjects.assignedProjects];

                filterProjects(val, filteredProjects);

                this.timeSheetProjects.assignedProjects = filteredProjects;
            }
        },
        timeSheetProjects: {
            handler: function (val, oldVal) {

                //console.log('a thing changed', val, oldVal);

                //this.availableProjectsChecked = ;
                //this.assignedProjectsChecked = ;

                this.availableProjectsChecked = val.availableProjects.filter(k => k.tasks.filter(a => a.isChecked).length).length > 0;
                this.assignedProjectsChecked = val.assignedProjects.filter(k => k.tasks.filter(a => a.isChecked).length).length > 0;
            },
            deep: true
        }
    },
    methods: {
        saveTimeSheet: function () {

            this.timeSheetForm.message = ''

            const pendingRecord = this.timeSheetForm.record

            const { fromDate, toDate } = pendingRecord

            if (toDate == '' || fromDate == '') {
                this.timeSheetForm.message = 'from/to dates are required';
                return;
            }

            if (moment(toDate) <= moment(fromDate)) {
                this.timeSheetForm.message = '"From Date" should be before "To Date"';
                return
            }

            const isNew = pendingRecord.id === null
            this.timeSheetForm.isSaving = true

            TimeSheetsService.Save(pendingRecord)
                .then(response => {

                    const record = response.data;

                    if (!record) {
                        this.timeSheetForm.message = BASIC_ERROR_MESSAGE;
                    }
                    else {

                        this.timeSheetForm.message = isNew ? 'TimeSheet Added!' : 'TimeSheet Updated!';

                        if (isNew) {
                            // append to view
                            this.timeSheets = [record, ...this.timeSheets]
                        }
                        else {

                            // update view
                            let timeSheets = [...this.timeSheets]

                            const idx = timeSheets.findIndex(k => k.id === record.id)

                            if (idx !== -1) {

                                timeSheets[idx] = { ...record }
                                this.timeSheets = timeSheets;
                            }
                            else {
                                location.reload()
                            }
                        }
                    }
                })
                .catch(e => {
                    this.timeSheetForm.message = getAxiosErrorMessage(e)
                })
                .then(() => {
                    this.timeSheetForm.isSaving = false
                })

        },
        newTimeSheet: function () {

            this.timeSheetForm = timeSheetFormObject();

            $('#SaveTimeSheetModal').modal('show')
        },
        editTimeSheet: function (timesheet) {


            this.timeSheetForm.isLoading = true
            this.timeSheetForm.message = 'Loading...'

            $('#SaveTimeSheetModal').modal('show')

            TimeSheetsService.GetById(timesheet.id)
                .then(r => {

                    const record = r.data

                    if (!record) {
                        this.timeSheetForm.message = BASIC_ERROR_MESSAGE;
                        return
                    }

                    this.timeSheetForm.message = ''

                    this.timeSheetForm = timeSheetFormObject(record);
                })
                .catch(e => {

                    console.error('get timesheet', e)

                    this.timeSheetForm.message = getAxiosErrorMessage(e)
                })
                .then(() => {
                    this.timeSheetForm.isLoading = false
                })
        },
        openTimeSheetProjectsModal: function (timeSheet) {

            const timeSheetId = timeSheet.id;

            let timeSheetProjects = { ...this.timeSheetProjects };

            this.availableProjectsSearchKey = ''
            this.assignedProjectsSearchKey = ''

            timeSheetProjects.title = timeSheet.fromDateDisplay + ' - ' + timeSheet.toDateDisplay;
            timeSheetProjects.timeSheetId = timeSheetId;
            timeSheetProjects.isLoading = true;
            timeSheetProjects.isFailed = false;

            this.timeSheetProjects = timeSheetProjects;

            let projects = [...this.projects];

            TimeSheetsService.TimeSheetTasksWithActivityCheck(timeSheetId)
                .then(r => {

                    timeSheetProjects.isFailed = false;

                    console.log('timesheet projects data', r.data)

                    const ids = r.data.map(k => k.taskId);
                    const locked_ids = r.data.filter(k => k.hasActivity).map(k => k.taskId);

                    console.log('available ids', ids);
                    console.log('locked ids', locked_ids);

                    let availableProjects = [];
                    let assignedProjects = [];

                    for (var i = 0; i < projects.length; i++) {

                        let project = projects[i];

                        if (!project.tasks.length) {

                            // project has no task
                            // put it out there to let the supervisor know
                            availableProjects.push(project)
                            continue;
                        }

                        const availableTasks = project.tasks.filter(k => !ids.includes(k.id));
                        const assignedTasks = project.tasks.filter(k => ids.includes(k.id));

                        let availableProject = { ...project };
                        availableProject.tasks = availableTasks;


                        for (var j = 0; j < availableProject.tasks.length; j++) {
                            availableProject.tasks[j].isLocked = false;
                        }

                        let assignedProject = { ...project };

                        // check locked tasks

                        assignedProject.tasks = assignedTasks;

                        // lock active tasks
                        for (var j = 0; j < assignedProject.tasks.length; j++) {
                            assignedProject.tasks[j].isLocked = locked_ids.includes(assignedProject.tasks[j].id);
                        }

                        // push the arrays
                        availableProjects.push(availableProject)
                        assignedProjects.push(assignedProject)
                    }


                    timeSheetProjects.availableProjects = availableProjects;
                    timeSheetProjects.assignedProjects = assignedProjects;

                    this.timeSheetProjects = timeSheetProjects;

                    //console.log('e projects', this.projects);
                    //console.log('u projects', projects);

                    //this.projects = projects;
                })
                .catch(e => {

                    timeSheetProjects.isFailed = true;
                    console.error('timesheet projects error', e)
                })
                .then(() => {
                    timeSheetProjects.isLoading = false;
                })

            $('#timeSheetProjectsModal').modal('show');
        },
        assignProjects: function () {

            let availableProjects = [...this.timeSheetProjects.availableProjects];
            let assignedProjects = [...this.timeSheetProjects.assignedProjects]

            let projectIds = extractSelectedIdsFromProjects(this.timeSheetProjects.availableProjects);
            let timeSheetId = this.timeSheetProjects.timeSheetId;

            this.assignProjectsLoading = true;

            TimeSheetsService.AddProjectToTimeSheet({ timeSheetId, projectIds })
                .then(r => {

                    console.log('add project to timesheet response', r.data);

                    if (r.data) {

                        for (var i = 0; i < availableProjects.length; i++) {

                            let project = availableProjects[i];

                            for (var j = 0; j < project.tasks.length; j++) {

                                let activity = { ...project.tasks[j] };

                                if (activity.isChecked) {

                                    // uncheck activity
                                    activity.isChecked = false;

                                    // check if activity's parent project exist 
                                    // in the assigned projects (right side)
                                    let assignedProject = assignedProjects.find(k => k.id === project.id);

                                    if (assignedProject) {
                                        // push activity under existing project's tasks
                                        // on the right side
                                        assignedProject.tasks.push(activity)
                                    }
                                    else {

                                        // clone the project and reset it's tasks
                                        assignedProject = { ...project };
                                        project.tasks = [];

                                        // push activity to cloned project
                                        project.tasks.push(activity);

                                        // push cloned project to the right side 
                                        assignedProjects.push(assignedProject);
                                    }

                                }
                            }
                        }

                        // filter left array (available projects)
                        // remove checked tasks under each project

                        availableProjects = availableProjects.filter(p => {

                            p.tasks = p.tasks.filter(a => !a.isChecked);

                            return p;
                        });

                        this.timeSheetProjects.availableProjects = availableProjects;
                        this.timeSheetProjects.assignedProjects = assignedProjects;

                    }
                    else {
                        alert('something went wrong, please try again')
                    }
                })
                .catch(e => {

                    alert('something went wrong, please try again')

                    console.error('assign error', e)
                })
                .then(() => {
                    this.assignProjectsLoading = false;
                });
        },
        removeProjects: function () {

            let availableProjects = [...this.timeSheetProjects.availableProjects];
            let assignedProjects = [...this.timeSheetProjects.assignedProjects]

            let projectIds = extractSelectedIdsFromProjects(this.timeSheetProjects.assignedProjects);
            let timeSheetId = this.timeSheetProjects.timeSheetId;

            this.assignProjectsLoading = true;



            TimeSheetsService.RemoveProjectFromTimeSheet({ timeSheetId, projectIds })
                .then(r => {
                    if (r.data) {



                        for (var i = 0; i < assignedProjects.length; i++) {

                            let project = assignedProjects[i];

                            for (var j = 0; j < project.tasks.length; j++) {

                                let activity = { ...project.tasks[j] };

                                if (activity.isChecked) {

                                    // uncheck activity
                                    activity.isChecked = false;

                                    // check if activity's parent project exist 
                                    // in the assigned projects (right side)
                                    let availableProject = availableProjects.find(k => k.id === project.id);

                                    if (availableProject) {
                                        // push activity under existing project's tasks
                                        // on the right side
                                        availableProject.tasks.push(activity)
                                    }
                                    else {

                                        // clone the project and reset it's tasks
                                        availableProject = { ...project };
                                        project.tasks = [];

                                        // push activity to cloned project
                                        project.tasks.push(activity);

                                        // push cloned project to the right side 
                                        availableProjects.push(assignedProject);
                                    }

                                }
                            }
                        }

                        // filter left array (available projects)
                        // remove checked tasks under each project

                        assignedProjects = assignedProjects.filter(p => {

                            p.tasks = p.tasks.filter(a => !a.isChecked);

                            return p;
                        });

                        this.timeSheetProjects.availableProjects = availableProjects;
                        this.timeSheetProjects.assignedProjects = assignedProjects;


                    }
                    else {

                        alert('something went wrong, please try again')

                    }
                })
                .catch(e => {

                    alert('something went wrong, please try again')

                    console.error('assign error', e)
                })
                .then(() => {
                    this.assignProjectsLoading = false;
                })

            this.timeSheetProjects.availableProjects = availableProjects;
            this.timeSheetProjects.assignedProjects = assignedProjects;
        },
        getLatestTimeSheet: function () {
            TimeSheetsService.GetLatest(this.userId).then(response => {

                const { data } = response;

                if (data != null) {
                    this.timeSheetForm.record.fromDate = data.toDatePlusOneDay
                    this.timeSheetForm.record.toDate = data.toDatePlusMonth
                }

                console.log('latest timesheet', response);

            }).catch(error => {
                console.log(error)
            });
        },
        changeDate: function (date) {
            console.log({ date })

            let oDate = moment(date)
            this.timeSheetForm.record.fromDate = oDate
            this.timeSheetForm.record.toDate = oDate.clone().add(1, 'months').subtract(1, 'day')

        },
        deleteTimeSheet: function (timesheet, idx) {

            if (!confirm('Confirm Deletion')) {
                return;
            }

            this.timesheetDeleteModal.isDeleting = true
            this.timesheetDeleteModal.message = ''

            const id = timesheet.id

            TimeSheetsService.Delete(id)
                .then(r => {

                    const record = r.data

                    if (record) {

                        let timeSheets = [...this.timeSheets]

                        timeSheets.splice(idx, 1)

                        this.timeSheets = timeSheets

                        $('#DeleteTimeSheetModal').modal('hide')

                        alert('Timesheet Deleted!')
                    }
                    else {
                        this.timesheetDeleteModal.message = BASIC_ERROR_MESSAGE
                    }

                })
                .catch(e => {
                    this.timesheetDeleteModal.message = getAxiosErrorMessage(e)
                })
                .then(() => {
                    this.timesheetDeleteModal.isDeleting = false

                })
        },
        filterTimeSheets: function () {

            TimeSheetsService.GetTimeSheets(this.userId, this.selectedTimeSheetYear, false)
                .then(response => {
                    console.log('response timesheets', response.data);
                    this.timeSheets = response.data;
                })
                .catch(e => {
                    console.error('err', e);
                })
                .then(() => {
                    this.timeSheetsLoading = false;
                });
        },
        nextDateDisplay: function (date) {

            return date.clone().add(1, 'months').format('MMM')
        }
    },
    mounted: function () {

        this.userId = userProfileData.user();
        this.currentUserId = userProfileData.currentUser();
        this.teamId = userProfileData.team() ? parseInt(userProfileData.team()) : null;

        console.log('uid', this.userId)

        //this.getLatestTimeSheet();

        ProjectsService.GetByTeam(this.teamId)
            .then(r => {
                console.log('projects', r.data)
                this.projects = r.data;
            })
            .catch(e => {
                console.error('projects', e)
            })
            .then(() => {
                this.projectsLoading = false;
            })

        TimeSheetsService.GetTimeSheetYears(this.userId)
            .then(r => {
                console.log('get timesheet years', r)
                this.timeSheetYears = r.data
            })
            .catch(e => {
                console.error('error', e)
            })


        this.filterTimeSheets()
    }
});

