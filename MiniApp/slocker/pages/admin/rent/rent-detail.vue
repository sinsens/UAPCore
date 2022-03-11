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
				id: '',
				rentInfo: {}
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
			}
		}
	}
</script>

<style>
	
</style>
