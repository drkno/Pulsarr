//import signalr from '@aspnet/signalr';
//
//class WebsocketConnection {
//    _url = null;
//    _connection = null;
//    _connectionEvents = {connect: [], disconnect: []};
//
//    constructor(url) {
//        this._url = url;
//        this.onDisconnect(null);
//    }
//
//    async onDisconnect(e) {
//        for (let listener of this._connectionEvents.disconnect) {
//            listener(e);
//        }
//        this._connection = new signalR.HubConnectionBuilder()
//        .withUrl(this._url)
//        .configureLogging(signalR.LogLevel.Information)
//        .build();
//
//        this._connection.onclose(e => this.onDisconnect(e));
//        await this._connection.start();
//        for (let listener of this._connectionEvents.disconnect) {
//            listener(e);
//        }
//    }
//
//    async emit(eventName, ...args) {
//        return await this._connection.invoke(eventName, ...args);
//    }
//
//    on(eventName, callback) {
//        if (eventName === 'connect' || eventName === 'disconnect') {
//            this.__connectionEvents.connect.push(callback);
//        }
//        else {
//            this._connection.on(eventName, callback);
//        }
//    }
//
//    off(eventName, callback) {
//        if (eventName === 'connect' || eventName === 'disconnect') {
//            this.__connectionEvents.connect.push(callback);
//        }
//        else {
//            this._connection.off(eventName, callback);
//        }
//    }
//}
//
//export default new WebsocketConnection('/ws');
