import React from 'react';
import Switch from 'rc-switch';
import { Form, FormGroup, Label, Input, Col, Button } from 'reactstrap';
import BindPreference, { BindSwitchPreference } from '../bindPreference';

import 'rc-switch/assets/index.css';

class MetadataEditor extends React.Component {
    state = {
        goodreads: {},
        goodreadsSavable: false
    };

    goodReadsChanges = [];

    renderGoodReads() {
        const addSave = s => {
            this.goodReadsChanges.push(s);
            this.setState({goodreadsSavable: true});
        };
        const executeSave = () => {
            for (let s of this.goodReadsChanges) {
                s();
            }
            this.goodReadsChanges = [];
            this.setState({goodreadsSavable: false});
        };
        return (
            <div>
                <Form>
                    <FormGroup row>
                        <Label sm={2} for='goodreads-switch'>Enabled</Label>
                        <Col sm={10}>
                            <BindSwitchPreference preference='goodreads.enabled' defaultValue={true} onSave={addSave}>
                                <Switch id='goodreads-switch' />
                            </BindSwitchPreference>
                        </Col>
                    </FormGroup>
                    <FormGroup row>
                        <Label sm={2} for='goodreads-api-key'>API Key</Label>
                        <Col sm={10}>
                            <BindPreference preference='goodreads.apikey' defaultValue='' onSave={addSave}>
                                <Input type='text' id='goodreads-api-key' />
                            </BindPreference>
                        </Col>
                    </FormGroup>
                    <FormGroup row>
                        <Label sm={2} for='goodreads-api-secret'>API Secret</Label>
                        <Col sm={10}>
                            <BindPreference preference='goodreads.apisecret' defaultValue='' onSave={addSave}>
                                <Input type='password' id='goodreads-api-secret' />
                            </BindPreference>
                        </Col>
                    </FormGroup>
                </Form>
                <Button color='primary' onClick={executeSave} disabled={!this.state.goodreadsSavable}>Save Changes</Button>
            </div>
        );
    }

    render() {
        return (
            <div>
                <br />
                <h4>GoodReads</h4>
                <hr />
                {this.renderGoodReads()}
            </div>
        );
    }
}

export default MetadataEditor;
