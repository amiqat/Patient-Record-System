import React from "react";
import Joi from "joi-browser";
import Form from "./common/form";
import { getRecord, saveRecord } from "../services/RecordService";
import { toast } from 'react-toastify';
import { clean } from "../services/httpService";
import * as moment from 'moment';

class RecordForm extends Form {
    state = {
        data: {
            PatientId: "",
            DiseaseName: "",
            Amount: 0,
            TimeOfEntry:new Date(),
            Discripion: "",
        },
        errors: {}
    };

    schema = {
        id: Joi.number(),
        PatientId: Joi.number()
            .required()
            .label("Patient Id"),
        DiseaseName: Joi.string()
            .required()
            .label("Disease Name"),
        Amount: Joi.number()
            .required()
            .label("Bill"),
        TimeOfEntry: Joi.optional().allow(null)
            .label("Time Of Entry"),
        Discripion: Joi.string().optional().allow(null).allow('')
    };

    async populateData() {
        try {
            const RecoirdId = this.props.match.params.id;
            if (!RecoirdId) return;

            let { data: record } = await getRecord(RecoirdId);
            record = clean(this.mapToViewModel(record));
            console.log(record);
            await this.setState(prevState => ({
                data: { ...prevState.data, ...record }
            }));
            console.log();
        } catch (ex) {
            if (ex.response && ex.response.status === 404)
                this.props.history.replace("/not-found");
        }
    }

    async componentDidMount() {
        await this.populateData();
    }

    mapToViewModel(record) {
        return {
            id: record.id,
            PatientId: record.patientId,
            TimeOfEntry: moment(record.timeOfEntry).format("YYYY-MM-DD"),
            DiseaseName: record.diseaseName,
            Discripion: record.discripion,
            Amount: record.amount
        };
    }

    doSubmit = async () => {
        try {
            await saveRecord(this.state.data);
            this.props.history.push("/Records");
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
            <div className="card p-2">
                <h1>Record Form</h1>
                <form onSubmit={this.handleSubmit}>
                    {this.renderInput("PatientId", "Patient Id")}

                    {this.renderInput("DiseaseName", "Disease Name")}
                    {this.renderInput("Amount", "Bills","number")}
                    {this.renderInput("TimeOfEntry", "Time Of Entry", "date")}
                    {this.renderInput("Discripion", "Discripion")}
                    {this.renderButton("Save")}
                    <button type="rest" className="btn btn-danger m-2" onClick={() => this.props.history.push("/Records")}> Cancle</button>
                </form>
            </div>
        );
    }
}

export default RecordForm;