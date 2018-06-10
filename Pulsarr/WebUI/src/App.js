import React, {Component} from 'react';
import { Row, Col, Card, CardImg, CardText, CardBody, CardTitle, CardSubtitle, Button, Badge } from 'reactstrap';

import PlusIcon from 'react-open-iconic-svg/dist/Plus';
import StarIcon from 'react-open-iconic-svg/dist/Star';
import SearchBar from './search-bar';

import data from './search.json';

import SidebarContainer from './sidebar';

import { faPlusSquare, faBook, faHistory, faCog } from '@fortawesome/fontawesome-free-solid';

import 'bootstrap/dist/css/bootstrap.css';
import './App.css';
import Logo from './icon.png';

class App extends Component {
    renderBook(book) {
        return (
            <Col sm={3} style={{maxWidth: '250px'}}>
                <Card>
                    <CardImg top width="100%" src={book.image} alt={book.title} />
                    <CardBody>
                        <CardTitle>{book.title}</CardTitle>
                        <CardSubtitle style={{display: book.rating && book.rating > 0 ? void (0) : 'none'}}>
                            <Badge color='warning'>
                                {book.rating}
                                &nbsp;
                                <StarIcon />
                            </Badge>
                        </CardSubtitle>
                        <CardText><i>{book.author}</i></CardText>
                        <Button color='success'>
                            <PlusIcon style={{fill: 'white', transform: 'scale(1.3) translateY(-20%)'}} />
                            &nbsp;
                            Add
                        </Button>
                    </CardBody>
                </Card>
            </Col>
        );
    }

    render() {
        const icons = [
            {
                icon: faPlusSquare,
                text: 'New Audiobook',
                link: '#new'
            },
            {
                icon: faBook,
                text: 'Library',
                link: '#books'
            },
            {
                icon: faHistory,
                text: 'Activity',
                link: '#activity'
            },
            {
                icon: faCog,
                text: 'Settings',
                link: '#settings'
            }
        ];
        return (
            <SidebarContainer items={icons} logo={<img style={{height: '4em', margin: '1.75em'}} src={Logo} />}>
                <SearchBar />
                <Row>
                    {data.map(w => this.renderBook(w))}
                </Row>
            </SidebarContainer>
        );
    }
}

export default App;
