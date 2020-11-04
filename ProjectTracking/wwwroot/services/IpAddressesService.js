
const IP_ADDRESS_SERVICE_URI = (method) => `/IpAddresses/${method}`;

const IpAddressesService = {
    Create: function (model) {

        const url = IP_ADDRESS_SERVICE_URI(`Create`)

        return axios.post(url, model);


        //let data = new FormData();

        //data.append('address', address);
        //data.append('title', title);

        //let url = IP_ADDRESS_SERVICE_URI('create')
        //let payload = BASIC_AJAX_PAYLOAD(url, 'post', data);

        //return $.ajax(payload);
    },
    Delete: function (address) {

        const query = serialize({ address })

        const url = IP_ADDRESS_SERVICE_URI(`Delete?${query}`)

        return axios.delete(url);

        //let data = new FormData();
        //data.append('id', id);

        //let url = IP_ADDRESS_SERVICE_URI('delete')
        //let payload = BASIC_AJAX_PAYLOAD(url, 'post', data);

        //return $.ajax(payload);
    },
    Update: function (model) {


        const url = IP_ADDRESS_SERVICE_URI(`Update`)

        return axios.post(url, model);


        //let data = new FormData();

        ////data.append('id', id);
        //data.append('address', address);
        //data.append('title', title);

        //let url = IP_ADDRESS_SERVICE_URI('update')
        //let payload = BASIC_AJAX_PAYLOAD(url, 'post', data);

        //return $.ajax(payload);
    },
    Save: function (model) {

        const url = IP_ADDRESS_SERVICE_URI(`Save`)

        return axios.post(url, model);

        //let data = new FormData();

        ////data.append('id', id);
        //data.append('address', address);
        //data.append('title', title);

        //let url = IP_ADDRESS_SERVICE_URI('update')
        //let payload = BASIC_AJAX_PAYLOAD(url, 'post', data);

        //return $.ajax(payload);
    },
    GetAll: function () {

        //const query = serialize({ categoryId, keyword, page, countPerPage })

        const url = IP_ADDRESS_SERVICE_URI(`GetAll`)

        return axios.get(url);


        //let url = IP_ADDRESS_SERVICE_URI('getall')
        //let payload = BASIC_AJAX_PAYLOAD(url, 'get');

        //return $.ajax(payload);
    },
    GetListed: function () {

        //const query = serialize({ categoryId, keyword, page, countPerPage })

        const url = IP_ADDRESS_SERVICE_URI(`GetListed`)

        return axios.get(url);


        //let url = IP_ADDRESS_SERVICE_URI('GetListed')
        //let payload = BASIC_AJAX_PAYLOAD(url, 'get');

        //return $.ajax(payload);
    },
    GetUnlistedIps: function () {

        //const query = serialize({ categoryId, keyword, page, countPerPage })

        const url = IP_ADDRESS_SERVICE_URI(`GetUnlistedIps`)

        return axios.get(url);

        //let url = IP_ADDRESS_SERVICE_URI('GetUnlistedIps')
        //let payload = BASIC_AJAX_PAYLOAD(url, 'get');

        //return $.ajax(payload);
    },
}

