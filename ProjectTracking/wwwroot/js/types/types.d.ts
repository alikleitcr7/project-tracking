declare interface TimeSheet {
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



declare interface IClientResponseModel<T> {
    record: T;
    status: string;
    statusCode: number;
    message: string;
    internalMessage: string;
    extraData: string;
    isSuccess: boolean;
}

