import React from 'react';
import { TabContent, TabPane, Nav, NavItem, NavLink } from 'reactstrap';
import DownloadActivity from './downloads';
import HistoryActivity from './history';

class Activity extends React.Component {
    constructor(props, ...other) {
        super(props, ...other);
        this.state = {
            activeTab: props.match.params.activityType !== 'downloads' ? 1 : 0
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
                    <NavItem>
                        <NavLink className={this.state.activeTab === 0 ? 'active' : ''} onClick={() => this.toggle(0)}>
                            Downloads
                        </NavLink>
                    </NavItem>
                    <NavItem>
                        <NavLink className={this.state.activeTab === 1 ? 'active' : ''} onClick={() => this.toggle(1)}>
                            History
                        </NavLink>
                    </NavItem>
                </Nav>
                <TabContent activeTab={this.state.activeTab.toString()}>
                    <TabPane tabId="0">
                        <DownloadActivity />
                    </TabPane>
                    <TabPane tabId="1">
                        <HistoryActivity />
                    </TabPane>
                </TabContent>
            </div>            
        );
    }
}

export default Activity;
