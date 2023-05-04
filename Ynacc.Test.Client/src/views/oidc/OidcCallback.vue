<template>
          <div>
          </div>
</template>
<script>
    import { mapActions } from 'vuex'
    import { getVRouter } from '@/utils/globals'
    import { setToken } from '@/utils/auth' // get token from cookie

export default {
  name: 'OidcCallback',
  methods: {
    ...mapActions('oidcStore', [
      'oidcSignInCallback'
    ])
  },
  created () {
    this.oidcSignInCallback()
      .then((redirectPath) => {
        //setToken("vue_admin_template_token");
        //console.log("redeirect to :" + redirectPath);
        //this.$router.push(redirectPath);
        this.$router.push(redirectPath);
      })
      .catch((err) => {
        console.error(err)
        this.$router.push('/signin-oidc-error') // Handle errors any way you want
      })
  }
}
</script>
   