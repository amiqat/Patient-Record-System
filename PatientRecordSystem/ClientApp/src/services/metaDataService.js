import http from "./httpService";

const apiEndpoint = "api/MetaData";


export async function getMetaData() {
    await http.setJwt();
    return http.get(apiEndpoint);
}

