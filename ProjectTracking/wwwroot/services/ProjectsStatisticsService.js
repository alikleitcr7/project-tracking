
const PROJECTS_STATISTICS_SERVICE_URI = (method) => `/Projects/${method}`;

const ProjectsStatisticsService = {
    GetProjectsProgress: function (filterByYear, filterByYearAndMonth,userId) {
        let url = PROJECTS_STATISTICS_SERVICE_URI(`GetProjectsProgress?byYear=${filterByYear}&byYearAndMonth=${filterByYearAndMonth}&userId=${userId}`)
        return axios.get(url);
        
    },
    GetMeasurementUnitsTotalProgress: function (userId,year,month,day) {
        let url = PROJECTS_STATISTICS_SERVICE_URI(`GetMeasurementUnitsTotalProgress?userId=${userId}&year=${year}&month=${month}&day=${day}`)
        return axios.get(url);
    },
    GetProjectProgressYears: function (userId) {
        let url = PROJECTS_STATISTICS_SERVICE_URI(`GetProjectProgressYears?userId=${userId}`)
        return axios.get(url);
    },
    
    GetProjectProgressMonthsByYear: function (userId,year) {
        let url = PROJECTS_STATISTICS_SERVICE_URI(`GetProjectProgressMonthsByYear?year=${year}&userId=${userId}`)
        return axios.get(url);
    },

    GetProjectProgressDaysByMonthAndYear: function (userId,year,month) {
        let url = PROJECTS_STATISTICS_SERVICE_URI(`GetProjectProgressDayByMonthAndYear?year=${year}&month=${month}&userId=${userId}`)
        return axios.get(url);
    },
   

    //AddTimeSheet: function (timeSheet) {
    //    return axios.post('/TimeSheets/Add', timeSheet);
    //}
}