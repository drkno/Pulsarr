import React from 'react';
import { faBars } from '@fortawesome/fontawesome-free-solid';
import MenuItem from './menu-item';
import './menu.css';

class Menu extends React.Component {
    state = {
        collapsed: true
    };

    toggleCollapsed() {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }

    render() {
        return (
            <ul className='sidebar-menu'>
                <MenuItem
                    link='#'
                    icon={faBars}
                    collapsed={this.state.collapsed}
                    onClick={() => this.toggleCollapsed()}
                    style={{transform: this.state.collapsed ? void(0) : 'rotate(90deg)'}} />
                {this.props.items.map((i, j) =>
                    (<MenuItem key={`sidebar-menu-${j}`} collapsed={this.state.collapsed} {...i} />))}
            </ul>
        );
    }
}

export default Menu;
