<template>
	<view class="container">
		<view class="title">{{$t('index.title')}}</view>
		<view class="locale-setting">{{$t('index.language')}}</view>
		<view class="locale-list">
			<view class="locale-item" v-for="(item, index) in locales" :key="index" @click="onLocaleChange(item)">
				<text class="text uni-label">{{item.text}}</text>
				<text class="icon-check" v-if="item.code == applicationLocale"></text>
			</view>
		</view>
		<view class="locale-setting">{{$t('mine.profile')}}</view>
		<view class="locale-list">
			<view class="locale-item">
				<label class="uni-label">{{$t('mine.name')}}</label><input v-model="name" />
			</view>
			<view class="locale-item">
				<label class="uni-label">{{$t('mine.phone')}}</label><input v-model="phone" />
			</view>
			<view class="locale-item">
				<button class="uni-button" @click="saveProfile()">{{$t('mine.savebtn')}}</button>
			</view>
		</view>
	</view>
</template>

<script>
	import {
		updateNameAndPhone
	} from '../../api/user.js'
	export default {
		data() {
			return {
				systemLocale: '',
				applicationLocale: '',
				name: this.$store.state.name,
				phone: this.$store.state.phone
			}
		},
		computed: {

			locales() {
				return [{
						text: this.$t('locale.auto'),
						code: 'auto'
					}, {
						text: this.$t('locale.en'),
						code: 'en'
					},
					{
						text: this.$t('locale.zh-hans'),
						code: 'zh-Hans'
					},
					{
						text: this.$t('locale.zh-hant'),
						code: 'zh-Hant'
					},
					{
						text: this.$t('locale.ja'),
						code: 'ja'
					}
				]
			}
		},
		onLoad() {
			const lang = this.$store.state.lang
			if (lang) {
				this.$i18n.locale = lang
				this.applicationLocale = lang
			}
		},
		methods: {
			saveProfile() {
				const options = {
					id: this.$store.state.userid,
					name: this.name,
					phone: this.phone
				}
				updateNameAndPhone(options)
				this.$store.commit('setProfile', options)
			},
			onLocaleChange(e) {
				if (this.isAndroid) {
					uni.showModal({
						content: this.$t('index.language-change-confirm'),
						success: (res) => {
							if (res.confirm) {
								this.applicationLocale = e.code
								uni.setLocale(e.code);
								this.$store.commit('setLang', e.code)
							}
						}
					})
				} else {
					this.applicationLocale = e.code
					uni.setLocale(e.code);
					this.$i18n.locale = e.code;
					this.$store.commit('setLang', e.code)
				}
			}
		}
	}
</script>

<style>
	.uni-label-pointer {
		width: 80px;
	}

	.title {
		font-size: 16px;
		font-weight: bold;
		margin-bottom: 15px;
	}

	.description {
		font-size: 14px;
		opacity: 0.6;
		margin-bottom: 15px;
	}

	.detail-link {
		font-size: 14px;
		word-break: break-all;
	}

	.link {
		color: #007AFF;
		margin-left: 10px;
	}

	.locale-setting {
		font-size: 16px;
		font-weight: bold;
		margin-top: 25px;
		margin-bottom: 5px;
		padding-bottom: 5px;
		border-bottom: 1px solid #f0f0f0;
	}

	.list-item {
		font-size: 14px;
		padding: 10px 0;
	}

	.list-item .v {
		margin-left: 5px;
	}

	.locale-item {
		display: flex;
		flex-direction: row;
		padding: 10px 0;
	}

	.locale-item .text {
		flex: 1;
	}

	.icon-check {
		margin-right: 5px;
		border: 2px solid #007aff;
		border-left: 0;
		border-top: 0;
		height: 12px;
		width: 6px;
		transform-origin: center;
		/* #ifndef APP-NVUE */
		transition: all 0.3s;
		/* #endif */
		transform: rotate(45deg);
	}
</style>
