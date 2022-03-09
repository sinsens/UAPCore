<template>
	<view class="container">
		<uni-list>
			<uni-list-item link to="apply/apply" title="申请列表" @click="audit" show-badge="true" :badge-text="applyCount">
			</uni-list-item>
			<uni-list-item link to="audit-return/audit-return" title="归还" @click="audit" show-badge="true"
				:badge-text="waitReturnCount">
			</uni-list-item>
		</uni-list>
	</view>
</template>

<script>
	import {
		getList
	} from '../../api/apply.js'
	import rentApi from '../../api/rent.js'

	export default {
		data() {
			return {
				applyCount: 0,
				waitReturnCount: 0
			}
		},
		onShow() {
			this.updateStatus()
		},
		methods: {
			updateStatus() {
				getList({
					status: 0
				}).then(res => {
					this.applyCount = res.totalCount
				})
				rentApi.getList({
					status: 1
				}).then(res => {
					this.waitReturnCount = res.totalCount
				})
			},
			audit() {
				uni.navigateTo({
					url: './apply/apply'
				})
			},
			auditReturn() {
				uni.navigateTo({
					url: 'audit-return/audit-return'
				})
			}
		}
	}
</script>

<style>
	.container {
		margin: 200rpx 10rpx;
	}
</style>
