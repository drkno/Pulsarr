import React from 'react';
import LoadingDots from '../../components/loading';
import PreferenceClient from './preferenceClient';

import './settings.css';

class SettingsLoader extends React.Component {
    state = {
        loaded: false
    };

    shouldSetValues = null;

    async componentDidMount() {
        this.shouldSetValues = true;
        const promises = this.props.preferences.map(p => PreferenceClient.getPreference(p));
        const values = await Promise.all(promises);
        if (this.shouldSetValues) {
            const obj = {};
            for (let i = 0; i < values.length; i++) {
                obj[this.props.preferences[i]] = values[i];
            }
            this.props.preferenceValues(obj);
            this.setState({
                loaded: true
            });
        }
    }

    componentWillUnmount() {
        this.shouldSetValues = false;
    }

    renderLoading() {
        return (
            <LoadingDots className='settings-loading' />
        );
    }

    render() {
        if (this.state.loaded === false) {
            return this.renderLoading();
        }
        return this.props.children;
    }
}

export default SettingsLoader;
