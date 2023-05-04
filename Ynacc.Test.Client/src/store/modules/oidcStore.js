import { vuexOidcCreateStoreModule } from 'vuex-oidc'
import { setToken } from '@/utils/auth'
import router, { resetRouter } from '@/router'
import store from '@/store'
// import { getVRouter } from '@/utils/globals'

const oidcStore = vuexOidcCreateStoreModule(
  JSON.parse(process.env.VUE_APP_OIDC_CONFIG),
  // NOTE: If you do not want to use localStorage for tokens, in stead of just passing oidcSettings, you can
  // spread your oidcSettings and define a userStore of your choice
  // {
  //   ...oidcSettings,
  //   userStore: new WebStorageStateStore({ store: window.sessionStorage })
  // },
  // Optional OIDC store settings
  {
    namespaced: true,
    dispatchEventsOnWindow: true
  },
  // Optional OIDC event listeners
  {
    userLoaded: (user) => {
      console.log('OIDC user is loaded', user)
      setToken(user.access_token)
      const { commit } = store
      commit('user/SET_TOKEN', user.access_token)
    },
    userUnloaded: () => {
      console.log('OIDC user is unloaded')
    },
    accessTokenExpiring: () => {
      console.log('Access token will expire')
    },
    accessTokenExpired: () => {
      // console.log('Access token did expire')
      const { dispatch } = store
      const { push } = router
      setToken('')
      dispatch('user/logout')
      push(`/login?redirect=${this.$route.fullPath}`)
      // setToken('');
      // const { dispatch} = store
      // dispatch('oidcStore/removeOidcUser')
    },
    silentRenewError: () => {
      console.log('OIDC user is unloaded')
    },
    userSignedOut: () => {
      console.log('OIDC user is signed out')
    },
    oidcError: (payload) => {
      console.log('OIDC error', payload)
      if (payload.error === 'Network Error') {
        router.push('/404')
      }
    },
    automaticSilentRenewError: (payload) => {
      console.log('OIDC automaticSilentRenewError', payload)
    }
  }
)

export default oidcStore
