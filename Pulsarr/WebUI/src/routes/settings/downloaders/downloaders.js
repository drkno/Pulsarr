import React from 'react';
import Switch from 'rc-switch';
import CardPreferences from '../../../components/carditems';
import './downloaders.css';

const clients = [
    {
        id: 0,
        name: 'Transmission',
        badges: ['Torrent'],
        url: 'https://torrent.somewhere.co.nz'
    },
    {
        id: 1,
        name: 'Sabnzbd',
        badges: ['Usenet'],
        url: 'https://nzb.somewhere.co.nz'
    },
    {
        id: 2,
        name: 'Sabnzbd',
        badges: ['Usenet'],
        url: 'https://nzb.somewhere.co.nz'
    },
    {
        id: 3,
        name: 'Sabnzbd',
        badges: ['Usenet'],
        url: 'https://nzb.somewhere.co.nz'
    },
    {
        id: 4,
        name: 'Sabnzbd',
        badges: ['Usenet'],
        url: 'https://nzb.somewhere.co.nz'
    }
];

const newClients = [
    {
        name: 'Transmission',
        badges: ['Torrent'],
        preferences: [
        ]
    },
    {
        name: 'Sabnzbd',
        badges: ['Usenet'],
        preferences: [
        ]
    }
];

class Downloaders extends React.Component {
    render() {
        return (
            <div>
                <br />
                <h4>Download Clients</h4>
                <hr />
                <CardPreferences
                    items={clients}
                    newItems={newClients}
                    newName='Download Client'
                    newDescription='Add a new download client such as Transmission or Sabnzbd.' />
                <h4>Global Options</h4>
                <hr />
                <span><Switch />&nbsp;Remove On Completion</span>
            </div>
        );
    }
}

export default Downloaders;
