import * as React from 'react';

import './console.css';

export const Prompt = ({children}) => (
    <span>
        <span className='user' />
        <span className='at' />
        <span className='hostname' />
        <span className='directory' />
        {children}
        <br />
    </span>
);

export default ({children, onWheel}) => (
    <div className='console-area' onWheel={onWheel}>
        {children}
    </div>
);
