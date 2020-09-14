
const Modals = {
    Countries: {
        Show: () => {
            $('#CountriesModal').modal('show')
        },
        Hide: () => {
            $('#CountriesModal').hide('show')
        }
    },
    InventoryStatus: {
        Show: () => {
            $('#InventoryStatusesModal').modal('show')
        },
        Hide: () => {
            $('#InventoryStatusesModal').hide('show')
        }
    },
    InventoryTypes: {
        Show: () => {
            $('#InventoryTypesModal').modal('show')
        },
        Hide: () => {
            $('#InventoryTypesModal').hide('show')
        }
    },
    PublishingChannels: {
        Show: () => {
            $('#PublishingChannelsModal').modal('show')
        },
        Hide: () => {
            $('#PublishingChannelsModal').hide('show')
        }
    },
    UpdateFrequencies: {
        Show: () => {
            $('#UpdateFrequenciesModal').modal('show')
        },
        Hide: () => {
            $('#UpdateFrequenciesModal').hide('show')
        }
    },
    Projects: {
        Show: () => {
            $('#ProjectsModal').modal('show')
        },
        Hide: () => {
            $('#ProjectsModal').hide('show')
        }
    },
    ProjectsFilter: {
        Show: () => {
            $('#FilterProjectsModal').modal('show')
        },
        Hide: () => {
            $('#FilterProjectsModal').hide('show')
        }
    },
    SubProjects: {
        Show: () => {
            $('#SubProjectsModal').modal('show')
        },
        Hide: () => {
            $('#SubProjectsModal').hide('show')
        }
    },
}

// NUGET VUEJS EXTENSIONS
Vue.component('paginate', VuejsPaginate)

Vue.component('date-picker', VueBootstrapDatetimePicker);

// METHODS

var countriesMethods = {

    openCountriesModal: function () {
        Modals.Countries.Show()
    },
    addCountry: function () {
        console.log('fired add')
        let { name } = { ...this.countries };

        this.countries.isLoading = true;

        CountriesService.Create(name).done((r) => {

            this.countries.data = [r, ...this.countries.data]
            this.countries.message = 'Added!';

        }).fail((e) => {

            this.countries.message = BASIC_ERROR_MESSAGE;

        }).always(() => {

            this.countries.name = ''
            this.countries.isLoading = false;
        });
    },
    deleteCountry: function (idx) {

        if (!confirm('Confirm Delete')) {
            return;
        }

        let data = [...this.countries.data];

        let country = data[idx];

        CountriesService.Delete(country.id).done((r) => {

            if (r) {
                console.log({ data, idx })
                data.splice(idx, 1);
                this.countries.data = data
                this.countries.message = 'Deleted!';
            }
            else {
                this.countries.message = BASIC_ERROR_MESSAGE;
            }

        }).fail((e) => {

            console.error({ e })
            this.countries.message = BASIC_ERROR_MESSAGE;

        }).always(() => {
            this.countries.isLoading = false
        });
    },
    updateCountry: function (idx) {

        if (!confirm('Confirm Save')) {
            return;
        }

        let data = [...this.countries.data];

        let country = data[idx];

        CountriesService.Update(country.id, country.name)
            .done((r) => {

                if (r) {

                    data[idx].isEdit = false;
                    this.countries.message = 'Saved!'

                    this.countries.data = data;
                }
                else {

                    this.countries.message = BASIC_ERROR_MESSAGE
                }

            }).fail((e) => {

                this.countries.message = BASIC_ERROR_MESSAGE

                console.error('save country', e)
            }).always(() => {

            });
    },
    editCountry: function (idx) {

        let data = [...this.countries.data];

        for (var i = 0; i < data.length; i++) {
            data[i].isEdit = i === idx;

            if (i === idx) {
                this.countries.backupEdit.name = data[i].name;
            }
        }

        this.countries.data = data;
    },
    cancelCountryEdit: function (idx) {
        let data = [...this.countries.data];

        for (var i = 0; i < data.length; i++) {
            data[i].isEdit = false;

            if (i === idx) {
                data[i].name = this.countries.backupEdit.name;
            }
        }

        this.countries.data = data;
    }
}

var inventoryStatusesMethods = {

    openInventoryStatusesModal: function () {
        Modals.InventoryStatus.Show()
    },
    addInventoryStatus: function () {

        let { name } = { ...this.inventoryStatuses };

        this.inventoryStatuses.isLoading = true;

        InventoryStatusesService.Create(name).done((r) => {

            this.inventoryStatuses.data = [r, ...this.inventoryStatuses.data]
            this.inventoryStatuses.message = 'Added!';

        }).fail((e) => {

            this.inventoryStatuses.message = BASIC_ERROR_MESSAGE;

        }).always(() => {

            this.inventoryStatuses.name = ''
            this.inventoryStatuses.isLoading = false;
        });
    },
    deleteInventoryStatus: function (idx) {

        if (!confirm('Confirm Delete')) {
            return;
        }

        let data = [...this.inventoryStatuses.data];

        let inventoryStatus = data[idx];

        InventoryStatusesService.Delete(inventoryStatus.id).done((r) => {

            if (r) {
                console.log({ data, idx })
                data.splice(idx, 1);
                this.inventoryStatuses.data = data
                this.inventoryStatuses.message = 'Deleted!';
            }
            else {
                this.inventoryStatuses.message = BASIC_ERROR_MESSAGE;
            }

        }).fail((e) => {

            console.error({ e })
            this.inventoryStatuses.message = BASIC_ERROR_MESSAGE;

        }).always(() => {
            this.inventoryStatuses.isLoading = false
        });
    },
    updateInventoryStatus: function (idx) {

        if (!confirm('Confirm Save')) {
            return;
        }

        let data = [...this.inventoryStatuses.data];

        let inventoryStatus = data[idx];

        InventoryStatusesService.Update(inventoryStatus.id, inventoryStatus.name)
            .done((r) => {

                if (r) {

                    data[idx].isEdit = false;
                    this.inventoryStatuses.message = 'Saved!'

                    this.inventoryStatuses.data = data;
                }
                else {

                    this.inventoryStatuses.message = BASIC_ERROR_MESSAGE
                }

            }).fail((e) => {

                this.inventoryStatuses.message = BASIC_ERROR_MESSAGE

                console.error('save inventoryStatus', e)
            }).always(() => {

            });
    },
    editInventoryStatus: function (idx) {

        let data = [...this.inventoryStatuses.data];

        for (var i = 0; i < data.length; i++) {
            data[i].isEdit = i === idx;

            if (i === idx) {
                this.inventoryStatuses.backupEdit.name = data[i].name;
            }
        }

        this.inventoryStatuses.data = data;
    },
    cancelInventoryStatusEdit: function (idx) {
        let data = [...this.inventoryStatuses.data];

        for (var i = 0; i < data.length; i++) {
            data[i].isEdit = false;

            if (i === idx) {
                data[i].name = this.inventoryStatuses.backupEdit.name;
            }
        }

        this.inventoryStatuses.data = data;
    }
}

var inventoryTypesMethods = {

    openInventoryTypeModal: function () {
        Modals.InventoryTypes.Show()
    },
    addInventoryType: function () {
        console.log('fired add')
        let { name } = { ...this.inventoryTypes };

        this.inventoryTypes.isLoading = true;

        InventoryTypesService.Create(name).done((r) => {

            this.inventoryTypes.data = [r, ...this.inventoryTypes.data]
            this.inventoryTypes.message = 'Added!';

        }).fail((e) => {

            this.inventoryTypes.message = BASIC_ERROR_MESSAGE;

        }).always(() => {

            this.inventoryTypes.name = ''
            this.inventoryTypes.isLoading = false;
        });
    },
    deleteInventoryType: function (idx) {

        if (!confirm('Confirm Delete')) {
            return;
        }

        let data = [...this.inventoryTypes.data];

        let inventoryTypes = data[idx];

        InventoryTypesService.Delete(inventoryTypes.id).done((r) => {

            if (r) {
                console.log({ data, idx })
                data.splice(idx, 1);
                this.inventoryTypes.data = data
                this.inventoryTypes.message = 'Deleted!';
            }
            else {
                this.inventoryTypes.message = BASIC_ERROR_MESSAGE;
            }

        }).fail((e) => {

            console.error({ e })
            this.inventoryTypes.message = BASIC_ERROR_MESSAGE;

        }).always(() => {
            this.inventoryTypes.isLoading = false
        });
    },
    updateInventoryType: function (idx) {

        if (!confirm('Confirm Save')) {
            return;
        }

        let data = [...this.inventoryTypes.data];

        let inventoryTypes = data[idx];

        InventoryTypesService.Update(inventoryTypes.id, inventoryTypes.name)
            .done((r) => {

                if (r) {

                    data[idx].isEdit = false;
                    this.inventoryTypes.message = 'Saved!'

                    this.inventoryTypes.data = data;
                }
                else {

                    this.inventoryTypes.message = BASIC_ERROR_MESSAGE
                }

            }).fail((e) => {

                this.inventoryTypes.message = BASIC_ERROR_MESSAGE

                console.error('save inventoryTypes', e)
            }).always(() => {

            });
    },
    editInventoryType: function (idx) {

        let data = [...this.inventoryTypes.data];

        for (var i = 0; i < data.length; i++) {
            data[i].isEdit = i === idx;

            if (i === idx) {
                this.inventoryTypes.backupEdit.name = data[i].name;
            }
        }

        this.inventoryTypes.data = data;
    },
    cancelInventoryTypeEdit: function (idx) {
        let data = [...this.inventoryTypes.data];

        for (var i = 0; i < data.length; i++) {
            data[i].isEdit = false;

            if (i === idx) {
                data[i].name = this.inventoryTypes.backupEdit.name;
            }
        }

        this.inventoryTypes.data = data;
    }
}

var publishingChannelsMethods = {

    openPublishingChannelsModal: function () {
        Modals.PublishingChannels.Show()
    },
    addPublishingChannel: function () {
        console.log('fired add')
        let { name } = { ...this.publishingChannels };

        this.publishingChannels.isLoading = true;



        PublishingChannelsService.Create(name).done((r) => {

            this.publishingChannels.data = [r, ...this.publishingChannels.data]
            this.publishingChannels.message = 'Added!';

        }).fail((e) => {

            this.publishingChannels.message = BASIC_ERROR_MESSAGE;

        }).always(() => {

            this.publishingChannels.name = ''
            this.publishingChannels.isLoading = false;
        });
    },
    deletePublishingChannel: function (idx) {

        if (!confirm('Confirm Delete')) {
            return;
        }

        let data = [...this.publishingChannels.data];

        let publishingChannel = data[idx];

        PublishingChannelsService.Delete(publishingChannel.id).done((r) => {

            if (r) {
                console.log({ data, idx })
                data.splice(idx, 1);
                this.publishingChannels.data = data
                this.publishingChannels.message = 'Deleted!';
            }
            else {
                this.publishingChannels.message = BASIC_ERROR_MESSAGE;
            }

        }).fail((e) => {

            console.error({ e })
            this.publishingChannels.message = BASIC_ERROR_MESSAGE;

        }).always(() => {
            this.publishingChannels.isLoading = false
        });
    },
    updatePublishingChannel: function (idx) {

        if (!confirm('Confirm Save')) {
            return;
        }

        let data = [...this.publishingChannels.data];

        let publishingChannel = data[idx];

        PublishingChannelsService.Update(publishingChannel.id, publishingChannel.name)
            .done((r) => {

                if (r) {

                    data[idx].isEdit = false;
                    this.publishingChannels.message = 'Saved!'

                    this.publishingChannels.data = data;
                }
                else {

                    this.publishingChannels.message = BASIC_ERROR_MESSAGE
                }

            }).fail((e) => {

                this.publishingChannels.message = BASIC_ERROR_MESSAGE

                console.error('save publishingChannel', e)
            }).always(() => {

            });
    },
    editPublishingChannel: function (idx) {

        let data = [...this.publishingChannels.data];

        for (var i = 0; i < data.length; i++) {
            data[i].isEdit = i === idx;

            if (i === idx) {
                this.publishingChannels.backupEdit.name = data[i].name;
            }
        }

        this.publishingChannels.data = data;
    },
    cancelPublishingChannelEdit: function (idx) {
        let data = [...this.publishingChannels.data];

        for (var i = 0; i < data.length; i++) {
            data[i].isEdit = false;

            if (i === idx) {
                data[i].name = this.publishingChannels.backupEdit.name;
            }
        }

        this.publishingChannels.data = data;
    }
}

var updateFrequenciesMethods = {

    openUpdateFrequenciesModal: function () {
        Modals.UpdateFrequencies.Show()
    },
    addUpdateFrequency: function () {
        console.log('fired add')
        let { name } = { ...this.updateFrequencies };

        this.updateFrequencies.isLoading = true;

        UpdateFrequenciesService.Create(name).done((r) => {

            this.updateFrequencies.data = [r, ...this.updateFrequencies.data]
            this.updateFrequencies.message = 'Added!';

        }).fail((e) => {

            this.updateFrequencies.message = BASIC_ERROR_MESSAGE;

        }).always(() => {

            this.updateFrequencies.name = ''
            this.updateFrequencies.isLoading = false;
        });
    },
    deleteUpdateFrequency: function (idx) {

        if (!confirm('Confirm Delete')) {
            return;
        }

        let data = [...this.updateFrequencies.data];

        let updateFrequency = data[idx];

        UpdateFrequenciesService.Delete(updateFrequency.id).done((r) => {

            if (r) {
                console.log({ data, idx })
                data.splice(idx, 1);
                this.updateFrequencies.data = data
                this.updateFrequencies.message = 'Deleted!';
            }
            else {
                this.updateFrequencies.message = BASIC_ERROR_MESSAGE;
            }

        }).fail((e) => {

            console.error({ e })
            this.updateFrequencies.message = BASIC_ERROR_MESSAGE;

        }).always(() => {
            this.updateFrequencies.isLoading = false
        });
    },
    updateUpdateFrequency: function (idx) {

        if (!confirm('Confirm Save')) {
            return;
        }

        let data = [...this.updateFrequencies.data];

        let updateFrequency = data[idx];

        UpdateFrequenciesService.Update(updateFrequency.id, updateFrequency.name)
            .done((r) => {

                if (r) {

                    data[idx].isEdit = false;
                    this.updateFrequencies.message = 'Saved!'

                    this.updateFrequencies.data = data;
                }
                else {

                    this.updateFrequencies.message = BASIC_ERROR_MESSAGE
                }

            }).fail((e) => {

                this.updateFrequencies.message = BASIC_ERROR_MESSAGE

                console.error('save updateFrequency', e)
            }).always(() => {

            });
    },
    editUpdateFrequency: function (idx) {

        let data = [...this.updateFrequencies.data];

        for (var i = 0; i < data.length; i++) {
            data[i].isEdit = i === idx;

            if (i === idx) {
                this.updateFrequencies.backupEdit.name = data[i].name;
            }
        }

        this.updateFrequencies.data = data;
    },
    cancelUpdateFrequencyEdit: function (idx) {
        let data = [...this.updateFrequencies.data];

        for (var i = 0; i < data.length; i++) {
            data[i].isEdit = false;

            if (i === idx) {
                data[i].name = this.updateFrequencies.backupEdit.name;
            }
        }

        this.updateFrequencies.data = data;
    }
}

var subProjectsMethods = {

    openSubProjectsModal: function () {
        Modals.SubProjects.Show()
    },
    addSubProject: function () {
        console.log('fired add')
        let { name } = { ...this.subProjects };

        this.subProjects.isLoading = true;

        SubProjectsService.Create(name).done((r) => {

            this.subProjects.data = [r, ...this.subProjects.data]
            this.subProjects.message = 'Added!';

        }).fail((e) => {

            this.subProjects.message = BASIC_ERROR_MESSAGE;

        }).always(() => {

            this.subProjects.name = ''
            this.subProjects.isLoading = false;
        });
    },
    deleteSubProject: function (idx) {

        if (!confirm('Confirm Delete')) {
            return;
        }

        let data = [...this.subProjects.data];

        let subProject = data[idx];

        SubProjectsService.Delete(subProject.id).done((r) => {

            if (r) {
                console.log({ data, idx })
                data.splice(idx, 1);
                this.subProjects.data = data
                this.subProjects.message = 'Deleted!';
            }
            else {
                this.subProjects.message = BASIC_ERROR_MESSAGE;
            }

        }).fail((e) => {

            console.error({ e })
            this.subProjects.message = BASIC_ERROR_MESSAGE;

        }).always(() => {
            this.subProjects.isLoading = false
        });
    },
    updateSubProject: function (idx) {

        if (!confirm('Confirm Save')) {
            return;
        }

        let data = [...this.subProjects.data];

        let subProject = data[idx];

        SubProjectsService.Update(subProject.id, subProject.name)
            .done((r) => {

                if (r) {

                    data[idx].isEdit = false;
                    this.subProjects.message = 'Saved!'

                    this.subProjects.data = data;
                }
                else {

                    this.subProjects.message = BASIC_ERROR_MESSAGE
                }

            }).fail((e) => {

                this.subProjects.message = BASIC_ERROR_MESSAGE

                console.error('save subProject', e)
            }).always(() => {

            });
    },
    editSubProject: function (idx) {

        let data = [...this.subProjects.data];

        for (var i = 0; i < data.length; i++) {
            data[i].isEdit = i === idx;

            if (i === idx) {
                this.subProjects.backupEdit.name = data[i].name;
            }
        }

        this.subProjects.data = data;
    },
    cancelSubProjectEdit: function (idx) {
        let data = [...this.subProjects.data];

        for (var i = 0; i < data.length; i++) {
            data[i].isEdit = false;

            if (i === idx) {
                data[i].name = this.subProjects.backupEdit.name;
            }
        }

        this.subProjects.data = data;
    }
}

var projectsMethods = {
    openProjectModal: function () {

        this.projects.id = 0
        this.projects.title = ''
        this.projects.description = ''
        this.projects.datePublished = ''
        this.projects.dateModified = ''
        this.projects.needsUpdate = null
        this.projects.inventoryTypeId = ''
        this.projects.inventoryStatusId = ''
        this.projects.countryId = ''
        this.projects.updateFrequencyId = ''
        this.projects.publishingChannelIds = []

        Modals.Projects.Show()
    },
    addProject: function () {

        const {
            title,
            description,
            datePublished,
            inventoryTypeId,
            inventoryStatusId,
            needsUpdate,
            countryId,
            updateFrequencyId,
            publishingChannelIds,
            subProjectIds,
            personResponsibleId,
            secondaryPersonResponsibleId,
        } = this.projects;

        const PublishingChannels = publishingChannelIds.map(k => ({ InventoryProjectId: 0, PublishingChannelId: k }))
        const InventorySubProjects = subProjectIds.map(k => ({ InventoryProjectId: 0, InventorySubProjectId: k }))

        console.log({ InventorySubProjects })

        const project = {

            Title: title,
            Description: description,
            DatePublished: datePublished,
            NeedsUpdate: needsUpdate,

            InventoryTypeId: inventoryTypeId,
            InventoryStatusId: inventoryStatusId,
            CountryId: countryId,
            UpdateFrequencyId: updateFrequencyId,
            PersonResponsibleId: personResponsibleId,
            SecondaryPersonResponsibleId: secondaryPersonResponsibleId,

            PublishingChannels,
            InventorySubProjects
        }

        console.log({ project })

        this.projects.isLoading = true;

        InventoryProjectsService.Add(project)
            .then((r) => {

                //console.log('add inv proj', { r })
                if (r && r.data) {

                    this.projects.data = [r.data, ...this.projects.data]

                    this.projects.title = ''
                    this.projects.description = ''
                    this.projects.datePublished = ''
                    this.projects.dateModified = ''
                    this.projects.needsUpdate = null
                    this.projects.inventoryTypeId = ''
                    this.projects.inventoryStatusId = ''
                    this.projects.countryId = ''
                    this.projects.updateFrequencyId = ''
                    this.projects.publishingChannelIds = []

                    this.projects.message = 'Added!';
                }
                else {
                    this.projects.message = BASIC_ERROR_MESSAGE;
                }

            })
            .catch((e) => {

                this.projects.message = BASIC_ERROR_MESSAGE;

            })
            .then(() => {

                this.projects.name = ''
                this.projects.isLoading = false;
            });
    },
    deleteProject: function (idx) {

        if (!confirm('Confirm Delete')) {
            return;
        }

        let data = [...this.projects.data];

        let project = data[idx];

        InventoryProjectsService.Delete(project.id).done((r) => {

            if (r) {
                console.log({ data, idx })
                data.splice(idx, 1);
                this.projects.data = data
                this.projects.message = 'Deleted!';
            }
            else {
                this.projects.message = BASIC_ERROR_MESSAGE;
            }

        }).fail((e) => {

            console.error({ e })
            this.projects.message = BASIC_ERROR_MESSAGE;

        }).always(() => {
            this.projects.isLoading = false
        });
    },
    updateProject: function () {

        if (!confirm('Confirm Save')) {
            return;
        }

        console.log('projects', this.projects)

        const {
            id,
            title,
            description,
            datePublished,
            needsUpdate,
            inventoryTypeId,
            inventoryStatusId,
            countryId,
            updateFrequencyId,
            publishingChannelIds,
            subProjectIds,
            personResponsibleId,
            secondaryPersonResponsibleId,

        } = this.projects;


        const PublishingChannels = publishingChannelIds.map(k => ({ InventoryProjectId: 0, PublishingChannelId: k }))
        const InventorySubProjects = subProjectIds.map(k => ({ InventoryProjectId: 0, InventorySubProjectId: k }))

        const updatedData = {
            ID: id,
            Title: title,
            Description: description,
            DatePublished: datePublished,
            NeedsUpdate: needsUpdate,
            InventoryTypeId: inventoryTypeId,
            InventoryStatusId: inventoryStatusId,
            CountryId: countryId,
            UpdateFrequencyId: updateFrequencyId,
            PublishingChannels,
            InventorySubProjects,
            PersonResponsibleId: personResponsibleId,
            SecondaryPersonResponsibleId: secondaryPersonResponsibleId
        }

        InventoryProjectsService.Update(updatedData)
            .then((r) => {

                console.log('updated', r.data)
                //alert('updated')
                //location.reload();

                if (r && r.data) {

                    let data = [...this.projects.data];
                    console.log({ data, idx: this.projects.editIdx })

                    data[this.projects.editIdx] = { ...r.data };

                    this.projects.data = data
                    this.projects.message = 'Saved!'
                }
                else {

                    this.projects.message = BASIC_ERROR_MESSAGE
                }

            }).catch((e) => {

                this.projects.message = BASIC_ERROR_MESSAGE

                console.error('save project', e)
            }).then(() => {

            });
    },
    editProject: function (idx) {

        let data = [...this.projects.data];

        let project = data[idx]

        this.projects.editIdx = idx;
        this.projects.id = project.id
        this.projects.title = project.title
        this.projects.description = project.description
        this.projects.datePublished = project.datePublished
        this.projects.dateModified = project.dateModified
        this.projects.inventoryTypeId = project.inventoryTypeId
        this.projects.inventoryStatusId = project.inventoryStatusId
        this.projects.countryId = project.countryId
        this.projects.updateFrequencyId = project.updateFrequencyId
        this.projects.needsUpdate = project.needsUpdate
        console.log({ project })
        this.projects.publishingChannelIds = project.publishingChannels.map(k => k.publishingChannelId)
        this.projects.subProjectIds = project.inventorySubProjects.map(k => k.inventorySubProjectId)

        this.projects.personResponsibleId = project.personResponsibleId
        this.projects.secondaryPersonResponsibleId = project.secondaryPersonResponsibleId


        Modals.Projects.Show()

        //for (var i = 0; i < data.length; i++) {
        //    data[i].isEdit = i === idx;

        //    if (i === idx) {

        //        this.projects.backupEdit.title = data[i].title
        //        this.projects.backupEdit.description = data[i].description
        //        this.projects.backupEdit.datePublished = data[i].datePublished
        //        this.projects.backupEdit.dateModified = data[i].dateModified
        //        this.projects.backupEdit.inventoryTypeId = data[i].inventoryTypeId
        //        this.projects.backupEdit.inventoryStatusId = data[i].inventoryStatusId
        //        this.projects.backupEdit.countryId = data[i].countryId
        //        this.projects.backupEdit.updateFrequencyId = data[i].updateFrequencyId
        //        this.projects.backupEdit.publishingChannelId = data[i].publishingChannelId
        //        this.projects.backupEdit.personResponsibleId = data[i].personResponsibleId
        //        this.projects.backupEdit.secondaryPersonResponsibleId = data[i].secondaryPersonResponsibleId
        //    }
        //}

        //this.projects.data = data;
    },
    clickCallback: function (pageNum) {

        console.log('page', pageNum)

        if (this.projectsFilter && this.projectsFilter.filterMode) {
            console.log('filter mode pag')

            this.filterProject(undefined, pageNum - 1)
        }
        else {
            console.log('all mode pag')

            this.getAllProjects(pageNum - 1)
        }
        //this.page = pageNum - 1;
        //this.search(pageNum - 1, true);
    },
    getAllProjects: function (page) {
        InventoryProjectsService.GetAll(page, this.projects.dataPaging.length)
            .done((r) => {
                console.log({ r })
                this.projects.data = r.records
                this.projects.dataPaging.totalCount = r.totalCount
            })
            .fail((e) => {
                console.error({ e })
            })
            .always(() => {
                this.projects.dataIsLoading = false;
            })
    }
}

var filterProjectsMethods = {
    openFilterProjectsModal: function () {
        Modals.ProjectsFilter.Show()
    },
    resetFilterProject: function () {
        this.projectsFilter = projectsFilterModalObject();
        this.getAllProjects(0)
    },
    filterProject: function (event, page) {

        let filter = { ...this.projectsFilter }

        this.projectsFilter.filterMode = true;
        filter.page = page === undefined ? 0 : page;
        filter.countPerPage = this.projects.dataPaging.length;

        console.log({ p: filter })
        InventoryProjectsService.Search(filter)
            .then(r => {
                console.log('proj filter r', r.data.totalCount)

                this.projects.data = r.data.records
                this.projects.dataPaging.totalCount = r.data.totalCount
            })
            .catch(e => {
                console.error('inv proj search', e)
            })
            .then(r => {

            })
    }
}

var projectExportMethods = {
    openFilterModal: function () {
        $('#ExportProjectsModal').modal('show')
    },
    exportToExcel: function () {

        const selectedColumns = this.projectExport.selectedColumns;

        if (!selectedColumns || !selectedColumns.length) {
            alert('Choose Columns');
            return;
        }

        const GET_ALL_METHOD = 'AllToExcel';
        const FILTERED_METHOD = 'FilteredToExcel';
        let IsFiltered = false

        if (this.projectsFilter && this.projectsFilter.filterMode) {
            IsFiltered = true
        }

        if (IsFiltered) {

            let url = window.location.protocol + "//" + window.location.host + `/InventoryProjects/${FILTERED_METHOD}`


            let data = { ...this.projectsFilter }

            data.page = 0;
            data.countPerPage = 10000;
            data.selectedColumns = selectedColumns.join(',')

            axios({
                url,
                method: 'POST',
                data,
                responseType: 'blob',
            }).then((response) => {

                console.log({ response })

                const url = window.URL.createObjectURL(new Blob([response.data]));
                const link = document.createElement('a');
                link.href = url;

                var d = new Date();
                let name = `Inventory_${d.getDate()}${d.getMonth() + 1}${d.getFullYear()}${d.getHours()}${d.getMinutes()}${d.getSeconds()}`;

                link.setAttribute('download', `${name}.xlsx`);
                document.body.appendChild(link);
                link.click();
            });
        }
        else {

            let selectedColumnsQuery = selectedColumns.join(',')

            let url = window.location.protocol + "//" + window.location.host + `/InventoryProjects/${GET_ALL_METHOD}?selectedColumns=${selectedColumnsQuery}`

            window.open(url)
        }

    }
}

// DATA

const projectModalObject = () => {
    return {
        id: 0,
        title: '',
        description: '',
        datePublished: '',
        dateModified: '',
        needsUpdate: null,
        inventoryTypeId: '',
        inventoryStatusId: '',
        countryId: '',
        updateFrequencyId: '',
        publishingChannelIds: [],
        subProjectIds: [],
        personResponsibleId: null,
        secondaryPersonResponsibleId: null,
        dataIsLoading: true,
        data: [],
        dataPaging: {
            page: 0,
            totalPages: 0,
            length: 20,
            totalCount: 0,
            pagination: 'custom-pagination',
            prev: 'Prev',
            next: 'Next',
        },
        form: {
            title: '',
            description: '',
            datePublished: '',
            dateModified: '',
            inventoryTypeId: '',
            inventoryStatusId: '',
            countryId: '',
            updateFrequencyId: '',
            publishingChannelId: '',
            personResponsibleId: '',
            secondaryPersonResponsibleId: '',
        },
        backupEdit: {
            id: 0,
            title: '',
            description: '',
            datePublished: '',
            dateModified: '',
            inventoryTypeId: '',
            inventoryStatusId: '',
            countryId: '',
            updateFrequencyId: '',
            publishingChannelId: '',
            personResponsibleId: '',
            secondaryPersonResponsibleId: '',
        },
        isLoading: false,
        message: ''
    }
}

const projectsFilterModalObject = () => {
    return {
        filterMode: false,
        title: '',
        dateModifiedFrom: '',
        dateModifiedTo: '',
        needsUpdate: null,
        datePublishedFrom: '',
        datePublishedTo: '',
        inventoryTypeId: '',
        inventoryStatusId: '',
        countryId: '',
        updateFrequencyId: '',
        publishingChannelIds: [],
        subProjectIds: [],
        personResponsibleId: '',
        secondaryPersonResponsibleId: ''
    }
}

const projectsExportObject = () => {
    return {
        availableColumns: [
            { prop: 'ID', name: 'ID' },
            { prop: 'Title', name: 'Title' },
            { prop: 'Description', name: 'Description' },
            { prop: 'DatePublishedDisplay', name: 'Date Published' },
            { prop: 'DateAddedDisplay', name: 'Date Added' },
            { prop: 'DateModifiedDisplay', name: 'Date Modified' },
            { prop: 'NeedsUpdate', name: 'Needs Update' },
            { prop: 'InventoryTypeDisplay', name: 'Inventory Type' },
            { prop: 'InventoryStatusDisplay', name: 'Inventory Status' },
            { prop: 'CountryDisplay', name: 'Country' },
            { prop: 'UpdateFrequencyDisplay', name: 'Update Frequency' },
            { prop: 'PublishingChannelDisplay', name: 'Publishing Channel' },
            { prop: 'SubProjectsDisplay', name: 'SubProjects' },
            { prop: 'PersonResponsibleDisplay', name: 'Person Responsible' },
            { prop: 'SecondaryPersonResponsibleDisplay', name: 'Secondary PersonResponsible' },
        ],
        selectedColumns: []
    }
}

// APPLICATION

new Vue({
    el: "#ProjectsInventory",
    data: {
        dateOptions,
        dateTimeOptions,
        countries: basicModalObject(),
        inventoryStatuses: basicModalObject(),
        inventoryTypes: basicModalObject(),
        publishingChannels: basicModalObject(),
        updateFrequencies: basicModalObject(),
        subProjects: basicModalObject(),
        projects: projectModalObject(),
        projectsFilter: projectsFilterModalObject(),
        projectExport: projectsExportObject(),
        users: []
    },
    computed: {
    },
    methods: {
        ...countriesMethods,
        ...inventoryStatusesMethods,
        ...inventoryTypesMethods,
        ...publishingChannelsMethods,
        ...updateFrequenciesMethods,
        ...subProjectsMethods,
        ...projectsMethods,
        ...filterProjectsMethods,
        ...projectExportMethods
    },
    watch: {
        projects: {
            handler: function (newVal, oldVal) {

                this.projects.dataPaging.totalPages = Math.ceil(newVal.dataPaging.totalCount / newVal.dataPaging.length);
            },
            deep: true
        },
        countries: {
            handler: function (newVal, oldVal) {
                this.getAllProjects(0)
            },
            deep: true
        },
        inventoryStatuses: {
            handler: function (newVal, oldVal) {
                this.getAllProjects(0)
            },
            deep: true
        },
        inventoryTypes: {
            handler: function (newVal, oldVal) {
                this.getAllProjects(0)
            },
            deep: true
        },
        publishingChannels: {
            handler: function (newVal, oldVal) {
                this.getAllProjects(0)
            },
            deep: true
        },
        updateFrequencies: {
            handler: function (newVal, oldVal) {
                this.getAllProjects(0)
            },
            deep: true
        },
        subProjects: {
            handler: function (newVal, oldVal) {
                this.getAllProjects(0)
            },
            deep: true
        }
    },
    mounted: function () {
        CountriesService.GetAll()
            .done((r) => {

                this.countries.data = r

            })
            .fail((e) => {
                console.error({ e })
            })

        InventoryStatusesService.GetAll()
            .done((r) => {

                this.inventoryStatuses.data = r

            })
            .fail((e) => {
                console.error({ e })
            })

        InventoryTypesService.GetAll()
            .done((r) => {


                this.inventoryTypes.data = r
            })
            .fail((e) => {
                console.error({ e })
            })

        PublishingChannelsService.GetAll()
            .done((r) => {


                this.publishingChannels.data = r
            })
            .fail((e) => {
                console.error({ e })
            })

        UpdateFrequenciesService.GetAll()
            .done((r) => {

                this.updateFrequencies.data = r
            })
            .fail((e) => {
                console.error({ e })
            })

        SubProjectsService.GetAll()
            .done((r) => {

                this.subProjects.data = r
            })
            .fail((e) => {
                console.error({ e })
            })

        this.getAllProjects(0)

        InventoryProjectsService.GetUsers()
            .then(r => {

                var users = r.map(k => ({ id: k.id, name: k.fullName }))
                this.users = users
            })
            .catch(e => console.error('getusers', e))
            .then(() => {

            })

    }
})








