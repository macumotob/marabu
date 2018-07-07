var ws;
function run_web_socket() {

    // ws = new WebSocket('ws://192.168.1.3:13003/chat');
    ws = new WebSocket('ws://##ADRESS##:##PORT##/chat');

    // Log messages from the server
    ws.onmessage = function (e) {
        var res = JSON.parse(e.data);
        if (res.cmd == "Hi") {
            id("label_info").innerText = res.user;
        }
        console.log(e);
    };
    ws.onerror = function (e) {
        console.log("error:");
        console.log(e);
    };
    ws.onopen = function () {
        ws.send(JSON.stringify({ cmd: 'Hi', user: 'buba' }));
        console.log("Connection opened...");
    };

    // второй - когда соединено закроется
    ws.onclose = function () { console.log("Connection closed...") };
    //  CONNECTION.send('Hellow World');
}
run_web_socket();
