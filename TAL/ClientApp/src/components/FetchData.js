import React, { Component } from 'react';

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = { forecasts: [], loading: true };
  }

  componentDidMount() {
      this.populateOccupationsData();
  }

  static renderOccupationsDropdown(occupations) {
      return (

          <select key="OccupationsDdl" name="Occupations">
              {occupations.map(forecast =>
                  <option key={forecast.occupationName} value={forecast.occupationName}>{forecast.occupationName}</option>
                )}
          </select>
        );
  }

  render() {
    let contents = this.state.loading
      ? <em>Loading...</em>
      : FetchData.renderOccupationsDropdown(this.state.occupations);

    return (
      <div>
        <h1 id="tabelLabel" >Premium Calculator</h1>
            <p>Name: <input type="text" id="customername" /></p>
            <p>Age: <input type="text" id="customerage" /></p>
            <p>Date Of Birth: <input type="text" id="customerdob" /></p>
            <p>Occupation: {contents}</p>
            <p>Death - Sum Insured: <input type="text" id="suminsured" /></p>
        
      </div>
    );
  }

  async populateOccupationsData() {
      const response = await fetch('PremiumCalculator/GetOccupations');
    const data = await response.json();
    this.setState({ occupations: data, loading: false });
  }
}
