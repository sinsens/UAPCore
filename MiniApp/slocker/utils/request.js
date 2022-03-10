import config from '../static/config.js'
import store from '@/store/index.js'

let appid = ''
let token = ''
let userid = ''
let tenantid = ''

function service(options = {}) {
	store.state.appid && (appid = store.state.appid)
	store.state.token && (token = store.state.token)
	store.state.userid && (userid = store.state.userid)
	store.state.tenantid && (tenantid = store.state.tenantid)

	options.url = `${options.baseURL || config.APP_BASE_API}${options.url}`
	options.header = {
		'content-type': options['content-type'] || 'application/json',
		'Authorization': `Bearer ${token}`,
		'uap_appid': `${appid}`,
		'__tenant': tenantid,
		'uap_userid': userid
	};

	return new Promise((resolved, rejected) => {
		options.success = (res) => {
			const statusCode = Number(res.statusCode)
			if (statusCode <= 204) { //请求成功
				resolved(res.data);
			} else {
				handleErrorResponse(res)
				rejected(res.data.msg); //错误
			}
		}
		options.fail = (err) => {
			console.log(err)
			uni.showToast({
				icon: 'none',
				duration: 3000,
				title: `网络异常，请稍后再试`
			});
			rejected(err); //错误
		}
		uni.request(options);

	});
}

function handleErrorResponse(res) {
	console.log('==============handleErrorResponse==============')
	console.log(res)
	let error = res.data.error || {}
	switch (res.statusCode) {
		case 401:
			uni.showModal({
				content: `未授权或登录过期，请重新登录！`
			})
			break

		case 400:
			if (typeof(error) == 'string') {
				const err = error
				error = {}
				error.message = err
				break
			}
			error.message = error.message
			error.details = error.details
			break

		case 403:
			error.message = error.message
			error.details = error.code
			break

		case 404:
			error.message = '未找到服务'
			error.details = '未找到服务'
			break

		case 408:
			error.message = error.message
			error.details = error.details
			break

		case 500:
			error.message = '内部服务错误'
			error.details = error.message
			break

		case 501:
			error.message = error.message
			error.details = error.details
			break

		case 502:
			error.message = '网关错误'
			error.details = '网络错误'
			break

		case 503:
			error.message = error.message
			error.details = error.details
			break

		case 504:
			error.message = error.message
			error.details = error.details
			break

		case 505:
			error.message = error.message
			error.details = error.details
			break
	}
	if (error.details) {
		uni.showModal({
			title: error.message,
			content: error.details,
			showCancel: false
		})
	} else if (error.message || error.details) {
		const message = error.message || error.details
		uni.showModal({
			title: '发生错误',
			content: message
		})
	}
}


/**
 * POST
 */
export function post(url, data) {
	return service({
		url: url,
		method: "POST",
		data: data
	})
}

export function get(url, data) {
	return service({
		url: url,
		method: "GET",
		data: data
	})
}

/*
export function delete(url, data) {
	return service({
		url: url,
		method: "DELETE",
		data: data
	})
}
*/

export function put(url, data) {
	console.log('put')
	return service({
		url: url,
		method: "PUT",
		data: data
	})
}

export function request(options) {
	return service(options)
}

export function getPageParams(params) {
	console.log(params)
	const options = {}
	if (!typeof(params) == 'object') {
		params = {}
	}

	options.page = params.page || 1
	options.status = params.hasOwnProperty('status') ? params.status : ''
	options.skipCount = params.skipCount || 0
	options.maxResultCount = params.maxResultCount || 10

	for (const key in params) {
		if (!options.hasOwnProperty(key)) {
			options[key] = params[key]
		}
	}

	let urlpost = ''
	for (const key in options) {
		const rVal = options[key]
		let value = undefined
		if (Array.isArray(rVal)) {
			if (rVal.length == 0) {
				continue
			}
			value = rVal.toString()
		} else {
			value = typeof(rVal) == 'object' ? JSON.parse(rVal) : rVal
		}
		urlpost += `${key}=${value}&`
	}

	return urlpost.substring(0, urlpost.length - 1)
}

export default service
