import { LogLevel } from "@azure/msal-browser";

export const msalConfig = {
    auth: {
        clientId: "some-client-id",
        authority: "https://login.microsoftonline.com/some-tenant-id",
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
    scopes: ["User.Read"]
};

export const graphConfig = {
    graphMeEndpoint: "https://graph.microsoft.com/v1.0/me",
};