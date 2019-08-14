import React, { Component } from 'react';

export class About extends Component {
    render() {
        return (
            <div className="text-center">
                <div className="py-4 row align-items-md-center">
                    <div className="col-md-6">
                        <img
                            src="https://images.pexels.com/photos/326559/pexels-photo-326559.jpeg?auto=compress&cs=tinysrgb"
                            alt="Swoldier Description"
                            className="img-fluid about-photo" />
                    </div>
                    <div className="col-md-6 text-md-left">
                        <h3>The Swoldier Project</h3>
                        <h6 className="lead">This is a project meant to guide you through the world of health and fitness.</h6>
                        <p>In 2018, a man by the name of Tony T. had an idea for a friend: Get them into fitness instead of going to D&D.
                            That friend did not take the plunge into the realm of dumbells and squat racks until they were told that it would better them in the long run, and maybe get them out of the house a bit.
                Of course, he had to find ways of making it fun and enjoyable; but also make it compelling enough for his friend to keep at it with himself and others joining along on his journey.</p>
                        <p>This was the background story behind one of the most ambitious ideas for those looking to get better acquainted with the Swole life without leaving the nerd life.</p>
                    </div>
                </div>
                <hr />
                <div className="py-4 row align-items-md-center">
                    <div className="col-md-6 order-md-2">
                        <img
                            src="https://images.pexels.com/photos/343/man-couple-people-woman.jpg?auto=compress&cs=tinysrgb"
                            alt="Swoldier Description"
                            className="img-fluid about-photo" />
                    </div>
                    <div className="col-md-6 text-md-right">
                        <h3>The Fitness Campaigns</h3>
                        <h6 className="lead">Instead of slaying regular demons, why not slay some inner demons?</h6>
                        <p>The campaigns created here are designed to take a player through the basics of getting in shape. Such topics covered include:</p>
                        <ul className="list-group list-unstyled">
                            <li>Basic form and movements for weight training, be it bodyweighted or heavy-plated</li>
                            <li>Gym guides and etiquette</li>
                            <li>Nutrition, recovery, and basic kinesiology</li>
                        </ul>
                    </div>
                </div>
                <hr />
                <div className="py-4 row align-items-md-center">
                    <div className="col-md-6">

                        <img
                            src="https://images.pexels.com/photos/39671/physiotherapy-weight-training-dumbbell-exercise-balls-39671.jpeg?auto=compress&cs=tinysrgb"
                            alt="Swoldier Description"
                            className="img-fluid about-photo" />
                    </div>
                    <div className="col-md-6 text-md-left">
                        <h3>The Path to Greatness</h3>
                        <h6 className="lead">Swole but Steady Wins the Race</h6>
                        <p>DM's and Coaches can coexist in the same place with:</p>
                        <ul className="list-group list-unstyled">
                            <li>Customizable plans from leading coaches translated by experienced dungeon masters</li>
                            <li>Fitness experts and accountability buddies to keep quest-goers moving forward</li>
                            <li>24/7 support via forums</li>
                            <li>Personalized profiles for those looking to party up and take on a challenge together</li>
                        </ul>
                    </div>
                </div>
            </div>
        );
    }
}