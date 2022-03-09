<template>
	<view>
		<uni-search-bar v-model="keyword" @cancel="cancelSearch" @clear="cancelSearch" @confirm="search" @input="search"
			@blur="search" placeholder="输入姓名或手机号"></uni-search-bar>
		<view class="container">
			<no-data v-if="list.length == 0"></no-data>
			<uni-card v-else v-for="item in list" :key="item.id">
				<slot name="header">
					<view class="header">
						<view class="title">{{item.creationTime | formatDatetime}}</view>
						<view class="status">
							<uni-tag :type="fetchTagType(item.status)" :text="item.statusDesc"></uni-tag>
						</view>
					</view>
				</slot>
				<uni-list>
					<uni-list-item title="申请人" :rightText="item.name"></uni-list-item>
					<uni-list-item title="联系电话" :rightText="item.phone"></uni-list-item>
					<uni-list-item title="申请数量" :rightText="item.count"></uni-list-item>
					<uni-list-item title="申请租用时间" :rightText="item.rentTime | formatDatetime">
					</uni-list-item>
					<uni-list-item title="申请时间" :rightText="item.creationTime | formatDatetime"></uni-list-item>
				</uni-list>
				<slot name="footer">
					<button type="primary" @click="acceptApply(item)">通过</button>
					<button type="warn" @click="rejectApply(item)">拒绝</button>
				</slot>
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
						<uni-data-picker v-model="form.lockerIds"></uni-data-picker>
					</uni-forms-item>
					<uni-forms-item label="备注">
						<uni-easyinput maxlength="500" type="textarea" v-model="form.remark"></uni-easyinput>
					</uni-forms-item>
				</uni-forms>
				<slot name="footer"><button type="primary" @click="accept">通过</button></slot>
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
				<slot name="footer"><button type="warn" @click="reject">拒绝</button></slot>
			</uni-card>
		</uni-popup>
	</view>
</template>

<script>
	import {
		getList,
		detail
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
		onLoad() {
			this.getList()
			this.loadLockers()
		},
		onReachBottom() {
			this.more()
		},
		onPullDownRefresh() {
			this.search()
		},
		methods: {
			accept() {
				const that = this
				uni.showModal({
					content: '是否通过该申请？'
				})
			},
			reject() {
				const that = this
				uni.showModal({
					content: '是否拒绝该申请？'
				})
			},
			loadLockers() {
				lockerApi.getList({
					status: 1,
					maxResultCount: 99
				}).then(res => {
					this.lockers = res.items
				})
			},
			acceptApply(info) {
				this.applyInfo = info
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
					status: 0
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
				this.page += 1
				this.getList()
			}
		}
	}
</script>

<style>
	.header {
		height: 1.85em;
	}

	.header .title {
		font-size: 1.2em;
		font-weight: 500;
		display: inline-block;
	}

	.header .status {
		float: right;
		display: inline;
	}
</style>
