import React from 'react';
import Menu from './menu';
import {Table, Td, Tr} from '../table';
import './sidebar.css';

export default ({items, children, logo}) => (
    <Table>
        <Tr>
            <Td className='sidebar-sidebar'>
                {logo}
                <Menu items={items} />
            </Td>
            <Td className='sidebar-content'>
                {children}
            </Td>
        </Tr>
    </Table>
);
