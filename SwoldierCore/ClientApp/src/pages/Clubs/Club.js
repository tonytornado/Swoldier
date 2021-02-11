import { Component } from 'react';


export default class Club extends Component {
  static displayName = Club.name;

  constructor(props) {
    super(props);

    this.state = {
      loaded: false,
      profile: []
    };
  }
}
