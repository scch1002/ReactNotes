import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as NotesState from '../store/Notes';

// At runtime, Redux will merge together...
type NotesProps =
    NotesState.NotesState        // ... state we've requested from the Redux store
    & typeof NotesState.actionCreators
    & RouteComponentProps < {} > // ... plus action creators we've requested

class Notes extends React.Component<NotesProps, {}> {
    componentWillMount() {
        this.props.requestNotes();
    }

    componentWillReceiveProps(nextProps: NotesProps) {
        this.props.requestNotes();
    }

    public render() {
        return <div>
            <h1>Notes</h1>
            <p>These are the stored notes</p>
            {this.renderNotesTable()}
        </div>;
    }

    private renderAddNote() {
        return <div>
            <button content='Add' />
            <input type='text' />
        </div>;
    }

    private renderNotesTable() {
        return <table className='table'>
            <thead>
                <tr>
                    <th>Date</th>
                    <th>Text</th>
                </tr>
            </thead>
            <tbody>
                {this.props.notes.map(note =>
                    <tr key={note.id}>
                        <td>{note.time}</td>
                        <td>{note.text}</td>
                    </tr>
                )}
            </tbody>
        </table>;
    }
}

export default connect(
    (state: ApplicationState) => state.notes, // Selects which state properties are merged into the component's props
    NotesState.actionCreators                 // Selects which action creators are merged into the component's props
)(Notes) as typeof Notes;
