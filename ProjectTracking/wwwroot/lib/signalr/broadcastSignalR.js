"use strict";

var connection2 = new signalR.HubConnectionBuilder()
    .withUrl('/broadcastshub')
    .build();

connection2.on('ReceiveBroadcast', (message) => {

    alert('YAY!')
    console.log('ReceiveBroadcast',{message})
})


connection2.start().catch(function (err) {
    return console.error(err.toString());
})

connection2.on('finished', (update) => {
    connection.stop();
});

