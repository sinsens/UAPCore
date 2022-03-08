<template>
	<view class="container">
		<no-data v-if="list.length == 0"></no-data>
		<uni-card v-else v-for="item in list" :key="item.id" :title="item.creationTime | formatDatetime"
			:extra="item.statusDesc">
			<uni-list>
				<uni-list-item :title="$t('rent.process.name')" :note="item.name"></uni-list-item>
				<uni-list-item :title="$t('rent.process.rentTime')" :note="item.rentTime | formatDatetime">
				</uni-list-item>
			</uni-list>
		</uni-card>
	</view>
</template>

<script>
	import {
		getMyList,
		getMyProcessList
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
				getMyProcessList({
					page: 1,
					status: 2
				}).then(res => {
					this.list = res.items
				})
			}
		}
	}
</script>

<style>

</style>
