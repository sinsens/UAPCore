<template>
	<view class="container">
		<!-- 申请表 -->
		<uni-card v-if="!onprocess">
			<view slot="title">
				<view class="title">{{$t('rent.form-title')}}</view>
			</view>
			<view>
				<uni-forms ref="form" :modelValue="form" :rules="rules">
					<uni-forms-item name="name" :label="$t('rent.form-name')">
						<uni-easyinput v-model="form.name" maxlength="20"></uni-easyinput>
					</uni-forms-item>
					<uni-forms-item name="phone" :label="$t('rent.form-phone')">
						<uni-easyinput v-model="form.phone" maxlength="11"></uni-easyinput>
					</uni-forms-item>
					<uni-forms-item name="applyCount" :label="$t('rent.form-applyCount')">
						<uni-easyinput v-model="form.applyCount" :type="'number'" :min="1" :max="999"></uni-easyinput>
					</uni-forms-item>
					<uni-forms-item name="time" :label="$t('rent.form-time')">
						<uni-datetime-picker type="datetime" v-model="form.time"></uni-datetime-picker>
					</uni-forms-item>
					<uni-forms-item name="remark" :label="$t('rent.form-remark')">
						<uni-easyinput type="textarea" maxlength="1000" v-model="form.remark"></uni-easyinput>
					</uni-forms-item>
				</uni-forms>
				<button type="primary" @click="submit">{{$t('rent.submit')}}</button>
				<button type="warn" @click="cancel">{{$t('rent.cancel')}}</button>
			</view>
		</uni-card>
		<!--进度表-->
		<uni-card v-else>
			<uni-list>
				<uni-list-item :title="$t('rent.process.name')" :note="lastApply.name"></uni-list-item>
				<uni-list-item :title="$t('rent.process.rentTime')" :note="lastApply.rentTime | formatDatetime">
				</uni-list-item>
				<uni-list-item :title="$t('rent.process.creationTime')" :note="lastApply.creationTime | formatDatetime">
				</uni-list-item>
				<uni-list-item :title="$t('rent.process.statusDesc')" :note="lastApply.statusDesc"></uni-list-item>
			</uni-list>
		</uni-card>
	</view>
</template>

<script>
	import {
		toDatetime,
		formatDateTime
	} from '@/utils/timehelper.js'
	import store from '@/store/index.js'
	import {
		apply,
		getLast,
		getList
	} from '@/api/apply.js'
	const formDefault = {
		name: store.state.name,
		phone: store.state.phone,
		applyCount: 1,
		rentTime: '',
		time: '',
		remark: ''
	}
	export default {
		data() {
			return {
				page: 1,
				onprocess: false,
				lastApply: {},
				form: JSON.parse(JSON.stringify(formDefault)),
				rules: {
					name: {
						rules: [{
							required: true,
							errorMessage: this.$t('rent.rule-name-required')
						}]
					},
					phone: {
						rules: [{
							required: true,
							errorMessage: this.$t('rent.rule-phone-required')
						}]
					},
					applyCount: {
						rules: [{
							minimum: 1,
							maximum: 99,
							errorMessage: this.$t('rent.rule-applyCount-error')
						}, {
							required: true,
							format: 'number',
							errorMessage: this.$t('rent.rule-applyCount-required')
						}]
					},
					time: {
						rules: [{
							required: true,
							errorMessage: this.$t('rent.rule-time-required')
						}]
					}
				}
			}
		},
		computed: {

		},
		onLoad() {

		},
		onShow() {
			this.updateStatus()
		},
		methods: {
			getList() {

			},
			updateStatus() {
				getLast().then(res => {
					console.log('2333')
					console.log(res)
					this.lastApply = res
					this.onprocess = res && res.status === 0
				})
			},
			submit() {
				const that = this
				this.$refs.form.validate().then(res => {
					console.log('表单数据信息o：', res);
					uni.showModal({
						content: that.$t('rent.form-confirm'),
						success(con) {
							if (con.confirm) {
								that.doSubmit()
							}
						}
					})
				}).catch(err => {
					console.log('表单错误信息e：', err);
				})
			},
			doSubmit() {
				console.log()
				this.form['rentTime'] = toDatetime(this.form['time'])
				apply(this.form).then((response) => {
					console.log(response)
				})
			}
		}
	}
</script>

<style>
	.title {
		text-align: center;
		padding: 15px;
		font-size: 1.3em;
		font-weight: 600;
	}
</style>
