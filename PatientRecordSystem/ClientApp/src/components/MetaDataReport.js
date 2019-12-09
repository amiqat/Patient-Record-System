import React, { Component } from 'react';
import { getMetaData } from "../services/metaDataService";
import { Link } from "react-router-dom";


class MetaDataReport extends Component {
    static displayName = MetaDataReport.name;

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
    }
    RenderList() {
        const { data } = this.state;
        console.log(data.topMetaData);
        if (!data.topMetaData || data.topMetaData.length === 0) {
            return (<ul></ul>)
        }

            return (
                <ul>
                    {data.topMetaData.map(item => 
                     <li key={item.key}>{item.key}  <span className="badge badge-info">{item.count} </span></li>
                    )}

                </ul>
            );
        

   
}
    render() {
        const { loading, data } = this.state;
        
    
        return (
            <React.Fragment>
                <div className="card p-2">
                    <p>
                        Average namber of used meta-data for patient:  <span className="badge badge-info">{data.averagePerPatient} </span>
                    </p>    
                <p>
                    Max namber of used meta-data for patients:  <span className="badge badge-info">{data.maxPerPatient} </span>
                    </p>
                    <p>
                       Top 3 highest keys used in meta-data: 
                    </p>
                    
                        {this.RenderList()}
                    
                    </div>
            </React.Fragment>
        );
    }
    async populateData() {

        return await getMetaData();
    }



}

export default MetaDataReport;