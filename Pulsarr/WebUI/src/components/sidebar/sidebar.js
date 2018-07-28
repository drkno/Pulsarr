import * as React from 'react';
import { Link } from 'react-router-dom';
import FontAwesomeIcon from '@fortawesome/react-fontawesome';
import '@fortawesome/fontawesome/styles.css';
import './SideBar.css';

class SideBar extends React.Component {
    state = {
        showNav: false
    };

    renderNavItem(item) {
        return (
            <li className='nav-item' onClick={() => this.setState({ showNav: false })}>
                <Link className='nav-link' to={item.link}>
                    <FontAwesomeIcon icon={item.icon} />
                    <span>&nbsp;{item.text}</span>
                </Link>
            </li>
        );
    }

    render() {
        return (
            <nav className="navbar navbar-expand-lg navbar-dark bg-primary fixed-top" id="sideNav">
                <Link className='navbar-brand' to='/'>
                    <span className="d-block d-lg-none">
                        Pulsarr
                    </span>
                    <span className="d-none d-lg-block">
                        <img className="img-fluid img-profile rounded-circle mx-auto mb-2 sidebar-profile-img" src={this.props.logo} alt="" />
                    </span>
                </Link>
                <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent"
                        aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation"
                        onClick={() => this.setState({ showNav: !this.state.showNav })}>
                    <span className="navbar-toggler-icon"/>
                </button>
                <div className={`navbar-collapse ${this.state.showNav ? '' : 'collapse'}`} id="navbarSupportedContent">
                    <ul className="navbar-nav">
                        {this.props.items.map(item => this.renderNavItem(item))}
                    </ul>
                </div>
            </nav>
        );
    }
}

export default SideBar;
