import React, { Component } from 'react';

export class ExerciseModal extends Component {
    constructor(props) {
        super(props);
        this.state({
            types: [],
            groups: [],
            loading: false
        });
    }

    componentDidMount() {
        // Get the exercise groups
        fetch("api/Fitness/ExerciseTypes")
            .then(response => {
                if (response.ok) {
                    return response.json()
                } else {
                    throw Error(`Something has screwed up: ${response.status}`);
                }
            })
            .then(data => {
                this.setState({
                    types: data,
                });
            })
            .catch(console.error);

        // Get the muscle groups
        fetch("api/Fitness/MuscleTypes")
            .then(response => {
                if (response.ok) {
                    return response.json()
                } else {
                    throw Error(`Something has screwed up: ${response.status}`);
                }
            })
            .then(data => {
                this.setState({
                    groups: data,
                    loading: true
                });
            })
            .catch(console.error);
    }

    handleSubmit(e) {

    }

    render() {
        return (
            <div>
                <form method='post'>
                    <div className='form-control'>
                        <label htmlFor='EName' />
                        <input name='EName' className='form-control' />
                    </div>
                </form>
            </div>
        );
    }
}