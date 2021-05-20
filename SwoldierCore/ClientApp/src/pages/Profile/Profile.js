import React, { Component } from 'react';
import authService from '../../components/api-authorization/AuthorizeService';
import { Wall } from './Wall/Wall';
import { Loader } from '../../components/modules/Loader';
import '../../css/squares.css';

export default class Profile extends Component {
  static displayName = Profile.name;

  constructor(props) {
    super(props);

    this.state = {
      loaded: false,
      profile: [],
      wall: []
    };
  }

  componentDidMount() {
    this.getProfile();
  }

  async getProfile() {
    const token = await authService.getAccessToken();
    const user = await authService.getUser();
    if (user !== null || user !== undefined) {
      const response = await fetch(`api/Profile/userway?username=${user.name}`, {
        headers: !token ? {} : {
          'Authorization': `Bearer ${token}`
        }
      });
      const data = await response.json();
      this.setState({
        loaded: true,
        profile: data
      })
    }
  }

  // async getWall(wallId) {
  //   const token = await authService.getAccessToken();
  //   if (user) {
  //     const response = await fetch(`api/Profile/userway?username=${user.name}`, {
  //       headers: !token ? {} : {
  //         'Authorization': `Bearer ${token}`
  //       }
  //     });
  //     const data = await response.json();
  //     this.setState({
  //       wall: data
  //     })
  //   }
  // }

  render() {
    const pa = this.state.profile;
    const wall = [
      { "postId": 1, "postText": "Where are we?" },
      { "postId": 2, "postText": "Where are we?" }
    ];
    const photoset = [
      { "src": "https://via.placeholder.com/300" },
      { "src": "https://via.placeholder.com/300" }
    ];

    if (this.state.loaded !== true && user) {
      return <Loader />
    }

    return (
      <main>
        <section id="ProfileBadge" className="row rounded border shadow p-3 my-3">
          {/* {this.GetProfilePhoto(pa)} */}

          <div id="NameLoc" className="col-md">
            <img src="https://via.placeholder.com/300" width="300px" height="300px" className="rounded mx-auto d-block" />
            <div className="text-center">
              <h2>{pa.fullName}</h2>
              <h4>{pa.location}</h4>
            </div>
          </div>
        </section>
        <section className="row rounded border shadow p-3 my-3">
          <div className="col-md">
            <h5>Age</h5>
            <p>{pa.age}</p>
          </div>
          <div className="col-md">
            <h5>Bio</h5>
            <p>{pa.bio}</p>
          </div>
        </section>
        <section id="Social" className="row">
          {/* <Social /> */}
          {/* <Photoset photos={photoset} /> */}
          {/* {renderWall(this.state.wall)} */}
        </section>
      </main>
    );
  }

  renderWall(wall){
    if(wall.Length > 0){
      return <div>No Wall Data Available</div>
    }

    return <Wall posts={wall} />
  }

  GetProfilePhoto(pa) {
    return <div id="ProfilePhoto">
      <p>
        <img src={`${pa.profilePhoto}.jpg`} alt="" />
      </p>
    </div>;
  }
}


