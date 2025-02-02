"use strict";


var connection;

connection = new signalR.HubConnectionBuilder()
    .withUrl('/observer')
    .build();

connection.start().catch(function (err) {
    return console.error(err.toString());
})

connection.on('finished', (update) => {
    connection.stop();
});

connection.on('ReceiveMessage', (message) => {

    console.log({ message })

    //bootbox.alert(message, function () {
    //    //console.log("Alert Callback");
    //});
    //console.log('receive message',message);

    //alert('message from server', message);
});

connection.on('RefreshLogs', () => {

    console.log('refresh signal')

    if (homeApp) {
        homeApp.refreshSignal();
    }

});

connection.on('SessionEnd', (message) => {

    console.log('end session flag', message)

    sidebar_app.endSession(message || null)

});



