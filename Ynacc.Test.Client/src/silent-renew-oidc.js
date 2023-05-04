
import { vuexOidcProcessSilentSignInCallback } from 'vuex-oidc'

vuexOidcProcessSilentSignInCallback().then(
    console.log("vuexOidcProcessSilentSignInCallbackDo")
).catch((err) => {
    console.log("refresh", err);
})
