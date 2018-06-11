import React from 'react';
import { Link } from 'react-router-dom';
import FontAwesomeIcon from '@fortawesome/react-fontawesome';
import '@fortawesome/fontawesome/styles.css';

export default ({icon, text, link, collapsed, onClick, ...iconProps}) => (
    <li className={`sidebar-menu-item${collapsed ? ' sidebar-collapsed' : ''}`}>
        <Link to={link} onClick={onClick}>
            <FontAwesomeIcon icon={icon} {...iconProps} />
            <span>&nbsp;{text}</span>
        </Link>
    </li>
);
