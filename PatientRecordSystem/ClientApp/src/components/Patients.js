import React, { Component } from 'react';
import DataTable from 'react-data-table-component';
import * as moment from 'moment';
import { getPatients } from "../services/PatientService";
import { Link } from "react-router-dom";
const columns = [
    {
        name: 'ID number',

        selector: 'id',
        sortable: true,
    },
    {
        name: 'Patient Name',
        selector: 'patientName',
        sortable: true,
    },

    {
        name: 'data of birth',
        selector: 'dateOfBirth',
        format: row => row.dateOfBirth &&  moment(row.dateOfBirth).format('lll'),
        sortable: true,
    },
    {
        name: 'Last entry',
        selector: 'lastEntry',
        format: row => row.lastEntry && moment(row.lastEntry).format('lll'),
        sortable: true,
    },
    {
        name: 'Meta-Data count',
        selector: 'metaDataCount',
        sortable: true,
    },
    {
        cell: (row) => <Link to={`/PatientForm/${row.id}`}>Edit</Link>,
    },
    {
        cell: (row) => <Link to={`/PatientReport/${row.id}`}>Report</Link>,
    }

];

class Patients extends Component {
    static displayName = Patients.name;

    constructor(props) {
        super(props);
        this.state = {
            query: {},
            data: [],
            loading: false,
            totalRows: 0,
            perPage: 10,
        };
    }

    async componentDidMount() {
        this.setState({ loading: true });
        const { perPage } = this.state;

        await this.setState(prevState => ({
            query: {
                ...prevState.query,
                PageNumber: 1, PageSize: perPage
            }
        }));

        console.log(this.state.query);
        const response = await this.populatePatientsData();

        const pagination = JSON.parse(response.headers['x-pagination']);
        this.setState({
            data: response.data,
            totalRows: pagination.TotalCount,
            loading: false,
        });
    }
    render() {
        const { loading, data, totalRows } = this.state;

        return (
            <div className="card p-2">
            <Link
                to="/patientForm"
                className="btn btn-primary col-2"
                style={{ marginBottom: 20 }}
            >
                New Patient
          </Link>
            <DataTable
                title="Patients"
                columns={columns}
                data={data}
                progressPending={loading}
                onSort={this.handleSort}
                sortServer
                pagination
                paginationPerPage={10}
                paginationRowsPerPageOptions={[ 5,10, 15, 20, 25, 30]}
                paginationServer
                paginationTotalRows={totalRows}
                onChangeRowsPerPage={this.handlePerRowsChange}
                onChangePage={this.handlePageChange}
                />
                </div>
        );
    }
    async populatePatientsData() {
        const { query } = this.state;

        return await getPatients(query);
    }
    handlePerRowsChange = async (perPage, page) => {
        this.setState({ loading: true });
        await this.setState(prevState => ({
            query: {
                ...prevState.query,
                PageNumber: page, PageSize: perPage
            }
        }));
        const response = await this.populatePatientsData()
        const pagination = JSON.parse(response.headers['x-pagination']);
        console.log(response);
        this.setState({
            loading: false,
            data: response.data,
            perPage,
            totalRows: pagination.TotalCount
        });
    }
    handlePageChange = async page => {
        this.setState({ loading: true });
        await this.setState(prevState => ({
            query: {
                ...prevState.query,
                PageNumber: page
            }
        }));

        const response = await this.populatePatientsData();
        const pagination = JSON.parse(response.headers['x-pagination']);
        this.setState({
            loading: false,
            data: response.data,
            totalRows: pagination.TotalCount
        });
    }
    handleSort = async (column, sortDirection) => {
        console.log(sortDirection === 'asc');
        console.log(column.selector);
        await this.setState(prevState => ({
            query: {
                ...prevState.query,
                sortBy: column.selector, IsSortAscending: sortDirection === 'asc'
            }
        }));
        this.setState({ loading: true });
        const response = await this.populatePatientsData();
        const pagination = JSON.parse(response.headers['x-pagination']);
        this.setState({
            loading: false,
            data: response.data,
            totalRows: pagination.TotalCount
        });
    };
}

export default Patients;