import React from 'react';
import { Link } from 'react-router-dom';
import { Row, Col, Card, CardImg, CardText, CardBody, CardTitle, CardSubtitle, Button, Badge } from 'reactstrap';
import PlusIcon from 'react-open-iconic-svg/dist/Plus';
import StarIcon from 'react-open-iconic-svg/dist/Star';
import Loading from '../../components/loading';
import Item from '../../components/item';

class LibraryOverview extends React.Component {
    state = {
        books: {
            items: [],
            totalItems: 0,
            rangeStart: null,
            rangeEnd: null,
            sortingBy: null,
            sortOrder: 0
        },
        loaded: false
    };

    async getBooks() {
        const response = await fetch('/api/library', {
            method: 'GET',
            headers: {
                Accept: 'application/json'
            }
        });
        const json = await response.json();
        this.setState({
            books: json,
            loaded: true
        });
    }

    componentDidMount() {
        this.getBooks();
    }

    async onDeleteItem(b) {
        await fetch(`/api/library/${b.id}`, {
            method: 'DELETE'
        });
        const books = this.state.books.items.slice();
        books.splice(books.indexOf(b), 1);
        this.setState({
            books: Object.assign({}, this.state.books, {
                items: books
            })
        });
    }

    render() {
        if (this.state.loaded) {
            if (this.state.books.items.length === 0) {
                return (
                    <p className='text-center lead'>
                        Looks like you don't have any audiobooks.
                        <br />
                        Would you like to <Link to={'/new'}>add some</Link>?
                    </p>
                );
            }
            else {
                return (
                    <Row>
                        {this.state.books.items.map(b => 
                            (<Item
                                onDeleteItem={() => this.onDeleteItem(b)}
                                {...b}
                                key={`book-${b.id}`} />))}
                    </Row>
                );
            }
        }
        else {
            return (<Loading />);
        }
    }
}

export default LibraryOverview;
