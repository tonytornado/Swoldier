import React, { Component } from 'react';
import { NavMenu } from './sections/NavMenu';
import { Footer } from './sections/Footer';
import "./site.css";
//import { Header } from './sections/Header';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div>
        <NavMenu />

        <div className="container body-content py-5">
          {this.props.children}
        </div>
        <Footer />
      </div>
    );
  }
}
