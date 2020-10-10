Vue.component('paginate', VuejsPaginate)

const debugTeams = true;

const teamFields = [
    {
        name: 'name',
        displayName: 'name',
        min: 0,
        max: 255,
        type: DATA_TYPES.TEXT,
        required: false,
    },
]

const Modals_Teams = {
    Team: {
        Show: function () {
            $('#TeamModal').modal('show');
        },
        Hide: function () {
            $('#TeamModal').modal('hide');
        }
    },
    TeamUsers: {
        Show: function () {
            $('#TeamUsersModal').modal('show');
        },
        Hide: function () {
            $('#TeamUsersModal').modal('hide');
        }
    }
}

const teamFormObject = (obj) => {

    let record = obj || {
        name: null,
        selectedSupervisor: null,
        selectedMembers: []
    }

    return {
        record,
        message: '',
        image: null,
        isLoading: false,
        isSaving: false,
    }
}

const teamObject = () => {
    return {
        data: [],
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
        form: teamFormObject(),
        supervisors: {
            data: [],
            message: '',
            isLoading: false
        },
        members: {
            data: [],
            message: '',
            isLoading: false
        },
        formRelatedDataAreLoaded: false
    }
}

const teamsMethods = {
    teams_validateForm: function (obj) {

        const form = obj || this.teams.form.record

        console.log({ form })

        var isValid = true
        let finalMessage = '';

        for (var i = 0; i < teamFields.length; i++) {

            const field = teamFields[i]
            const fieldValue = form[field.name];
            const validator = new CoreValidator(field.name, fieldValue, field.required, field.type, field.min, field.max)
            isValid = validator.validate()

            if (!isValid) {
                finalMessage = validator.message();
                break;
            }
        }

        if (!isValid) {
            this.teams_setFormMessage(finalMessage || 'Fill Required Fields to Continue')
        }

        return isValid;
    },
    teams_setFormMessage: function (message) {

        this.teams.form.message = message
    },
    teams_setFormLoading: function (isLoading) {
        this.teams.form.isLoading = isLoading
    },
    teams_setFormSaving: function (isSaving) {
        this.teams.form.isSaving = isSaving
    },
    teams_setProcessing: function (isProcessing) {
        this.teams.isProcessing = isProcessing
    },
    teams_setLoading: function (isLoading) {
        this.teams.isLoading = isLoading
    },
    teams_setMessage: function (message) {
        this.teams.message = message
    },
    teams_openModal: function () {

        this.teams_loadFormRelatedData();

        this.teams.message = ''
        this.teams.form = teamFormObject()


        Modals_Teams.Team.Show()
    },
    teams_edit: function (idx) {

        this.teams_loadFormRelatedData();

        // INDEX VALIDATION if (idx < 0 || idx>

        if (idx > this.teams.data.length - 1) {
            console.warn(`INVALID INDEX ${idx}, TOTAL RECORDS ARE ${this.teams.data.length}`)
            return
        }

        // GET RECORD
        const record = this.teams.data[idx]

        // OPEN MODAL
        Modals_Teams.Team.Show();
        this.teams.form.isLoading = true

        this.teams_setFormLoading(true)
        this.teams_setFormMessage('Loading...');

        // GET REQUEST
        TeamsService.GetById(record.id)
            .then(r => {

                const record = r.data

                if (!record) {
                    this.teams_setFormMessage(BASIC_ERROR_MESSAGE);
                    return
                }

                this.teams_setFormMessage('');

                this.teams.form = teamFormObject(record);
            })
            .catch(e => {

                console.error('get error', e)

                this.teams_setFormMessage(BASIC_ERROR_MESSAGE);
            })
            .then(() => {
                this.teams_setFormLoading(false)
            })
    },
    teams_save: function () {

        this.teams_setFormMessage('');

        let pendingRecord = { ...this.teams.form.record }

        // required fields validation

        if (!this.teams_validateForm()) {

            //this.teams_setFormMessage('Fill the required fields to continue')
            return;
        }


        const sendForm = () => {

            // START UPDATE/CREATE REQUEST
            this.teams_setFormSaving(true)


            // EXISTING RECORD
            if (pendingRecord.id) {

                TeamsService.Update(pendingRecord)
                    .then((r) => {

                        /** @type {IClientResponseModel<ISubject>} */
                        const record = r.data

                        if (debugTeams) {
                            console.log('update response', r)
                        }

                        if (record) {

                            // feedback
                            this.teams_setFormMessage('Updated!')

                            // update data array
                            let data = [...this.teams.data]

                            const idx = data.findIndex(k => k.id === record.id)

                            if (idx !== -1) {

                                data[idx] = { ...record }
                                this.teams.data = data;
                            }
                            else {
                                location.reload()
                            }
                        }
                        else {
                            this.teams_setFormMessage(BASIC_ERROR_MESSAGE)
                        }
                    })
                    .catch((e) => {

                        console.error('Updated!', e)

                        this.teams_setFormMessage(BASIC_ERROR_MESSAGE)

                    })
                    .then(() => {
                        this.teams_setFormSaving(false)
                    });

                return
            }

            // NEW RECORD
            TeamsService.Add(pendingRecord)
                .then((r) => {

                    const record = r.data

                    if (debugTeams) {
                        console.log('add response', r)
                    }


                    if (record) {

                        let data = [...this.teams.data]
                        data.unshift(record)

                        this.teams.data = data
                        this.teams.form = teamFormObject();

                        this.teams_setFormMessage('Added!')
                    }
                    else {
                        this.teams_setFormMessage(BASIC_ERROR_MESSAGE)
                    }

                })
                .catch((e) => {

                    console.error('create', e)

                    this.teams_setFormMessage(BASIC_ERROR_MESSAGE)
                })
                .then(() => {
                    this.teams_setFormSaving(false)
                });

        }

        sendForm()
    },
    teams_delete: function (idx) {

        // INDEX VALIDATION
        if (idx < 0 || idx > this.teams.data.length - 1) {
            console.warn(`INVALID INDEX ${idx}, TOTAL RECORDS ARE ${this.teams.data.length}`)
            return
        }

        // CONFIRMATION
        if (!confirm('Confirm Deletion')) {
            return;
        }

        /** @type {Array<ISubject>} */
        let data = [...this.teams.data];

        // set deleting
        let record = data[idx];

        this.teams_setProcessing(true)

        TeamsService.Delete(record.id)
            .then((r) => {

                const record = r.data

                if (debugTeams) {
                    console.log('delete response', r)
                }

                if (record) {

                    data.splice(idx, 1);
                    this.teams.data = data
                    this.teams_getAll(0)
                    this.teams_setMessage('Subject deleted!')
                }
                else {
                    this.teams_setMessage(BASIC_ERROR_MESSAGE)
                }
            })
            .catch((e) => {

                console.error('delete', e)

                this.teams_setMessage(BASIC_ERROR_MESSAGE)

            })
            .then(() => {

                this.teams_setProcessing(false)
            });
    },
    teams_getAll: function (page = 0) {

        this.teams_setLoading(true)
        this.teams_setMessage('Loading...')

        return TeamsService.GetAll()
            .then((r) => {

                /** @type {IClientResponseModel<ISubject>} */
                const record = r.data

                if (debugTeams) {
                    console.log('getall team response', r)
                }

                if (record) {
                    this.teams.data = [...record]
                    //this.teams.dataPaging.totalCount = extraData.totalCount
                }
                else {
                    this.teams_setMessage(BASIC_ERROR_MESSAGE)
                }
            })
            .catch((e) => {
                console.error('teams getall', e)
                this.teams_setMessage(getAxiosErrorMessage(e))
            })
            .then(() => {
                this.teams_setLoading(false)
            })

    },
    teams_pageClick: function (pageNum) {
        this.teams_getAll(pageNum - 1)
    },
    teams_loadFormRelatedData: function () {

        if (!this.teams.formRelatedDataAreLoaded) {
            this.teamsSupervisors_getAll();
            this.teamsMembers_getAll();
            this.teams.formRelatedDataAreLoaded = true
        }
    },
}

const teamsSupervisorsMethods = {
    teamsSupervisors_setLoading: function (isLoading) {
        this.teams.supervisors.isLoading = isLoading
    },
    teamsSupervisors_setMessage: function (message) {
        this.teams.supervisors.message = message
    },
    teamsSupervisors_getAll: function () {

        this.teamsSupervisors_setLoading(true)
        this.teamsSupervisors_setMessage('Loading...')

        this.teams.supervisors.data = []

        return UsersService.GetUsersByRoleKeyValue(APP_USER_ROLES.supervisor.key)
            .then((r) => {

                const record = r.data

                if (record) {
                    this.teams.supervisors.data = record
                }
                else {
                    this.teamsSupervisors_setMessage(BASIC_ERROR_MESSAGE)
                }
            })
            .catch((e) => {
                console.error('teamsSupervisors getall', e)
                this.teamsSupervisors_setMessage(getAxiosErrorMessage(e))
            })
            .then(() => {
                this.teamsSupervisors_setLoading(false)
            })
    },
}

const teamsMembersMethods = {
    teamsMembers_setLoading: function (isLoading) {
        this.teams.members.isLoading = isLoading
    },
    teamsMembers_setMessage: function (message) {
        this.teams.members.message = message
    },
    teamsMembers_getAll: function () {

        this.teamsMembers_setLoading(true)
        this.teamsMembers_setMessage('Loading...')

        this.teams.members.data = []

        return UsersService.GetUsersByRoleKeyValue(APP_USER_ROLES.teamMember.key)
            .then((r) => {

                const record = r.data

                if (record) {
                    this.teams.members.data = record
                }
                else {
                    this.teamsMembers_setMessage(BASIC_ERROR_MESSAGE)
                }
            })
            .catch((e) => {
                console.error('teamsMembers getall', e)
                this.teamsMembers_setMessage(getAxiosErrorMessage(e))
            })
            .then(() => {
                this.teamsMembers_setLoading(false)
            })
    },
}

const teamUserFormObject = (teamId = null, userIds = []) => {

    let record = {
        teamId,
        userIds
    }

    return {
        record,
        message: '',
        image: null,
        isLoading: false,
        isSaving: false,
    }
}

const teamUserObject = () => {
    return {
        data: [],
        isLoading: false,
        isProcessing: false,
        form: teamUserFormObject(),
        message: '',
    }
}

const teamUsersMethods = {
    teamUsers_setFormMessage: function (message) {

        this.teamUsers.form.message = message
    },
    teamUsers_setFormLoading: function (isLoading) {
        this.teamUsers.form.isLoading = isLoading
    },
    teamUsers_setFormSaving: function (isSaving) {
        this.teamUsers.form.isSaving = isSaving
    },
    teamUsers_edit: function (teamId) {

        // OPEN MODAL
        Modals_Teams.TeamUsers.Show();
        this.teamUsers.form.isLoading = true

        this.teamUsers_setFormLoading(true)
        this.teamUsers_setFormMessage('Loading...');

        this.teamUsers_getAll(teamId)

        // GET REQUEST
        TeamsService.GetTeamUsers(teamId)
            .then(r => {

                const record = r.data

                if (!record) {
                    this.teamUsers_setFormMessage(BASIC_ERROR_MESSAGE);
                    return
                }

                this.teamUsers_setFormMessage('');

                this.teamUsers.form = teamUserFormObject(teamId, record);

            })
            .catch(e => {

                console.error('get error', e)

                this.teamUsers_setFormMessage(BASIC_ERROR_MESSAGE);
            })
            .then(() => {
                this.teamUsers_setFormLoading(false)
            })
    },
    teamUsers_save: function () {

        this.teamUsers_setFormMessage('');

        let pendingRecord = { ...this.teamUsers.form.record }

        // START UPDATE/CREATE REQUEST
        this.teamUsers_setFormSaving(true)

        // UPDATE
        TeamsService.AddRemoveTeamsUsers(pendingRecord)
            .then((r) => {

                const record = r.data

                if (record) {

                    // update members count (teams)
                    let data = [...this.teams.data]

                    let teamToUpdate = data.find(k => k.id === pendingRecord.teamId)

                    teamToUpdate.membersCount = pendingRecord.userIds.length

                    this.teams.data = data

                    // saved feeback (teamUsers)
                    this.teamUsers_setFormMessage('Saved!')
                }
                else {
                    this.teamUsers_setFormMessage(BASIC_ERROR_MESSAGE)
                }

            })
            .catch((e) => {
                console.error('save team teamUsers', e)
                this.teamUsers_setFormMessage(getAxiosErrorMessage(e))
            })
            .then(() => {
                this.teamUsers_setFormSaving(false)
            });
    },
    teamUsers_setLoading: function (isLoading) {
        this.teamUsers.isLoading = isLoading
    },
    teamUsers_setMessage: function (message) {
        this.teamUsers.message = message
    },
    teamUsers_getAll: function (teamId) {

        this.teamUsers_setLoading(true)
        this.teamUsers_setMessage('Loading...')

        this.teamUsers.data = []

        return AdminsService.GetAllUsersExecludeTeamSupervisors(teamId)
            .then((r) => {

                const record = r.data

                if (record) {
                    this.teamUsers.data = [...record]
                }
                else {
                    this.teamUsers_setMessage(BASIC_ERROR_MESSAGE)
                }
            })
            .catch((e) => {
                console.error('teamUsers getall', e)
                this.teamUsers_setMessage(getAxiosErrorMessage(e))
            })
            .then(() => {
                this.teamUsers_setLoading(false)
            })
    },
}

var teams_app = new Vue({
    el: "#Teams",
    data: {
        dateOptions,
        dateTimeOptions,
        teams: teamObject(),
        //teamUsers: teamUserObject(),
    },
    computed: {
        teamsTotalPages: function () {
            const totalCount = this.teams.dataPaging.totalCount
            const length = this.teams.dataPaging.length

            const totalPages = Math.ceil(totalCount / length);

            console.log({ totalPages })


            return totalPages || 0
        }
    },
    methods: {
        ...teamsMethods,
        //...teamUsersMethods,
        ...teamsSupervisorsMethods,
        ...teamsMembersMethods,
    },
    mounted: function () {

        this.teams_getAll()
    }
})