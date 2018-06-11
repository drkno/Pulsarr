import React from 'react';
import './table.css';

export const Table = ({children, style, className, subtype, onClick}) => (
    <div className={`div-table${subtype || ''} ${className || ''}`} style={style} onClick={onClick}>
        {children}
    </div>
);

export const Tr = (props) => (<Table subtype='-row' {...props}>{props.children}</Table>);
export const Td = (props) => (<Table subtype='-cell' {...props}>{props.children}</Table>);
