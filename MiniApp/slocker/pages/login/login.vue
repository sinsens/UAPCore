<template>
	<view class="container">
		<view class="btn-area">
			<button type="primary" @click="login()">点击授权登录</button>
		</view>
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
		onLoad() {
			if (this.$store.state.hasLogin) {
				uni.reLaunch({
					url: '/pages/index/index'
				})
			}
			uni.getProvider({
				service: 'oauth',
				success(res) {
					console.log('登录服务类型')
					console.log(res)
					if (res.provider.length == 0) {
						// 不支持一键授权登录，跳到密码登录
						uni.reLaunch({
							url: './passwd-login'
						})
					}
				}
			})
		},
		methods: {
			login() {
				login()
			}
		}
	}
</script>

<style>
	.container {
		padding: 20rpx;
	}

	.container .btn-area {
		position: fixed;
		bottom: 100rpx;
		left: 20rpx;
		right: 20rpx;
	}
</style>
