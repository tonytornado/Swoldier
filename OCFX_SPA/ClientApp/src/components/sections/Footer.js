import React, { Component } from 'react';

export class Footer extends Component {
    static displayName = Footer.name

    render() {
        return (
            <footer className="bg-dark text-white">
                <div className="text-center lead">
                    <p>
                        &copy;  Phantom-Hex 2019
                    </p>
                </div>
            </footer>
        );
    }
}