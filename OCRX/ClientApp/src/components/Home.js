import React, { Component } from 'react';

export class Home extends Component {
    static displayName = Home.name;

    render() {
        return (
            <main>
                <div className="text-center">
                    <p>This is a simple project meant to encourage users to jump into fitness without losing too much sleep over how to actually do it.</p>
                    <p>While this is an application still in development, you can enjoy some of the many features already packed into it already and design your own fitness lore!</p>
                </div>
                <hr />
                <div className="row py-3">
                    <div className="col-md-6">
                        <img src="https://images.pexels.com/photos/866027/pexels-photo-866027.jpeg?auto=compress&cs=tinysrgb&h=750&w=1260" alt="A thing" className="front-photo" />
                    </div>
                    <div className="col-md-6 align-self-center">
                        <h4>Teamwork and Dreamwork</h4>
                        <p className="lead">There's a whole new thing happening here; and it's all for you and your friends.</p>
                        <p>We're bringing you the first application to put LFG to rest and skills to the test when it comes to your fitness goals! We'll walk you through the steel jungle.</p>
                    </div>
                </div>
                <div className="row py-3">
                    <div className="col-md-6 order-md-2">
                        <img src="https://images.pexels.com/photos/685534/pexels-photo-685534.jpeg?auto=compress&cs=tinysrgb&h=750&w=1260" alt="A thing" className="front-photo" />
                    </div>
                    <div className="col-md-6 order-md-1 align-self-center">
                        <h4>Dungeons and Dumbbells</h4>
                        <p className="lead">It's all about coming together.</p>
                        <p>Who would've thought to get your friends out of the know of fitness into it with - what else - Dungeons and Dragons? I mean, how the heck do you even record stats? We found a way to both organize your swole mates and keep you all together for truly active campaigns.</p>
                    </div>
                </div>
                <hr />
                <div className="text-justify container">
                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin vestibulum eros nec massa luctus vulputate. Nullam eleifend semper turpis ac laoreet. Phasellus augue ipsum, tempor ac libero venenatis, semper hendrerit ante. In condimentum hendrerit arcu et mattis. Sed sit amet sem fermentum, fermentum nibh vitae, facilisis dui. Mauris a bibendum leo. Phasellus tincidunt, metus vitae hendrerit ullamcorper, massa justo pretium ante, porttitor egestas mi justo ac nibh. Mauris ut dictum lectus, vel malesuada odio. Phasellus non nisl sed ipsum dignissim feugiat sit amet sit amet purus. Nulla facilisi. Praesent quis bibendum lacus.</p>

                    <p>Sed efficitur dignissim cursus. Nullam at tempor lectus, eu sodales quam. Vestibulum id pellentesque turpis. Aenean ut libero vel mi mollis imperdiet. Donec eget elit lobortis, porttitor mi eu, pharetra orci. Nunc fermentum metus eget nulla hendrerit, vitae molestie ex dictum. Aliquam ac libero feugiat, maximus est at, sagittis lectus. Donec varius nulla quis justo egestas, nec sodales libero posuere. Ut ut faucibus magna. Integer ultricies ex sit amet quam sodales molestie. Donec varius metus ut mi sagittis laoreet. In vel diam vitae lectus tristique pellentesque eu et risus. Mauris sit amet malesuada purus, non vehicula augue.</p>

                    <p>Aliquam accumsan risus odio, et iaculis libero placerat a. Suspendisse ullamcorper condimentum ligula, nec iaculis dolor fermentum sit amet. In rutrum ex mauris, quis facilisis tellus iaculis ut. Etiam a placerat enim. Sed quis tincidunt velit. Aenean mattis gravida elementum. Curabitur neque libero, egestas vel bibendum ut, mattis mattis mi. Integer imperdiet urna lacinia pharetra lobortis. Sed quam odio, dignissim accumsan ex nec, scelerisque vestibulum nibh. Nulla sollicitudin lorem turpis, at viverra est luctus sit amet. Cras ut pulvinar mi, ut eleifend ante.</p>
                </div>
            </main>
        );
    }
}

