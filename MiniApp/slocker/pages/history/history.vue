<template>
	<view class="container">
		<uni-card v-for="item in list" :key="item.id">
			<template slot="header">
				<view class="header">
					<view class="title">{{item.creationTime | formatDatetime}}</view>
					<view class="status">
						<uni-tag :type="fetchTagType(item.status)" :text="item.statusDesc"></uni-tag>
					</view>
				</view>
			</template>
			<uni-list>
				<uni-list-item :title="$t('rent.history.name')" :rightText="item.name"></uni-list-item>
				<uni-list-item :title="$t('rent.history.rentTime')" :rightText="item.rentTime | formatDatetime">
				</uni-list-item>
			</uni-list>
		</uni-card>
		<uni-load-more :status="moreStatus"></uni-load-more>
	</view>
</template>

<script>
	import {
		getMyList
	} from '@/api/apply.js'
	import {
		rentApplyStatus
	} from '@/static/enums.js'


	export default {
		data() {
			return {
				rentApplyStatus: rentApplyStatus,
				list: [],
				title: this.$t(''),
				page: 1,
				count: 0,
				total: 0,
				moreStatus: 'noMore'
			}
		},
		computed: {
			canLoadMore() {
				return this.count < this.total
			}
		},
		onShow() {
			this.getList()
		},
		onReachBottom() {
			if (this.canLoadMore) {
				this.getList()
			}
		},
		onPullDownRefresh() {
			this.count = 0
			this.list = []
			this.page = 1
			this.getList()
		},
		methods: {
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
				getMyList({
					page: this.page,
					skipCount: this.page * this.count
				}).then(res => {
					res.items.map(item => {
						this.list.push(item)
					})
					uni.stopPullDownRefresh()
					this.total = res.totalCount
					this.count += res.items.length
					this.moreStatus = this.canLoadMore ? 'more' : 'noMore'
				}).catch(() => {
					uni.stopPullDownRefresh()
					this.moreStatus = 'noMore'
				})
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
