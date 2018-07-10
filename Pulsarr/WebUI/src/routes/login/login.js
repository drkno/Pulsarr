import React from 'react';
import Console, { Prompt } from '../../components/console';
import 'whatwg-fetch';
import './login.css';

const Cursor = () => (
    <span className='login-cursor-blink'>█</span>
);

class LoginPage extends React.Component {
    state = {
        username: '',
        password: '',
        selected: 0,
        loadingChar: 0
    }

    loadingInterval = null;

    constructor(...args) {
        super(...args);
        this.onKey = this.onKey.bind(this);
        this.onBackspaceArrow = this.onBackspaceArrow.bind(this);
    }

    componentDidMount() {
        window.addEventListener('keypress', this.onKey);
        window.addEventListener('keydown', this.onBackspaceArrow);
        this.loadingInterval = setInterval(() => {
            this.setState({
                loadingChar: (this.state.loadingChar + 1) % 4
            });
        }, 100);
    }

    componentWillUnmount() {
        clearInterval(this.loadingInterval);
        window.removeEventListener('keypress', this.onKey);
        window.removeEventListener('keydown', this.onBackspaceArrow);
    }

    async performLogin() {
        try {
            const response = await fetch('/api/authorisation', {
                method: 'POST',
                data: JSON.stringify({username: this.state.username, password: this.state.password}),
                headers: {
                    'Content-Type': 'application/json'
                }
            });
            await response.json();
            document.location.href = '/';
        }
        catch (e) {
            this.setState({
                selected: 3
            })
        }
    }

    onKey(e) {
        if (this.state.selected >= 2) {
            return;
        }
        if (e.key === 'Enter') {
            if (this.state.selected + 1 === 2) {
                this.performLogin();
            }
            this.setState({
                selected: this.state.selected + 1
            });
        }
        else {
            const key = this.state.selected === 0 ? 'username' : 'password';
            this.setState({
                [key] : this.state[key] + e.key
            });
        }
        e.preventDefault();
    }

    onBackspaceArrow(e) {
        if (this.state.selected >= 2) {
            return;
        }
        if (e.key === 'Backspace') {
            const key = this.state.selected === 0 ? 'username' : 'password';
            this.setState({
                [key] : this.state[key].substring(0, this.state[key].length - 1)
            });
            e.preventDefault();
        }
        else if (e.key === 'ArrowUp') {
            this.setState({
                selected: Math.max(0, this.state.selected - 1)
            });
            e.preventDefault();
        }
        else if (e.key === 'ArrowDown') {
            if (this.state.selected + 1 === 2) {
                this.performLogin();
            }
            this.setState({
                selected: this.state.selected + 1
            });
            e.preventDefault();
        }
    }

    renderUsernameInput() {
        return (
            <span>
                {this.state.username}
                {this.state.selected === 0 ? <Cursor /> : void(0)}
            </span>
        );
    }

    renderPasswordInput() {
        return (
            <span>
                {'*'.repeat(this.state.password.length)}
                {this.state.selected === 1 ? <Cursor /> : void(0)}
            </span>
        );
    }

    renderLogin() {
        return (
            <div>
                #### LOGIN REQUIRED ####
                <br />
                Username:&nbsp;{this.renderUsernameInput()}
                <br />
                Password:&nbsp;{this.renderPasswordInput()}
            </div>
        );
    }

    renderLoggingIn() {
        return (
            <div>
                Logging you in {['-', '\\', '|', '/'][this.state.loadingChar]}
            </div>
        );
    }

    renderLoginFailed() {
        return (
            <div className='login-failure'>
                LOGIN FAILURE
            </div>
        );
    }

    render() {
        return (
            <Console>
                <Prompt>
                    ./pulsarr --open swebpage
                </Prompt>
                <br />
                {this.state.selected < 2 ? this.renderLogin() : void(0)}
                {this.state.selected === 2 ? this.renderLoggingIn() : void(0)}
                {this.state.selected === 3 ? this.renderLoginFailed() : void(0)}
            </Console>
        );
    }
}

export default LoginPage;
