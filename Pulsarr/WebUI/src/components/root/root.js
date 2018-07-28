import React from 'react';
import SideBar from '../sidebar';
import './root.css';

export default ({items, children, logo}) => (
    <div>
        <SideBar items={items} logo={logo} />
        <div className='container-fluid p-0'>
            <section className='content-section p-3 p-lg-5 d-flex flex-column'>
                <div>
                    {children}
                </div>
            </section>
        </div>
    </div>
);
