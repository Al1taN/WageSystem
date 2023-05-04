import ynaccrequest from '@/utils/ynaccrequest'

//已弃用
export function GetTime() {
  return ynaccrequest({
    url: '/api/app/GetTime',
    method: 'get'
  })
}
//已弃用
export function UpdateDdl(year,month) {
    return ynaccrequest({
      url: '/api/app/UpdateDdl',
      method: 'get',
      params: { 
        year:year,
        month:month }
    })
  }

  export function GetDdlTime() {
    return ynaccrequest({
      url: '/api/app/GetDdlTime',
      method: 'get'
    })
  }
  
  export function UpdateDdltime(year,month,datetime,workyear,workmonth,workdate) {
      return ynaccrequest({
        url: '/api/app/UpdateDdltime',
        method: 'get',
        params: { 
          year:year,
          month:month,
          datetime:datetime,
          workyear:workyear,
          workmonth:workmonth,
          workdate:workdate }
      })
    }

  export function ImpDept(data) {
    return ynaccrequest({
      url: '/api/app/ImpDept',
      method: 'post',
      data:JSON.stringify(data),
      headers: {'Content-Type': 'application/json'}
    })
  }

  export function GetWageUser() {
    return ynaccrequest({
      url: '/api/app/GetWageUser',
      method: 'get'
    })
  } 

  export function DeleteWageUser(pid,dept) {
    return ynaccrequest({
      url: '/api/app/DeleteWageUser',
      method: 'get',
      params: { pid:pid,
                dept:dept }
    })
  } 