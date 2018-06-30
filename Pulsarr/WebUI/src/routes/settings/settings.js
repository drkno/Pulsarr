import React from 'react';
import { TabContainer, Tab } from '../../components/tabs';
import RawEditor from './raw';
import IndexerEditor from './indexers';
import DownloadClientEditor from './downloaders';
import MetadataEditor from './metadata';

export default ({match}) => {
    let selectedPage = '0';
    if (match.params.settingsPage) {
        selectedPage = [
            'metadata',
            'indexers',
            'downloadclient',
            'mediamanagement',
            'plex',
            'raw'
        ].indexOf(match.params.settingsPage.toLowerCase()).toString();
    }
    return (
        <TabContainer selectedTabIndex={selectedPage}>
            <Tab name='Metadata'>
                <MetadataEditor />
            </Tab>
            <Tab name='Indexers'>
                <IndexerEditor />
            </Tab>
            <Tab name='Download Client'>
                <DownloadClientEditor />
            </Tab>
            <Tab name='Media Management'>
            </Tab>
            <Tab name='Plex'>
            </Tab>
            <Tab name='Raw'>
                <RawEditor />
            </Tab>
        </TabContainer>
    );
};
