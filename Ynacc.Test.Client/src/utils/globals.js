const datas = {}
export const setVStore = store => {
   // console.log("set Store")
  datas.store = store
}
export const getVStore = () => datas.store
