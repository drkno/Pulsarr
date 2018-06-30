import React from 'react';
import Switch from 'rc-switch';
import { Card, Badge, Button, CardTitle, CardText, CardBody, Col } from 'reactstrap';

import 'rc-switch/assets/index.css';
import './carditems.css';

export default ({name, url, enabled, badges, onDelete, onEdit, onEnableDisableToggle}) => (
    <Col className='carditem-card'>
        <Card>
            <CardBody>
                <CardTitle>
                    {name}
                    {badges.map(c => (
                        <span>
                            &nbsp;
                            <Badge color={c.toLowerCase() === 'torrent' ? 'primary': 'success'}>{c}</Badge>
                        </span>
                    ))}
                </CardTitle>
                <CardText>
                    <Switch defaultChecked={enabled} onClick={onEnableDisableToggle} />
                    <br />
                    <a href={url}>{url}</a>
                </CardText>
                <Button className='carditem-button' onClick={onEdit}>Edit</Button>
                &nbsp;
                <Button className='carditem-button' color='danger' onClick={onDelete}>Delete</Button>
            </CardBody>
        </Card>
    </Col>
)
