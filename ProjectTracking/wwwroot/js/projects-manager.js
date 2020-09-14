const AddQuest = 'are you sure you want to Add this item?';
const EditQuest = 'are you sure you want to Edit this item?';
const deleteQuest = 'are you sure you want to delete this item?';
Vue.component('paginate', VuejsPaginate)
//app


Vue.component('file-object-modal', {
    data: function () {
        return {
            count: 0
        }
    },
    props: ['model', 'title', 'id'],
    template: `
    
    <div v-bind:id="id" class="modal " role="dialog">

        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">{{title}}</h4>
                </div>
                <div class="modal-body">

                    <div class="modal-controls">
                        <label>Title</label>

                        <div class="control-with-button">
                            <input type="text" class="form-control" v-model="model.name" />
                            <button type="button" class="Button-Shared" v-on:click="$emit('add-click')">Add</button>
                        </div>
                    </div>
                    <h4 class="display-message">{{model.message}}</h4>
                    <table class="table table-small-display  ">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Title</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="(item ,idx) in model.data">
                                <td>{{item.id}}</td>
                                <td>
                                    <span v-show="!item.isEdit">{{item.name}}</span>
                                    <input class="form-control" v-show="item.isEdit" type="text" v-model="item.name" />
                                </td>
                                <td>
                                    <button v-show="item.isEdit" class="btn  btn-sm btn-default" type="button" v-on:click="$emit('update-click',idx)">Save</button>
                                    <button v-show="item.isEdit" class="btn  btn-sm btn-default" type="button" v-on:click="$emit('cancel-click',idx)">Cancel</button>
                                    <button v-show="!item.isEdit" class="btn  btn-sm btn-default" type="button" v-on:click="$emit('edit-click',idx)">Edit</button>
                                    <button v-show="!item.isEdit" class="btn btn-sm btn-danger" type="button" v-on:click="$emit('delete-click',idx)">Delete</button>
                                    <a class="btn btn-sm btn-default" target="_blank" v-bind:href="'/projects/files?fileId=' +item.id">Track</a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
        
    `
})


const Modals = {

    ProjectFiles: {
        Show: () => {
            $('#ProjectFilesModal').modal('show')
        },
        Hide: () => {
            $('#ProjectFilesModal').hide('show')
        }
    },
}

var projectFilesMethods = {

    openProjectFilesModal: function (projectId) {

        this.ProjectIdOfProjectFile = projectId;
        ProjectFilesService.GetByProject(projectId).then(response => {
            this.projectFiles.data = response;
        })
        Modals.ProjectFiles.Show()
    },
    addProjectFile: function () {
        console.log('fired add')
        let { name } = { ...this.projectFiles };

        this.projectFiles.isLoading = true;

        ProjectFilesService.Create(name, this.ProjectIdOfProjectFile).done((r) => {

            this.projectFiles.data = [r, ...this.projectFiles.data]
            this.projectFiles.message = 'Added!';

        }).fail((e) => {

            this.projectFiles.message = BASIC_ERROR_MESSAGE;

        }).always(() => {

            this.projectFiles.name = ''
            this.projectFiles.isLoading = false;
        });
    },
    deleteProjectFile: function (idx) {

        if (!confirm('Confirm Delete')) {
            return;
        }

        let data = [...this.projectFiles.data];

        let projectFile = data[idx];

        ProjectFilesService.Delete(projectFile.id).done((r) => {

            if (r) {
                console.log({ data, idx })
                data.splice(idx, 1);
                this.projectFiles.data = data
                this.projectFiles.message = 'Deleted!';
            }
            else {
                this.projectFiles.message = BASIC_ERROR_MESSAGE;
            }

        }).fail((e) => {

            console.error({ e })
            this.projectFiles.message = BASIC_ERROR_MESSAGE;

        }).always(() => {
            this.projectFiles.isLoading = false
        });
    },
    updateProjectFile: function (idx) {

        if (!confirm('Confirm Save')) {
            return;
        }

        let data = [...this.projectFiles.data];

        let projectFile = data[idx];

        ProjectFilesService.Update(projectFile.id, projectFile.name)
            .done((r) => {

                if (r) {

                    data[idx].isEdit = false;
                    this.projectFiles.message = 'Saved!'

                    this.projectFiles.data = data;
                }
                else {

                    this.projectFiles.message = BASIC_ERROR_MESSAGE
                }

            }).fail((e) => {

                this.projectFiles.message = BASIC_ERROR_MESSAGE

                console.error('save projectFile', e)
            }).always(() => {

            });
    },
    editProjectFile: function (idx) {

        let data = [...this.projectFiles.data];

        for (var i = 0; i < data.length; i++) {
            data[i].isEdit = i === idx;

            if (i === idx) {
                this.projectFiles.backupEdit.name = data[i].name;
            }
        }

        this.projectFiles.data = data;
    },
    cancelProjectFileEdit: function (idx) {
        let data = [...this.projectFiles.data];

        for (var i = 0; i < data.length; i++) {
            data[i].isEdit = false;

            if (i === idx) {
                data[i].name = this.projectFiles.backupEdit.name;
            }
        }

        this.projectFiles.data = data;
    }
}


new Vue({
    el: '#ProjectManager',
    data: {
        projectFiles: basicModalObject(),
        dataPaging: {
            page: 0,
            totalPages: 0,
            length: 5,
            totalCount: 0,
            pagination: 'custom-pagination',
            prev: 'Prev',
            next: 'Next',
        },
        ProjectIdOfProjectFile: '',
        departments: [],
        companies: [],

        departmentId: 0,
        companyId: 0,

        projects: [], test: false,
        isLoading: true,
        project: {
            id: '',
            title: '',
            description: '',
            parentId: null,
            departmentId: '',
            companyId: '',

            activities: [],
            dateadded: '',
            isExpanded: false

        }, errors: [],
        activeParent: null
        , areExpandAll: false
        , projectsLoading: false
    },
    methods: {
        ...projectFilesMethods,
        clickCallback: function (pageNum) {
            GetProjects(this, pageNum - 1, this.dataPaging.length)
        },

        openAddModal: function (parent) {
            this.activeParent = parent
        },

        addProject: function () {
            AddProject(this);
        },
        deleteProject: function (id, project = null) {
            DeleteProject(id, this, project);
        },
        expandProject: function (project) {
            project.isExpanded = !project.isExpanded;
            this.test = !this.test;
        },
        editProject: function (project) {
            EditProject(project, this);
            this.test = !this.test;
            project.editMode.active = !project.editMode.active;
        },
        toggleEditMode: function (project) {
            this.test = !this.test;
            if (!project.editMode) {
                project.editMode = {};
            }
            project.editMode.active = !project.editMode.active;

        }
        , expanddAll: function () {
            this.areExpandAll = !this.areExpandAll;
            var appExpanAll = this.areExpandAll

            this.projects.forEach(function (project) {
                project.isExpanded = appExpanAll;

                this.test = !this.test;
            });
        }
    },
    watch: {
        dataPaging: {
            handler: function (newVal, oldVal) {

                this.dataPaging.totalPages = Math.ceil(newVal.totalCount / newVal.length);
            },
            deep: true
        },

        departmentId: {
            handler: function (newVal, oldVal) {
                this.projectsLoading = true;
                GetProjects(this, 0, this.dataPaging.length);
            }
        }
        ,
        companyId: {
            handler: function (newVal, oldVal) {
                if (this.departmentId == 0)
                    return;
                this.projectsLoading = true;
                GetProjects(this, 0, this.dataPaging.length);
            }
        }
    },
    mounted: function () {
        getDepartments(this);
        getCompanies(this);

    }
});

// Tool function
function addEditModeToProjects(projectsList) {
    projectsList.forEach(function (project) {
        project.editMode = {
            id: project.id,
            title: project.title,
            description: project.description,
            departmentId: project.departmentId,
            companyId: project.companyId
            , active: false
        }
        project.activities.forEach(function (activity) {
            activity.editMode = {
                id: activity.id,
                title: activity.title,
                description: activity.description,
                departmentId: activity.departmentId,
                companyId: activity.companyId
                , active: false
            }
        });
    });
    return projectsList;
}

function getDepartments(application) {
    axios.get('Projects/GetDepartments').then(
        function (response) {
            application.departments = response.data;
        }
    )
}
function getCompanies(application) {
    axios.get('Projects/GetCompanies').then(
        function (response) {
            console.log('companies', response.data);
            application.companies = response.data;
            application.isLoading = false;
        }
    )
}
//Project Crud Methods

function AddProject(app) {

    if (app.departmentId == 0 || app.companyId == 0) {
        alert('please chose a company and a department correctly');
        return;
    }

    if (!confirm(AddQuest)) {
        return;
    }

    let dataTransfer = {
        departmentId: app.departmentId,
        companyId: app.companyId,
        parentId: null,
        title: app.project.title,
        description: app.project.description,
    }

    if (app.activeParent) {
        dataTransfer.parentId = app.activeParent.id;
    }

    axios.post('Projects/Add', dataTransfer)
        .then(function (response) {
            var projectData = response.data;
            console.log('response', response.data);
            if (!projectData) {
                alert('error');
                return;
            }
            if (!projectData.success) {
                projectData.error[0].forEach(error => {
                    app.errors.push(error.errorMessage);
                });
                return;
            }

            let newProj = projectData.added;
            newProj.departmentId = app.departmentId;
            newProj.companyId = app.companyId;

            newProj.editMode = {
                id: newProj.id,
                title: app.project.title,
                description: app.project.description,
                departmentId: app.departmentId,
                companyId: app.companyId,

                active: false
            }
            this.errors = [];
            if (app.activeParent) {
                app.activeParent.activities.unshift(newProj);
            }

            else {
                app.projects.unshift(newProj);
            }
            alert('an Item has been added');
            app.project.title = '';
            app.project.description = '';
        });
}

function GetProjects(application, page, countPerPage) {
    axios.get(`Projects/Get?departmentId=${application.departmentId}&companyId=${application.companyId}&page=${page}&countPerPage=${countPerPage}`).then(
        function (response) {
            const { data } = response
            console.log('projects', data);

            application.projects = data.records;
            application.dataPaging.totalCount = data.totalCount;
            application.projects = addEditModeToProjects(application.projects);
        }
    ).catch(function (e) {
        console.error('erorr dep change', e.request);
    }).then(function (e) { application.projectsLoading = false });
}

function DeleteProject(id, application, Parent) {
    if (!confirm(deleteQuest)) {
        return;
    }
    axios.delete(`Projects/Delete?id=${id}`).then(function () {
        if (Parent) {
            Parent.activities = Parent.activities.filter(activity => activity.id != id);
        }
        else {
            application.projects = application.projects
                .filter(project => project.id != id);
        }
    });
}

function EditProject(project, app) {
    if (!confirm(EditQuest)) {
        return;
    }
    if (project.editMode.title.length < 2 ) {
        alert('please insert correct information')
        return;
    }
    axios.put(`Projects/Update`, project.editMode).then(function () {
        let changedProjectDepartmentOrCompany = project.companyId != project.editMode.companyId || project.departmentId != project.editMode.departmentId;
        if (changedProjectDepartmentOrCompany) {
            app.projects = app.projects.filter(appProject => appProject.id != project.id);
            return
        }
        project.title = project.editMode.title
        project.description = project.editMode.description
    })
}

