import React from 'react';
import PreferenceClient from './preferenceClient';
import SettingsLoader from './loading';

class BindPreference extends React.Component {
    state = {
        value: null
    };

    notified = false;

    async onSave() {
        this.notified = false;
        let value = this.state.value;
        if (typeof(this.props.defaultValue) === 'boolean') {
            value = PreferenceClient.toBoolean(value);
        }
        return await PreferenceClient.setPreference(this.props.preference, value);
    }

    onChange(...args) {
        let newValue;
        if (this.props.onChangeGet) {
            newValue = this.props.onChangeGet(...args);
        }
        else {
            newValue = args[0].target.value;
        }
        if (typeof(this.props.defaultValue) === 'boolean') {
            newValue = PreferenceClient.toBoolean(newValue);
        }
        this.setState({
            value: newValue.toString()
        });
        if (!this.notified) {
            this.props.onSave(this.onSave.bind(this));
            this.notified = true;
        }
    }

    onPreferenceValues(values) {
        const value = values[this.props.preference];
        this.setState({
            value: value === void(0) || value === null ? '' : value
        });
    }

    getValue() {
        if (this.state.value === null) {
            return this.props.defaultValue;
        }
        const typeDef = typeof(this.props.defaultValue);
        if (this.state.value === '' && typeDef !== 'string') {
            return this.props.defaultValue;
        }

        switch(typeDef) {
            case 'boolean': return PreferenceClient.fromBoolean(this.state.value, this.props.defaultValue);
            case 'number': return parseFloat(this.state.value);
            default: return this.state.value;
        }
    }

    render() {
        return (
            <SettingsLoader
                preferences={[this.props.preference]}
                preferenceValues={v => this.onPreferenceValues(v)}>
            {
                React.cloneElement(this.props.children, {
                    [this.props.onChangeProperty || 'onChange']: this.onChange.bind(this),
                    [this.props.valueProperty || 'value']: this.getValue()
                })
            }
            </SettingsLoader>
        )
    }
}

const BindSwitchPreference = ({defaultValue, onSave, preference, children}) => (
    <BindPreference valueProperty='defaultChecked' onChangeGet={g => g[0]} preference={preference} defaultValue={defaultValue} onSave={onSave}>
        {children}
    </BindPreference>
);

export default BindPreference;
export { BindSwitchPreference };
