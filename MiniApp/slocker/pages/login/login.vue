<template>
	<view>
		<button @click="login()">点击授权登录</button>
	</view>
</template>

<script>
	import {
		login
	} from '../../api/user.js'
	export default {
		data() {
			return {

			}
		},
		mounted() {
			if (getApp().globalData.hasLogin) {
				uni.reLaunch({
					url: '/pages/index/index'
				})
			}
		},
		methods: {
			login() {
				login()
			}
		}
	}
</script>

<style>

</style>
