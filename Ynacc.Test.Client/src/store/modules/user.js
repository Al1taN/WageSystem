import { login, logout, getInfo } from '@/api/user-old'
import { getToken, setToken, removeToken } from '@/utils/auth'
import router, { resetRouter } from '@/router'
import data from '@/views/pdf/content'

const state = {
  token: getToken(),
  name: '',
  avatar: '',
  introduction: '',
  roles: [],
  Users:''
}

const mutations = {
  SET_TOKEN: (state, token) => {
    state.token = token
  },
  SET_INTRODUCTION: (state, introduction) => {
    state.introduction = introduction
  },
  SET_NAME: (state, name) => {
    state.name = name
  },
  SET_AVATAR: (state, avatar) => {
    state.avatar = avatar
  },
  SET_ROLES: (state, roles) => {
    state.roles = roles
  },
  SET_USERS: (state, Users) => {
    state.Users = Users
  }
}

const actions = {
  getAchive({ commit },  test){
    //获取用户信息
    //const { pid,dept} = test[0]
    commit('SET_USERS', test)
  },
  // user login
  login({ commit }, userInfo) {
    const { username, password } = userInfo
    return new Promise((resolve, reject) => {
      login({ username: username.trim(), password: password }).then(response => {
        const { data } = response
        commit('SET_TOKEN', data.token)
        setToken(data.token)
        resolve()
      }).catch(error => {
        reject(error)
      })
    })
  },

  // get user info
  getInfo({ commit, state ,rootState}) {
    return new Promise((resolve, reject) => {
      var roles = []
      if(!rootState.oidcStore.user)
      {
        console.log("no user reset")
        removeToken()
        resetRouter()
        resolve()
      }
      if (rootState.oidcStore && rootState.oidcStore.user.role instanceof Array)
      {
        rootState.oidcStore.user.role.forEach(role =>
        {
          roles.push(role)
        })
      }
      else  if (typeof rootState.oidcStore.user.role == "string")
      {
        roles.push(rootState.oidcStore.user.role)
      }
      if (!roles || roles.length <= 0) {
        console.log('getInfo: roles must be a non-null array!')
        reject('getInfo: roles must be a non-null array!')
      }
      if(rootState.oidcStore.user.cnname)
        commit('SET_NAME', rootState.oidcStore.user.cnname)
      else
        commit('SET_NAME', rootState.oidcStore.user.name)
      commit('SET_ROLES', roles)
      
      //commit('SET_NAME', name)
      //commit('SET_AVATAR', avatar)
      //commit('SET_INTRODUCTION', introduction)


    //   getInfo(state.token).then(response => {
    //     const { data } = response

    //     if (!data) {
    //       reject('Verification failed, please Login again.')
    //     }

    //     const { roles, name, avatar, introduction } = data

    //     // roles must be a non-empty array
    //     if (!roles || roles.length <= 0) {
    //       reject('getInfo: roles must be a non-null array!')
    //     }

    //     commit('SET_ROLES', roles)
    //     commit('SET_NAME', name)
    //     commit('SET_AVATAR', avatar)
    //     commit('SET_INTRODUCTION', introduction)
    //     resolve(data)
    //   }).catch(error => {
    //     reject(error)
    //   })
    var date = new Object()
    data.roles = roles
    resolve(data)
     })
  },

  // user logout
  logout({ commit, state, dispatch }) {
   
    return new Promise((resolve, reject) => {
      try
      {
        commit('SET_TOKEN', '')
        commit('SET_ROLES', [])
        removeToken()
        resetRouter()
        dispatch('tagsView/delAllViews', null, { root: true })
        dispatch('oidcStore/signOutOidc', null, { root: true })
        // // logout(state.token).then(() => {
        //   commit('SET_TOKEN', '')
        //   commit('SET_ROLES', [])
        //   removeToken()
        //   resetRouter()
        //   // reset visited views and cached views
        //   // to fixed https://github.com/PanJiaChen/vue-element-admin/issues/2485
        //   dispatch('tagsView/delAllViews', null, { root: true })
        //   resolve()
        // }).catch(error => {
        //   reject(error)
        // })
        resolve()
      }
      catch (error) {
        reject(error)
      }
    })
  },

  // remove token
  resetToken({ commit,dispatch }) {
    return new Promise(resolve => {
      commit('SET_TOKEN', '')
      commit('SET_ROLES', [])
      removeToken()
      //dispatch('oidcStore/signOutOidc', null, { root: true })
      resolve()
    })
  },

  // dynamically modify permissions
  async changeRoles({ commit, dispatch }, role) {
    const token = role + '-token'

    commit('SET_TOKEN', token)
    setToken(token)

    const { roles } = await dispatch('getInfo')

    resetRouter()

    // generate accessible routes map based on roles
    const accessRoutes = await dispatch('permission/generateRoutes', roles, { root: true })
    // dynamically add accessible routes
    router.addRoutes(accessRoutes)

    // reset visited views and cached views
    dispatch('tagsView/delAllViews', null, { root: true })
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}
