import React, { Component } from 'react';
import { Link } from "react-router-dom";

export default class About extends Component {
  static displayName = About.name;

  render () {
    return (
      <div>
        <h1>About Psychologies!</h1>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus lacinia massa luctus diam vestibulum imperdiet. Donec porta consequat dolor nec ultrices. Sed pulvinar ante ac elementum lacinia. Quisque lacinia placerat ligula, vel efficitur dui euismod et. Sed condimentum faucibus velit sed consequat. Etiam arcu mauris, maximus nec ultricies sit amet, dignissim nec ante. Aliquam id dolor scelerisque, convallis arcu at, aliquet arcu. Integer augue nisi, porta ut malesuada eget, maximus sed magna. Quisque eget urna vel justo congue consequat at ac augue.</p>
      </div>
    );
  }
}
