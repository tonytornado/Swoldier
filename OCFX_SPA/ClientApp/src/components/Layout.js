import React, { Component } from 'react';
//import { Container } from 'reactstrap';
import { NavMenu } from './sections/NavMenu';
import { Footer } from './sections/Footer';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div>
        <NavMenu />
        <div className="container">
          {this.props.children}
        </div>
        <Footer />
      </div>
    );
  }
}
