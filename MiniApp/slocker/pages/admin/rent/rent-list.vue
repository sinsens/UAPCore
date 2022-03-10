<template>
	<view>
		<uni-search-bar v-model="keyword" @cancel="cancelSearch" @clear="cancelSearch" @confirm="search" @input="search"
			@blur="search" placeholder="输入姓名或手机号"></uni-search-bar>
		<view class="container">
			<uni-card v-for="item in list" :key="item.id">
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
				<slot name="footer">
					<button v-if="item.status == rentStatus.InService" type="primary" @click="preReturn(item)">归还</button>
					<button @click="detail(item.id)">详情</button>
				</slot>
			</uni-card>
		</view>
		<uni-load-more :status="moreStatus"></uni-load-more>
		<uni-popup ref="returnDialog" background-color="#fff" :isMaskClick="false">
			<uni-card>
				<uni-list>
					<uni-list-item title="租用人" :rightText="rentInfo.name"></uni-list-item>
					<uni-list-item title="联系电话" :rightText="rentInfo.phone"></uni-list-item>
					<uni-list-item title="租用数量" :rightText="rentInfo.count"></uni-list-item>
					<uni-list-item title="租用时间" :rightText="rentInfo.rentTime | formatDatetime">
					</uni-list-item>
					<uni-list-item title="储物柜">

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
				<slot name="footer">
					<button type="primary" @click="submit">确认归还</button>
					<button @click="closeDialog">取消</button>
				</slot>
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
				list: [],
				rentInfo: {},
				keyword: '',
				rentStatus: rentStatus,
				status: [],
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
				let title = ''
				switch (this.status) {
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
			closeDialog() {
				this.$refs.returnDialog.close()
			},
			preReturn(rentInfo) {
				this.rentInfo = rentInfo
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
					this.$refs.returnDialog.close()
				}).catch(err => {
					uni.showToast({
						icon: 'error',
						title: err || '网络异常'
					})
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
