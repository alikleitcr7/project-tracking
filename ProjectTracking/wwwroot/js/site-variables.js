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


var colors = {
    mainLight: '#5bcbee',
    main: '#2286c3',
    pending: '#f78438',
    done: '#3fcc35',
    progress: '#4b59ff',
    failed: '#bb0000',
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