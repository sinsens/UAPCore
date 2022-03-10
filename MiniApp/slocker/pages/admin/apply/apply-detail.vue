<template>
	<view>
		<uni-card>
			<slot name="header">
				<view class="header">
					<view class="title">申请信息</view>
					<view class="status">
						<uni-tag :type="fetchTagType(applyInfo.status)" :text="applyInfo.statusDesc"></uni-tag>
					</view>
				</view>
			</slot>
			<uni-list>
				<uni-list-item title="申请人" :rightText="applyInfo.name"></uni-list-item>
				<uni-list-item title="联系电话" :rightText="applyInfo.phone"></uni-list-item>
				<uni-list-item title="申请数量" :rightText="applyInfo.count"></uni-list-item>
				<uni-list-item title="申请租用时间" :rightText="applyInfo.rentTime | formatDatetime"></uni-list-item>
				<uni-list-item title="申请时间" :rightText="applyInfo.creationTime | formatDatetime">
				</uni-list-item>
			</uni-list>
		</uni-card>
		<uni-card>
			<slot name="header">
				<view class="header">
					<view class="title">审核信息</view>
				</view>
			</slot>
			<uni-list>
				<uni-list-item title="审核人" :rightText="applyInfo.auditor"></uni-list-item>
				<uni-list-item title="审核时间" :rightText="applyInfo.auditTime | formatDatetime"></uni-list-item>
				<uni-list-item title="备注" :rightText="applyInfo.auditRemark"></uni-list-item>
			</uni-list>
		</uni-card>
		<uni-card v-if="applyInfo.status == rentApplyStatus.Accepted">
			<slot name="header">
				<view class="header">
					<view class="title">储物柜分配</view>
					<view class="status">
						<uni-tag :type="fetchTagType(applyInfo.status)" :text="applyInfo.statusDesc"></uni-tag>
					</view>
				</view>
			</slot>
			<uni-list>
				<uni-list-item title="审核人" :rightText="applyInfo.auditor"></uni-list-item>
				<uni-list-item title="审核时间" :rightText="applyInfo.auditTime | formatDatetime"></uni-list-item>
				<uni-list-item title="备注" :rightText="applyInfo.auditRemark"></uni-list-item>
			</uni-list>
		</uni-card>
	</view>
</template>

<script>
	import {
		detail
	} from '@/api/apply.js'
	import {
		rentApplyStatus
	} from '@/static/enums.js'
	export default {
		data() {
			return {
				id: '',
				applyInfo: {},
				rentApplyStatus: rentApplyStatus
			}
		},
		onLoad(options) {
			this.id = options.id
			this.updateStatus()
		},
		onPullDownRefresh() {
			this.updateStatus()
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
			updateStatus() {
				detail(this.id).then(applyInfo => {
					this.applyInfo = applyInfo
				})
			}
		}
	}
</script>

<style>
	.header {
		height: 1.85em;
	}

	.header .title {
		font-size: 1.1em;
		font-weight: 500;
		display: inline-block;
	}

	.header .status {
		float: right;
		display: inline;
	}
</style>
