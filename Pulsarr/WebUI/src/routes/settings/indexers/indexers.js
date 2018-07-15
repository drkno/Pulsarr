import React from 'react';
import CardPreferences from '../../../components/carditems';

const indexers = [
    {
        id: 0,
        name: 'Newznab',
        badges: [],
        url: 'https://thepiratebay.org'
    },
    {
        id: 1,
        name: 'Newznab',
        badges: [],
        url: 'https://isohunt.org'
    },
    {
        id: 2,
        name: 'Newznab',
        badges: [],
        url: 'https://nzb.cat'
    },
    {
        id: 3,
        name: 'Newznab',
        badges: [],
        url: 'https://foo.bar'
    },
    {
        id: 4,
        name: 'Newznab',
        badges: [],
        url: 'https://bar.foo'
    }
];

const newIndexers = [
    {
        name: 'Newznab',
        badges: [],
        preferences: [
            {
                type: 'bool',
                name: 'Enabled',
                key: 'enabled',
                defaultValue: true
            },
            {
                type: 'text',
                name: 'Url',
                key: 'url',
                defaultValue: 'http://127.0.0.1:5076'
            },
            {
                type: 'text',
                name: 'API Key',
                key: 'apikey',
                defaultValue: ''
            },
            {
                type: 'text',
                name: 'Categories',
                key: 'categories',
                defaultValue: '3000,310,3030,3040'
            }
        ]
    }
];

class Indexers extends React.Component {
    render() {
        return (
            <div>
                <br />
                <h4>Indexers</h4>
                <hr />
                <CardPreferences
                    items={indexers}
                    newItems={newIndexers}
                    newName='Indexer'
                    newDescription='Add a new indexer such as NzbHydra or Jackett.' />
            </div>
        );
    }
}

export default Indexers;
