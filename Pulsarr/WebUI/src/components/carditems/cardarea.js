import React from 'react';
import { InputGroup, InputGroupButtonDropdown, DropdownToggle, DropdownMenu, DropdownItem, Card, CardTitle, CardText, CardBody, Row, Col } from 'reactstrap';
import CardItem from './card';
import DeleteDialog from './delete';
import NewRemoteDialog from './new';

import './carditems.css';

class CardArea extends React.Component {
    state = {
        newMenuOpen: false,
        deleteItem: null,
        newItem: null,
        isEdit: false
    };

    toggleNewMenuOpen() {
        this.setState({
            newMenuOpen: !this.state.newMenuOpen
        });
    }

    onNewItemClick(n) {
        this.setState({
            newItem: n,
            isEdit: false
        });
    }

    onEnableDisableToggle() {

    }

    onDelete(i) {
        this.setState({
            deleteItem: this.props.items[i]
        });
    }

    onEdit(n) {
        this.setState({
            newItem: n,
            isEdit: true
        });
    }

    onSave(e) {
        const newItem = this.state.newItem;
        const isEdit = this.state.isEdit;
        this.setState({
            newItem: null
        });
    }

    render() {
        return (
            <Row>
                <NewRemoteDialog edit={this.state.isEdit} {...(this.state.newItem || {})} onSave={e => this.onSave(e)} />
                <DeleteDialog item={this.state.deleteItem} />
                <Col className='carditem-card'>
                    <Card>
                        <CardBody>
                            <CardTitle>
                                New {this.props.newName}
                            </CardTitle>
                            <CardText>{this.props.newDescription}</CardText>
                            <InputGroup>
                                <InputGroupButtonDropdown addonType="append" isOpen={this.state.newMenuOpen} toggle={this.toggleNewMenuOpen.bind(this)}>
                                    <DropdownToggle caret>
                                        Select a new {this.props.newName}
                                    </DropdownToggle>
                                    <DropdownMenu>
                                        {this.props.newItems.map((n, i) => (
                                            <DropdownItem onClick={this.onNewItemClick.bind(this, n)} key={i}>{n.name}</DropdownItem>
                                        ))}
                                    </DropdownMenu>
                                </InputGroupButtonDropdown>
                            </InputGroup>
                        </CardBody>
                    </Card>
                </Col>
                {this.props.items.map((c, i) => (
                    <CardItem
                        key={c.id}
                        name={c.name}
                        url={c.url}
                        enabled={c.enabled}
                        badges={c.badges}
                        onDelete={this.onDelete.bind(this, i)}
                        onEdit={this.onEdit.bind(this, c)}
                        onEnableDisableToggle={this.onEnableDisableToggle.bind(this, i)} />
                ))}
            </Row>
        );
    }
}

export default CardArea;
