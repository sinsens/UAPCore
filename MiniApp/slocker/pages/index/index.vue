<template>
	<view class="container">
		<!-- 申请表 -->
		<uni-card v-if="!onprocess">
			<template slot="title">
				<view class="title">{{$t('rent.form-title')}}</view>
			</template>
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
				<button type="primary" @click="submit" :loading="onsubmit">{{$t('rent.submit')}}</button>
			</view>
		</uni-card>
		<!--进度表-->
		<uni-card v-else>
			<template slot="title">
				<view class="title">{{$t('rent.form-title')}}</view>
			</template>
			<uni-list>
				<uni-list-item :title="$t('rent.process.name')" :rightText="lastApply.name"></uni-list-item>
				<uni-list-item :title="$t('rent.process.rentTime')" :rightText="lastApply.rentTime | formatDatetime">
				</uni-list-item>
				<uni-list-item :title="$t('rent.process.creationTime')"
					:rightText="lastApply.creationTime | formatDatetime">
				</uni-list-item>
				<uni-list-item :title="$t('rent.process.statusDesc')" :rightText="lastApply.statusDesc"></uni-list-item>
			</uni-list>
			<template slot="actions">
				<button type="primary" @click="showQrCode">{{$t('rent.showQrCode')}}</button>
				<button type="warn" @click="cancel">{{$t('rent.cancel')}}</button>
			</template>
		</uni-card>
		<uni-popup ref="qrCode" background-color="#fff" mask-background-color="rgba(250, 250, 250, 0.9)">
			<uqrcode :text="qrCodeText"></uqrcode>
		</uni-popup>
	</view>
</template>

<script>
	import {
		rentStatus,
		rentApplyStatus
	} from '@/static/enums.js'
	import {
		qrCodeType
	} from '../../static/enums.js'
	import {
		getRandNumText
	} from '../../utils/randomhelper.js'
	import {
		toDatetime,
		formatDateTime
	} from '@/utils/timehelper.js'
	import store from '@/store/index.js'
	import {
		apply,
		cancel,
		getLast,
		getList
	} from '@/api/apply.js'
	import w_md5 from "../../js_sdk/zww-md5/w_md5.js"

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
				qrCodeText: '',
				page: 1,
				onsubmit: false,
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
		onShow() {
			if (this.$store.state.token == '') {
				uni.reLaunch({
					url: '/pages/login/login'
				})
			} else {
				this.updateStatus()
			}
		},
		methods: {
			showQrCode() {
				const vcode = getRandNumText()
				const signStr = `${this.$store.state.tenantid}${this.lastApply.id}${vcode}`
				const sign = w_md5.hex_md5_16(signStr)
				const rawData = JSON.stringify({
					type: this.lastApply.status == rentApplyStatus.PendingAudit ? qrCodeType.Apply : qrCodeType
						.Rent,
					id: this.lastApply.id,
					code: vcode,
					sign: sign
				})
				this.qrCodeText = rawData
				this.$refs.qrCode.open()
			},
			updateStatus() {
				getLast().then(res => {
					this.lastApply = res
					this.onprocess = res && res.status === 0
				}).catch(err => {
					this.onprocess = false
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
				const that = this
				this.onsubmit = true
				this.form['rentTime'] = toDatetime(this.form['time'])
				apply(this.form).then((response) => {
					if (response && response.status == 0) {
						uni.showToast({
							title: that.$t('rent.form.submit-ok')
						})
						that.onsubmit = false
						that.updateStatus()
					}
				}).catch(() => {
					this.onsubmit = false
				})
			},
			cancel() {
				const that = this
				const {
					id
				} = this.lastApply

				if (!id) return

				uni.showModal({
					content: this.$t('rent.process.confirm-cancel'),
					success(res) {
						if (res.confirm) {
							cancel(id).then(() => {
								uni.showToast({
									title: that.$t('rent.process.cancel-done')
								})
								that.updateStatus()
							})
						}
					}
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
