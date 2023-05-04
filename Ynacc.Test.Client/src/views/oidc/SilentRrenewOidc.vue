<template>
          <div>
          </div>
</template>
<script>
    import { vuexOidcProcessSilentSignInCallback } from 'vuex-oidc'
    
export default {
  name: 'SilentRrenewOidc',
  created () {
    vuexOidcProcessSilentSignInCallback()
  }
}
</script>
   