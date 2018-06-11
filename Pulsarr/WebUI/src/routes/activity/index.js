import React from 'react';
import { Route, Redirect } from 'react-router-dom';

import Activity from './activity';

export default [
    <Route path='/activity/:activityType' component={Activity} />,
    <Route path='/activity' component={Activity} />
];