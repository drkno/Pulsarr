import React from 'react';
//import WebSocketConnection from './signalr';

class WebSocketComponent extends React.Component {
//    _eventRegistry = {};
//
//    on(event, callback) {
//        const cb = callback.bind(this);
//        if (!_eventRegistry[event]) {
//            _eventRegistry[event] = [cb];
//        }
//        else {
//            _eventRegistry[event].push(cb);
//        }
//        WebSocketConnection.on(event, callback.bind(this));
//    }
//
//    async emit(event, ...data) {
//        return await WebSocketConnection.emit(event, ...data);
//    }
//
//    componentWillUnmount() {
//        for (let event in this._eventRegistry) {
//            for (let cb of this._eventRegistry[event]) {
//                WebSocketConnection.off(event, cb);
//            }
//        }
//    }
}

export default WebSocketComponent;
