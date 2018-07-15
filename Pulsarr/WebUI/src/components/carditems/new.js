import React from 'react';
import { Modal, ModalHeader, ModalFooter, ModalBody, Button, FormGroup, Form, Input, Label } from 'reactstrap';
import Switch from 'rc-switch';

class NewRemoteDialog extends React.Component {
    state = {
        values: []
    };

    componentDidUpdate(prevProps) {
        if (prevProps.name !== this.props.name) {
            this.setState({
                values: Array((this.props.preferences || []).length)
            });
        }
    }

    onSave() {
        this.props.onSave(this.state.values);
    }

    onCancel() {
        this.props.onSave([]);
    }

    updateFormValue(item, e, i) {
        const val = this.state.values.slice();
        val[i] = [item.key, e.target.value];
        this.setState({
            value: val
        });
    }

    renderTextFormItem(item, i) {
        return (
            <FormGroup key={i}>
                <Label>{item.name}</Label>
                <Input type='text'
                    onChange={e => this.updateFormValue(item, e, i)}
                    value={this.state.values[i] === void(0) ? item.defaultValue : this.state.values[i][1]} />
            </FormGroup>
        );
    }

    renderNumberFormItem(item, i) {
        return (
            <FormGroup key={i}>
                <Label>{item.name}</Label>
                <Input type='number'
                    onChange={e => this.updateFormValue(item, e, i)}
                    value={this.state.values[i] === void(0) ? item.defaultValue : this.state.values[i][1]} />
            </FormGroup>
        );
    }

    renderBoolFormItem(item, i) {
        return (
            <FormGroup key={i}>
                <Label>{item.name}</Label>
                <Switch
                    onClick={e => this.updateFormValue(item, e, i)}
                    defaultChecked={this.state.values[i] === void(0) ? item.defaultValue : this.state.values[i][1]} />
            </FormGroup>
        );
    }

    renderFormItem(item, i) {
        switch(item.type) {
            case 'number':
                return this.renderNumberFormItem(item, i);
            case 'bool':
                return this.renderBoolFormItem(item, i);
            case 'text':
            default:
                return this.renderTextFormItem(item, i);
        }
    }

    render() {
        return (
            <Modal isOpen={!!this.props.name}>
                <ModalHeader>{!!this.props.edit ? 'Edit' : 'New'} {this.props.name || ''}</ModalHeader>
                <ModalBody>
                    <Form>
                        {(this.props.preferences || []).map((p, i) => this.renderFormItem(p, i))}
                    </Form>
                </ModalBody>
                <ModalFooter>
                    <Button color='success' onClick={() => this.onSave()}>Save</Button>
                    <Button color='danger' onClick={() => this.onCancel()}>Cancel</Button>
                </ModalFooter>
            </Modal>
        );
    }
}

export default NewRemoteDialog;
