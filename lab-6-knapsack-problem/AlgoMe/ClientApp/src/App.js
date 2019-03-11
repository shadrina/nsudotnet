import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { RequestList } from './components/RequestList';
import { NewRequestForm } from './components/NewRequestForm';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={NewRequestForm} />
        <Route path='/fetch-data' component={RequestList} />
      </Layout>
    );
  }
}
