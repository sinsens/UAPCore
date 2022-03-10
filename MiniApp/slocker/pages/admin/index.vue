<template>
	<view class="container">
		<uni-list>
			<uni-list-item link to="apply/apply-list?status=0" title="申请列表(待审核)" @click="audit" show-badge="true"
				:badge-text="waitAuditCount"></uni-list-item>
			<uni-list-item link to="apply/apply-list" title="申请列表(全部)" @click="audit" show-badge="true"
				:badge-text="applyCount">
			</uni-list-item>
			<uni-list-item link to="rent/rent-list?status=1" title="待归还列表" @click="audit" show-badge="true"
				:badge-text="waitReturnCount">
			</uni-list-item>
			<uni-list-item link to="rent/rent-list" title="租用记录" @click="audit" show-badge="true"
				:badge-text="rentCount">
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
				waitAuditCount: 0,
				waitReturnCount: 0,
				rentCount: 0,
			}
		},
		onShow() {
			if (this.$store.state.token == '') {
				uni.reLaunch({
					url: '../login/admin'
				})
			}
			this.updateStatus()
		},
		methods: {
			updateStatus() {
				getList({
					status: 0,
					maxResultCount: 0
				}).then(res => {
					this.waitAuditCount = res.totalCount
				})
				getList({
					status: '',
					maxResultCount: 0
				}).then(res => {
					this.applyCount = res.totalCount
				})
				rentApi.getList({
					status: 1,
					maxResultCount: 0
				}).then(res => {
					this.waitReturnCount = res.totalCount
				})
				rentApi.getList({
					maxResultCount: 0,
					status: []
				}).then(res => {
					this.rentCount = res.totalCount
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
