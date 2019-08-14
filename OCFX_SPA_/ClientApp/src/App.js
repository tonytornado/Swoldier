import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { About } from './components/About';
import { Workout } from './areas/Workouts';
import { Exercise } from './areas/Exercises';

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route path='/about' component={About} />
                <Route path='/exercise' component={Exercise} />
                <Route path='/workout' component={Workout} />
            </Layout>
        );
    }
}
