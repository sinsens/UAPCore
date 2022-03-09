/**
 * 租赁API
 */
import {
	post,
	put,
	get,
	getPageParams
} from '@/utils/request.js'

/**
 * 租用
 */
function create(param = {
	"name": "string",
	"phone": "string",
	"rentTime": "2022-03-09T07:10:14.562Z",
	"remark": "string",
	"appId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
	"lockerIds": [
		"3fa85f64-5717-4562-b3fc-2c963f66afa6"
	]
}) {
	return post('/api/shared-locker/rent/rent', param)
}

/**
 * 归还
 */
function finish(id, param = {
	"returnTime": "2022-03-09T07:11:10.499Z", // 归还时间
	"returnRemark": "string" // 归还备注
}) {
	return put(`/api/shared-locker/rent/rent/${id}`, param)
}

/**
 * 获取详情
 * @param {Object} id
 */
function detail(id) {
	return get(`/api/shared-locker/rent/get/${id}`)
}

/**
 * 分页查询
 */
function getList(param = {

}) {
	const postfix = getPageParams(param)
	return get(
		`/api/shared-locker/rent/getlist?${postfix}`
	)
}

/**
 * 作废
 * @param {Object} id
 */
function discard(id) {
	return put(`/api/shared-locker/rent/discard/${postfix}`)
}

const rentApi = {
	create,
	finish,
	detail,
	getList,
	discard
}

export default rentApi
