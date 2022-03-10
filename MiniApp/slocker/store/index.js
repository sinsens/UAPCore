import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex); //vue的插件机制

const key = '__state'

const tempSave = {}

/**
 * 更新缓存
 */
function updateStorage() {
	uni.setStorage({
		key: key,
		data: store.state,
		success: function() {
			console.log('success');
		}
	});
}

//Vuex.Store 构造器选项
const store = new Vuex.Store({
	state: { //存放状态
		userid: uni.getStorageSync(`${key}_userid`),
		tenantid: uni.getStorageSync(`${key}_tenantid`),
		appid: uni.getStorageSync(`${key}_appid`),
		token: uni.getStorageSync(`${key}_token`),
		hasLogin: uni.getStorageSync(`${key}_haslogin`) || false,
		name: uni.getStorageSync(`${key}_name`),
		phone: uni.getStorageSync(`${key}_phone`),
		lang: uni.getStorageSync(`${key}_lang`) || 'zh-Hans',
	},
	mutations: {
		setName(state, name) {
			state.name = name
			uni.setStorageSync(`${key}_name`, name)
		},
		setUserId(state, userid) {
			state.userid = userid
			uni.setStorageSync(`${key}_userid`, userid)
		},
		setTenantId(state, tenantid) {
			state.tenantid = tenantid
			uni.setStorageSync(`${key}_tenantid`, tenantid)
		},
		setAppId(state, appid) {
			state.appid = appid
			uni.setStorageSync(`${key}_appid`, appid)
		},
		setToken(state, token) {
			state.token = token
			state.hasLogin = token && token.length > 0
			uni.setStorageSync(`${key}_token`, token)
			uni.setStorageSync(`${key}_haslogin`, state.hasLogin)
		},
		setProfile(state, param) {
			state.name = param.name
			state.phone = param.phone
			uni.setStorageSync(`${key}_name`, param.name)
			uni.setStorageSync(`${key}_phone`, param.phone)
		},
		setLang(state, lang) {
			state.lang = lang
			uni.setStorageSync(`${key}_lang`, lang)
		},
		logout(state) {
			for(const key in state){
				state[key] = ''
			}
			uni.clearStorageSync()
		}
	}
})

export default store
