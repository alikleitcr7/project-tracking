
// define
Vue.component('date-picker', VueBootstrapDatetimePicker);


const datetimeOptions = {
    format: 'YYYY-MM-DD HH:mm:ss',
}

var projectComponent = Vue.extend({
    props: ['records'],
    template: `
          <div class="group-content">

              <template v-for="project in records" v-if="!records.isLoading && project.activities.length">
                  <div class="section-item" v-show="!project.isHidden">
                      <div class="item-container">
         
                          <p class="flex-right-left">
                              <span class="left-side text-bold">{{project.id}}. {{project.title}}</span>
                              <span class="right-side small-date">{{project.displayDate}}</span>
                          </p>
         
                          <small>{{project.description}}</small>
                      </div>
         
                      <div class="item-child-items">
                          <small v-show="!project.activities.length">project does not contain any activities</small>
                          <table class="shared-child-table table" v-show="project.activities.length">
                              <tr v-for="activity in project.activities" v-show="!activity.isHidden" v-bind:class="{'activeActivity':activity.isLocked > 0}">
         
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

var supervisingComponent = Vue.extend({
    props: ['users', 'ismanager']
    , template: `
                 <div class="section-item" v-if="users">
                            <div class="item-child-items">
                                <small v-show="!users.length">No Records</small>
                                <table class="shared-child-table table" v-show="users.length">
                                    <tr v-for="notsupervising in users">
                                        <td style="width:40px"  v-show='ismanager'>
                                            <input type="checkbox" v-model="notsupervising.isChecked" />
                                        </td>
                                        <td>
                                                <p class="flex-right-left">
                                                <span class="left-side ">  {{notsupervising.fullName}} ({{notsupervising.email}})</span>
                                            </p>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
`
});


var RolesComponent = Vue.extend({
    props: ['roles', 'ismanager']
    , template: `
                 <div class="section-item" v-if="roles">
                            <div class="item-child-items">
                                <small v-show="!roles.length">No roles</small>
                                <table class="shared-child-table table" v-show="roles.length">
                                    <tr v-for="role in roles">
                                        <td style="width:40px" v-show='ismanager'>
                                            <input type="checkbox" v-model="role.isChecked" />
                                        </td>
                                        <td>
                                                <p class="flex-right-left">
                                                <span class="left-side ">  {{role.name}}</span>
                                            </p>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
`
});

Vue.component('rolesManagement', RolesComponent);
Vue.component('subUsers', supervisingComponent);
Vue.component('projects', projectComponent)

// services

const GetProjectsByDepartment = (departmentId) => {
    return axios.get('/Employees/?departmentId=' + departmentId);
}

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

const GetSuperVised = (userId) => {
    return axios.get(`/Employees/GetSuperVising?userId=${userId}`);
}

const GetUserRoles = (userId) => {
    return axios.get(`/Employees/GetRoles?userId=${userId}`);
}

const GetSuperVisors = (userId) => {
    return axios.get(`/Employees/GetSuperVisors?userId=${userId}`);
}

function FilterProjects(val, filteredProjects) {

    val = val.toLowerCase();

    if (!val) {
        for (var i = 0; i < filteredProjects.length; i++) {
            filteredProjects[i].isHidden = false;
            for (var j = 0; j < filteredProjects[i].activities.length; j++) {
                filteredProjects[i].activities[j].isHidden = false;
            }
        }

        return;
    }

    for (var i = 0; i < filteredProjects.length; i++) {

        let project = filteredProjects[i];

        let showProject = project.title.toLowerCase().indexOf(val) > -1 || project.description.toLowerCase().indexOf(val) > -1;

        // show project if some activities met the search criteria
        project.isHidden = !project.activities.some(k => k.title.toLowerCase().indexOf(val) > -1);

        // criteria met the parent but not the activities
        // so discard the activity filtering 
        // and show all the activities under the parent project
        if (showProject && project.isHidden) {

            project.isHidden = false;

            for (var j = 0; j < project.activities.length; j++) {
                project.activities[j].isHidden = false;
            }

            continue;
        }

        // some activities met the criteria
        // filter activities under the project
        if (!project.isHidden) {

            for (var j = 0; j < project.activities.length; j++) {

                let activity = project.activities[j];
                activity.isHidden = activity.title.toLowerCase().indexOf(val) == -1;
            }
        }
    }

}

function ExtractSelectedIdsFromProjects(projects) {

    let selectedIds = [];

    projects.forEach(p => {
        selectedIds.pushArray(p.activities.filter(a => a.isChecked).map(a => a.id));
    });

    return selectedIds;
}

new Vue({
    el: '#UserProfile',
    data: {
        isInAdministration: false,
        timeSheetStatuses: [],
        timesheetModel: {
            userId: $('input[name=userId]').val(),
            fromDate: '',
            toDate: '',
            timeSheetError: '',
            dateList: []
        },
        dateOptions,
        datetimeOptions,
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
        departmentId: null,
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
        supervisingUsersChecked: function () {
            return this.superVised.filter(k => k.isChecked).length > 0;
        },
        notSupervisingUsersChecked: function () {
            return this.notSuperVised.filter(k => k.isChecked).length > 0;
        },
        supervisorUsersChecked: function () {
            return this.superVisors.filter(k => k.isChecked).length > 0;
        },
        notSupervisorUsersChecked: function () {
            return this.notSuperVisors.filter(k => k.isChecked).length > 0;
        },
        rolesAssignedChecked: function () {
            return this.rolesAssigned.filter(k => k.isChecked).length > 0;
        },
        rolesNotAssignedChecked: function () {
            return this.rolesNotAssigned.filter(k => k.isChecked).length > 0;
        },
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

                this.availableProjectsChecked = val.availableProjects.filter(k => k.activities.filter(a => a.isChecked).length).length > 0;
                this.assignedProjectsChecked = val.assignedProjects.filter(k => k.activities.filter(a => a.isChecked).length).length > 0;
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

                    const ids = r.data.map(k => k.projectId);
                    const locked_ids = r.data.filter(k => k.activities.length > 0).map(k => k.projectId);


                    console.log('available ids', ids);
                    console.log('locked ids', locked_ids);

                    let availableProjects = [];
                    let assignedProjects = [];

                    for (var i = 0; i < projects.length; i++) {

                        let project = projects[i];

                        if (!project.activities.length) {

                            availableProjects.push(project)
                            continue;
                        }

                        const availableActivities = project.activities.filter(k => !ids.includes(k.id));
                        const assignedActivities = project.activities.filter(k => ids.includes(k.id));

                        let availableProject = { ...project };
                        availableProject.activities = availableActivities;


                        for (var j = 0; j < availableProject.activities.length; j++) {
                            availableProject.activities[j].isLocked = false;
                        }

                        let assignedProject = { ...project };

                        // check locked activities

                        assignedProject.activities = assignedActivities;

                        for (var j = 0; j < assignedProject.activities.length; j++) {
                            assignedProject.activities[j].isLocked = locked_ids.includes(assignedProject.activities[j].id);
                        }

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
        assignAsSuperving: function () {

            let listOfIdsToAssign = this.notSuperVised.filter(c => c.isChecked).map(s => s.id);

            axios.post('/Employees/AddSupervising', { 'userId': this.userId, 'SuperViseIds': listOfIdsToAssign })
                .then(response => {
                    let vrnotSuperVised = [...this.notSuperVised];
                    let vrsuperVised = [...this.superVised];
                    let newSupervising = vrnotSuperVised.filter(k => listOfIdsToAssign.includes(k.id));

                    vrnotSuperVised = vrnotSuperVised.filter(k => !listOfIdsToAssign.includes(k.id));

                    newSupervising.forEach(user => {
                        user.isChecked = false;
                        vrsuperVised.push(user);
                    })
                    this.notSuperVised = vrnotSuperVised;
                    this.superVised = vrsuperVised;
                }).catch(response => {
                });

        },
        RemoveSuperVised: function () {
            var listOfIdsToRemoveAssign = this.superVised.filter(c => c.isChecked).map(s => s.id);
            axios.post('/Employees/RemoveSuperVised', { 'userId': this.userId, 'SuperViseIds': listOfIdsToRemoveAssign })
                .then(response => {
                    let vrNotSuperVised = [...this.notSuperVised];
                    let vrSuperVised = [...this.superVised];
                    let vrNewNotSuppervising = vrSuperVised.filter(k => listOfIdsToRemoveAssign.includes(k.id));

                    vrSuperVised = vrSuperVised.filter(k => !listOfIdsToRemoveAssign.includes(k.id));
                    //let newNotSupervising= ne
                    vrNewNotSuppervising.forEach(user => {
                        user.isChecked = false;
                        vrNotSuperVised.push(user);
                    });


                    this.notSuperVised = vrNotSuperVised;
                    this.superVised = vrSuperVised;

                    console.log('vrNotSuperVised', vrNotSuperVised)
                    console.log('vrSuperVised', vrSuperVised)
                    console.log('vrNewNotSuppervising', vrNewNotSuppervising);
                }).catch(response => {
                });
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

                            for (var j = 0; j < project.activities.length; j++) {

                                let activity = { ...project.activities[j] };

                                if (activity.isChecked) {

                                    // uncheck activity
                                    activity.isChecked = false;

                                    // check if activity's parent project exist 
                                    // in the assigned projects (right side)
                                    let assignedProject = assignedProjects.find(k => k.id === project.id);

                                    if (assignedProject) {
                                        // push activity under existing project's activities
                                        // on the right side
                                        assignedProject.activities.push(activity)
                                    }
                                    else {

                                        // clone the project and reset it's activities
                                        assignedProject = { ...project };
                                        project.activities = [];

                                        // push activity to cloned project
                                        project.activities.push(activity);

                                        // push cloned project to the right side 
                                        assignedProjects.push(assignedProject);
                                    }

                                }
                            }
                        }

                        // filter left array (available projects)
                        // remove checked activities under each project

                        availableProjects = availableProjects.filter(p => {

                            p.activities = p.activities.filter(a => !a.isChecked);

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

                            for (var j = 0; j < project.activities.length; j++) {

                                let activity = { ...project.activities[j] };

                                if (activity.isChecked) {

                                    // uncheck activity
                                    activity.isChecked = false;

                                    // check if activity's parent project exist 
                                    // in the assigned projects (right side)
                                    let availableProject = availableProjects.find(k => k.id === project.id);

                                    if (availableProject) {
                                        // push activity under existing project's activities
                                        // on the right side
                                        availableProject.activities.push(activity)
                                    }
                                    else {

                                        // clone the project and reset it's activities
                                        availableProject = { ...project };
                                        project.activities = [];

                                        // push activity to cloned project
                                        project.activities.push(activity);

                                        // push cloned project to the right side 
                                        availableProjects.push(assignedProject);
                                    }

                                }
                            }
                        }

                        // filter left array (available projects)
                        // remove checked activities under each project

                        assignedProjects = assignedProjects.filter(p => {

                            p.activities = p.activities.filter(a => !a.isChecked);

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
        assignAsSupervors: function () {
            /* superVisors: [],
        notSuperVisors: [], */
            let listOfIdsToAssign = this.notSuperVisors.filter(c => c.isChecked).map(s => s.id);

            axios.post('/Employees/AddSupervisors', { 'userId': this.userId, 'SuperViseIds': listOfIdsToAssign })
                .then(response => {
                    let vrnotSuperVisors = [...this.notSuperVisors];
                    let vrsuperVisors = [...this.superVisors];
                    let newSupervisors = vrnotSuperVisors.filter(k => listOfIdsToAssign.includes(k.id));

                    vrnotSuperVisors = vrnotSuperVisors.filter(k => !listOfIdsToAssign.includes(k.id));

                    newSupervisors.forEach(user => {
                        user.isChecked = false;
                        vrsuperVisors.push(user);
                    })
                    this.notSuperVisors = vrnotSuperVisors;
                    this.superVisors = vrsuperVisors;
                }).catch(response => {
                });

        },
        RemoveSuperVisors: function () {
            /*   superVisors: [],
        notSuperVisors: [], */
            var listOfIdsToRemoveAssign = this.superVisors.filter(c => c.isChecked).map(s => s.id);
            axios.post('/Employees/RemoveSuperVisors', { 'userId': this.userId, 'SuperViseIds': listOfIdsToRemoveAssign })
                .then(response => {
                    let vrnotSuperVisors = [...this.notSuperVisors];
                    let vrsuperVisors = [...this.superVisors];
                    let vrNewNotsuperVisors = vrsuperVisors.filter(k => listOfIdsToRemoveAssign.includes(k.id));

                    vrsuperVisors = vrsuperVisors.filter(k => !listOfIdsToRemoveAssign.includes(k.id));
                    //let newNotSupervising= ne
                    vrNewNotsuperVisors.forEach(user => {
                        user.isChecked = false;
                        vrnotSuperVisors.push(user);
                    });

                    this.notSuperVisors = vrnotSuperVisors;
                    this.superVisors = vrsuperVisors;

                    console.log('vrNotSuperVised', notSuperVisors)
                    console.log('vrSuperVised', superVisors)
                    console.log('vrNewNotSuppervising', vrNewNotsuperVisors);
                }).catch(response => {
                });
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
        assignRoles: function () {
            /*  rolesAssigned:[],
        rolesNotAssigned:[], */
            let listOfIdsToAssign = this.rolesNotAssigned.filter(c => c.isChecked).map(s => s.id);

            axios.post('/Employees/AddRolesToUser', { 'userId': this.userId, 'SuperViseIds': listOfIdsToAssign })
                .then(response => {
                    let vrrolesNotAssigned = [...this.rolesNotAssigned];
                    let vrrolesAssigned = [...this.rolesAssigned];
                    let newRolesToAssigne = vrrolesNotAssigned.filter(k => listOfIdsToAssign.includes(k.id));

                    vrrolesNotAssigned = vrrolesNotAssigned.filter(k => !listOfIdsToAssign.includes(k.id));

                    newRolesToAssigne.forEach(role => {
                        role.isChecked = false;
                        vrrolesAssigned.push(role);
                    })
                    this.rolesNotAssigned = vrrolesNotAssigned;
                    this.rolesAssigned = vrrolesAssigned;
                }).catch(response => {
                });

        },
        RemoveRoles: function () {
            /*rolesAssigned:[],
        rolesNotAssigned:[],, */
            var listOfIdsToRemoveAssign = this.rolesAssigned.filter(c => c.isChecked).map(s => s.id);
            axios.post('/Employees/RemoveRolesToUser', { 'userId': this.userId, 'SuperViseIds': listOfIdsToRemoveAssign })
                .then(response => {
                    let rolesNotAssigned = [...this.rolesNotAssigned];
                    let rolesAssigned = [...this.rolesAssigned];
                    let vrNewrolesNotAssigned = rolesAssigned.filter(k => listOfIdsToRemoveAssign.includes(k.id));

                    rolesAssigned = rolesAssigned.filter(k => !listOfIdsToRemoveAssign.includes(k.id));
                    //let newNotSupervising= ne
                    vrNewrolesNotAssigned.forEach(role => {
                        role.isChecked = false;
                        rolesNotAssigned.push(role);
                    });

                    this.rolesNotAssigned = rolesNotAssigned;
                    this.rolesAssigned = rolesAssigned;

                    console.log('vrNotSuperVised', rolesNotAssigned)
                    console.log('vrSuperVised', rolesAssigned)
                    console.log('vrNewNotSuppervising', vrNewrolesNotAssigned);
                }).catch(response => {
                });
        },
        setTimeSheetStatuses: function (timeSheetId) {

            this.timeSheetStatuses = this.timeSheets.find(c => c.id == timeSheetId).timeSheetStatuses;
            console.log('timesheetStatues', this.timeSheetStatuses);
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
        openSignTimeSheetModal: function (idx) {
            this.timesheetSignModal.activeIdx = idx
            $('#SignTimeSheetModal').modal('show')
        },
        signTimeSheet: function () {

            if (!confirm('Confirm Signing')) {
                return;
            }

            const password = this.timesheetSignModal.password
            this.timesheetSignModal.isSigning = true
            this.timesheetSignModal.message = ''

            UserProfileRequests.ValidateCurrentUserPassword(password).then(r => {
                console.log('pass validation', r.data)

                const timeSheetToSign = this.timeSheets[this.timesheetSignModal.activeIdx]
                console.log({ timeSheetToSign })

                if (r.data) {
                    TimeSheetsService.SignTimeSheet(timeSheetToSign.id).then(r => {

                        console.log('ts sign', r.data)

                        if (r.data) {

                            let timeSheets = [...this.timeSheets]

                            //timeSheets.splice(this.timesheetSignModal.activeIdx, 1)

                            timeSheets[this.timesheetSignModal.activeIdx].isSigned = true
                            this.timeSheets = timeSheets

                            $('#SignTimeSheetModal').modal('hide')

                            alert('Timesheet Signd!')
                        }
                        else {
                            this.timesheetSignModal.message = BASIC_ERROR_MESSAGE
                        }

                    })
                }
                else {
                    this.timesheetSignModal.message = 'Invalid Password'
                }

            }).catch(e => {
                console.error('pass validation', e)
                this.timesheetSignModal.message = BASIC_INTERNAL_ERROR_MESSAGE
            }).then(() => {
                this.timesheetSignModal.isSigning = false
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

        UserProfileRequests.GetCurrentUserId().then(r => {
            //console.log('GetCurrentUserId', r.data)
            this.currentUserId = r.data
        }).catch(e => console.error('get curent user', e))

        let userId = $('input[name=userId]').val();
        let departmentId = $('input[name=departmentId]').val();
        let isInAdministration = $('#isInAdministration').val() == "True";
        console.log({ isInAdministration });
        this.userId = userId;

        this.departmentId = departmentId;
        this.isInAdministration = isInAdministration;
        console.log('userid', userId);
        this.getLatestTimeSheet();

        GetProjectsByDepartment(0)
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


        GetTimeSheetYears(userId)
            .then(r => {
                console.log('get timesheet years', r)
                this.timeSheetYears = r.data
            })
            .catch(e => {
                console.error('error', e)
            })

        GetTimeSheets(userId)
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

        GetSuperVised(userId)
            .then(response => {
                console.log(response);

                const users = response.data.all; //arr(usr)
                const supervisedIds = response.data.superVise;  //arr(int)
                const notSupervising = users.filter(k => !supervisedIds.includes(k.id));
                const supervising = users.filter(k => supervisedIds.includes(k.id));
                console.log(users);
                console.log('not supervising', notSupervising)
                console.log('supervising', supervising)
                this.superVised = supervising;
                this.notSuperVised = notSupervising;
            })
            .catch(e => {
                console.error('supervised get', e)
            });

        GetSuperVisors(userId)
            .then(response => {
                console.log(response);

                const users = response.data.all; //arr(usr)
                const supervisorsIds = response.data.superVise;  //arr(int)
                const notSupervisors = users.filter(k => !supervisorsIds.includes(k.id));
                const supervisors = users.filter(k => supervisorsIds.includes(k.id));
                console.log(users);
                console.log('not supervising', notSupervisors)
                console.log('supervising', supervisors)
                this.superVisors = supervisors;
                this.notSuperVisors = notSupervisors;
            })
            .catch(e => {
                console.error('supervisors get', e)
            });

        GetUserRoles(userId)
            .then(response => {
                console.log(response);

                const allRoles = response.data.all; //arr(usr)
                const assignedRolesIds = response.data.rolesTakes;  //arr(int)
                const notAssignedRoles = allRoles.filter(k => !assignedRolesIds.includes(k.id));
                const assignedRoles = allRoles.filter(k => assignedRolesIds.includes(k.id));
                console.log(allRoles);
                console.log('not assigned roles', notAssignedRoles)
                console.log('assigned roles', assignedRoles)
                this.rolesAssigned = assignedRoles;
                this.rolesNotAssigned = notAssignedRoles;
            })
            .catch(e => {
                console.error('supervisors get', e)
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

