import React from 'react';
import { Row } from 'reactstrap';
import SearchBar from '../../components/search-bar';
import Item from '../../components/item';
import Loading from '../../components/loading';

import 'whatwg-fetch';

class NewBook extends React.Component {
    state = {
        suggestions: [],
        state: 0
    };

    updateDebounce = 0;
    clearSearch = null;

    async updateSuggestions(input) {
        if (input.length === 0) {
            this.setState({
                suggestions: [],
                state: 0
            });
        }
        else {
            const ud = ++this.updateDebounce;
            const response = await fetch('/api/find', {
                method: 'POST',
                body: JSON.stringify({title: input}),
                headers: {
                    Accept: 'application/json',
                    'Content-Type': 'application/json'
                }
            });
            const json = await response.json();
            if (this.updateDebounce === ud) {
                this.setState({
                    suggestions: json,
                    state: 2
                });
            }
        }
    }

    onSearchChange(e) {
        this.setState({
            state: 1
        });
        this.updateSuggestions(e);
    }

    onAddItem(book) {
        if (this.clearSearch) {
            this.clearSearch();
        }
        this.setState({
            suggestions: [],
            state: 0
        });
        fetch(`/api/library/${book.dataSourceId}/${book.sourceBookId}`, {
            method: 'POST'
        });
    }

    render() {
        let content;
        switch(this.state.state) {
            case 0:
                content = (
                    <p className='text-center lead'>
                        Search for a new audiobook above.
                    </p>
                );
                break;
            case 1:
                content = (<Loading />);
                break;
            case 2:
                if (this.state.suggestions.length === 0) {
                    content = (
                        <p className='text-center lead'>
                            No search results.
                        </p>
                    );
                }
                else {
                    content = (
                        <Row>
                            {this.state.suggestions.map(s => (
                                <Item
                                    key={`new-${s.dataSourceId}-${s.sourceBookId}`}
                                    imageUrl={s.imageUrl}
                                    title={s.title}
                                    author={s.author}
                                    onAddItem={() => this.onAddItem(s)} />
                            ))}
                        </Row>
                    );
                }
                break;
            default:
                throw new Error();
        }
        return (
            <div>
                <SearchBar
                    suggestions={this.state.suggestions}
                    onChange={e => this.onSearchChange(e)}
                    clear={c => this.clearSearch = c} />
                <hr />
                {content}
            </div>
        );
    }
}

export default NewBook;
