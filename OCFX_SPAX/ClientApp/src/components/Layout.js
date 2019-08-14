import React, { Component } from 'react';
import { NavMenu } from './NavMenu';
import "./site.css";

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div>
        <NavMenu />
        <div className="container body-content py-5">
          {this.props.children}
        </div>
      </div>
    );
  }
}
