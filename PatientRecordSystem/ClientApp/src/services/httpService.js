import axios from "axios";
import logger from "./logService";
import { toast } from "react-toastify";
import authService from '../components/api-authorization/AuthorizeService'
axios.interceptors.response.use(null,  error => {
    const expectedError =
        error.response &&
        error.response.status >= 400 &&
        error.response.status < 500;

    if (!expectedError) {
        logger.log(error);
        toast.error("An unexpected error occurrred.");
    }

    return Promise.reject(error);
});

async function setJwt() {
    const token = await authService.getAccessToken();
    if (token)
        axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
}
function toQueryString(obj) {
    var parts = [];
    for (var property in obj) {
        var value = obj[property];
        if (value !== null && value !== undefined)
            parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
    }

    return parts.join('&');
}
function clean(obj) {
    var propNames = Object.getOwnPropertyNames(obj);
    for (var i = 0; i < propNames.length; i++) {
        var propName = propNames[i];
        if (obj[propName] === null || obj[propName] === undefined || obj[propName]==='') {
            delete obj[propName];
        }
    }
    return obj;
}
export default {
    get: axios.get,
    post: axios.post,
    put: axios.put,
    delete: axios.delete,
    setJwt, toQueryString 
};
export { clean };