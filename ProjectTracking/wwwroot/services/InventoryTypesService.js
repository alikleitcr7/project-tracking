const INVENTORY_TYPES_SERVICE_URI = (method) => `/InventoryTypes/${method}`;

const InventoryTypesService = {
    Create: function (name) {

        let data = new FormData();
        data.append('name', name);

        let url = INVENTORY_TYPES_SERVICE_URI('create')
        let payload = BASIC_AJAX_PAYLOAD(url, 'post', data);

        return $.ajax(payload);
    },
    Delete: function (id) {

        let data = new FormData();
        data.append('id', id);

        let url = INVENTORY_TYPES_SERVICE_URI('delete')
        let payload = BASIC_AJAX_PAYLOAD(url, 'post', data);

        return $.ajax(payload);
    },
    Update: function (id, name) {

        let data = new FormData();

        data.append('id', id);
        data.append('name', name);

        let url = INVENTORY_TYPES_SERVICE_URI('update')
        let payload = BASIC_AJAX_PAYLOAD(url, 'post', data);

        return $.ajax(payload);
    },
    GetAll: function () {

        let url = INVENTORY_TYPES_SERVICE_URI('getall')
        let payload = BASIC_AJAX_PAYLOAD(url, 'get');

        return $.ajax(payload);
    },
}