import ynaccrequest from '@/utils/ynaccrequest'

export function GetData(year,month,dept) {
    return ynaccrequest({
      url: '/api/app/GetData',
      method: 'get',
      params: { 
        year:year,
        month:month,
        deptID:dept }
    })
  }
  
  export function GetData02(year,month,dept) {
    return ynaccrequest({
      url: '/api/app/GetData02',
      method: 'get',
      params: { 
        year:year,
        month:month,
        deptID:dept }
    })
  }

  export function GetData03(year,month,dept) {
    return ynaccrequest({
      url: '/api/app/GetData03',
      method: 'get',
      params: { 
        year:year,
        month:month,
        deptID:dept }
    })
  }

  export function GetData04(year,month,dept) {
    return ynaccrequest({
      url: '/api/app/GetData04',
      method: 'get',
      params: { 
        year:year,
        month:month,
        deptID:dept }
    })
  }

  export function Recheck(data) {
    return ynaccrequest({
      url: '/api/app/Recheck',
      method: 'post',
      data:JSON.stringify(data),
      headers: {'Content-Type': 'application/json'}
    })
  }

  export function Recheck02(data) {
    return ynaccrequest({
      url: '/api/app/Recheck02',
      method: 'post',
      data:JSON.stringify(data),
      headers: {'Content-Type': 'application/json'}
    })
  }

  export function Recheck03(data) {
    return ynaccrequest({
      url: '/api/app/Recheck03',
      method: 'post',
      data:JSON.stringify(data),
      headers: {'Content-Type': 'application/json'}
    })
  }

  export function Recheck04(data) {
    return ynaccrequest({
      url: '/api/app/Recheck04',
      method: 'post',
      data:JSON.stringify(data),
      headers: {'Content-Type': 'application/json'}
    })
  }

  export function Unrecheck(data) {
    return ynaccrequest({
      url: '/api/app/Unrecheck',
      method: 'post',
      data:JSON.stringify(data),
      headers: {'Content-Type': 'application/json'}
    })
  }

  export function Unrecheck02(data) {
    return ynaccrequest({
      url: '/api/app/Unrecheck02',
      method: 'post',
      data:JSON.stringify(data),
      headers: {'Content-Type': 'application/json'}
    })
  }

  export function Unrecheck03(data) {
    return ynaccrequest({
      url: '/api/app/Unrecheck03',
      method: 'post',
      data:JSON.stringify(data),
      headers: {'Content-Type': 'application/json'}
    })
  }

  export function Unrecheck04(data) {
    return ynaccrequest({
      url: '/api/app/Unrecheck04',
      method: 'post',
      data:JSON.stringify(data),
      headers: {'Content-Type': 'application/json'}
    })
  }