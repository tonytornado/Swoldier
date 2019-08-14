import React, { Component } from 'react';

export class Workout extends Component {
    static displayName = Workout.name;

    constructor(props) {
        super(props);
        this.state = {
            workouts: [],
            loading: true,
            error: false,
        };
    }

    componentDidMount() {
        fetch('api/Fitness/GetWorkouts')
            .then(response => {
                if (response.ok) {
                    return response.json()
                } else {
                    throw Error(`Something has screwed up: ${response.status}`);
                }
            })
            .then(data => {
                this.setState({
                    workouts: data,
                    loading: false,
                    error: false
                });
            }).catch(
                console.error,
                this.setState({
                    workouts: [],
                    loading: false,
                    error: true
                })
            )
    }


    static renderWorkouts(workouts) {
        return (
            <table className='table table-striped'>
                <thead className='thead-dark'>
                    <tr>
                        <th>Name</th>
                        <th>Description</th>
                        <th>Muscle Group</th>
                        <th>Duration</th>
                        <th>Date</th>
                    </tr>
                </thead>
                <tbody>
                    {workouts.map(w =>
                        <tr key={w.id}>
                            <td>{w.title}</td>
                            <td>{w.description}</td>
                            <td>{w.targetedMuscles}</td>
                            <td>{w.duration}</td>
                            <td>{w.dateAdded}</td>
                        </tr>
                )}
                </tbody>
            </table>
        );
    }

    render() {
        let table = this.state.loading
            ? <p className="text-center"><i className="fas fa-spinner"></i></p>
            : Workout.renderWorkouts(this.state.workouts)

        return (
            <section>
                <h1>Current Workouts in Library</h1>
                    {table}
            </section>
        );
    }
}