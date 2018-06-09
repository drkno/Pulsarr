import React from 'react';
import { Input, InputGroup, InputGroupAddon, Button } from 'reactstrap';
import MagnifingGlassIcon from 'react-open-iconic-svg/dist/MagnifyingGlass';
import ClearIcon from 'react-open-iconic-svg/dist/Trash';
import debounce from 'lodash.debounce';

class SearchBar extends React.Component {
    constructor(props, ...other) {
        super(props, ...other);
        this.onChangeDebounce = debounce((...args) => this.onChange(...args), 300);
        this.clear = this.clear.bind(this);
        this.ref = null;
        this.lastVal = '';
        this.listId = `search-${Math.random() * 1000.0}`;
    }

    componentDidMount() {
        if (this.props.clear) {
            this.props.clear(this.clear);
        }
    }

    componentWillUnmount() {
        if (this.props.clear) {
            this.props.clear(null);
        }
    }

    onChange(e) {
        if (this.lastVal === e) {
            return;
        }
        this.lastVal = e;
        if (this.props.onChange) {
            this.props.onChange(e);
        }
    }

    clear() {
        this.ref.value = '';
        this.onChangeDebounce('');
    }

    renderSuggestions() {
        if (this.props.suggestions) {
            return (
                <datalist id={this.listId}>
                    {this.props.suggestions.map(s => (<option value={s} />))}
                </datalist>
            );
        }
        else {
            return void(0);
        }
    }

    render() {
        return (
            <div>
                <InputGroup>
                    <Input
                        list={this.listId}
                        autocomplete='off'
                        innerRef={ref => this.ref = ref}
                        placeholder="Search"
                        onChange={e => this.onChangeDebounce(e.target.value)}
                        style={{textAlign: 'center'}} />
                    <InputGroupAddon addonType="append">
                        <Button onClick={() => this.onChangeDebounce(this.ref.value)}>
                            <MagnifingGlassIcon style={{fill: 'white', height: '1em', width: '1em'}} />
                        </Button>
                    </InputGroupAddon>
                    <InputGroupAddon addonType="append">
                        <Button color='danger' onClick={this.clear}>
                            <ClearIcon style={{fill: 'white', height: '1em', width: '1em'}} />
                        </Button>
                    </InputGroupAddon>
                    {this.renderSuggestions()}
                </InputGroup>
            </div>
        );
    }
}

export default SearchBar;
