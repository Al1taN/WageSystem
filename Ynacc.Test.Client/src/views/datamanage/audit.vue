<template>
  <div>
    <div class="main"></div>
    <span class="time">
      上报年份：<input type="number" v-model="year">
      上报月份： <input type="number" v-model="month">
      <span class="time">
      </span>
    </span>
    <span class="gap"></span>
    <span class="time"><el-button @click="check">查询</el-button></span>
    <span class="time"><el-button @click="recheck">审核签字</el-button></span>
    <span class="time"><el-button @click="unrecheck">取消审核签字</el-button></span>
    <span class="time"><el-button @click="save">保存</el-button></span>
    <span class="time"><el-button @click="close">关闭</el-button></span>
    <span class="time"> <span class="font">{{sign}}</span></span>
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
        value: '',
        year:'',
        month:'',
        tableData: [{'部门':1,'工资号':2,'姓名':3,'复核':false,'审核':true}],
        tableHeader: ['部门','工资号','姓名','复核','审核'],
        flag: false,
        sign:''
      } 
  },
  methods:{
    dataFormat(row, column) {
      const field = column.property
      if (Object.prototype.hasOwnProperty.call(row, field)) {
        if (row[field] === true || row[field] === false) {
          return '暂不要求'
        }
      }
      return row[field]
    },
    check(){
        if(1900<= this.year && 1<=this.month&&this.month<=12 ){
          this.flag = true
          this.tableData = ''
          this.tableHeader = ''
              this.$axios({
              method: 'GET',
              url: 'http://localhost:5189/api/app/GetData',
              params:{
                year:this.year,
                month:this.month,
                deptID:'16'
              }
              }).then(res => {this.tableData=res.data})
              this.tableHeader=['部门','工资号','姓名','缺勤类型','缺勤年度','缺勤月份','缺勤开始日期','缺勤结束日期','缺勤天数含休息日','缺勤天数不含休息日','补考勤扣款'
            ,'补工作餐积点扣款','备注']
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
    recheck(){
      this.$axios({
              method: 'GET',
              url: 'http://localhost:5189/api/app/Audit',
              params:{
                year:this.year,
                month:this.month,
                deptID:'16'
              }
              }).then(err=>{console.log(err);})
      this.$message({
        message: '审核签字成功！',
        type: 'success'
      })
      this.sign = '已审核'
    },
    unrecheck(){
      this.$axios({
              params: 'GET',
              url: 'http://localhost:5189/api/app/Unaudit',
              params:{
                year:this.year,
                month:this.month,
                deptID:'16'
              }
              }).then(err=>{console.log(err);})
      this.$message({
        message: '取消审核签字成功！',
        type: 'success'
      })
      this.sign = '未审核'
      },
    close(){
      this.flag = false
      this.sign = ''
    },
    save(){
        this.$message({
          message: '保存成功！',
          type: 'success'
        })
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
.font{
  font-size: 25px;
  color: crimson;
}
.el-select{
  margin-left: 2%;
}
.gap{
  padding-left:10%;
}
</style>