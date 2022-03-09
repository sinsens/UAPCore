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
					<uni-list-item :title="$t('rent.history.name')" :rightText="item.name"></uni-list-item>
					<uni-list-item :title="$t('rent.history.rentTime')" :rightText="item.rentTime | formatDatetime">
					</uni-list-item>
				</uni-list>
			</uni-card>
		</view>
		<uni-load-more :status="moreStatus"></uni-load-more>
	</view>
</template>

<script>
	import {
		getList,
		detail,
		audit,
		discard
	} from '../../../api/rent.js'
	import {
		rentApplyStatus
	} from '@/static/enums.js'
	export default {
		data() {
			return {
				list: [],
				total: 0,
				keyword: '',
				page: 1
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
		},
		onReachBottom() {
			this.more()
		},
		onPullDownRefresh() {
			this.search()
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
				getList({
					page: this.page,
					skipCount: this.skipCount,
					filter: this.keyword,
					status: 1
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
