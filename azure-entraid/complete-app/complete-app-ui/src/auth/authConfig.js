import { LogLevel } from "@azure/msal-browser";

export const msalConfig = {
    auth: {
        clientId: "afba52a9-3314-4cb4-94fe-ff281c5770af",
        authority: "https://login.microsoftonline.com/27b8e21b-adae-4ec5-8fe0-85042b64d2d4",
        redirectUri: "http://localhost:3000",
    },
    cache: {
        cacheLocation: "sessionStorage",
        storeAuthStateInCookie: false,
    },
    system: {
        loggerOptions: {
            loggerCallback(logLevel, message, containsPii) {
                console.info(message);
                if (containsPii) {
                    return;
                }
                switch (logLevel) {
                    case LogLevel.Error:
                        console.error(message);
                        return;
                    case LogLevel.Info:
                        console.info(message);
                        return;
                    case LogLevel.Verbose:
                        console.debug(message);
                        return;
                    case LogLevel.Warning:
                        console.warn(message);
                        return;
                    default:
                        return;
                }
            },
            logLevel: LogLevel.Verbose,
        },
    },
};


export const loginRequest = {
    scopes: ["User.Read", "api://99762a7e-c9e6-4b4a-91f3-24e09112f1e7/Read"]
};

export const loginBackendRequest = {
    scopes: ["api://99762a7e-c9e6-4b4a-91f3-24e09112f1e7/Read"]
};

export const graphConfig = {
    graphMeEndpoint: "https://graph.microsoft.com/v1.0/me",
};

export const backendConfig = {
    itemsEndpoint: "http://localhost:5275/api/items",
}