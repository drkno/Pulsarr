import React from 'react';
import Switch from 'rc-switch';
import { Button, Form, FormGroup, Label, Col } from 'reactstrap';
import { BindSwitchPreference } from '../bindPreference';
import CardPreferences from '../../../components/carditems';
import SettingsLoader from './loading';
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
            {
                type: 'bool',
                name: 'Enabled',
                key: 'enabled',
                defaultValue: true
            },
            {
                type: 'text',
                name: 'Url',
                key: 'url',
                defaultValue: 'http://127.0.0.1'
            },
            {
                type: 'number',
                name: 'Port',
                key: 'port',
                defaultValue: '9091'
            },
            {
                type: 'text',
                name: 'Username',
                key: 'username',
                defaultValue: ''
            },
            {
                type: 'password',
                name: 'Password',
                key: 'password',
                defaultValue: ''
            }
        ]
    },
    {
        name: 'Sabnzbd',
        badges: ['Usenet'],
        preferences: [
            {
                type: 'bool',
                name: 'Enabled',
                key: 'enabled',
                defaultValue: true
            },
            {
                type: 'text',
                name: 'Url',
                key: 'url',
                defaultValue: 'http://127.0.0.1'
            },
            {
                type: 'number',
                name: 'Port',
                key: 'port',
                defaultValue: '8080'
            },
            {
                type: 'text',
                name: 'API Key',
                key: 'apikey',
                defaultValue: ''
            },
            {
                type: 'category',
                name: 'Category',
                key: 'category',
                defaultValue: ''
            }
        ]
    }
];

class Downloaders extends React.Component {
    state = {
        globalOptionsSavable: false,
        clients: []
    };

    globalOptionsSave = [];

    onPreferenceValues(values) {
        values.map(v => {
            const nc = Object.assign({}, newClients.find(n => n.name === v.name));
            return ({
                name: 'Transmission',
                badges: ['Torrent'],
                url: 'https://torrent.somewhere.co.nz'
            });
        });
    }

    onGlobalOptionsChange(save) {
        this.globalOptionsSave.push(save);
        this.setState({globalOptionsSavable: true});
    }

    saveGlobalOptions() {
        for (let s of this.globalOptionsSave) {
            s();
        }
        this.globalOptionsSave = [];
        this.setState({globalOptionsSavable: false});
    }

    render() {
        return (
            <div>
                <br />
                <h4>Download Clients</h4>
                <hr />
                <SettingsLoader
                    preferences={['downloadclient.']}
                    preferenceValues={v => this.onPreferenceValues(v)}>
                    <CardPreferences
                        items={this.state.clients}
                        newItems={newClients}
                        newName='Download Client'
                        newDescription='Add a new download client such as Transmission or Sabnzbd.' />
                </SettingsLoader>
                <h4>Global Options</h4>
                <hr />
                <Form>
                    <FormGroup row>
                        <Label sm={2} for='downloader-remove-on-complete'>Remove On Completion</Label>
                        <Col sm={10}>
                            <BindSwitchPreference preference='downloader.removecomplete' defaultValue={true} onSave={this.onGlobalOptionsChange.bind(this)}>
                                <Switch id='downloader-remove-on-complete' />
                            </BindSwitchPreference>
                        </Col>
                    </FormGroup>
                </Form>
                <Button color='primary' disabled={!this.state.globalOptionsSavable} onClick={this.saveGlobalOptions.bind(this)}>Save Changes</Button>
            </div>
        );
    }
}

export default Downloaders;
