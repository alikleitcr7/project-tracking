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
