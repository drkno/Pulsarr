import React from 'react';
import { Link } from 'react-router-dom';
import { Row, Button, ButtonGroup, Table as BootstrapTable } from 'reactstrap';
import Loading from '../../components/loading';
import Item, { renderState, renderDeleteButton } from '../../components/item';
import SearchBar from '../../components/search-bar';
import FontAwesomeIcon from '@fortawesome/react-fontawesome';
import { faPlusSquare, faTable, faList } from '@fortawesome/fontawesome-free-solid';
import { Table, Tr, Td } from '../../components/table';
import './library.css';

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
        loaded: false,
        searchQuery: '',
        viewMode: 0
    };

    clearSearch = null;

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
        const viewMode = parseInt(window.localStorage.getItem('libraryViewMode'));
        if (!isNaN(viewMode)) {
            this.setState({
                viewMode: viewMode
            });
        }
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

    onSearchChange(e) {
        this.setState({
            searchQuery: e.toLowerCase()
        });
    }

    getFilteredItems() {
        return this.state.books.items.filter(i => i.title.toLowerCase().indexOf(this.state.searchQuery) >= 0);
    }

    setViewMode(val) {
        this.setState({
            viewMode: val
        });
        window.localStorage.setItem('libraryViewMode', val);
    }

    renderGridView() {
        return (
            <Row>
                {this.getFilteredItems().map(b => 
                    (<Item
                        onDeleteItem={() => this.onDeleteItem(b)}
                        {...b}
                        key={`book-${b.id}`} />))}
            </Row>
        );
    }

    renderTableView() {
        return (
            <BootstrapTable responsive>
                <thead>
                    <tr>
                        <th>Title</th>
                        <th>Author</th>
                        <th>State</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {this.getFilteredItems().map(b => (
                        <tr key={b.id}>
                            <td>{b.title}</td>
                            <td>{b.author}</td>
                            <td>{renderState(b.state)}</td>
                            <td>
                                {renderDeleteButton(() => this.onDeleteItem(b), true)}
                            </td>
                        </tr>
                    ))}
                </tbody>
            </BootstrapTable>
        );
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
                    <div>
                        <Table>
                            <Tr>
                                <Td className='library-button'>
                                    <Button color='success' onClick={() => this.props.history.push('/new')}>
                                        <FontAwesomeIcon icon={faPlusSquare} />
                                        &nbsp;New
                                    </Button>
                                </Td>
                                <Td className='library-button'>
                                    <ButtonGroup>
                                        <Button active={this.state.viewMode === 0} onClick={() => this.setViewMode(0)}>
                                            <FontAwesomeIcon icon={faTable} />
                                        </Button>
                                        <Button active={this.state.viewMode === 1} onClick={() => this.setViewMode(1)}>
                                            <FontAwesomeIcon icon={faList} />
                                        </Button>
                                    </ButtonGroup>
                                </Td>
                                <Td>
                                    <SearchBar
                                        suggestions={this.state.books.items.map(i => i.title)}
                                        onChange={e => this.onSearchChange(e)}
                                        clear={c => this.clearSearch = c} />
                                </Td>
                            </Tr>
                        </Table>
                        <hr />
                        {this.state.viewMode === 0 ? this.renderGridView() : this.renderTableView()}
                    </div>
                );
            }
        }
        else {
            return (<Loading />);
        }
    }
}

export default LibraryOverview;
