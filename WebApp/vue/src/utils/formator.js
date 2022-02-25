import moment from 'moment'


export function formatDate(value) {
    if (value) {
        return moment(String(value)).format('YYYY-MM-DD')
    }
    return ''
}

export function formatDateTime(value) {
    if (value) {
        return moment(String(value)).format('YYYY-MM-DD HH:mm:ss')
    }
    return ''
}