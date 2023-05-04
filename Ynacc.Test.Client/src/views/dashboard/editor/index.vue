<template>
  <div class="dashboard-editor-container">
    <div class=" clearfix">
      <pan-thumb :image="avatar" style="float: left">
        Your roles:
        <span v-for="item in roles" :key="item" class="pan-info-roles">{{ item }}</span>
      </pan-thumb>
      <div class="info-container">
        <span class="display_name">{{ name }}</span>
        <span style="font-size:20px;padding-top:20px;display:inline-block;">欢迎使用</span>
        <span class="form01"> <el-button @click="help">帮助</el-button></span>
      </div>
    </div>
    <div>
      <img src="" class="emptyGif">
    </div>
    <br><br><br><br>
    <p v-show="show">
      1.数据导入要严格按照员工考勤格式和早夜班加班格式,打开表格按照第二行的说明逐行输入数据，并删除第二行<br><br>
      2.只能导入、复核、审核当前截止年月的数据<br><br>
      3.新用户需要配置角色权限，并添加部门数据<br><br>
      4.数据统计的合计天数、合计补考勤扣款、合计补工作餐积点扣款为此人当月份的总合计数据（此人本月多种请假类型的数据总计）<br><br>
      5.如遇到未知错误请先使用Ctrl+F5刷新页面！！！<br><br>
    </p>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import PanThumb from '@/components/PanThumb'
import {GetDept} from '@/api/dashboard'
//import GithubCorner from '@/components/GithubCorner'

export default {
  name: 'DashboardEditor',
  components: { PanThumb },
  created:async function(){
    await GetDept().then(res => {this.$store.dispatch('user/getAchive', res)})
  },
  data() {
    return {
      emptyGif: 'https://wpimg.wallstcn.com/0e03b7da-db9e-4819-ba10-9016ddfdaed3',
      show:false
    }
  },
  computed: {
    ...mapGetters([
      'name',
      'avatar',
      'roles'
    ])
  },
  methods:{
    help(){
      this.show = !this.show
    }
  }
}
</script>

<style lang="scss" scoped>
  .emptyGif {
    display: block;
    width: 45%;
    margin: 0 auto;
  }
  .form01{
    margin-left: 10%;
  }

  .dashboard-editor-container {
    background-color: #e3e3e3;
    min-height: 100vh;
    padding: 50px 60px 0px;
    .pan-info-roles {
      font-size: 12px;
      font-weight: 700;
      color: #333;
      display: block;
    }
    .info-container {
      position: relative;
      margin-left: 190px;
      height: 150px;
      line-height: 200px;
      .display_name {
        font-size: 48px;
        line-height: 48px;
        color: #212121;
        position: absolute;
        top: 25px;
      }
    }
  }
</style>
