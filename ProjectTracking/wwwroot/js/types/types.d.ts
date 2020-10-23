﻿declare interface TimeSheet {
    id: number;
    fromDate: Date;
    toDate: Date;
    dateAdded: Date;
    userId: null;
    timeSheetStatus: Array<TimeSheetStatus>;
}

declare interface TimeSheetActivity {
    id: number;
    fromDate: Date;
    toDate: Date;
    comment: string;
    timeSheetId: number;
    timeSheetProjectId: number;
    ipAddress: string;
}

declare interface TimeSheetStatus {
    id: number;
    isApproved: number;
    comments: string;
    timeSheetId: number;
    superviserId: string;
    superVisor: User;
}
declare interface User {
    id: string;
    email: string;
    userName: string;
    firstName: string;
    lastName: string;
    fullName: string;
    dateOfBirth: Date;
    department: Department;
    company: Company

}
declare interface Department {
    id: number;
    name: string;
    nameDisplay: string;
}

declare interface Company {
    id: number;
    name: string;
}

declare interface IProjectTask {
    id: number;
    title: string;
    description: string;
    dateAdded: Date;
    projectId: string;
    statusCode: string;
    startDate: Date;
    plannedEnd: Date;
    actualEnd: Date;
    lastModifiedDate: Date;

    startDateDisplay: string;
    plannedEndDisplay: string;
    actualEndDisplay: string;
    lastModifiedDateDisplay: string;

    statusByUserId: string;
    statusByUserName: string;

    timeSheetTaskId: number;
    numberOfActivities: number;


}

declare interface ITaskPerformance {
    totalCount: number;
    doneCount: number;
    progressCount: number;
    pendingCount: number;
    failedOrTerminatedCount: number;
}

declare class TasksWorkload{
    doneCount: number;
    progressCount: number;
    pendingCount: number;
}

declare interface KeyValuePair<T1, T2> {
    key: T1;
    value: T2;
}

declare class SupervisingTeamModel {

    projectsCount: number;
    membersCount: number;
    tasksPerformance: ITaskPerformance;
    members: Array<KeyValuePair<string, string>>;
    activitiesFrequency: Array<KeyValuePair<Date, number>>;
}

declare class TeamViewModel extends SupervisingTeamModel {

    workload: Array<KeyValuePair<UserKeyValuePair, TasksWorkload>>;
    activeActivities: Array<IActiveActivity>;
}

declare class UserKeyValuePair  {
    id: string;
    name: string;
}

declare interface IActiveActivity {
    id: number;
    fromDate: Date;
    fromDateDisplay: string;
    projectTask: IProjectTask;
    user: User;
}


declare interface IClientResponseModel<T> {
    record: T;
    status: string;
    statusCode: number;
    message: string;
    internalMessage: string;
    extraData: string;
    isSuccess: boolean;
}

