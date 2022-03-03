import config from '@/static/config.js'
import store from '@/store/index.js'
import {
	post,
	put
} from '@/utils/request.js'

const loginInfo = {
	appId: config.APP_ID,
	lookupUseRecentlyTenant: true
}

export function login() {
	uni.getProvider({
		service: 'oauth',
		success: function(res) {
			console.log(res.provider)
			uni.login({
				provider: res.provider,
				success: function(loginRes) {
					console.log(JSON.stringify(loginRes));
					loginInfo['code'] = loginRes.code
					post('/api/wechat-management/mini-programs/login/login', loginInfo).then(
						(res) => {
							console.log('登录调用成功！')
							console.log(res)
							const rawData = JSON.parse(res.rawData)
							console.log(rawData)
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
	return put(`/api/wechat-management/mini-programs/user-info/updateNameAndPhone/${options.id}`, options).then(res => {
		console.log('更新成功')
	})
}
