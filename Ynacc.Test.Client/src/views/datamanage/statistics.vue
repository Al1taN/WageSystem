<template>
  <div>
    <div class="main"></div>
    <el-select v-model="value" placeholder="请选择数据查询类别">
      <el-option
        v-for="item in options"
        :key="item.value"
        :label="item.label"
        :value="item.value">
      </el-option>
    </el-select>
    <span class="time">
      时间段： 从<input type="date" v-model="begin">
      到 <input type="date" v-model="end">
    </span>
    <span class="time">
      工资号： <el-input type="text" placeholder="默认不区分" maxlength=6 style="width: 10%;" show-word-limit v-model="number"></el-input>
    </span>
    <span class="gap"></span>
    <span class="time"><el-button @click="check">查询</el-button></span>
    <span class="time"><el-button :loading="downloadLoading" @click="handleDownload">导出EXCEL</el-button></span>
    <span class="time"><el-button @click="close">关闭</el-button></span>
    <el-table v-if="flag" :data="tableData" border highlight-current-row style="width: 100%;margin-top:20px;">
      <el-table-column v-for="item of tableHeader" :key="item" :prop="item" :label="item" />
    </el-table>
  </div>
</template>

<script>
  import { parseTime } from '@/utils'
  export default {
    data() {
    return {
      options: [{
        value: '新版员工考勤统计',
        label: '新版员工考勤统计'
        }, {
          value: '新版劳务制员工考勤统计',
          label: '新版劳务制员工考勤统计'
        }, {
          value: '老版员工考勤统计',
          label: '老版员工考勤统计'
        }, {
          value: '老版劳务制员工考勤统计',
          label: '老版劳务制员工考勤统计'  
        }],
        value: '',
        begin:'',
        end:'',
        tableData: [{'部门':1,'工资号':2,'姓名':3,'复核':4,'审核':5}],
        tableHeader: ['部门','工资号','姓名','复核','审核'],
        downloadLoading: false,
        autoWidth: true,
        bookType: 'xlsx',
        flag: false,
        number:''
      } 
  },
  methods:{
    check(){
      if(this.value && this.begin && this.end && this.end >= this.begin){
        this.flag = true
        this.tableData = ''
        this.tableHeader = ''
        if(this.number == ''){
          this.number = '000000'
        }
        //判断是调用哪张表
        switch(this.value){
          case '新版员工考勤统计':{
            //获取数据，条件：时间，复核，审核
            this.$axios({
            method: 'GET',
            url: 'http://localhost:5189/api/app/GetStaticsAttend',
            params:{
              ID:this.number,
              beginDay:this.begin,
              endDay:this.end,
            }
            }).then(res => {this.tableData=res.data})
            this.tableHeader=['工资号','姓名','发生年份','发生月份','请假类型','天数','补考勤扣款','补工作餐积点扣款','合计天数','合计补考勤扣款','合计补工作餐积点扣款']
          }
          //repeat method
          case '新版早夜班加班':{

          }
        }
        this.$message({
          message: '查询成功！',
          type: 'success'
        })
      }
      else{
        this.flag = false
        this.$message({
        message: '查询失败！请填写合法且完整的信息！',
        type: 'error'
      })}    
    },
    close(){
      this.flag = false
    },
    handleDownload() {
        this.downloadLoading = true
        import('@/vendor/Export2Excel').then(excel => {
          const list = this.tableData
          const filterVal = this.tableHeader
          const data = this.formatJson(filterVal, list)
          this.$message({
            message: data,
            type: 'success'
          })
          excel.export_json_to_excel({
            header: this.tableHeader,
            data,
            filename: this.value,
            autoWidth: this.autoWidth,
            bookType: this.bookType
          })
          this.downloadLoading = false
        })
      },
    formatJson(filterVal, jsonData) {
      return jsonData.map(v => filterVal.map(j => {
        if (j === 'timestamp') {
          return parseTime(v[j])
        } else {
          return v[j]
        }
      }))
    }
  }
}
</script>

<style lang="css" scoped>
.main{
  margin-top: 2%;
}
.time{
  padding-left:2%;
}
.el-select{
  margin-left: 2%;
}
.gap{
    padding-left:10%;
  }
</style>