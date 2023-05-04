import Vue from 'vue'
import Router from 'vue-router'
import OidcCallback from '@/views/oidc/OidcCallback.vue'
import OidcCallbackError from '@/views/oidc/OidcCallbackError.vue'
import { vuexOidcCreateRouterMiddleware } from 'vuex-oidc'
import { setVRouter, getVStore } from '@/utils/globals'

Vue.use(Router)

/* Layout */
import Layout from '@/layout'

/* Router Modules */
import componentsRouter from './modules/components'
import chartsRouter from './modules/charts'
import tableRouter from './modules/table'
import nestedRouter from './modules/nested'

/**
 * Note: sub-menu only appear when route children.length >= 1
 * Detail see: https://panjiachen.github.io/vue-element-admin-site/guide/essentials/router-and-nav.html
 *
 * hidden: true                   if set true, item will not show in the sidebar(default is false)
 * alwaysShow: true               if set true, will always show the root menu
 *                                if not set alwaysShow, when item has more than one children route,
 *                                it will becomes nested mode, otherwise not show the root menu
 * redirect: noRedirect           if set noRedirect will no redirect in the breadcrumb
 * name:'router-name'             the name is used by <keep-alive> (must set!!!)
 * meta : {
    roles: ['admin','editor']    control the page roles (you can set multiple roles)
    title: 'title'               the name show in sidebar and breadcrumb (recommend set)
    icon: 'svg-name'/'el-icon-x' the icon show in the sidebar
    noCache: true                if set true, the page will no be cached(default is false)
    affix: true                  if set true, the tag will affix in the tags-view
    breadcrumb: false            if set false, the item will hidden in breadcrumb(default is true)
    activeMenu: '/example/list'  if set path, the sidebar will highlight the path you set
  }
 */

/**
 * constantRoutes
 * a base page that does not have permission requirements
 * all roles can be accessed
 */
export const constantRoutes = [
  {
    path: '/redirect',
    component: Layout,
    hidden: true,
    children: [
      {
        path: '/redirect/:path(.*)',
        component: () => import('@/views/redirect/index')
      }
    ]
  },
  {
    path: '/login',
    component: () => import('@/views/login/index'),
    hidden: true
  },
  {
    path: '/auth-redirect',
    component: () => import('@/views/login/auth-redirect'),
    hidden: true,
    meta: {
      isPublic: true
    }
  },
  { path: '/oidc-callback',
    component: OidcCallback,
    hidden: true,
    meta: {
      isPublic: true
    }
  },
  {
    path: '/oidc-callback-error',
    component: OidcCallbackError,
    hidden: true,
    meta: {
      isPublic: true
    }
  },
  {
    path: '/404',
    component: () => import('@/views/error-page/404'),
    hidden: true,
    meta: {
      isPublic: true
    }
  },
  {
    path: '/401',
    component: () => import('@/views/error-page/401'),
    hidden: true,
    meta: {
      isPublic: true
    }
  },
  {
    path: '/',
    component: Layout,
    redirect: '/dashboard',
    children: [
      {
        path: 'dashboard',
        component: () => import('@/views/dashboard/index'),
        name: 'Dashboard',
        meta: { title: '首页', icon: 'dashboard', affix: true, isPublic: true }
      }
    ]
  },
  {
    path: '/profile',
    component: Layout,
    redirect: '/profile/index',
    hidden: true,
    children: [
      {
        path: 'index',
        component: () => import('@/views/profile/index'),
        name: 'Profile',
        meta: { title: 'Profile', icon: 'user', noCache: true }
      }
    ]
  }
]

/**
 * asyncRoutes
 * the routes that need to be dynamically loaded based on user roles
 */
 export const asyncRoutes = [
  {
    path: '/sysManage',
    redirect: '/sysManage/index',
    component: Layout,
    meta: {
      title: '系统管理',
      icon: 'user',
      roles:['Wage_Admin','Wage_Commiter','Wage_Checker','Wage_Auditer']
    },
    children: [
      {
        path: 'sysManage',
        component: () => import('@/views/sysManage/index'),
        name: 'sysManage',
        meta: { title: '系统管理' }
      }
    ]
  },
  {
    path: '/dataImport',
    redirect: '/dataImport/index',
    component: Layout,
    meta: {
      title: '数据导入',
      icon: 'user',
      roles:['Wage_Admin','Wage_Commiter']
    },
    children: [
      {
        path: 'dataImport',
        component: () => import('@/views/dataImport/index'),
        name: 'dataImport',
        meta: { title: '数据导入' }
      }
    ]
  },
  {
    path: '/recheck',
    redirect: '/recheck/index',
    component: Layout,
    meta: {
      title: '数据复核',
      icon: 'user',
      roles:['Wage_Admin','Wage_Checker']
    },
    children: [
      {
        path: 'recheck',
        component: () => import('@/views/recheck/index'),
        name: 'recheck',
        meta: { title: '数据复核' }
      }
    ]
  },
  {
    path: '/audit',
    redirect: '/audit/index',
    component: Layout,
    meta: {
      title: '数据审核',
      icon: 'user',
      roles:['Wage_Admin','Wage_Auditer']
    },
    children: [
      {
        path: 'audit',
        component: () => import('@/views/audit/index'),
        name: 'audit',
        meta: { title: '数据审核' }
      }
    ]
  },
  {
    path: '/dataSQL',
    redirect: '/dataSQL/index',
    component: Layout,
    meta: {
      title: '数据查询',
      icon: 'user',
      roles:['Wage_Admin','Wage_Commiter','Wage_Checker','Wage_Auditer']
    },
    children: [
      {
        path: 'dataSQL',
        component: () => import('@/views/dataSQL/index'),
        name: 'dataSQL',
        meta: { title: '数据查询' }
      }
    ]
  },
  {
    path: '/statistics',
    redirect: '/statistics/index',
    component: Layout,
    meta: {
      title: '数据统计',
      icon: 'user',
      roles:['Wage_Admin','Wage_Commiter','Wage_Checker','Wage_Auditer']
    },
    children: [
      {
        path: 'statistics',
        component: () => import('@/views/statistics/index'),
        name: 'statistics',
        meta: { title: '数据统计' }
      }
    ]
  },

  // 404 page must be placed at the end !!!
  { path: '*', redirect: '/404', hidden: true }
]

const createRouter = () => new Router({
  mode: 'history', // require service support
  scrollBehavior: () => ({ y: 0 }),
  routes: constantRoutes
})

const router = createRouter()

// Detail see: https://github.com/vuejs/vue-router/issues/1234#issuecomment-357941465
export function resetRouter() {
  const newRouter = createRouter()
  router.matcher = newRouter.matcher // reset router
  console.log('DO ResetRouter')
}

router.beforeEach(vuexOidcCreateRouterMiddleware(getVStore(), 'oidcStore'))
export default router
