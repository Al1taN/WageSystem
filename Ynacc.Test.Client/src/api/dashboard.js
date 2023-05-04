import ynaccrequest from '@/utils/ynaccrequest'

export function GetDept() {
  return ynaccrequest({
    url: '/api/app/GetDept',
    method: 'get'
  })
}
