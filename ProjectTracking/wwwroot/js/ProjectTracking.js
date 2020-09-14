
const GetProjectWithActivitiesFilterOptions = (projectId, year, month) => {

    let url = `/Projects/GetProjectWithActivitiesFilterOptions?projectId=${projectId}&year=${year}&month=${month}`

    let payload = BASIC_AJAX_PAYLOAD(url, 'get');

    return $.ajax(payload);
}


new Vue({
    el: '#ProjectTrack',
    data: {
        dateFilterOptions: {},
        year: null,
        month: null,
        day: null,
        isLoading: true
    },
    methods: {
        onFilterChange: function (event) {

            const { name, value } = event.target
            const projectId = getParam('projectId')


            this.isLoading = true

            switch (name) {
                case 'year':
                    GetProjectWithActivitiesFilterOptions(projectId, value)
                        .done(r => {
                            //console.log('GetProjectWithActivitiesFilterOptions', r)
                            let dateFilterOptions = { ...this.dateFilterOptions }

                            dateFilterOptions.months = r.months
                            dateFilterOptions.days = []

                            this.dateFilterOptions = dateFilterOptions
                        })
                        .fail(e => {
                            console.error('GetProjectWithActivitiesFilterOptions', e)
                        })
                        .always(() => {
                            this.isLoading = false
                        })

                    break;
                case 'month':

                    GetProjectWithActivitiesFilterOptions(projectId, this.year, value)
                        .done(r => {
                            //console.log('GetProjectWithActivitiesFilterOptions', r)
                            let dateFilterOptions = { ...this.dateFilterOptions }

                            dateFilterOptions.days = r.days

                            this.dateFilterOptions = dateFilterOptions  
                        })
                        .fail(e => {
                            console.error('GetProjectWithActivitiesFilterOptions', e)
                        })
                        .always(() => {
                            this.isLoading = false
                        })

                    break;
                default:
                    break;
            }
        }
    },
    mounted: function () {

        const projectId = getParam('projectId')
        const year = getParam('year')
        const month = getParam('month')
        const day = getParam('day')


        this.year = year ? year : null
        this.month = month ? month : null
        this.day = day ? day : null

        GetProjectWithActivitiesFilterOptions(projectId, year, month)
            .done(r => {
                //console.log('GetProjectWithActivitiesFilterOptions', r)
                this.dateFilterOptions = r
            })
            .fail(e => {
                console.error('GetProjectWithActivitiesFilterOptions', e)
            })
            .always(() => {
                this.isLoading = false
            })
    }
})