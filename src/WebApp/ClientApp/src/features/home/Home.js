import React, { Component } from 'react';
import { Link } from "react-router-dom";

export default class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <h1>Welcome to Psychologies!</h1>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus lacinia massa luctus diam vestibulum imperdiet. Donec porta consequat dolor nec ultrices. Sed pulvinar ante ac elementum lacinia.</p>
                  
          <hr/>
          <h1><Link to='/test/1'>Start personality test</Link></h1>
      </div>
    );
  }
}
