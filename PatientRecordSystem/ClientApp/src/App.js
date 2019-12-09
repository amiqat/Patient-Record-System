import React, { Component } from 'react';
import { Route, Switch, Redirect } from 'react-router';
import { Layout } from './components/Layout';
import Patients from './components/Patients';
import PatientForm from './components/PatientForm';
import Records from './components/Records';
import RecordForm from './components/RecordForm';
import MetaDataReport from './components/MetaDataReport';
import PatientReport from './components/PatientReport';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants';
import NotFound from "./components/common/notFound";
import { ToastContainer } from "react-toastify";
import './custom.css'
import "react-toastify/dist/ReactToastify.css";
export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <React.Fragment>
                <ToastContainer />
                <Layout>
                    <Switch>
                        <Route exact path='/' >
                            <Redirect from="/" to="/Patients" />
                        </Route>
                        <AuthorizeRoute path='/PatientForm/:id?' component={PatientForm} />
                        <AuthorizeRoute path='/Patients' component={Patients} />
                        <AuthorizeRoute path='/Records' component={Records} />
                        <AuthorizeRoute path='/RecordForm/:id?' component={RecordForm} />
                        <AuthorizeRoute path='/MetaData' component={MetaDataReport} />
                        <AuthorizeRoute path='/PatientReport/:id' component={PatientReport} />
                        <Route path="/not-found" component={NotFound} />

                        <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
                    </Switch>
                </Layout>
            </React.Fragment>
        );
    }
}