declare interface TimeSheet {
    id: number;
    fromDate: Date;
    toDate: Date;
    dateAdded: Date;
    userId: null;
    projects: Array<TimeSheetProjectModelProject>,
}

declare interface BasicObject {
    id: number;
    name: string;
}

declare interface ProjectFile {
    id: number;
    name: string;
    projectId: number;
}

declare interface TimeSheetActivity {

    id: number;
    fromDate: Date;
    toDate: Date;
    comment: string;
    number: number;
    ipAddress: string;
    fromDateDisplay: string;
    toDateDisplay: string;
    tagDisplay: string;
    tagDisplayHours: string;
    typeOfWorkDisplay: string;
    measurementUnitDisplay: string;
    projectFileDisplay: string;
    timeSheetProjectId: number;
    typeOfWorkId: number;
    measurementUnitId: number;
    projectFileId: number;
    projectFile: ProjectFile;
    timeSheetProject: TimeSheetProject;
    typeOfWork: BasicObject;
    measurementUnit: BasicObject;

}

declare interface TimeSheetProject {
    projectId: number;
    timeSheetId: number;
}

declare interface Project {
    id: number;
    title: string;
    description: string;
    dateAdded: Date;

    parentId: number;
    departmentId: number;

    parent: Project;

    activities: Array<Project>;
    timeSheetProjects: Array<TimeSheetProject>;
}

declare interface TimeSheetProjectModelProject {
    id: number;
    title: string;
    description: string;
    subProjects: Array<TimeSheetProjectModelSubProject>;
}

declare interface TimeSheetProjectModelSubProject {
    id: number;
    projectId: number;
    parentId: number;
    title: string;
    description: string;
    activities: Array<TimeSheetActivity>;
}

declare interface TimeSheetProjectModel {
    projects: Array<TimeSheetProjectModelProject>;
}

declare interface UserTimeSheetsApp {
    timesheets: Array<TimeSheet>,
    timesheetsAreLoading: Boolean,
    activeTimeSheet: TimeSheet,
    activeTimeSheetLoading: Boolean,
    filteredProjects: Array<TimeSheetProjectModelProject>,
    filteredProjectsLoading: Boolean,
    activeDate: Date
}


declare interface ActiveActivity {
    activity: TimeSheetActivity;
    tagIndex: number;
    projectIndex: number;
    subProjectIndex: number;
}

declare interface ActivityModalForm {
    id: number;
    timeSheetTaskId: number;
    fromDate: Date;
    toDate: Date;
    message: string;
}

declare interface ActivityModalObject {
    title: string,
    data: Array<TimeSheetActivity>,
    form: ActivityModalForm,
    backupEdit: ActivityModalForm,
    isLoading: Boolean,
    isDeleting: Boolean,
    isSaving: Boolean,
    message: string
}
