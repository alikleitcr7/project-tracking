"use strict";

var connection;

connection = new signalR.HubConnectionBuilder()
    .withUrl('/notificationshub')
    .build();

connection.on('ReceiveNotification', (message) => {
    //console.log('received message', message)

    if (message) {

        setTimeout(() => {

            notify('New Notification from ' + message.fromUserDisplay)

            let notifications = [...notification_app.notifications]

            message.isNew = true
            notifications.unshift(message)

            if (notifications.length > notification_app.dataPaging.length) {

                notifications.pop()
            }

            notification_app.notifications = notifications
            sidebar_app.hasNewNotification = true

            let dataPaging = { ...notification_app.dataPaging };

            dataPaging.totalCount = dataPaging.totalCount + 1

            notification_app.dataPaging = dataPaging

        }, 100)
    }

    //bootbox.alert(message, function () {
    //    //console.log("Alert Callback");
    //});
    //console.log('receive message',message);

    //alert('message from server', message);
});


connection.start().catch(function (err) {
    return console.error(err.toString());
})

connection.on('finished', (update) => {
    connection.stop();
});

