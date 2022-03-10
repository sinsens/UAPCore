<script>
	import config from 'static/config.js'

	export default {
		globalData: {
			text: 'text',
			filBaseUrl: config.APP_STORAGE_API
		},
		onLaunch: function(obj) {
			console.log('App Launch')
			console.log(obj)
		},
		onShow: function() {
			console.log('App Show')
		},
		onHide: function() {
			console.log('App Hide')
		}
	}
</script>

<style>
	@import './common/uni.css';

	.container {
		padding: 15px;
	}

	button {
		margin-bottom: 15px;
		width: 100%;
	}
</style>
