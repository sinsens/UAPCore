<template>
  <div class="dashboard-container">
    <component :is="currentRole" />
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import adminDashboard from './admin'
import editorDashboard from './editor'
import tenantDashborad from './tenant'

export default {
  name: 'Dashboard',
  components: { adminDashboard, editorDashboard, tenantDashborad },
  data() {
    return {
      currentRole: 'adminDashboard',
    }
  },
  computed: {
    ...mapGetters(['roles']),
  },
  created() {
    if (!this.roles.includes('AbpIdentity.Roles')) {
      //this.currentRole = 'editorDashboard'
      this.currentRole = 'tenantDashborad'
    }
  },
}
</script>
