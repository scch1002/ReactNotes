import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface NotesState {
    newNote: Note;
    notes: Note[];
}

export interface Note {
    id: string;
    text: string;
    time: Date;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface AddNoteAction {
    type: 'ADD_NOTE';
    newNote: Note
}

interface RequestNotesAction {
    type: 'REQUEST_NOTES';
}

interface ReceiveNotesAction {
    type: 'RECEIVE_NOTES';
    notes: Note[];
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestNotesAction | ReceiveNotesAction | AddNoteAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestNotes: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        let fetchTask = fetch('api/Notes/')
            .then(response => response.json() as Promise<Note[]>)
            .then(data => {
                dispatch({ type: 'RECEIVE_NOTES', notes: data });
            });

        addTask(fetchTask); // Ensure server-side prerendering waits for this to complete
        dispatch({ type: 'REQUEST_NOTES'});
    },
    addNote: (): AppThunkAction<AddNoteAction> => (dispatch, getState) => {
        
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: NotesState = { notes: [], newNote: { id: '0', text: '', time: new Date() } };

export const reducer: Reducer<NotesState> = (state: NotesState, incomingAction: Action) => {
    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'RECEIVE_NOTES':
                return {
                    notes: action.notes,
                    newNote: state.newNote 
                };
        case 'ADD_NOTE':
            return {
                notes: state.notes,
                newNote: { id: '0', text: '', time: new Date() }
            }; 
        default:
    }

    return state || unloadedState;
};
