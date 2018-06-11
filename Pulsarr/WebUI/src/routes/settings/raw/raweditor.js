import React from 'react';
import { Table, Button, Form, Input } from 'reactstrap';
import 'whatwg-fetch';
import './raw.css';

class RawEditor extends React.Component {
    state = {
        preferences: {},
        searchTerm: null
    };

    async componentDidMount() {
        const response = await fetch('/api/preferences');
        const json = await response.json();
        this.setState({
            preferences: json
        })
    }

    editValue(preference) {
        const newValue = prompt(preference, this.state.preferences[preference] || '');
        if (newValue !== null) {
            this.setState({
                preferences: Object.assign({}, this.state.preferences, {[preference]: newValue})
            });
            fetch('/api/preferences', {
                method: 'POST',
                body: JSON.stringify({[preference]: newValue}),
                headers: {
                    Accept: 'application/json',
                    'Content-Type': 'application/json'
                }
            });
        }
    }

    deleteValue(preference) {
        const sure = window.confirm(`Are you sure you want to delete "${preference}"?`);
        if (sure) {
            const newPreferences = Object.assign({}, this.state.preferences);
            delete newPreferences[preference];
            this.setState({
                preferences: newPreferences
            });
            fetch('/api/preferences', {
                method: 'DELETE',
                body: JSON.stringify([preference]),
                headers: {
                    Accept: 'application/json',
                    'Content-Type': 'application/json'
                }
            });
        }
    }

    newValue() {
        const newKey = prompt('What is the name for your preference?', '');
        if (newKey !== null && newKey.trim() !== '') {
            this.editValue(newKey);
        }
    }

    filter(search) {
        if (search === null || search.trim() === '') {
            return this.state.preferences;
        }
        search = search.trim().toLowerCase();
        const result = {};
        for (let key in this.state.preferences) {
            if (key.toLowerCase().indexOf(search) >= 0 || this.state.preferences[key].toLowerCase().indexOf(search) >= 0) {
                result[key] = this.state.preferences[key];
            }
        }
        return result;
    }

    updateFilter(e) {
        this.setState({
            searchTerm: e.target.value
        });
    }

    render() {
        return (
            <div>
                <Input type='text' placeholder='Search' onChange={e => this.updateFilter(e)} />
                <Table striped size="sm" hover responsive className='raw-config-editor'>
                    <thead>
                        <tr>
                            <th style={{minWidth:'33%'}}>Key</th>
                            <th style={{width:'100%'}}>Value</th>
                            <th style={{minWidth:'10em'}}>
                                <Form inline>
                                    <Button color='secondary' size='sm' onClick={() => this.newValue()} className='raw-config-editor-new'>New</Button>
                                </Form>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    {Object.keys(this.filter(this.state.searchTerm)).sort().map(k => (
                        <tr key={`preference-${k}`}>
                            <td>{k}</td>
                            <td>{this.state.preferences[k]}</td>
                            <td>
                                <Button color='primary' size='sm' onClick={() => this.editValue(k)}>Edit</Button>
                                &nbsp;
                                <Button color='danger' size='sm' onClick={() => this.deleteValue(k)}>Delete</Button>
                            </td>
                        </tr>
                    ))}
                    </tbody>
                </Table>
            </div>
            
        );
    }
}

export default RawEditor;
