import React, { Component } from 'react';

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = { occupations: [], loading: true, calculatedPremiumText: '', messagestyle: '' };
        this.calculatePremium = this.calculatePremium.bind(this);
    }
    async calculatePremium() {
        var customername = this.refs.customername.value;
        var occupation = this.refs.selectedoccupation.value;
        var age = this.refs.customerage.value;
        var dob = this.refs.customerdob.value;
        var coveramount = this.refs.suminsured.value;
        if (customername === '' || occupation === '' || age === '' || dob === '' || coveramount === '') {
            this.setState({
                calculatedPremiumText: 'All fields are mandatory'
            });
            this.setState({ messagestyle: 'red' });
        }
        else {
            await fetch('PremiumCalculator/GetPremium?Occupation=' + this.refs.selectedoccupation.value + '&Age=' + this.refs.customerage.value + '&CoverAmount=' + this.refs.suminsured.value)
                .then(response => response.json())
                .then(response =>
                    this.setState({ calculatedPremiumText: 'Calculated Premium: ' + response, messagestyle: 'black' })
                );
        }
    }
    componentDidMount() {
        this.populateOccupationsData();
    }

    static renderOccupationsDropdown(occupations) {
        return (

            <select key="OccupationsDdl" ref="selectedoccupation" name="Occupations">
                {occupations.map(forecast =>
                    <option key={forecast.occupationName} value={forecast.occupationName}>{forecast.occupationName}</option>
                )}
            </select>
        );
    }

    render() {
        let occupationsPlaceHolder = this.state.loading
            ? <em>Loading...</em>
            : Home.renderOccupationsDropdown(this.state.occupations);

        return (
            <div>
                <h1 id="tabelLabel" >Premium Calculator</h1>
                <p>Name: <input required ref="customername" type="text" id="customername" /></p>
                <p>Age: <input ref="customerage" type="text" id="customerage" /></p>
                <p>Date Of Birth: <input ref="customerdob" type="text" id="customerdob" /></p>
                <p>Occupation: {occupationsPlaceHolder}</p>
                <p>Death - Sum Insured: <input ref="suminsured" type="text" id="suminsured" /></p>
                <button onClick={this.calculatePremium}>Calculate</button>
                <p><label style={{color: this.state.messagestyle}}>{this.state.calculatedPremiumText}</label></p>
            </div>
        );
    }

    async populateOccupationsData() {
        const response = await fetch('PremiumCalculator/GetOccupations');
        const data = await response.json();
        this.setState({ occupations: data, loading: false });
    }
}
