import { asyncRoutes, constantRoutes } from '@/router'

/**
 * Use meta.role to determine if the current user has permission
 * @param roles
 * @param route
 */
function hasPermission(roles, route) {
  if (route.meta && route.meta.roles) {
    return roles.some(role => route.meta.roles.includes(role))
  } else {
    return true
  }
}

/**
 * Filter asynchronous routing tables by recursion
 * @param routes asyncRoutes
 * @param roles
 */
export function filterAsyncRoutes(routes, roles) {
  const res = []

  routes.forEach(route => {
    const tmp = { ...route }
    if (hasPermission(roles, tmp)) {
      if (tmp.children) {
        tmp.children = filterAsyncRoutes(tmp.children, roles)
      }
      res.push(tmp)
    }
  })

  return res
}

const state = {
  routes: [],
  addRoutes: []
}

const mutations = {
  SET_ROUTES: (state, routes) => {
    state.addRoutes = routes
    state.routes = constantRoutes.concat(routes)
  }
}

const actions = {
  generateRoutes({ commit }, roles) {
    return new Promise(resolve => {
      let accessedRoutes
      if (roles) {
        //accessedRoutes = asyncRoutes || []
        var arr = []
        //根据角色权限判断路由
        var role = roles[0]
        switch(role){
          case 'Wage_Admin':{
            asyncRoutes.forEach((item) => {
              if (item.hasOwnProperty('meta')) {
                if (item.meta.hasOwnProperty('roles')) {
                  if (item.meta.roles.includes('Wage_Admin')) {
                    // 将含有admin角色路由加入新数组
                    arr.push(item)
                  }
                }
              }
            })
            break
          }
          case 'Wage_Commiter':{
            asyncRoutes.forEach((item) => {
              if (item.hasOwnProperty('meta')) {
                if (item.meta.hasOwnProperty('roles')) {
                  if (item.meta.roles.includes('Wage_Commiter')) {
                    // 将含有admin角色路由加入新数组
                    arr.push(item)
                  }
                }
              }
            })
            break
          }
          case 'Wage_Checker':{
            asyncRoutes.forEach((item) => {
              if (item.hasOwnProperty('meta')) {
                if (item.meta.hasOwnProperty('roles')) {
                  if (item.meta.roles.includes('Wage_Checker')) {
                    // 将含有admin角色路由加入新数组
                    arr.push(item)
                  }
                }
              }
            })
            break
          }
          case 'Wage_Auditer':{
            asyncRoutes.forEach((item) => {
              if (item.hasOwnProperty('meta')) {
                if (item.meta.hasOwnProperty('roles')) {
                  if (item.meta.roles.includes('Wage_Auditer')) {
                    // 将含有admin角色路由加入新数组
                    arr.push(item)
                  }
                }
              }
            })
            break
          }
        }
        //允许所有路由进入 roles = guest
        //asyncRoutes.forEach((item) => {arr.push(item)})
        //
        accessedRoutes = arr || []
      } else {
        accessedRoutes = filterAsyncRoutes(asyncRoutes, roles)
      }
      commit('SET_ROUTES', accessedRoutes)
      resolve(accessedRoutes)
    })
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}
