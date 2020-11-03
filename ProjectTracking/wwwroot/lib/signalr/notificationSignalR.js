"use strict";

var connection;

connection = new signalR.HubConnectionBuilder()
    .withUrl('/notificationshub')
    .build();


//var connection2 = new signalR.HubConnectionBuilder()
//    .withUrl('/broadcastshub')
//    .build();

//connection2.on('ReceiveBroadcast', (message) => {

//    alert('YAY!')
//    console.log('ReceiveBroadcast',{message})
//})


//connection2.start().catch(function (err) {
//    return console.error(err.toString());
//})

//connection2.on('finished', (update) => {
//    connection.stop();
//});


connection.on('ReceiveNotification', (message) => {

    console.log('received message', message)

    if (message) {

        setTimeout(() => {

            notify('New Notification from ' + message.fromUserDisplay)

            sidebar_app.hasNewNotification = true

            message.isNew = true

            const isToTeam = message.toTeamId ? true : false


            if (isToTeam) {

                let broadcasts = { ...notification_app.broadcasts }

                //console.log({ broadcasts})

                let data = [message,...broadcasts.data]
                broadcasts.data = data;
                //data.unshift(message)

                if (data.length > broadcasts.dataPaging.length) {

                    data.pop()
                }

                let dataPaging = { ...broadcasts.dataPaging };

                dataPaging.totalCount = dataPaging.totalCount + 1

                broadcasts.dataPaging = dataPaging

                //console.log({ broadcasts })

                notification_app.broadcasts = broadcasts
            }
            else {

                let notifications = [...notification_app.notifications]

                //console.log('not1',{ notifications })

                //message.isNew = true
                notifications.unshift(message)

                if (notifications.length > notification_app.dataPaging.length) {

                    notifications.pop()
                }

                //console.log('not2', { notifications })

                notification_app.notifications = notifications
                //sidebar_app.hasNewNotification = true

                let dataPaging = { ...notification_app.dataPaging };

                dataPaging.totalCount = dataPaging.totalCount + 1

                notification_app.dataPaging = dataPaging
            }

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

