import * as React from 'react';
import Typing from 'react-typing-animation';
import Console, { Prompt } from '../console';
import Text from './text';

class Error extends React.Component {
    interval = null;

    componentDidMount() {
        this.interval = setInterval(() => {
            window.scrollTo(0, document.documentElement.scrollHeight);
        }, 50);
    }

    componentWillUnmount() {
        clearInterval(this.interval);
    }

    render() {
        const lines = Text.split('\n');
        return (
            <Typing speed={-1000} cursor={'█'}>
                <Console onWheel={() => clearInterval(this.interval)}>
                    <Typing.Speed ms={100} />
                    <Prompt>
                        ./marvin&nbsp;<Typing.Delay ms={250} />--error 404
                    </Prompt>
                    <br />
                    <Typing.Speed ms={75} />
                    <Typing.Delay ms={500} />
                    {
                        lines.map((l, i) => (
                            <span key={`marvin-${i}`}>
                            {l}
                                <br />
                            <Typing.Delay ms={500} />
                        </span>
                        ))
                    }
                </Console>
            </Typing>
        );
    }
}

export default Error;
