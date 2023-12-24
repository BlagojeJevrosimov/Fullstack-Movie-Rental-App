/**
 * This file contains authentication parameters. Contents of this file
 * is roughly the same across other MSAL.js libraries. These parameters
 * are used to initialize Angular and MSAL Angular configurations in
 * in app.module.ts file.
 */

import { LogLevel, Configuration, BrowserCacheLocation } from '@azure/msal-browser';

const isIE = window.navigator.userAgent.indexOf("MSIE ") > -1 || window.navigator.userAgent.indexOf("Trident/") > -1;

/**
 * Configuration object to be passed to MSAL instance on creation.
 * For a full list of MSAL.js configuration parameters, visit:
 * https://github.com/AzureAD/microsoft-authentication-library-for-js/blob/dev/lib/msal-browser/docs/configuration.md
 */
export const msalConfig: Configuration = {
    auth: {
        clientId: "4e1ff54b-bf34-4f45-83ce-e50fc32967cd", // Application (client) ID from the app registration
        authority: 'https://login.microsoftonline.com/common', // The Azure cloud instance and the app's sign-in audience (tenant ID, common, organizations, or consumers)
        redirectUri: "http://localhost:4200", // This is your redirect URI
    },
    cache: {
        cacheLocation: "localStorage"
    },
}


export const protectedResources = {
    MovieStoreApi: {
        endpoint: 'https://localhost:7113/',
        scopes: {
            read: ['api://dbf7f51e-d046-435b-88ee-c4f9ee872967/to-do-lists.read'],
            write: ['api://dbf7f51e-d046-435b-88ee-c4f9ee872967/to-do-lists.write'],
        },
    },
};

