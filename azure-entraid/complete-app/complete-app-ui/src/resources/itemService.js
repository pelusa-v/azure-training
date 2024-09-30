import { backendConfig } from "../auth/authConfig";

// Get data user from protected backend
export async function callItems(accessToken) {
    const headers = new Headers();
    const bearer = `Bearer ${accessToken}`;

    headers.append("Authorization", bearer);

    console.log(accessToken);
    console.log("accessToken");

    const options = {
        method: "GET",
        headers: headers
    };

    return fetch(backendConfig.itemsEndpoint, options)
        .then(response => response.json())
        .catch(error => {
            console.log(error);
            console.log("HEREEEEEEE");
        });
}