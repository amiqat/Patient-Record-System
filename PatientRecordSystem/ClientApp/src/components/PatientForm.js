import React from "react";
import Joi from "joi-browser";
import Form from "./common/form";
import { getPatient, savePatient } from "../services/PatientService";
import { toast } from 'react-toastify';
import { clean } from "../services/httpService";
import * as moment from 'moment';

class PatientForm extends Form {
    state = {
        data: {
            PatientName: "",
            OffcialId: "",
            DateOfBirth:new Date(),
            Email: "",
            MetaData: [],
        },
       
        errors: {}
    };

    schema = {
        id: Joi.number(),
        PatientName: Joi.string()
            .required()
            .label("Patient Name"),
        OffcialId: Joi.number()
            .required()
            .label("Official Id"),
        DateOfBirth: Joi.optional().allow(null)
            .label("Date Of Birth"),
        Email: Joi.string().optional().allow(null).allow('').label("Email Address"),
        MetaData: Joi.optional().allow([]).allow(null)
    };

    async populatePatient() {
        try {
            const PatientId = this.props.match.params.id;
            if (!PatientId) return;

            let { data: patient } = await getPatient(PatientId);
            patient = clean(this.mapToViewModel(patient));
            console.log(patient);
            await this.setState(prevState => ({
                data: { ...prevState.data, ...patient }
            }));
            console.log();
        } catch (ex) {
            if (ex.response && ex.response.status === 404)
                this.props.history.replace("/not-found");
        }
    }

    async componentDidMount() {
        await this.populatePatient();
    }

    mapToViewModel(patient) {
        return {
            id: patient.id,
            PatientName: patient.patientName,
            DateOfBirth: moment(patient.dateOfBirth).format("YYYY-MM-DD"),
            Email: patient.email,
            OffcialId: patient.offcialId,
            MetaData: patient.metaData
        };
    }

    doSubmit = async () => {
        try {
            await savePatient(this.state.data);
            this.props.history.push("/Patients");
        } catch (error) {
            if (error.response && error.response.status === 400) {
             
                toast.error('Error Submiting data');
                const errors = error.response.data.errors ? error.response.data.errors : error.response.data;
              
                if (errors) { 
                    this.setState({ errors });
                }
            }
        }
    }

    render() {
        return (
         
            <div className = "card p-2">
                <h1>Patient Form</h1>
                <form onSubmit={this.handleSubmit}>
                    {this.renderInput("PatientName", "PatientName")}

                    {this.renderInput("OffcialId", "Official Id")}
                    {this.renderInput("DateOfBirth", "Date Of Birth", "date")}
                    {this.renderInput("Email", "Email Address", "email")}
                    {this.renderButton("Save")}
                    <button type="rest" className="btn btn-danger m-2" onClick={() => this.props.history.push("/Patients")}> Cancle</button>
                </form>
            </div>
        );
    }
}

export default PatientForm;