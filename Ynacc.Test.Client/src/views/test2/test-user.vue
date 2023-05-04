<template>
  <div class="app-container">
    <div class="filter-container">
      test_user可以访问
    </div>
    <el-button v-waves class="filter-item" type="primary" icon="el-icon-search" @click="DoSearch">
      调用服务
    </el-button>
    {{ res }}
  </div>
</template>

<script>
import { WeatherForecast } from '@/api/test'
import waves from '@/directive/waves' // waves directive
export default {
  name: 'TestUser',
  directives: { waves },
  filters: {
  },
  data() {
    return {
      res: ''
    }
  },
  created() {
  },
  methods: {
    DoSearch() {
      this.res = ''
      WeatherForecast().then(response => {
        this.res = response
      }
      )
    }
  }
}
</script>
