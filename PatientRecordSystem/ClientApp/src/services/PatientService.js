import http, { clean } from "./httpService";

const apiEndpoint = "api/Patients";

function PatientUrl(id) {
    return `${apiEndpoint}/${id}`;
}

export async function getPatients(filter) {
    await http.setJwt();

    return http.get(apiEndpoint + '?' + http.toQueryString(filter));
}
export async function getPatientReport(PatientId) {
    await http.setJwt();
    return http.get(`${apiEndpoint}/GetReport/${PatientId}`);
}
export async function getPatient(PatientId) {
    await http.setJwt();
    return http.get(PatientUrl(PatientId));
}

export async function savePatient(patient) {
    await http.setJwt();

    patient = clean(patient);

    if (patient.id) {
        const body = { ...patient };

        return await http.put(PatientUrl(patient.id), body);
    }

    return http.post(apiEndpoint, patient);
}