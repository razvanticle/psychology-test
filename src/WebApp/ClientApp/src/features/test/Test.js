import React, { Component } from 'react';

export default class Test extends Component {
  static displayName = Test.name;

  constructor(props) {
    super(props);
    this.state = { template: [], loading: true };
  }

  componentDidMount() {
    this.getTestTemplate();
  }

  static renderTestTemplate(template) {
    return (
        <h1>{template.title}</h1>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : Test.renderTestTemplate(this.state.template);

    return (
      <div>        
        {contents}
      </div>
    );
  }

  async getTestTemplate() {    
    const template=await fetch('https://localhost:7137/templates/1');
    const templateData= await template.json();
    
    this.setState({ template: templateData, loading: false });
  }
}
