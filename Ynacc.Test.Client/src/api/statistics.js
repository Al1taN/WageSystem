import ynaccrequest from '@/utils/ynaccrequest'

export function GetStaticsAttend(data) {
    return ynaccrequest({
      url: '/api/app/GetStaticsAttend',
      method: 'post',
      data:JSON.stringify(data),
      headers: {'Content-Type': 'application/json'}
    })
  }

  export function GetWorkStaticsAttend(data) {
    return ynaccrequest({
      url: '/api/app/GetWorkStaticsAttend',
      method: 'post',
      data:JSON.stringify(data),
      headers: {'Content-Type': 'application/json'}
    })
  }