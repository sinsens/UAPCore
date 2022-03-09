/**
 * 申请相关API
 */
import {
	post,
	put,
	get,
	getPageParams
} from '@/utils/request.js'



/**
 * 提交申请
 */
export function apply(param = {
	name: '', // 姓名
	phone: '', // 联系电话
	applyCount: 1, // 申请数量
	rentTime: '', // 起租时间
	remark: '' // 备注
}) {
	return put(`/api/shared-locker/rent-apply/apply`, param)
}

/**
 * 获取最新待审核记录
 */
export function getLast() {
	return get(`/api/shared-locker/rent-apply/last`)
}


/**
 * 分页查询
 */
export function getList(param = {

}) {
	const postfix = getPageParams(param)
	return get(
		`/api/shared-locker/rent-apply/getlist?${postfix}`
	)
}

/**
 * 分页查询
 */
export function getMyList(param = {
	page: 1,
	status: '',
	skipCount: 0
}) {
	const postfix = getPageParams(param)
	return get(
		`/api/shared-locker/rent-apply/getmylist?${postfix}`
	)
}

/**
 * 分页查询
 */
export function getMyProcessList(param = {
	page: 1,
	status: '',
	skipCount: 0
}) {
	const postfix = getPageParams(param)
	return get(
		`/api/shared-locker/rent-apply/getprocesslist?${postfix}`
	)
}

/**
 * 取消
 * @param {Object} id
 */
export function cancel(id, param = {
	reason: ''
}) {
	return put(`/api/shared-locker/rent-apply/cancel/${id}`, param)
}

/**
 * 审核
 */
export function audit(id, param = {
	"result": true, // true:通过，false:不通过
	"remark": "string", // 备注
	"lockerIds": [ // 分配储物柜
		"id"
	]
}) {
	return put(`/api/shared-locker/rent-apply/audit/${id}`, param)
}

/**
 * 作废
 */
export function discard(id, param = {
	reason: '' // 作废原因
}) {
	return put(`/api/shared-locker/rent-apply/discard/${id}`, param)
}

/**
 * 获取详情
 * @param {Object} id
 */
export function detail(id) {
	return get(`/api/shared-locker/rent-apply/get/${id}`)
}
