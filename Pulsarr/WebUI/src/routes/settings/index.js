import React from 'react';
import { Route } from 'react-router-dom';
import SettingsContainer from './settings';

export default [
    <Route path='/settings/:settingsPage' component={SettingsContainer} />,
    <Route path="/settings" component={SettingsContainer}/>
];