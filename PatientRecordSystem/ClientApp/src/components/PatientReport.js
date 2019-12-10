import React, { Component } from 'react';
import { getPatientReport } from "../services/PatientService";

class PatientReport extends Component {
    static displayName = PatientReport.name;

    constructor(props) {
        super(props);
        this.state = {
            data: [],
            loading: false,
        };
    }

    async componentDidMount() {
        this.setState({ loading: true });
        const response = await this.populateData();

        await this.setState({
            data: response.data,
        });
        this.setState({ loading: false });
    }
    RenderRecord() {
        const { data } = this.state;
        console.log(data.record);
        if (!data.record) {
            return (<p>5th record entry : </p>)
        }
        return (
            <p>
                5th record entry : disease Name:  <span className="badge badge-info m-2">{data.record.diseaseName} </span>
                Time of entry : <span className="badge badge-info">{data.record.timeOfEntry} </span>
            </p>
        );
    }
    RenderPatient() {
        const { data } = this.state;
        console.log(data.patientDiseases);
        if (!data.patientDiseases) {
            return (<p>patients with similar diseases : </p>)
        }
        return (
            <React.Fragment>
                <p>
                patients with similar diseases :
            </p>
            <ul>
                    {data.patientDiseases.map(item =>
                        <li key={item.patientName}>{item.patientName}: <span className="badge badge-info"> {item.diseaseList.join(' , ')} </span></li>
                )}

            </ul>
            </React.Fragment>

           
        );
    }
    render() {
        const { data, loading } = this.state;
        if (loading) {
            return (<p><em>Loading...</em></p>)
        }
        return (

            <div className="card p-2">
                <p>
                    Name of patient:  <span className="badge badge-info">{data.patientName} </span>
                </p>
                <p>
                    Age:  <span className="badge badge-info">{data.age} </span>
                </p>
                <p>
                    Average of bills:  <span className="badge badge-info">{data.avg} </span>
                </p>
                <p>
                    Average of bills without outliers:  <span className="badge badge-info">{data.avgW} </span>
                </p>
             
                {this.RenderRecord()}
                {this.RenderPatient()}
                <p>
                    Most visit mounth :  <span className="badge badge-info">{data.mostVisitMounth} </span>
                </p>
                <button className="col-2 btn btn-info m-2" onClick={() => this.props.history.push("/Patients")}>Back</button>
            </div>

        );
    }
    async populateData() {
        return await getPatientReport(this.props.match.params.id);
    }
}

export default PatientReport;