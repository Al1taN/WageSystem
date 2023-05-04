import ynaccrequest from '@/utils/ynaccrequest'

export function GetNewAttend(data) {
    return ynaccrequest({
      url: '/api/app/GetNewAttend',
      method: 'post',
      data:JSON.stringify(data),
      headers: {'Content-Type': 'application/json'}
    })
  }

  export function GetOvertime(data) {
    return ynaccrequest({
      url: '/api/app/GetOvertime',
      method: 'post',
      data:JSON.stringify(data),
      headers: {'Content-Type': 'application/json'}
    })
  }

  export function GetWorkNewAttend(data) {
    return ynaccrequest({
      url: '/api/app/GetWorkNewAttend',
      method: 'post',
      data:JSON.stringify(data),
      headers: {'Content-Type': 'application/json'}
    })
  }

  export function GetWorkOvertime(data) {
    return ynaccrequest({
      url: '/api/app/GetWorkOvertime',
      method: 'post',
      data:JSON.stringify(data),
      headers: {'Content-Type': 'application/json'}
    })
  }