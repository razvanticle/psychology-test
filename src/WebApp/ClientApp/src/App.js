import React, { Component } from 'react';
import { Route } from 'react-router';
import Layout from './components/layout';
import Home from './features/home';
import Test from './features/test';
import About from './features/about';
import Result from "./features/result";

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />        
        <Route path='/tests/:id' component={Test} />
        <Route path='/results/:id' component={Result} />
        <Route path='/about' component={About} />
      </Layout>
    );
  }
}
