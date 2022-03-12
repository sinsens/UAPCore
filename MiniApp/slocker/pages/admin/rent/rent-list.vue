<template>
	<view>
		<uni-search-bar v-model="keyword" @cancel="cancelSearch" @clear="cancelSearch" @confirm="search" @input="search"
			@blur="search" placeholder="输入姓名或手机号"></uni-search-bar>
		<view class="container">
			<uni-card v-for="item in list" :key="item.id">
				<template slot="title">
					<view class="header">
						<view class="title">{{item.name}}</view>
						<view class="status">
							<uni-tag :type="fetchTagType(item.status)" :text="item.statusDesc"></uni-tag>
						</view>
					</view>
				</template>
				<uni-list>
					<uni-list-item title="联系方式" :rightText="item.phone">
					</uni-list-item>
					<uni-list-item title="租用时间" :rightText="item.rentTime | formatDatetime">
					</uni-list-item>
				</uni-list>
				<template slot="actions">
					<button @click="detail(item.id)">详情</button>
				</template>
			</uni-card>
		</view>
		<uni-load-more :status="moreStatus"></uni-load-more>
	</view>
</template>

<script>
	import rentApi from '../../../api/rent.js'
	import {
		rentStatus
	} from '@/static/enums.js'

	export default {
		data() {
			return {
				list: [],
				keyword: '',
				rentStatus: rentStatus,
				status: [],
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
				this.status = [Number(options['status'])]
				let title = '租用记录'
				switch (Number(options['status'])) {
					case rentStatus.InService:
						title = '待归还列表'
						break
					case rentStatus.EndService:
						title = '已归还列表'
						break
					case rentStatus.Discard:
						title = '已作废列表'
						break
				}
				uni.setNavigationBarTitle({
					title: title
				})
			}
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
					url: `../rent/rent-detail?id=${id}`
				})
			},

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
			getList() {
				this.moreStatus = 'loading'
				rentApi.getList({
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
