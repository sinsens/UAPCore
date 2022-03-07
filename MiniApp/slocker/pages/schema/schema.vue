<template>
	<view class="container">
		<no-data v-if="list.length == 0"></no-data>
		<uni-card v-else>
			<uni-list v-for="item in list" :key="item.id">
				<uni-list-item :title="$t('rent.process.name')" :note="item.name"></uni-list-item>
				<uni-list-item :title="$t('rent.process.rentTime')" :note="item.rentTime | formatDatetime">
				</uni-list-item>
				<uni-list-item :title="$t('rent.process.creationTime')" :note="item.creationTime | formatDatetime">
				</uni-list-item>
				<uni-list-item :title="$t('rent.process.statusDesc')" :note="item.statusDesc"></uni-list-item>
			</uni-list>
		</uni-card>
	</view>
</template>

<script>
	import {
		getList
	} from '@/api/apply.js'
	export default {
		data() {
			return {
				list: [],
				title: this.$t('')
			}
		},
		onShow() {
			this.getList()
		},
		methods: {
			getList() {
				getList({
					page: 1,
				}).then(res => {
					this.list = res.items
				})
			}
		}
	}
</script>

<style>

</style>
