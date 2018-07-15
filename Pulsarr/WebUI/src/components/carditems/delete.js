import React from 'react';
import { Modal, ModalHeader, ModalFooter, ModalBody, Button } from 'reactstrap';

class DeleteDialog extends React.Component {
    state = {
        isOpen: false
    };

    hide() {
        this.setState({
            isOpen: false
        });
    }

    show() {
        this.setState({
            isOpen: true
        });
    }

    onDelete() {
        this.hide();
    }

    onCancel() {
        this.hide();
    }


    render() {
        if (!this.props.item) {
            return (<div />);
        }
        return (
            <Modal isOpen={this.state.isOpen}>
                <ModalHeader>Delete</ModalHeader>
                <ModalBody>Are you sure you want to delete this instance of {this.props.item.name}?</ModalBody>
                <ModalFooter>
                    <Button color='danger' onClick={() => this.onDelete()}>Delete</Button>
                    <Button color='success' onClick={() => this.onCancel()}>Cancel</Button>
                </ModalFooter>
            </Modal>
        );
    }
}

export default DeleteDialog;
