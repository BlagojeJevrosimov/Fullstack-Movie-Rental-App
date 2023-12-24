import { InteractionType } from "@azure/msal-browser";
import { protectedResources } from "./auth-config";
import { MsalInterceptorConfiguration, ProtectedResourceScopes } from "@azure/msal-angular";

export function MSALInterceptorConfigFactory(): MsalInterceptorConfiguration {
    const protectedResourceMap = new Map<string, Array<string | ProtectedResourceScopes> | null>();

    protectedResourceMap.set(protectedResources.MovieStoreApi.endpoint, [
        'api://dbf7f51e-d046-435b-88ee-c4f9ee872967/to-do-lists.read',
        'api://dbf7f51e-d046-435b-88ee-c4f9ee872967/to-do-lists.write'
    ]
    );
    return {
        interactionType: InteractionType.Popup,
        protectedResourceMap,
    };

}