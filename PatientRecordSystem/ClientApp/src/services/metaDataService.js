import http, { clean } from "./httpService";

const apiEndpoint = "api/MetaData";


export async function getMetaData() {
    await http.setJwt();
    return http.get(apiEndpoint);
}

