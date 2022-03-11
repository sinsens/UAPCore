import config from '@/static/config.js'
import store from '@/store/index.js'
import {
	post,
	put,
	request
} from '@/utils/request.js'

const loginInfo = {
	appId: config.APP_ID,
	lookupUseRecentlyTenant: true
}

export function adminLogin(param = {
	username: '',
	password: '',
}) {
	const loginParam = {
		tenant: store.state.tenant || config.client.tenant,
		username: param.username || '',
		password: param.password || '',
		client_id: config.client.client_id,
		client_secret: config.client.client_secret,
		grant_type: config.client.grant_type,
		scope: config.client.scope,
	}
	request({
		baseURL: config.APP_AUTHSERVER_API,
		url: '/connect/token',
		data: loginParam,
		method: 'POST',
		'content-type': 'application/x-www-form-urlencoded;charset=UTF-8'
	}).then(response => {
		store.commit('setToken', response.access_token)
		uni.showToast({
			title: '登录成功！'
		})
		uni.reLaunch({
			url: '/pages/admin/index'
		})
	}).catch(err => {
		uni.showToast({
			icon: 'error',
			title: '用户名或密码错误'
		})
	})
}

/**
 * 用户登录
 */
export function login() {
	uni.getProvider({
		service: 'oauth',
		success: function(res) {
			console.log(res.provider)
			uni.login({
				provider: res.provider,
				success: function(loginRes) {
					loginInfo['code'] = loginRes.code
					post('/api/wechat-management/mini-programs/login/login', loginInfo).then(
						(res) => {
							console.log('登录调用成功！')
							const rawData = JSON.parse(res.rawData)
							store.commit('setUserId', res.userId)
							store.commit('setAppId', res.appId)
							store.commit('setTenantId', res.tenantId)
							store.commit('setToken', rawData.access_token)
							uni.reLaunch({
								url: '/pages/index/index'
							})
						})
				}
			});
		}
	});
}

/**
 * 更新姓名和手机号
 */
export function updateNameAndPhone(options = {
	id: '',
	name: '',
	phone: '',
	phoneIsConfirm: false
}) {
	console.log('options', options)
	return put(`/api/wechat-management/mini-programs/user-info/updateNameAndPhone/${options.id}`, options)
}
