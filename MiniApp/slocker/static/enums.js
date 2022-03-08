/**
 * 枚举
 */

/**
 * 申请状态
 */
export const rentApplyStatus = {
	/**
	 * 审核中
	 */
	PendingAudit: 0,
	/**
	 * 已通过
	 */
	Accepted: 2,
	/**
	 * 已拒绝
	 */
	Rejected: 5,

	/**
	 * 已取消
	 */
	Canceled: 10,

	/**
	 * 已作废
	 */
	Discard: 11,
}

/**
 * 租用状态
 */
export const rentStatus = {
	/**
	 * 租用中
	 */
	InService: 1,

	/**
	 * 已结束
	 */
	EndService: 8,

	/**
	 * 已作废
	 */
	Discard: 10,
}
