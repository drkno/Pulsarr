import React from 'react';
import { TabContainer, Tab } from '../../components/tabs';
import DownloadActivity from './downloads';
import HistoryActivity from './history';

const Activity = ({ match }) => {
    let activeTab = '0';
    if (match.params.activityType === 'history') {
        activeTab = '1';
    }
    return (
        <TabContainer selectedTabIndex={activeTab}>
            <Tab name='Downloads'>
                <DownloadActivity />
            </Tab>
            <Tab name='History'>
                <HistoryActivity />
            </Tab>
        </TabContainer>
    );
};

export default Activity;
