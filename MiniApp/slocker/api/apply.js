import config from '@/static/config.js'
import store from '@/store/index.js'
import {
	post,
	put,
	get
} from '@/utils/request.js'

/**
 * 提交申请
 */
export function apply(param = {
	name: '',
	phone: '',
	applyCount: 1,
	rentTime: '',
	remark: '',
	appId: ''
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
	page: 1,
	status: ''
}) {
	return get(`/api/shared-locker/rent-apply/getlist?page=${param.page}&status=${param.status}`)
}
