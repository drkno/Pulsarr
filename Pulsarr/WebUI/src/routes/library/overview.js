import React from 'react';
import { Row, Col, Card, CardImg, CardText, CardBody, CardTitle, CardSubtitle, Button, Badge } from 'reactstrap';
import PlusIcon from 'react-open-iconic-svg/dist/Plus';
import StarIcon from 'react-open-iconic-svg/dist/Star';
import data from './search.json';

class LibraryOverview extends React.Component {
    renderBook(book) {
        return (
            <Col sm={3} style={{maxWidth: '250px'}} key={book.id}>
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
        return (
            <Row>
                {data.map(w => this.renderBook(w))}
            </Row>
        )
    }
}

export default LibraryOverview;
