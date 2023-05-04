import ynaccrequest from '@/utils/ynaccrequest'

export function ImpData01(data) {
    return ynaccrequest({
      url: '/api/app/ImpData01',
      method: 'post',
      data:JSON.stringify(data),
      headers: {'Content-Type': 'application/json'}
    })
  }

  export function ImpData02(data) {
    return ynaccrequest({
      url: '/api/app/ImpData02',
      method: 'post',
      data:JSON.stringify(data),
      headers: {'Content-Type': 'application/json'}
    })
  }

  export function ImpData03(data) {
    return ynaccrequest({
      url: '/api/app/ImpData03',
      method: 'post',
      data:JSON.stringify(data),
      headers: {'Content-Type': 'application/json'}
    })
  }

  export function ImpData04(data) {
    return ynaccrequest({
      url: '/api/app/ImpData04',
      method: 'post',
      data:JSON.stringify(data),
      headers: {'Content-Type': 'application/json'}
    })
  }