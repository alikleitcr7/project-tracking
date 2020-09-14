const INVENTORY_PROJECTS_SERVICE_URI = (method) => `/InventoryProjects/${method}`;

const InventoryProjectsService = {
    Add: function (project) {
        let url = INVENTORY_PROJECTS_SERVICE_URI('Add')

        //let payload = BASIC_AJAX_PAYLOAD(url, 'post', JSON.stringify({ project }));

        //payload.dataType = "json";
        //payload.contentType = "application/json";

        return axios.post(url, project);
        //return $.ajax(payload);
    },
    Delete: function (id) {

        let data = new FormData();
        data.append('id', id);

        let url = INVENTORY_PROJECTS_SERVICE_URI('delete')
        let payload = BASIC_AJAX_PAYLOAD(url, 'post', data);

        return $.ajax(payload);
    },
    Update: function (project) {

        let url = INVENTORY_PROJECTS_SERVICE_URI('update')

        return axios.post(url, project);
    },
    GetUsers: function () {
        let url = INVENTORY_PROJECTS_SERVICE_URI('GetUsers')
        let payload = BASIC_AJAX_PAYLOAD(url, 'get');

        return $.ajax(payload);
    },
    Get: function (id) {

        let data = new FormData();
        data.append('id', id);

        let url = INVENTORY_PROJECTS_SERVICE_URI('get')
        let payload = BASIC_AJAX_PAYLOAD(url, 'get', data);

        return $.ajax(payload);
    },
    GetAll: function (page, countPerPage) {
        console.log({ page, countPerPage })
        //let data = new FormData();
        //data.append('page', page);
        //data.append('countPerPage', countPerPage);

        let url = INVENTORY_PROJECTS_SERVICE_URI(`getall?page=${page}&countPerPage=${countPerPage}`)
        let payload = BASIC_AJAX_PAYLOAD(url, 'get');

        return $.ajax(payload);
    },
    Search: function (filter) {

        let url = INVENTORY_PROJECTS_SERVICE_URI('search')

        return axios.post(url, filter)
    }
}