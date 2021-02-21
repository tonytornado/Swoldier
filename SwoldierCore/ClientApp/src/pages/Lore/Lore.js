import React, { Component } from 'react';
import { LoreStory } from './LoreStory';

class Lore extends Component(props) {
    constructor(props) {
        super(props);
        this.state = {
            loaded: false,
            lore: []
        }
    }

    render() {
        <main>
            <section>
                <h2 className="display-4">
                    Lore Section
                </h2>
                <p className="lead">
                    What's a game if there isn't any lore?
                </p>
            </section>
            {p.map(l => {
                LoreStory(l);
            })}
            <section>
                <h5>Create more lore...</h5>
                <p>Obviously, you can add more things here.</p>
            </section>
        </main>
    }
}


