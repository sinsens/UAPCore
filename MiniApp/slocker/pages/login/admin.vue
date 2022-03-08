<template>
	<view class="container">
		<uni-forms ref="form" :modelValue="form" :rules="rules">
			<uni-forms-item label="用户名">
				<uni-easyinput v-model="form.username" maxlength="20" placeholder="请输入用户名"></uni-easyinput>
			</uni-forms-item>
			<uni-forms-item label="密码">
				<uni-easyinput v-model="form.password" type="password" maxlength="20" placeholder="请输入密码">
				</uni-easyinput>
			</uni-forms-item>
			<button type="primary" @click="submit">登录</button>
		</uni-forms>
	</view>
</template>

<script>
	import {
		adminLogin
	} from '../../api/user.js'
	export default {
		data() {
			return {
				form: {
					username: '',
					password: ''
				},
				rules: {
					name: {
						rules: [{
							required: true,
							errorMessage: '用户名必填'
						}]
					},
					password: {
						rules: [{
							required: true,
							errorMessage: '密码必填'
						}]
					}
				}
			}
		},
		methods: {
			submit() {
				const that = this
				this.$refs.form.validate().then(res => {
					adminLogin(that.form)
				})
			}
		}
	}
</script>

<style>
	.container {
		padding: 300rpx 40rpx 0 40rpx;
	}
</style>
