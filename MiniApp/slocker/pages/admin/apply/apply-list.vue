<template>
	<view>
		<uni-search-bar v-model="keyword" @cancel="cancelSearch" @clear="cancelSearch" @confirm="search" @input="search"
			@blur="search" placeholder="输入姓名或手机号"></uni-search-bar>
		<view class="container">
			<uni-card v-for="item in list" :key="item.id">
				<template slot="title">
					<view class="header">
						<view class="title">申请时间:{{item.creationTime | formatDatetime}}</view>
						<view class="status">
							<uni-tag :type="fetchTagType(item.status)" :text="item.statusDesc"></uni-tag>
						</view>
					</view>
				</template>
				<uni-list>
					<uni-list-item title="申请人" :rightText="item.name"></uni-list-item>
					<uni-list-item title="联系电话" :rightText="item.phone"></uni-list-item>
					<uni-list-item title="申请数量" :rightText="item.count"></uni-list-item>
					<uni-list-item title="申请租用时间" :rightText="item.rentTime | formatDatetime">
					</uni-list-item>
				</uni-list>
				<template slot="actions">
					<view v-if="item.status === 0">
						<button type="primary" @click="acceptApply(item)">通过</button>
						<button type="warn" @click="rejectApply(item)">拒绝</button>
					</view>
					<view v-else>
						<button @click="detail(item.id)">详情</button>
					</view>
				</template>
			</uni-card>
		</view>
		<uni-load-more :status="moreStatus"></uni-load-more>
		<uni-popup ref="acceptDialog" background-color="#fff" :isMaskClick="false">
			<uni-card title="通过申请">
				<uni-list>
					<uni-list-item title="申请人" :rightText="applyInfo.name"></uni-list-item>
					<uni-list-item title="联系电话" :rightText="applyInfo.phone"></uni-list-item>
					<uni-list-item title="申请数量" :rightText="applyInfo.count"></uni-list-item>
					<uni-list-item title="申请租用时间" :rightText="applyInfo.rentTime | formatDatetime">
					</uni-list-item>
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
			<uni-card>
				<uni-list>
					<uni-list-item title="申请人" :rightText="applyInfo.name"></uni-list-item>
					<uni-list-item title="联系电话" :rightText="applyInfo.phone"></uni-list-item>
					<uni-list-item title="申请数量" :rightText="applyInfo.count"></uni-list-item>
					<uni-list-item title="申请租用时间" :rightText="applyInfo.rentTime | formatDatetime">
					</uni-list-item>
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
		rentApplyStatus
	} from '@/static/enums.js'
	export default {
		data() {
			return {
				list: [],
				total: 0,
				keyword: '',
				page: 1,
				applyInfo: {},
				lockers: [],
				status: '',
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
		computed: {
			count() {
				return this.list.length
			},
			canLoadMore() {
				return this.count < this.total
			},
			skipCount() {
				return this.page * this.count
			}
		},
		onLoad(options) {
			if (options && options.hasOwnProperty('status')) {
				this.status = Number(options.status)
				let title = '申请列表'
				switch (this.status) {
					case rentApplyStatus.PendingAudit:
						title = '待审核列表'
						break
					case rentApplyStatus.Accepted:
						title = '已通过列表'
						break
					case rentApplyStatus.Rejected:
						title = '已拒绝列表'
						break
					case rentApplyStatus.Canceled:
						title = '已取消列表'
						break
					case rentApplyStatus.Discard:
						title = '已作废列表'
						break
				}
				uni.setNavigationBarTitle({
					title: title
				})
			}
		},
		onShow() {
			this.getList()
		},
		onReachBottom() {
			this.more()
		},
		onPullDownRefresh() {
			this.search()
		},
		methods: {
			closeRejectDialog() {
				this.$refs.rejectDialog.close()
			},
			closeAcceptDialog() {
				this.$refs.acceptDialog.close()
			},
			detail(id) {
				uni.navigateTo({
					url: `apply-detail?id=${id}`
				})
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
					this.getList()
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
			getList() {
				this.moreStatus = 'loading'
				getList({
					page: this.page,
					skipCount: this.skipCount,
					filter: this.keyword,
					status: this.status
				}).then(res => {
					res.items.map(item => {
						this.list.push(item)
					})
					uni.stopPullDownRefresh()
					this.total = res.totalCount
					this.moreStatus = this.canLoadMore ? 'more' : 'noMore'
				}).catch(() => {
					uni.stopPullDownRefresh()
					this.moreStatus = 'noMore'
				})
			},
			search() {
				this.list = []
				this.page = 1
				this.getList()
			},
			cancelSearch() {
				this.keyword = ''
				this.search()
			},
			more() {
				if (this.canLoadMore) {
					this.page += 1
					this.getList()
				}
			}
		}
	}
</script>

<style>
</style>
