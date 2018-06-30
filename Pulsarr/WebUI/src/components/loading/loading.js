import React from 'react';
import './loading.css';

export default ({ className }) => (
    <div className={`loading-dots ${className || ''}`}>
        <h1 className='dot'>.</h1>
        <h1 className='dot'>.</h1>
        <h1 className='dot'>.</h1>
    </div>
);
