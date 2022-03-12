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
					<view>
						<button @click="detail(item.id)">详情</button>
					</view>
				</template>
			</uni-card>
		</view>
		<uni-load-more :status="moreStatus"></uni-load-more>
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
			detail(id) {
				uni.navigateTo({
					url: `apply-detail?id=${id}`
				})
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
