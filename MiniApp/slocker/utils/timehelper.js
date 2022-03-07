import moment from 'moment'

/**
 * 格式化时间字符串
 * @param {Object} t
 */
export function toDatetime(t) {
	if (t) {
		const datetime = moment(t)
		return datetime.format('YYYY-MM-DDTHH:mm')
	}
	return ''
}

/**
 * 格式化时间
 * @param {Object} dt
 */
export function formatDatetime(dt) {
	if (dt) {
		const datetime = moment(dt)
		return datetime.format('YYYY-MM-DD HH:mm')
	}
	return ''
}
