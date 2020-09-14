var editUserApp=new Vue({
    el: '#CrudUsers',
    data: {
        monthlySalary:0,nbOfHoursPerDay:0
    },
    computed: {
        hourlyRate: {
            // getter
            get: function () {
                return Math.round((this.monthlySalary / (22 * this.nbOfHoursPerDay))*100)/100;
            }
        },
    },
    methods: {

    },
    mounted: function () {
        this.nbOfHoursPerDay = $("#HoursPerDayHidden").val()
        this.monthlySalary = $("#SalaryHidden").val()
    }

});
(function () {
    $("#EmployementTypeSelect").on('change', function () {
        var EmployementTypeSelectValue = $(this).val();
        if (EmployementTypeSelectValue == 0)
            editUserApp.nbOfHoursPerDay = 8.5
        else if (EmployementTypeSelectValue == 1)
            editUserApp.nbOfHoursPerDay = 6
        else
            editUserApp.nbOfHoursPerDay = $("#HoursPerDayHidden").val()
    })
    // do jQuery
})(jQuery)