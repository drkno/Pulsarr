import React from 'react';
import { Table, Button, Form, FormGroup, Input, Label, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';
import 'whatwg-fetch';
import './raw.css';

class RawEditor extends React.Component {
    state = {
        preferences: {},
        searchTerm: null,
        editItem: null,
        deleteItem: null
    };

    async componentDidMount() {
        const response = await fetch('/api/preferences');
        const json = await response.json();
        this.setState({
            preferences: json
        })
    }

    deleteValue(preference) {
        this.setState({
            deleteItem: preference
        });
    }

    editValue(preference) {
        const val = this.state.preferences[preference];
        let type;
        if (val === 'True' || val === 'False') {
            type = 'b';
        }
        else if (!isNaN(val)) {
            type = 'n';
        } else {
            type = 's';
        }
        this.setState({
            editItem: {
                editable: false,
                key: preference,
                value: val,
                type: type
            }
        });
    }

    newValue() {
        this.setState({
            editItem: {
                editable: true,
                key: '',
                value: '',
                type: 's'
            }
        });
    }

    saveNewEditedValue() {
        this.setState({
            preferences: Object.assign({}, this.state.preferences, {[this.state.editItem.key]: this.state.editItem.value})
        });
        fetch('/api/preferences', {
            method: 'POST',
            body: JSON.stringify({[this.state.editItem.key]: this.state.editItem.value}),
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json'
            }
        });
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

    renderDeleteModal() {
        const preference = this.state.deleteItem;
        const onClose = () => this.setState({
            deleteItem: null
        });
        const onDelete = () => {
            onClose();
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
        };
        return (
            <Modal isOpen={this.state.deleteItem !== null}>
                <ModalHeader>Delete Key/Value</ModalHeader>
                <ModalBody>
                    Are you sure you want to delete <code>{preference || ''}</code>?
                </ModalBody>
                <ModalFooter>
                    <Button color='danger' onClick={onDelete}>Delete</Button>
                    <Button color='secondary' onClick={onClose}>Cancel</Button>
                </ModalFooter>
            </Modal>
        );
    }

    renderEditModal() {
        const editItem = this.state.editItem || {};
        const onChange = value => {
            this.setState({
                editItem: Object.assign({}, this.state.editItem, {
                    value: value
                })
            });
        };
        const onClose = () => this.setState({
            editItem: null
        });
        const onSave = () => {
            onClose();
            this.saveNewEditedValue();
        };
        let valueEditor;
        switch(editItem.type) {
            case 'b':
                valueEditor = (
                    <Input type='select' value={editItem.value} onChange={e => onChange(e.target.value)}>
                        <option value='True'>True</option>
                        <option value='False'>False</option>
                    </Input>
                );
                break;
            case 'n':
                valueEditor = (
                    <Input type='number' value={editItem.value} onChange={e => onChange(e.target.value)} />
                );
                break;
            default:
                valueEditor = (
                    <Input type='text' value={editItem.value} onChange={e => onChange(e.target.value)} />
                );
                break;
        }
        const onKeyChange = e => this.setState({
            editItem: Object.assign({}, this.state.editItem, {key: e.target.value})
        });
        const onTypeChange = e => this.setState({
            editItem: Object.assign({}, this.state.editItem, {type: e.target.value, value: ''})
        });
        return (
            <Modal isOpen={this.state.editItem !== null}>
                <ModalHeader>
                    {editItem.editable ? 'New' : 'Edit'} Key/Value
                </ModalHeader>
                <ModalBody>
                    <Form>
                        <FormGroup>
                            <Label>Key Name</Label>
                            <Input type='text' value={editItem.key} disabled={!editItem.editable} onChange={onKeyChange} />
                        </FormGroup>
                        <FormGroup>
                            <Label>Key Type</Label>
                            <Input type='select' value={editItem.type} onChange={onTypeChange}>
                                <option value='s'>String</option>
                                <option value='b'>Boolean</option>
                                <option value='n'>Number</option>
                            </Input>
                        </FormGroup>
                        <FormGroup>
                            <Label>Value</Label>
                            {valueEditor}
                        </FormGroup>
                    </Form>
                </ModalBody>
                <ModalFooter>
                    <Button color='danger' onClick={onSave}
                        disabled={(editItem.key || '').length === 0 || (editItem.key || '')[(editItem.key || '').length - 1] === '.'}>Save</Button>
                    <Button color='secondary' onClick={onClose}>Cancel</Button>
                </ModalFooter>
            </Modal>
        );
    }

    render() {
        return (
            <div>
                {this.renderDeleteModal()}
                {this.renderEditModal()}
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
