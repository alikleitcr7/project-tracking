


new Vue({
    el: '#Employees',
    data: {
        selectedDepartment: '',
        employees: [],
        departments: [],
        departmentsLoading: true,
        employeesLoading: false,
        searchKey: ''
    },
    methods: {
        expandEmployee: function (employee, index) {
            let oEmployee = { ...employee };

            oEmployee.isExpanded = !oEmployee.isExpanded;

            this.$set(this.employees, index, oEmployee);
        },
        expandAll: function () {
            console.log('expandin')
            let oEmployees = [...this.employees];

            for (var i = 0; i < oEmployees.length; i++) {


                oEmployees[i].isExpanded = true;

            }

            this.employees = oEmployees;
        },
        collapseAll: function () {
            let oEmployees = [...this.employees];

            for (var i = 0; i < oEmployees.length; i++) {
                oEmployees[i].isExpanded = false;
            }

            this.employees = oEmployees;
        }
    },
    watch: {
        searchKey: {
            handler: function (key) {

                console.log(key);
                for (var i = 0; i < this.employees.length; i++) {

                    if (!key) {
                        this.employees[i].isHidden = false;
                    }
                    else {
                        this.employees[i].isHidden = this.employees[i].fullName.indexOf(key) == -1;
                    }
                }
            }
        },
        selectedDepartment: {
            handler: function (newVal, oldVal) {
                this.employeesLoading = true;
                axios.get('Employees/GetEmployeesByDepartment?departmentId=' + newVal).then(
                    response => {
                        console.log(response.data);

                        this.employees = response.data;
                        this.employeesLoading = false;
                    }
                ).catch(e => {

                    this.employeesLoading = false;
                    console.error('err', e);
                })
            }
        }
    },
    mounted: function () {

        axios.get('Employees/GetDepartments').then(
            response => {
                console.log(JSON.stringify(response.data));
                this.departments = response.data;
                this.departmentsLoading = false;
            }
        ).catch(e => {

            this.departmentsLoading = false;
            console.error('err', e);
        })
    }
});


