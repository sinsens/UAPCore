<template>
	<view>
		<uni-card>
			<template slot="title">
				<view class="header">
					<view class="title">租用信息</view>
					<view class="status">
						<uni-tag :type="fetchTagType(rentInfo.status)" :text="rentInfo.statusDesc"></uni-tag>
					</view>
				</view>
			</template>
			<uni-list>
				<uni-list-item title="租用人" :rightText="rentInfo.name"></uni-list-item>
				<uni-list-item title="联系电话" :rightText="rentInfo.phone"></uni-list-item>
				<uni-list-item title="租用数量" :rightText="rentInfo.lockers.length"></uni-list-item>
				<uni-list-item title="租用时间" :rightText="rentInfo.rentTime | formatDatetime">
				</uni-list-item>
				<uni-list-item title="储物柜">
					<template slot="footer">
						<uni-tag :text="locker.number" :key="locker.id" circle v-for="locker in rentInfo.lockers">
						</uni-tag>
					</template>
				</uni-list-item>
			</uni-list>
			<template slot="actions">
				<button v-if="rentInfo.status == rentStatus.InService" type="primary"
					@click="preReturn(rentInfo.id)">归还</button>
			</template>
		</uni-card>
		<uni-card>
			<template slot="title">
				<view class="header">
					<view class="title">归还信息</view>
				</view>
			</template>
			<uni-list>
				<uni-list-item title="归还时间" :rightText="rentInfo.returnTime | formatDatetime"></uni-list-item>
				<uni-list-item title="备注" :rightText="rentInfo.returnRemark"></uni-list-item>
			</uni-list>
		</uni-card>
		<uni-popup ref="returnDialog" background-color="#fff" :isMaskClick="false">
			<uni-card>
				<uni-list>
					<uni-list-item title="租用人" :rightText="rentInfo.name"></uni-list-item>
					<uni-list-item title="联系电话" :rightText="rentInfo.phone"></uni-list-item>
					<uni-list-item title="租用数量" :rightText="rentInfo.count"></uni-list-item>
					<uni-list-item title="租用时间" :rightText="rentInfo.rentTime | formatDatetime">
					</uni-list-item>
					<uni-list-item title="储物柜">
						<template slot="footer">
							<uni-tag :text="locker.number" :key="locker.id" circle v-for="locker in rentInfo.lockers">
							</uni-tag>
						</template>
					</uni-list-item>
				</uni-list>
				<uni-forms ref="returnForm" :modelValue="form">
					<uni-forms-item label="归还时间">
						<uni-datetime-picker v-model="form.returnTime"></uni-datetime-picker>
					</uni-forms-item>
					<uni-forms-item label="备注">
						<uni-easyinput maxlength="500" type="textarea" v-model="form.returnRemark"></uni-easyinput>
					</uni-forms-item>
				</uni-forms>
				<template slot="actions">
					<button type="primary" @click="submit">确认归还</button>
					<button @click="closeDialog">取消</button>
				</template>
			</uni-card>
		</uni-popup>
	</view>
</template>

<script>
	import rentApi from '../../../api/rent.js'
	import {
		rentStatus
	} from '@/static/enums.js'
	import {
		toDatetime,
		formatDateTime
	} from '@/utils/timehelper.js'

	export default {
		data() {
			return {
				id: '',
				rentInfo: {},
				rentStatus: rentStatus,
				form: {
					returnTime: '',
					returnRemark: ''
				},
				rules: {
					returnTime: {
						rules: [{
							required: true,
							errorMessage: '请选择归还时间'
						}]
					}
				}
			}
		},
		onLoad(options) {
			this.id = options.id || ''
			this.updateStatus()
		},
		onPullDownRefresh() {
			this.updateStatus()
		},
		methods: {
			fetchTagType(status) {
				switch (status) {
					case rentStatus.InService:
						return 'warning'
					case rentStatus.EndService:
						return 'success'
					case rentStatus.Discard:
						return 'error'
					default:
					case rentStatus.Canceled:
					case rentStatus.Discard:
						return 'default'
				}
			},
			updateStatus() {
				rentApi.detail(this.id).then(response => {
					this.rentInfo = response
				})
			},
			closeDialog() {
				this.$refs.returnDialog.close()
			},
			preReturn(id) {
				this.$refs.returnDialog.open()
			},
			submit() {
				const that = this
				this.$refs.returnForm.validate().then(res => {
					uni.showModal({
						title: '继续归还?',
						content: '请确认归还信息是否填写正确',
						success(c) {
							if (c.confirm) {
								that.doSubmit()
							}
						}
					})
				})
			},
			doSubmit() {
				const {
					id
				} = this.rentInfo
				const {
					returnTime,
					returnRemark
				} = this.form
				const postData = {
					returnTime: toDatetime(returnTime),
					returnRemark
				}

				rentApi.finish(id, postData).then(res => {
					uni.showToast({
						title: '归还成功'
					})
					this.updateStatus()
					this.$refs.returnDialog.close()
				}).catch(err => {
					uni.showToast({
						icon: 'error',
						title: err || '网络异常'
					})
				})
			}
		}
	}
</script>

<style>

</style>
