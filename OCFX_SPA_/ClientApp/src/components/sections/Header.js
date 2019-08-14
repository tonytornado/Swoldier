import React, { Component } from 'react';

export default class Header extends Component {
    static displayName = Header.name;

    constructor(props) {
        super(props);
        this.state({
            Title: "",
            Header: this.Title,
            SubHeader: "",
        });
    }

    render() {
        return (
            <div class="text-center py-3 shadow bg-secondary text-light">
                <h2>{this.state.Header}</h2>
                <h5>{this.state.SubHeader}</h5>
            </div>
        );
    }
}