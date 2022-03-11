<script>
	import config from 'static/config.js'
	import store from 'store/index.js'

	export default {
		globalData: {
			text: 'text',
			filBaseUrl: config.APP_STORAGE_API
		},
		onLaunch: function(obj) {
			console.log('App Launch')
			console.log(obj)
			if (obj && obj.query && obj.query.hasOwnProperty('tenant')) {
				store.commit('setTenant', decodeURI(obj.query['tenant']))
			}
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
		/*width: 100%;*/
	}

	.header {
		height: 1.85em;
	}

	.header .title {
		margin-top: 15rpx;
		/*font-size: 1.1em;*/
		font-weight: 400;
		display: inline-block;
	}

	.header .status {
		float: right;
		display: inline;
	}
</style>
