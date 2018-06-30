import React from 'react';
import { TabContent, TabPane, Nav, NavItem, NavLink } from 'reactstrap';

class Tab extends React.Component {
    render() {
        return this.props.children;
    }
}

class TabContainer extends React.Component {
    constructor(props, ...other) {
        super(props, ...other);
        this.state = {
            activeTab: props.selectedTabIndex || '0'
        };
    }

    toggle(tab) {
        if (this.state.activeTab !== tab) {
            this.setState({
                activeTab: tab
            });
        }
    }

    render() {
        return (
            <div>
                <Nav tabs>
                    {React.Children.map(this.props.children, (c, i) => (
                        <NavItem key={`tab-nav-${i}`}>
                            <NavLink className={this.state.activeTab === i.toString() ? 'active' : ''} onClick={this.toggle.bind(this, i.toString())}>
                                {c.props.name}
                            </NavLink>
                        </NavItem>
                    ))}
                </Nav>
                <TabContent activeTab={this.state.activeTab.toString()}>
                    {React.Children.map(this.props.children, (c, i) => (
                        <TabPane tabId={i.toString()}>
                            {c.props.children}
                        </TabPane>
                    ))}
                </TabContent>
            </div>            
        );
    }
}

export { TabContainer, Tab };
