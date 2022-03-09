/**
 * 储物柜API
 */
import {
	post,
	put,
	get,
	getPageParams
} from '@/utils/request.js'

/**
 * 分页查询
 * @param {Object} param
 */
function getList(param) {
	const p = getPageParams(param)
	return get(`/api/shared-locker/rent/getlist?${p}`)
}


const lockerApi = {
	getList
}

export default lockerApi
