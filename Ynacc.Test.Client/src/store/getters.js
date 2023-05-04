const getters = {
  sidebar: state => state.app.sidebar,
  size: state => state.app.size,
  device: state => state.app.device,
  visitedViews: state => state.tagsView.visitedViews,
  cachedViews: state => state.tagsView.cachedViews,
  token: state => state.user.token,
  //token: state => state.oidcStore.access_token,
  //avatar: state => state.user.avatar,
  avatar: state => "./userNullPhoto.png",
  name: state => state.user.name,
  //name: state =>  state.oidcStore.user.name,
  introduction: state => state.user.introduction,
  roles: state => state.user.roles,
  //roles: state =>  state.oidcStore.user.role,
  permission_routes: state => state.permission.routes,
  errorLogs: state => state.errorLog.logs,
  oidcUser: state => state.oidcStore.user,
  Users: state =>state.user.Users
}
export default getters
