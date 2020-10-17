//#region Document
$(document).ready(function () {
    let dataTables_en = $('[data-custom-dt=en]');

    let dateTimePicker = $('.datetimepicker');
    let dateTimePicker_time = $('.datetimepicker-time');

    if (dateTimePicker.length) {
        dateTimePicker.datetimepicker({
            format: 'YYYY-MM-DD',
            maxDate: moment(),
        });
    }

    if (dateTimePicker_time.length) {
        dateTimePicker_time.datetimepicker({
            format: 'YYYY-MM-DD HH:mm:ss',
        });
    }
    if (dataTables_en.length) {
        dataTables_en.DataTable(dtOptions_TopControls_en);
    }
});

$(document).on('click', '.side-bar-arrow', function () {

    var sidebar = $('body');

    const collapsed = 'sidebar-collapsed'

    if (sidebar.hasClass(collapsed)) {
        sidebar.removeClass(collapsed)
    }
    else {
        sidebar.addClass(collapsed)
    }

});

//#endregion

//#region Extensions
const dateOptions = {
    format: 'YYYY-MM-DD',
}

const dateTimeOptions = {
    format: 'YYYY-MM-DD HH:mm:ss',

}


var dtOptions = {
    pageLength: 20,
    paging: true,
    info: false,
    order: false,
    search: false,
    ordering: false,
    language: {
        search: "ابحث في الجدول"
        ,
        paginate: {
            previous: 'السابق',
            next: 'التالي'
        },

        emptyTable: 'لا يوجد معلومات في الجدول',
        zeroRecords: 'لا يوجد نتائج للبحث'
    },
    lengthChange: false
};

let dtOptions_TopControls = JSON.parse(JSON.stringify(dtOptions));
let dtOptions_TopControls_en = JSON.parse(JSON.stringify(dtOptions));

dtOptions_TopControls.pageLength = 10;
dtOptions_TopControls.dom = '<"top"lfip>';
dtOptions_TopControls_en.dom = '<"top"flip>';
dtOptions_TopControls_en.language.search = "Search";
dtOptions_TopControls_en.language.paginate.previous = "Previous";
dtOptions_TopControls_en.language.paginate.next = "Next";
dtOptions_TopControls_en.language.emptyTable = "No Data Available";
dtOptions_TopControls_en.language.zeroRecords = "No Results";

const BASIC_AJAX_PAYLOAD = (url, method, data) => {

    let obj = {
        url,
        method,
        contentType: false,
        processData: false,
    }

    if (data) {
        obj['data'] = data
    }

    return obj;
}

//check your connection and
const BASIC_ERROR_MESSAGE = 'Something went wrong, try again!'
const BASIC_INTERNAL_ERROR_MESSAGE = 'An Error Occured, Kindly Contact System Admin.'

//#endregion 

//#region Other Functions

const serialize = (obj) => {
    var str = [];
    for (var p in obj)
        if (obj.hasOwnProperty(p)) {
            str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p] === null || obj[p] === undefined ? '' : obj[p]));
        }
    return str.join("&");
}

function onlyUnique(value, index, self) {
    return self.indexOf(value) === index;
}

function openTab(tab) {
    $('.nav-tabs a[href="#' + tab + '"]').tab('show');
};


Array.prototype.pushArray = function () {
    this.push.apply(this, this.concat.apply([], arguments));
};

const getParam = (key) => {
    var url_string = window.location.href
    var url = new URL(url_string);

    return url.searchParams.get(key);
}

function isNumber(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}

var tableToExcel = (function () {
    var uri = 'data:application/vnd.ms-excel;base64,'
        , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
        , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
        , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
    return function (table, name, filename) {
        if (!table.nodeType) table = document.getElementById(table)
        var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }

        document.getElementById("dlink").href = uri + base64(format(template, ctx));
        document.getElementById("dlink").download = filename;
        document.getElementById("dlink").click();
    }
})()


const DATA_TYPES = {
    TEXT: 0,
    NUMBER: 1,
    DATE: 2,
    OBJECT: 3,
    ARRAY: 4
}

class CoreValidator {
    constructor(name, value, isRequired = true, dataType, min = undefined, max = undefined, displayName = undefined) {
        this.name = name
        this.displayName = displayName
        this.value = value
        this.min = min
        this.max = max
        this.dataType = dataType
        this.isRequired = isRequired
    }

    validate() {
        switch (this.dataType) {
            case DATA_TYPES.NUMBER:
                return this.number()
            case DATA_TYPES.TEXT:
                return this.text()
            case DATA_TYPES.OBJECT:
                return this.object()
            case DATA_TYPES.ARRAY:
                return this.arrayValidation()
            default:
                return true;
        }
    }

    text() {
        const { value, min, max, isRequired } = this

        const valid_criteria_1 = isRequired ? this.hasValue(value) : true
        const valid_criteria_2 = this.hasValue(value) && value.length >= min && value.length <= max

        return valid_criteria_1 || valid_criteria_2
    }

    number() {
        const { value, min, max, isRequired } = this

        console.log({ value, min, max, isRequired })

        if (isNaN(value) || (value == null && isRequired )) {
            return false
        }

        if (isRequired) {

            // check min max
            if (this.hasValue(min) && this.hasValue(max)) {
                return value >= min && value <= max
            }

            // check min 
            if (this.hasValue(min)) {
                return value >= min
            }

            // check max
            if (this.hasValue(max)) {
                return value <= max
            }

            // any nb allowed
            return true
        }

        // not required?
        return true;

        //const valid_criteria_1 = isRequired ? this.hasValue(value) : true
        //const valid_criteria_2 = this.hasValue(value) && !isNaN(value) && value >= min && (this.hasValue(max) ? value <= max : true)

        //return valid_criteria_1 || valid_criteria_2
    }

    object() {
        const { value, min, max, isRequired } = this

        const valid_criteria_1 = isRequired ? this.hasValue(value) : true
        const valid_criteria_2 = this.hasValue(value)

        return valid_criteria_1 || valid_criteria_2
    }

    arrayValidation() {

        const { value, min, max, isRequired } = this

        if (!Array.isArray(value)) {
            return false
        }

        if (!isRequired) {
            return true
        }

        const len = value.length

        let isValid = true;

        if (this.hasValue(min) && len < min) {
            isValid = false
        }

        if (this.hasValue(max) && len > max) {
            isValid = false
        }

        return isValid
    }

    email() {
        return validateEmail(this.value)
    }

    hasValue(value) {

        return !(value === undefined || value === null)
    }

    message() {

        const { name, displayName, value, dataType, min, max, isRequired } = this

        const min_hasValue = this.hasValue(min)
        const max_hasValue = this.hasValue(max)

        let finalText = `${displayName || name}`

        if (isRequired) {
            finalText += ' is required'
        }

        if (min_hasValue && max_hasValue) {
            switch (dataType) {
                case DATA_TYPES.NUMBER:
                    finalText += ` value should be between (${min} and ${max})`

                    break;
                case DATA_TYPES.TEXT:
                    finalText += ` length should be between (${min} and ${max})`

                    break;
                case DATA_TYPES.ARRAY:

                    finalText += ` you must choose between (${min} and ${max}) ${finalText}`
                    break;
                default:
                    break
            }
        }
        else {

            const requiredPrefix = isRequired ? ' and ' : ' '

            switch (dataType) {
                case DATA_TYPES.NUMBER:
                    finalText += `${requiredPrefix}should be a number`
                default:
                    break
            }
        }

        return finalText

    }
}

function getAxiosErrorMessage(e) {
    if (e.response) {
        if (e.response.status === 500) {
            console.log('internal error', e.response.data)
            return 'Internal Error Occured'
        }

        if (e.response.status === 400) {
            return e.response.data ? (e.response.data.message ? e.response.data.message : e.response.data) : 'Bad request'
        }
    }

    return 'Something went wrong, check your connection and try again!.'
}

//#endregion 

//#region Moment Extensions

function calculateBusinessDays(d1, d2, holidays) {

    const days = d2.diff(d1, "days") + 1;

    let newDay = d1.toDate(),
        workingDays = 0,
        sundays = 0,
        hours = 0,
        minutes = 0,
        holidaysFound = [],
        saturdays = 0;

    const isHoliday = (newDay, holidayDate) => {
        return moment(newDay, 'YYYY-MM-DD').isSame(moment(holidayDate, 'YYYY-MM-DD'))
    }

    for (let i = 0; i < days; i++) {
        const day = newDay.getDay();
        newDay = d1.add(1, "days").toDate();
        const isWeekend = ((day % 6) === 0);
        if (!isWeekend) {

            if (holidays && holidays.length) {

                const holiday = holidays.find(k => isHoliday(newDay, k))

                console.log({ holiday })

                if (holiday) {
                    holidaysFound.push(holiday)
                }
                else {
                    workingDays++;
                }
            }
            else {

                workingDays++;
            }
        }
        else {
            if (day === 6) saturdays++;
            if (day === 0) sundays++;
        }
    }

    //console.log("Total Days:", days, "workingDays", workingDays, "saturdays", saturdays, "sundays", sundays, 'holidays',holidays);

    return {
        days,
        workingDays,
        saturdays,
        sundays,
        holidays,
    };
}

//#endregion

//#region Custom VueJs Components (Requires Vue js)
Vue.component('basic-object-modal', {
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
                                    <button v-show="!item.isLocked && !item.isEdit" class="btn btn-sm btn-danger" type="button" v-on:click="$emit('delete-click',idx)">Delete</button>
                                </td>
                                <td>
                                    {{item.isLocked ? 'Item is Active' : ''}}
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

const basicModalObject = () => {
    return {
        name: '',
        data: [],
        form: {
            name: '',
        },
        backupEdit: {
            name: '',
        },
        isLoading: false,
        message: ''
    }
}
//#endregion 

//#region Notify (Requires Notify Js)
$.notify.addStyle('shared-notification', {
    html: "<div><span data-notify-html/></div>",
    classes: {
        base: {
            "white-space": "nowrap",
            "background-color": "#b00000",
            "color": "#fff",
            "padding": "5px 20px",
            "border-radius": "15px",
            "font-size": '1.2em'
        },
        superblue: {
            "color": "white",
            "background-color": "blue"
        }
    }
});

const notify = (message) => {
    $.notify(message, { style: 'shared-notification', className: 'base', position: 'top right' });
}
//#endregion 

//#region bootbox

const bootboxExtension = {
    confirmDeletion: (title, message, cancelCallBack, okCallBack) => {
        bootbox.dialog({
            title,
            message,
            size: 'sm',
            className: `bootbox-danger`,
            buttons: {
                cancel: {
                    label: "Cancel",
                    className: 'btn-default',
                    callback: () => {
                        if (cancelCallBack) {
                            cancelCallBack();
                        }
                    }
                },
                ok: {
                    label: "Delete",
                    className: 'btn-danger',
                    callback: () => {
                        if (okCallBack) {
                            okCallBack();
                        }
                    }
                }
            }
        });
    }
}

//#endregion 

//#region Shared Requests (Requires Axios)
//const USERSLOGS_SERVICE_URI = (method) => `/UsersLogs/${method}`;

const GetRoles = () => {
    let url = `/Home/GetRoles`
    return axios.get(url);
}

//const UsersLogs = {

//    GetUsersLogs: function (page, countPerPage, fromDate, toDate) {

//        let url = USERSLOGS_SERVICE_URI(`GetUsersLogs?page=${page}&countPerPage=${countPerPage}&fromDate=${fromDate}&toDate=${toDate}`)
//        return axios.get(url);
//    },
//}
//#endregion 



