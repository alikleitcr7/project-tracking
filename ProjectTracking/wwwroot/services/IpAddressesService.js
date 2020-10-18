
const IP_ADDRESS_SERVICE_URI = (method) => `/IpAddresses/${method}`;

const IpAddressesService = {
    Create: function (address,title) {

        let data = new FormData();

        data.append('address', address);
        data.append('title', title);

        let url = IP_ADDRESS_SERVICE_URI('create')
        let payload = BASIC_AJAX_PAYLOAD(url, 'post', data);

        return $.ajax(payload);
    },
    Delete: function (id) {

        let data = new FormData();
        data.append('id', id);

        let url = IP_ADDRESS_SERVICE_URI('delete')
        let payload = BASIC_AJAX_PAYLOAD(url, 'post', data);

        return $.ajax(payload);
    },
    Update: function ( address, title) {

        let data = new FormData();

        //data.append('id', id);
        data.append('address', address);
        data.append('title', title);

        let url = IP_ADDRESS_SERVICE_URI('update')
        let payload = BASIC_AJAX_PAYLOAD(url, 'post', data);

        return $.ajax(payload);
    },
    GetAll: function () {

        let url = IP_ADDRESS_SERVICE_URI('getall')
        let payload = BASIC_AJAX_PAYLOAD(url, 'get');

        return $.ajax(payload);
    },
    GetListed: function () {

        let url = IP_ADDRESS_SERVICE_URI('GetListed')
        let payload = BASIC_AJAX_PAYLOAD(url, 'get');

        return $.ajax(payload);
    },
    GetUnlistedIps: function () {

        let url = IP_ADDRESS_SERVICE_URI('GetUnlistedIps')
        let payload = BASIC_AJAX_PAYLOAD(url, 'get');

        return $.ajax(payload);
    },
}

