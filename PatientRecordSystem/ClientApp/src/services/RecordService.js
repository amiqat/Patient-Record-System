import http, { clean } from "./httpService";

const apiEndpoint = "api/Records";

function RecordUrl(id) {
    return `${apiEndpoint}/${id}`;
}

export async function getRecords(filter) {
    await http.setJwt();
    return http.get(apiEndpoint + '?' + http.toQueryString(filter));
}

export async function getRecord(recordId) {
    await http.setJwt();
    return http.get(RecordUrl(recordId));
}

export async function saveRecord(record) {
    await http.setJwt();

    record = clean(record);

    if (record.id) {
        const body = { ...record };

        return await http.put(RecordUrl(record.id), body);
    }

    return http.post(apiEndpoint, record);
}