"use strict";


var connection;

connection = new signalR.HubConnectionBuilder()
    .withUrl('/observer')
    .build();

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


connection.start().catch(function (err) {
    return console.error(err.toString());
})

connection.on('finished', (update) => {
    connection.stop();
});

