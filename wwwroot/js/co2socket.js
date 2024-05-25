var sender;
$(function () {
    var protocol = location.protocol === "https:" ? "wss:" : "ws:";
    var wsUri = protocol + "//" + window.location.host;
    var socket = new WebSocket(wsUri);
    socket.onopen = e => {
        console.log("socket opened", e);
    };

    socket.onclose = function (e) {
        console.log("socket closed", e);
    };

    socket.onmessage = function (e) {
        console.log(e);
        $("#powerstream").append('<li class="list-group-item">' + e.data + '</li>');
    };

    socket.onerror = function (e) {
        console.error(e.data);
    };

    sender = function (message) {
        socket.send(message);
    }
   
});