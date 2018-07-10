import 'whatwg-fetch';

class PreferenceClient {
    async getAllPreferences() {
        const response = await fetch('/api/preferences');
        return await response.json();
    }

    async getPreference(preference) {
        const response = await fetch(`/api/preferences/${preference}`, {
            method: 'GET',
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json'
            }
        });
        return await response.json();
    }

    async setPreference(preference, value) {
        return await fetch('/api/preferences', {
            method: 'POST',
            body: JSON.stringify({[preference]: value}),
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json'
            }
        });
    }

    async deletePreference(preference) {
        return await fetch('/api/preferences', {
            method: 'DELETE',
            body: JSON.stringify([preference]),
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json'
            }
        });
    }

    fromBoolean(val, def = false) {
        if (typeof(val) === 'undefined') {
            return def;
        }
        return val === 'True';
    }

    toBoolean(val) {
        return val ? 'True' : 'False';
    }
}

export default new PreferenceClient();
