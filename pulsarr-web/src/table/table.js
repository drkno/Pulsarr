import React from 'react';
import './table.css';

export const Table = ({children, style, className, subtype}) => (
    <div className={`div-table${subtype || ''} ${className || ''}`} style={style}>
        {children}
    </div>
);

export const Tr = (props) => (<Table subtype='-row' {...props}>{props.children}</Table>);
export const Td = (props) => (<Table subtype='-cell' {...props}>{props.children}</Table>);
