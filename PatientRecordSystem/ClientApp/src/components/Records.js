import React, { Component } from 'react';
import DataTable from 'react-data-table-component';
import * as moment from 'moment';
import { getRecords } from "../services/RecordService";
import { Link } from "react-router-dom";
const columns = [
    {
        name: 'Patient Name',
      
        selector: 'patientName',
        sortable: true,
    },
    {
        name: 'Disease Name',
        selector: 'diseaseName',
        sortable: true,
    },

    {
        name: 'Time of Entry',
        selector: 'timeOfEntry',
        format: row => row.timeOfEntry && moment(row.timeOfEntry).format('lll'),
        sortable: true,
    },
    {
        cell: (row) => <Link to={`/RecordForm/${row.id}`}>Edit</Link>,
    },

];

class Records extends Component {
    static displayName = Records.name;

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
        const response = await this.populateData();
        console.info(response);
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
                    to="/RecordForm"
                    className="btn btn-primary col-2"
                    style={{ marginBottom: 20 }}
                >
                    New Record
          </Link>
                <DataTable
                    title="Records"
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
    async populateData() {
        const { query } = this.state;

        return await getRecords(query);
    }
    handlePerRowsChange = async (perPage, page) => {
        this.setState({ loading: true });
        await this.setState(prevState => ({
            query: {
                ...prevState.query,
                PageNumber: page, PageSize: perPage
            }
        }));
        const response = await this.populateData()
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

        const response = await this.populateData();
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
        const response = await this.populateData();
        const pagination = JSON.parse(response.headers['x-pagination']);
        this.setState({
            loading: false,
            data: response.data,
            totalRows: pagination.TotalCount
        });
    };
}

export default Records;