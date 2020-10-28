const APP_USER_ROLES = {
    teamMember: { key: 0, value: 'Team Member' },
    supervisor: { key: 1, value: 'Supervisor' },
    admin: { key: 2, value: 'Admin' },
    _toList: function () {
        return [
            { ...this.teamMember },
            { ...this.supervisor },
            { ...this.admin },
        ]
    }
}
//Pending, InProgress, Done, Failed, Terminated
const PROJECT_TASK_STATUS = {
    pending: { key: 0, value: 'Pending', code: 'pending' },
    inProgress: { key: 1, value: 'In Progress', code: 'progress' },
    done: { key: 2, value: 'Done', code: 'done' },
    failed: { key: 3, value: 'Failed', code: 'failed' },
    terminated: { key: 4, value: 'Terminated', code: 'failed' },
    _toList: function () {
        return [
            { ...this.pending },
            { ...this.inProgress },
            { ...this.done },
            { ...this.failed },
            { ...this.terminated },
        ]
    }
}

var colors = {
    mainLight: '#5bcbee',
    mainLightTransparent: 'rgba(91, 203, 238, 0.66)',
    main: '#2286c3',
    mainDark: '#1776b0',
    pending: 'rgba(247, 132, 56, 0.95)',
    done: 'rgba(63, 204, 53, 0.86)',
    progress: 'rgba(75, 89, 255, 0.84)',
    failed: 'rgba(187, 0, 0, 0.70)',
}


function initTasksPerformanceProgress() {
    return [
        {
            'fromProp': 'doneCount',
            'code': 'done',
            'name': 'Done',
        },
        {
            'fromProp': 'progressCount',
            'code': 'progress',
            'name': 'In Progress'
        },
        {
            'fromProp': 'pendingCount',
            'code': 'pending',
            'name': 'Pending'
        },
        {
            'fromProp': 'failedOrTerminatedCount',
            'code': 'failed',
            'name': 'Failed/Terminated'
        }
    ]
}