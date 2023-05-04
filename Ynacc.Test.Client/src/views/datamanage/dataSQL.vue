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
        <span class="time">
        <el-checkbox v-model="checked01">复核</el-checkbox>
        <el-checkbox v-model="checked02">审核</el-checkbox>
        </span>
      </span>
      <span class="gap"></span>
      <span class="time"><el-button @click="check">查询</el-button></span>
      <span class="time"><el-button :loading="downloadLoading" @click="handleDownload">导出EXCEL</el-button></span>
      <span class="time"><el-button @click="close">关闭</el-button></span>
      <el-table v-if="flag" :data="tableData" border highlight-current-row style="width: 100%;margin-top:20px;">
        <el-table-column v-for="item of tableHeader" :key="item" :prop="item" :label="item" :formatter="dataFormat"/>
      </el-table>
    </div>
  </template>
  
  <script>
    import { parseTime } from '@/utils'
    export default {
      data() {
      return {
        options: [{
          value: '新版员工考勤',
          label: '新版员工考勤'
          }, {
            value: '新版早夜班加班',
            label: '新版早夜班加班'
          }, {
            value: '老版员工考勤补扣款',
            label: '老版员工考勤补扣款'
          }, {
            value: '老版员工加班费',
            label: '老版员工加班费'
          }, {
            value: '老版员工早夜班费',
            label: '老版员工早夜班费'
          }, {
            value: '劳务制新版员工考勤',
            label: '劳务制新版员工考勤'
          }, {
            value: '劳务制新版早夜班加班',
            label: '劳务制新版早夜班加班'
          }, {
            value: '劳务制老版员工考勤补扣款',
            label: '劳务制老版员工考勤补扣款'
          }, {
            value: '劳务制老版加班工资',
            label: '劳务制老版加班工资'
          }, {
            value: '劳务制老版早夜班费',
            label: '劳务制老版早夜班费'           
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
          checked01:false,
          checked02:false,
        } 
    },
    methods:{
      dataFormat(row, column) {
        const field = column.property
        if (Object.prototype.hasOwnProperty.call(row, field)) {
          if (row[field] === true) {
            return 1
          }
          if (row[field] === false) {
            return 0
          }
        }
        return row[field]
      },
      check(){
        if(this.value && this.begin && this.end && this.end >= this.begin){
          this.flag = true
          this.tableData = ''
          this.tableHeader = ''
          //判断是调用哪张表
          switch(this.value){
            case '新版员工考勤':{
              //获取数据，条件：时间，复核，审核
              this.$axios({
              method: 'GET',
              url: 'http://localhost:5189/api/app/GetNewAttend',
              params:{
                checked01:this.checked01,
                checked02:this.checked02,
                begin:this.begin,
                end:this.end,
                ID:'009146'
              }
              }).then(res => {this.tableData=res.data})
              this.tableHeader=['部门','工资号','姓名','缺勤类型','缺勤年度','缺勤月份','缺勤开始日期','缺勤结束日期','缺勤天数含休息日','缺勤天数不含休息日','补考勤扣款'
            ,'补工作餐积点扣款','备注','复核','审核']
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