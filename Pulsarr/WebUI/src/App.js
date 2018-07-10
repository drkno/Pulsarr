import React, {Component} from 'react';
import { BrowserRouter as Router, Switch, Route, Redirect } from 'react-router-dom';
import { faPlusSquare, faBook, faHistory, faCog } from '@fortawesome/fontawesome-free-solid';
import SidebarContainer from './components/sidebar';
import Error from './components/error';
import routes from './routes';
import Logo from './icon.png';
import './App.css';
import 'bootstrap/dist/css/bootstrap.css';

class App extends Component {
    render() {
        const icons = [
            {
                icon: faPlusSquare,
                text: 'New Audiobook',
                link: '/new'
            },
            {
                icon: faBook,
                text: 'Library',
                link: '/library'
            },
            {
                icon: faHistory,
                text: 'Activity',
                link: '/activity'
            },
            {
                icon: faCog,
                text: 'Settings',
                link: '/settings'
            }
        ];
        return (
            <Router>
                <SidebarContainer items={icons} logo={<img className='sidebar-logo' src={Logo} alt='Logo' />}>
                    <Switch>
                        {routes}
                        <Route exact path="/" render={() => (<Redirect to="/library"/>)}/>
                        <Route component={Error} />
                    </Switch>
                </SidebarContainer>
            </Router>
        );
    }
}

export default App;
