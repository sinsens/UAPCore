<template>
	<view class="container">
		<view class=".btn-area">
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
