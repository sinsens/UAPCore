import dev from './env.dev.js'
import prod from './env.prod.js'

let config = {}
// uEnvDev
if (process.env.NODE_ENV === 'development') {
	// TODO
	config = {
		...dev
	}
}
// uEnvProd
if (process.env.NODE_ENV === 'production') {
	// TODO
	config = {
		...prod
	}
}

export default config
