
const PROJECT_FILES_SERVICE_URI = (method) => `/ProjectFiles/${method}`;

const ProjectFilesService = {
    Create: function(name, projectId) {

        let data = new FormData();
        data.append('name', name);
        data.append('projectId', projectId);


        let url = PROJECT_FILES_SERVICE_URI('create')
        let payload = BASIC_AJAX_PAYLOAD(url, 'post', data);

        return $.ajax(payload);
    },
    Delete: function(id) {

        let data = new FormData();
        data.append('id', id);

        let url = PROJECT_FILES_SERVICE_URI('delete')
        let payload = BASIC_AJAX_PAYLOAD(url, 'post', data);

        return $.ajax(payload);
    },
    Update: function(id, name) {

        let data = new FormData();

        data.append('id', id);
        data.append('name', name);

        let url = PROJECT_FILES_SERVICE_URI('update')
        let payload = BASIC_AJAX_PAYLOAD(url, 'post', data);

        return $.ajax(payload);
    },
    GetAll: function() {

        let url = PROJECT_FILES_SERVICE_URI('getall')
        let payload = BASIC_AJAX_PAYLOAD(url, 'get');

        return $.ajax(payload);
    },
    Search: function(keyword, projectId) {

        let url = PROJECT_FILES_SERVICE_URI(`Search?keyword=${keyword}&projectId=${projectId}` )
        let payload = BASIC_AJAX_PAYLOAD(url, 'get');

        return $.ajax(payload);
    },
    GetByProject: function(projectId) {

        let url = PROJECT_FILES_SERVICE_URI(`getbyproject?projectId=${projectId}`)
        let payload = BASIC_AJAX_PAYLOAD(url, 'get');

        return $.ajax(payload);
    },
}