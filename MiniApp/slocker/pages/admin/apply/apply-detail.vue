<template>
	<view>
		<uni-card>
			<template slot="title">
				<view class="header">
					<view class="title">申请信息</view>
					<view class="status">
						<uni-tag :type="fetchTagType(applyInfo.status)" :text="applyInfo.statusDesc"></uni-tag>
					</view>
				</view>
			</template>
			<uni-list>
				<uni-list-item title="申请人" :rightText="applyInfo.name"></uni-list-item>
				<uni-list-item title="联系电话" :rightText="applyInfo.phone"></uni-list-item>
				<uni-list-item title="申请数量" :rightText="applyInfo.count"></uni-list-item>
				<uni-list-item title="申请租用时间" :rightText="applyInfo.rentTime | formatDatetime"></uni-list-item>
				<uni-list-item title="申请时间" :rightText="applyInfo.creationTime | formatDatetime">
				</uni-list-item>
			</uni-list>
			<template slot="actions">
				<view v-if="applyInfo.status === 0">
					<button type="primary" @click="acceptApply(applyInfo)">通过</button>
					<button type="warn" @click="rejectApply(applyInfo)">拒绝</button>
				</view>
			</template>
		</uni-card>
		<uni-card v-if="applyInfo.status != rentApplyStatus.Canceled">
			<template slot="title">
				<view class="header">
					<view class="title">审核信息</view>
				</view>
			</template>
			<uni-list>
				<uni-list-item title="审核人" :rightText="applyInfo.auditor"></uni-list-item>
				<uni-list-item title="审核时间" :rightText="applyInfo.auditTime | formatDatetime"></uni-list-item>
				<uni-list-item title="备注" :rightText="applyInfo.auditRemark"></uni-list-item>
			</uni-list>
		</uni-card>
		<uni-card v-if="applyInfo.status == rentApplyStatus.Accepted">
			<template slot="title">
				<view class="header">
					<view class="title">租用信息</view>
					<view class="status">
						<uni-tag :type="fetchRentTagType(applyInfo.lockerRent.status)"
							:text="applyInfo.lockerRent.statusDesc">
						</uni-tag>
					</view>
				</view>
			</template>
			<uni-list>
				<uni-list-item title="储物柜">
					<template slot="footer">
						<uni-tag :text="locker.number" :key="locker.id" circle
							v-for="locker in applyInfo.lockerRent.lockers">
						</uni-tag>
					</template>
				</uni-list-item>
				<uni-list-item title="归还时间" :rightText="applyInfo.lockerRent.returnTime | formatDatetime">
				</uni-list-item>
				<uni-list-item title="备注" :rightText="applyInfo.lockerRent.returnRemark"></uni-list-item>
			</uni-list>
		</uni-card>
		<uni-popup ref="acceptDialog" background-color="#fff" :isMaskClick="false">
			<uni-card title="通过申请">
				<uni-list>
					<uni-list-item title="申请数量" :rightText="applyInfo.count"></uni-list-item>
				</uni-list>
				<uni-forms ref="acceptForm" :modelValue="form" :rules="acceptRules">
					<uni-forms-item label="选择储物柜" name="lockerIds">
						<uni-data-checkbox multiple emptyText="无空闲储物柜" :localdata="lockers" v-model="form.lockerIds">
						</uni-data-checkbox>
					</uni-forms-item>
					<uni-forms-item label="备注">
						<uni-easyinput maxlength="500" type="textarea" v-model="form.remark"></uni-easyinput>
					</uni-forms-item>
				</uni-forms>
				<template slot="actions">
					<button type="primary" @click="accept">通过</button>
					<button @click="closeAcceptDialog">取消</button>
				</template>
			</uni-card>
		</uni-popup>
		<uni-popup ref="rejectDialog" background-color="#fff" :isMaskClick="false">
			<uni-card title="拒绝申请">
				<uni-list>
					<uni-list-item title="申请数量" :rightText="applyInfo.count"></uni-list-item>
				</uni-list>
				<uni-forms ref="rejectForm" :modelValue="form" :rules="rejectRules">
					<uni-forms-item label="原因">
						<uni-easyinput maxlength="500" type="textarea" v-model="form.remark"></uni-easyinput>
					</uni-forms-item>
				</uni-forms>
				<template slot="actions">
					<button type="warn" @click="reject">拒绝</button>
					<button @click="closeRejectDialog">取消</button>
				</template>
			</uni-card>
		</uni-popup>
	</view>
</template>

<script>
	import {
		getList,
		detail,
		audit
	} from '../../../api/apply.js'
	import lockerApi from '../../../api/locker.js'
	import {
		rentStatus,
		rentApplyStatus
	} from '@/static/enums.js'
	export default {
		data() {
			return {
				id: '',
				applyInfo: {},
				rentApplyStatus: rentApplyStatus,
				rentStatus: rentStatus,
				lockers: [],
				form: {
					remark: '',
					result: false,
					lockerIds: []
				},
				acceptRules: {
					lockerIds: {
						rules: [{
							required: true,
							errorMessage: '请选择储物柜'
						}]
					}
				},
				rejectRules: {
					remark: {
						rules: [{
							required: true,
							errorMessage: '请输入拒绝原因'
						}]
					}
				}
			}
		},
		onLoad(options) {
			this.id = options.id
			this.updateStatus()
		},
		onPullDownRefresh() {
			this.updateStatus()
		},
		methods: {
			closeRejectDialog() {
				this.$refs.rejectDialog.close()
			},
			closeAcceptDialog() {
				this.$refs.acceptDialog.close()
			},
			accept() {
				const that = this
				const postData = this.form
				const applyInfo = this.applyInfo
				let note = ''
				const gotCount = postData.lockerIds.length
				if (applyInfo.count > gotCount) {
					note = `储物柜分配数量(${applyInfo.count})少于申请数量(${gotCount})`
				} else if (applyInfo.count < gotCount) {
					note = `储物柜分配数量(${applyInfo.count})超出申请数量(${gotCount})`
				}
				uni.showModal({
					title: '是否通过该申请？',
					content: note,
					success(c) {
						if (c.cancel) return
						postData.result = true
						that.$refs.acceptForm.validate().then(data => {
							that.doAudit(postData)
						})
					}
				})
			},
			reject() {
				const that = this
				const postData = this.form
				uni.showModal({
					content: '是否拒绝该申请？',
					confirmColor: '#CE3C39',
					success(c) {
						if (c.cancel) return
						postData.result = false
						that.$refs.rejectForm.validate().then(data => {
							that.doAudit(postData)
						})
					}
				})
			},
			doAudit(auditData = {}) {
				const {
					id
				} = this.applyInfo
				audit(id, auditData).then(() => {
					this.$refs.acceptDialog.close()
					this.$refs.rejectDialog.close()
					this.updateStatus()
				})
			},
			loadLockers(maxCount = 10) {
				lockerApi.getList({
					status: 0,
					maxResultCount: maxCount || 50
				}).then(res => {
					this.lockers = res.items.map(item => {
						return {
							"value": item.id,
							"text": `${item.name}(编号${item.number})`
						}
					});
				})
			},
			acceptApply(info) {
				this.applyInfo = info
				const count = (info.count + 1) / 5
				const resultCount = (count < 1 ? 1 : count) * 5
				this.loadLockers(resultCount)
				this.form.lockerIds = []
				this.$refs.acceptDialog.open()
			},
			rejectApply(info) {
				this.applyInfo = info
				this.$refs.rejectDialog.open()
			},
			fetchTagType(status) {
				switch (status) {
					case rentApplyStatus.PendingAudit:
						return 'warning'
					case rentApplyStatus.Accepted:
						return 'success'
					case rentApplyStatus.Rejected:
						return 'error'
					default:
					case rentApplyStatus.Canceled:
					case rentApplyStatus.Discard:
						return 'default'
				}
			},
			fetchRentTagType(status) {
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
				detail(this.id).then(applyInfo => {
					this.applyInfo = applyInfo
				})
			}
		}
	}
</script>

<style>
</style>
