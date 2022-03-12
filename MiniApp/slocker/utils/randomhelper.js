/**
 * 随机 helper
 */

/**
 * 生成随机数
 * @param {Number} length 长度
 */
export function getRandNumText(length = 4) {
	let code = ''
	for (let i = 0; i < length; i++)
		code += Math.round(Math.random() * 10)
	return code.substring(0, length)
}
