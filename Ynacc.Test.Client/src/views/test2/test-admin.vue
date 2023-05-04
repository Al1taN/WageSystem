<template>
  <div class="app-container">
    <div class="filter-container">
      test_Admin可以访问
    </div>

  </div>
</template>

<script>

import waves from '@/directive/waves' // waves directive
export default {
  name: 'TestAdmin',
  directives: { waves },
  filters: {
  },
  data() {
    return {
      tableKey: 0
    }
  },
  created() {
  },
  methods: {
  }
}
</script>
