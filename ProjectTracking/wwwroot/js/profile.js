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

const AddProjectToTimeSheet = (timeSheetId, projectIds) => {
    return axios.post(`/Employees/AddProjectToTimeSheet`, { timeSheetId: timeSheetId, projectIds: projectIds });
}

const RemoveProjectFromTimeSheet = (timeSheetId, projectIds) => {
    return axios.post(`/Employees/RemoveProjectFromTimeSheet`, { timeSheetId: timeSheetId, projectIds: projectIds });
}

function FilterProjects(val, filteredProjects) {

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

        let showProject = project.title.toLowerCase().indexOf(val) > -1 || project.description.toLowerCase().indexOf(val) > -1;

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

function ExtractSelectedIdsFromProjects(projects) {

    let selectedIds = [];

    projects.forEach(p => {
        selectedIds.pushArray(p.tasks.filter(a => a.isChecked).map(a => a.id));
    });

    return selectedIds;
}

const userProfileData = {
    user: () => {
        return $('#Profile').attr('data-user')
    },
    team: () => {
        return $('#Profile').attr('data-team')
    }
}

new Vue({
    el: '#Profile',
    data: {
        isInAdministration: false,
        timeSheetStatuses: [],
        timesheetModel: {
            userId: userProfileData.user(),
            fromDate: '',
            toDate: '',
            timeSheetError: '',
            dateList: []
        },
        dateOptions,
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
        superVised: [],
        notSuperVised: [],
        superVisors: [],
        notSuperVisors: [],
        rolesAssigned: [],
        rolesNotAssigned: [],
        test: true,
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
        timesheetSignModal: {
            activeIdx: null,
            message: '',
            isSigning: false,
        },
        timeSheetYears: [],
        selectedTimeSheetYear: new Date().getFullYear()
    },
    computed: {
        isCurrentUser: function () {

            const currentUserId = this.currentUserId
            const userId = this.userId

            //console.log({currentUserId,userId})
            return this.currentUserId && this.userId && this.currentUserId === this.userId
        }
    },
    watch: {
        availableProjectsSearchKey: {
            handler: function (val) {
                let filteredProjects = [...this.timeSheetProjects.availableProjects];

                FilterProjects(val, filteredProjects);

                this.timeSheetProjects.availableProjects = filteredProjects;
            }
        },
        assignedProjectsSearchKey: {
            handler: function (val) {

                let filteredProjects = [...this.timeSheetProjects.assignedProjects];

                FilterProjects(val, filteredProjects);

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
        addTimeSheet: function () {

            this.timesheetModel.timeSheetError = ''

            if (moment(this.timesheetModel.toDate) <= moment(this.timesheetModel.fromDate)) {
                this.timesheetModel.timeSheetError = 'to date can not be less than or equal  from date';
                return
            }
            else if (this.timesheetModel.toDate == '' || this.timesheetModel.fromDate == '') {
                this.timesheetModel.timeSheetError = 'please insert correct dates ';
                return;
            }
            console.log('user id', this.timesheetModel.userId);

            UserProfileRequests.AddTimeSheet(this.timesheetModel)
                .then(response => {
                    console.log('add Time Sheet Response', response);

                    const { data } = response;
                    if (data.timeSheet == null) {

                        this.timesheetModel.timeSheetError = `incorrect information :${data.message} `;
                    }
                    else {

                        alert("A new timesheet has been added ");
                        location.reload();
                        this.timeSheets = [data.timeSheet, ...this.timeSheets]
                    }

                })
                .catch(error => { console.log(error) })

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

            GetTimeSheetProjects(timeSheetId)
                .then(r => {

                    timeSheetProjects.isFailed = false;

                    console.log('timesheet projects data', r.data)

                    const ids = r.data.map(k => k.projectTaskId);
                    const locked_ids = r.data.filter(k => k.activities.length > 0).map(k => k.projectTaskId);

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

            let projectIds = ExtractSelectedIdsFromProjects(this.timeSheetProjects.availableProjects);
            let timeSheetId = this.timeSheetProjects.timeSheetId;

            this.assignProjectsLoading = true;

            AddProjectToTimeSheet(timeSheetId, projectIds)
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

            let projectIds = ExtractSelectedIdsFromProjects(this.timeSheetProjects.assignedProjects);
            let timeSheetId = this.timeSheetProjects.timeSheetId;

            this.assignProjectsLoading = true;



            RemoveProjectFromTimeSheet(timeSheetId, projectIds)
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
            UserProfileRequests.GetLatestTimeSheet(this.userId).then(response => {
                const { data } = response;
                if (data != null) {
                    this.timesheetModel.fromDate = data.toDatePlusOneDay
                    this.timesheetModel.toDate = data.toDatePlusMonth
                }
                //ToDatePlusMonth
                console.log('latest timesheet', response);

            }).catch(error => { console.log(error) });
        },
        changeDate: function (date) {
            console.log({ date })

            let oDate = moment(date)
            this.timesheetModel.fromDate = oDate
            this.timesheetModel.toDate = oDate.clone().add(1, 'months').subtract(1, 'day')

        },
        openDeleteTimeSheetModal: function (idx) {
            this.timesheetDeleteModal.activeIdx = idx
            $('#DeleteTimeSheetModal').modal('show')
        },
        deleteTimeSheet: function () {


            if (!confirm('Confirm Deletion')) {
                return;
            }

            const password = this.timesheetDeleteModal.password
            this.timesheetDeleteModal.isDeleting = true
            this.timesheetDeleteModal.message = ''

            UserProfileRequests.ValidateCurrentUserPassword(password).then(r => {
                console.log('pass validation', r.data)

                const timeSheetToDelete = this.timeSheets[this.timesheetDeleteModal.activeIdx]
                console.log({ timeSheetToDelete })

                if (r.data) {
                    TimeSheetsService.Delete(timeSheetToDelete.id).then(r => {

                        console.log('ts delete', r.data)

                        if (r.data) {

                            let timeSheets = [...this.timeSheets]

                            timeSheets.splice(this.timesheetDeleteModal.activeIdx, 1)


                            this.timeSheets = timeSheets

                            $('#DeleteTimeSheetModal').modal('hide')

                            alert('Timesheet Deleted!')
                        }
                        else {
                            this.timesheetDeleteModal.message = BASIC_ERROR_MESSAGE
                        }

                    })
                }
                else {
                    this.timesheetDeleteModal.message = 'Invalid Password'
                }

            }).catch(e => {
                console.error('pass validation', e)
                this.timesheetDeleteModal.message = BASIC_INTERNAL_ERROR_MESSAGE
            }).then(() => {
                this.timesheetDeleteModal.isDeleting = false
            })

        },
        filterTimeSheets: function () {
            GetTimeSheets(this.userId, this.selectedTimeSheetYear)
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
        this.teamId = userProfileData.team() ? parseInt(userProfileData.team()) : null;

        this.getLatestTimeSheet();

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

        GetTimeSheetYears(this.userId)
            .then(r => {
                console.log('get timesheet years', r)
                this.timeSheetYears = r.data
            })
            .catch(e => {
                console.error('error', e)
            })

        GetTimeSheets(this.userId)
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

        let currentDate = moment()

        currentDate = currentDate.set('date', 26)
        let prevDate = moment().set('date', 26).subtract(1, 'months')
        let nextDate = moment().set('date', 26).add(1, 'months')

        let dateList = [prevDate, currentDate, nextDate]
        console.log({ date: dateList[0].format() })

        this.timesheetModel.dateList = dateList
    }
});

