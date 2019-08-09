import React, { Component } from 'react';
import { ExerciseModal } from '../modals/ExerciseModal';

export class Workout extends Component {
    static displayName = Workout.name;

    constructor(props) {
        super(props);
        this.state = {
            exercises: [],
            loading: true,
            error: false,
        };
    }

    componentDidMount() {
        fetch('api/Fitness/GetExercises')
            .then(response => {
                if (response.ok) {
                    return response.json()
                } else {
                    throw Error(`Something has screwed up: ${response.status}`);
                }
            })
            .then(data => {
                this.setState({
                    exercises: data,
                    loading: false,
                    error: false
                });
            }).catch(
                console.error,
                this.setState({
                    exercises: [],
                    loading: false,
                    error: true
                })
            )
    }

    // Static method for rendering the table.
    static renderExercise(exercises) {
        return (
            <table className='table table-striped'>
                <thead className='thead-dark'>
                    <tr>
                        <th>Name</th>
                        <th>Type</th>
                        <th>Muscle Group</th>
                        <th>Description</th>
                    </tr>
                </thead>
                <tbody>
                    {exercises.map(e =>
                        <tr key={e.id}>
                            <td>{e.name}</td>
                            <td>{e.exerType}</td>
                            <td>{e.targetedMuscles}</td>
                            <td>{e.description}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let table = this.state.loading
            ? <p className="text-center"><i className="fas fa-spinner"></i></p>
            : Workout.renderWorkouts(this.state.exercises)

        return (
            <section>
                <h1>Exercises prepared</h1>
                <p>Workouts loaded</p>
                {table}
            </section>
            <section>
                <h4>
                    Load new exercise
                    <ExerciseModal />
                </h4>
            </section>
        );
    }
}