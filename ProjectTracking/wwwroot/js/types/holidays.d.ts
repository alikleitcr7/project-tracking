declare interface IHoliday {
    id: number;
    title: string;
    note: string;
    date: Date;
}

declare interface IHolidayGetAllPaged {
    records: Array<IHoliday>;
    totalCount: number
}

