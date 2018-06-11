import * as React from 'react';
import Typing from 'react-typing-animation';
import Text from './text';
import './Marvin.css';

class Marvin extends React.Component {
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
            <div onWheel={() => clearInterval(this.interval)}>
                <Typing speed={-1000} cursor={'█'}>
                    <span className='marvin-user' />
                    <span className='marvin-at' />
                    <span className='marvin-hostname' />
                    <span className='marvin-directory' />
                    <Typing.Speed ms={100} />
                    ./marvin&nbsp;<Typing.Delay ms={250} />--error 404
                    <br />
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
                </Typing>
            </div>
        );
    }
}
export default Marvin;
