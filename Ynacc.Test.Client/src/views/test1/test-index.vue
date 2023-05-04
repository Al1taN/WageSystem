<template>
  <div class="app-container">
    <div class="filter-container">
      test_user
    </div>

  </div>
</template>

<script>

import waves from '@/directive/waves' // waves directive
export default {
  name: 'ComplexTable',
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
