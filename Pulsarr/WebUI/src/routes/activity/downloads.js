import React from 'react';
import { Table, Button, Form, Input, Badge, Progress } from 'reactstrap';
//import WebSocketComponent from '../../websocket';

class DownloadActivity extends React.Component {
    state = {
        downloads: []
    };

    async componentDidMount() {
        const response = await fetch('/api/download');
        this.setState({
            downloads: await response.json()
        });
        this.on('activity.download', this.onDownloadsChanged);
    }

    onDownloadsChanged(...data) {
        console.log(data);
    }

    deleteDownload(id) {

    }

    render() {
        return (
            <Table striped size="sm" hover responsive>
                <thead>
                    <tr>
                        <th>Type</th>
                        <th>Name</th>
                        <th>Downloader</th>
                        <th>Progress</th>
                        <th>Size</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                {this.state.downloads.map(d => (
                    <tr key={`download-${d.id}`}>
                        <td>
                            <Badge color='success'>{d.type}</Badge>
                        </td>
                        <td>
                            {d.url}
                        </td>
                        <td>
                            {d.clientid}
                        </td>
                        <td>
                            <Progress value={d.percentage} />
                        </td>
                        <td>
                            {d.size}&nbsp;GB
                        </td>
                        <td>
                            <Button color='danger' size='sm' onClick={() => this.deleteDownload(d)}>Delete</Button>
                        </td>
                    </tr>
                ))}
                </tbody>
            </Table>
        );
    }
}

export default DownloadActivity;
