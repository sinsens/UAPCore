/** When your routing table is too long, you can split it into small modules **/

import Layout from '@/layout'

const sharedlockersRouter = {
  path: '/sharedlockers',
  component: Layout,
  redirect: 'noRedirect',
  name: 'SharedLockers',
  meta: {
    title: 'sharedLockers',
    icon: 'apps',
    roles: ['SharedLocker.Locker', 'SharedLocker.LockerRent']
  },
  children: [{
    path: 'rent',
    component: () => import('@/views/shared-locker/rent'),
    name: 'rent',
    meta: {
      title: 'sharedLockersRent', icon: 'locker-rent',
      roles: ['SharedLocker.Locker']
    }
  },
  {
    path: 'list',
    component: () => import('@/views/shared-locker/index'),
    name: 'list',
    meta: { title: 'sharedLockers', icon: 'lockers', roles: ['SharedLocker.LockerRent'] }
  }
  ]
}

export default sharedlockersRouter
