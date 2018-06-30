import React from 'react';
import { InputGroup, InputGroupButtonDropdown, DropdownToggle, DropdownMenu, DropdownItem, Card, CardTitle, CardText, CardBody, Row, Col } from 'reactstrap';
import CardItem from './card';

import './carditems.css';

class CardArea extends React.Component {
    state = {
        newMenuOpen: false
    };

    toggleNewMenuOpen() {
        this.setState({
            newMenuOpen: !this.state.newMenuOpen
        });
    }

    onNewItemClick() {

    }

    onEnableDisableToggle() {

    }

    onDelete() {

    }

    onEdit() {

    }

    render() {
        return (
            <Row>
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
                                            <DropdownItem onClick={this.onNewItemClick.bind(this, i)} key={i}>{n.name}</DropdownItem>
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
                        onEdit={this.onEdit.bind(this, i)}
                        onEnableDisableToggle={this.onEnableDisableToggle.bind(this, i)} />
                ))}
            </Row>
        );
    }
}

export default CardArea;
