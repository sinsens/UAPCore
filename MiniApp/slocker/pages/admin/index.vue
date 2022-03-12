<template>
	<view class="container">
		<uni-list>
			<uni-list-item>
				<template slot="body" @click="startQrCodeScan">
					扫码处理<uni-icons type="camera"></uni-icons>
				</template>
			</uni-list-item>
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
		qrCodeType
	} from '../../static/enums.js'
	import {
		getList
	} from '../../api/apply.js'
	import rentApi from '../../api/rent.js'
	import w_md5 from '../../js_sdk/zww-md5/w_md5.js'

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
			startQrCodeScan() {
				const that = this
				uni.scanCode({
					success(res) {
						console.log('扫码成功')
						console.log(res)
						if (res.scanType != 'QR_CODE') {
							uni.showToast({
								icon: 'error',
								title: '无法识别'
							})
						}
						that.resolveQrCode(res.result)
					}
				})
			},
			resolveQrCode(data) {
				try {
					const rawData = JSON.parse(data)
					const {
						type,
						id,
						code,
						sign
					} = rawData
					const signStr = w_md5.hex_md5_16(`${this.$store.state.tenantid}${id}${code}`)
					if (signStr != sign) {
						uni.showToast({
							icon: 'error',
							title: '校验失败'
						})
						return
					}
					if (type) {
						let url = ''
						switch (rawData.type) {
							case qrCodeType.Apply:
								url = `apply/apply-detail?id=${id}`
								break
							case qrCodeType.Rent:
								url = `rent/rent-detail?id=${id}`
								break
						}
						uni.redirectTo({
							url: url
						})
					}
				} catch (e) {
					console.log('二维码解析失败')
					uni.showToast({
						icon: 'error',
						title: '内容错误'
					})
				}
			},
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
