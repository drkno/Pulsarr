import React from 'react';
import { Col, Card, CardImg, CardText, CardBody, CardTitle, CardSubtitle, Button, Badge } from 'reactstrap';
import PlusIcon from 'react-open-iconic-svg/dist/Plus';
import TrashIcon from 'react-open-iconic-svg/dist/Trash';
import SearchIcon from 'react-open-iconic-svg/dist/MagnifyingGlass';
import DownloadIcon from 'react-open-iconic-svg/dist/CloudDownload';
import PlayIcon from 'react-open-iconic-svg/dist/PlayCircle';
import QuestionIcon from 'react-open-iconic-svg/dist/QuestionMark';
import './item.css';

const renderAddItemButton = onAddItem => onAddItem && (
    <Button color='success' onClick={onAddItem}>
        <PlusIcon className='item-button-icon' />
        &nbsp;
        Add
    </Button>
);

const renderDeleteButton = onDeleteItem => onDeleteItem && (
    <Button color='danger' onClick={onDeleteItem}>
        <TrashIcon className='item-button-icon' />
        &nbsp;
        Delete
    </Button>
);

const renderState = state => {
    if (state === void(0)) return;
    let stateText;
    let badgeColour;
    let icon;
    switch(state) {
        case 0:
            stateText = 'Missing';
            badgeColour = 'warning';
            icon = (<SearchIcon />);
            break;
        case 1:
            stateText = 'Downloading';
            badgeColour = 'primary';
            icon = (<DownloadIcon />);
            break;
        case 2:
            stateText = 'Found';
            badgeColour = 'success';
            icon = (<PlayIcon />);
            break;
        default:
            stateText = 'Unknown';
            badgeColour = 'secondary';
            icon = (<QuestionIcon />);
            break;
    }
    return (
        <Badge color={badgeColour}>
            {stateText}
            &nbsp;
            {icon}
        </Badge>
    );
};

const Item = ({imageUrl, title, author, state, onAddItem, onDeleteItem}) => (
    <Col sm={3} className='item-col'>
        <Card>
            <CardImg top width="100%" src={imageUrl} alt={title} />
            <CardBody>
                <CardTitle>{title}</CardTitle>
                <CardSubtitle style={{display: onDeleteItem ? void (0) : 'none'}}>
                    {renderState(state)}
                </CardSubtitle>
                <CardText><i>{author}</i></CardText>
                {renderAddItemButton(onAddItem)}
                {renderDeleteButton(onDeleteItem)}
            </CardBody>
        </Card>
    </Col>
);

export default Item;
