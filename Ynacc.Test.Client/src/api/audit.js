import ynaccrequest from '@/utils/ynaccrequest'

export function Audit(data) {
    return ynaccrequest({
      url: '/api/app/Audit',
      method: 'post',
      data:JSON.stringify(data),
      headers: {'Content-Type': 'application/json'}
    })
  }

  export function Audit02(data) {
    return ynaccrequest({
      url: '/api/app/Audit02',
      method: 'post',
      data:JSON.stringify(data),
      headers: {'Content-Type': 'application/json'}
    })
  }

  export function Audit03(data) {
    return ynaccrequest({
      url: '/api/app/Audit03',
      method: 'post',
      data:JSON.stringify(data),
      headers: {'Content-Type': 'application/json'}
    })
  }

  export function Audit04(data) {
    return ynaccrequest({
      url: '/api/app/Audit04',
      method: 'post',
      data:JSON.stringify(data),
      headers: {'Content-Type': 'application/json'}
    })
  }

  export function Unaudit(data) {
    return ynaccrequest({
      url: '/api/app/Unaudit',
      method: 'post',
      data:JSON.stringify(data),
      headers: {'Content-Type': 'application/json'}
    })
  }

  export function Unaudit02(data) {
    return ynaccrequest({
      url: '/api/app/Unaudit02',
      method: 'post',
      data:JSON.stringify(data),
      headers: {'Content-Type': 'application/json'}
    })
  }

  export function Unaudit03(data) {
    return ynaccrequest({
      url: '/api/app/Unaudit03',
      method: 'post',
      data:JSON.stringify(data),
      headers: {'Content-Type': 'application/json'}
    })
  }

  export function Unaudit04(data) {
    return ynaccrequest({
      url: '/api/app/Unaudit04',
      method: 'post',
      data:JSON.stringify(data),
      headers: {'Content-Type': 'application/json'}
    })
  }