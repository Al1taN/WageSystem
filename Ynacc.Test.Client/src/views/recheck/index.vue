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
        上报年份：<input type="number" v-model="year">
        上报月份： <input type="number" v-model="month">
        <span class="time">
        </span>
      </span>
      <span class="gap"></span>
      <span class="time"><el-button v-loading="loading01" @click="check">查询</el-button></span>
      <span class="time"><el-button v-loading="loading02" @click="recheck">复核签字</el-button></span>
      <span class="time"><el-button v-loading="loading03" @click="unrecheck">取消复核签字</el-button></span>
      <span class="time"><el-button @click="save">保存</el-button></span>
      <span class="time"><el-button @click="close">关闭</el-button></span>
      <span class="time"> <span class="font">{{sign}}</span></span>
      <el-table v-if="flag" :data="tableData" border highlight-current-row style="width: 100%;margin-top:20px;">
        <el-table-column v-for="item of tableHeader" :key="item" :prop="item" :label="item" :formatter="dataFormat"/>
      </el-table>
    </div>
  </template>
  
  <script>
    import { mapGetters } from 'vuex'
    import {GetDdlTime} from '@/api/sysManage'
    import {GetDept} from '@/api/dashboard'
    import {GetData,GetData02,GetData03,GetData04,Recheck,Recheck02,Recheck03,Recheck04,Unrecheck,Unrecheck02,Unrecheck03,Unrecheck04} from '@/api/recheck'
    export default {
      created:async function(){ 
        await GetDept().then(res => {this.$store.dispatch('user/getAchive', res)})
        await GetDdlTime().then(res=>{this.tableData=res})
        this.ddl01=this.tableData[0]["year"]
        this.ddl02 = this.tableData[0]["month"]
        this.ddl03 = this.tableData[0]["dateTime"]
        this.ddl04 = this.tableData[0]["workYear"]
        this.ddl05 = this.tableData[0]["workMonth"]
        this.ddl06 = this.tableData[0]["workDate"]
        this.year = this.ddl01
        this.month = this.ddl02
        }, 
      data() {
      return {
          options: [{
            value: '新版员工考勤',
            label: '新版员工考勤'
            }, {
              value: '新版早夜班加班',
              label: '新版早夜班加班'
            }, {
              value: '劳务制(新版)员工考勤',
              label: '劳务制(新版)员工考勤'
            }, {
              value: '劳务制(新版)早夜班加班',
              label: '劳务制(新版)早夜班加班'           
            }],
          value: '',
          year:'2022',
          month:'11',
          tableData: [{'部门':1,'工资号':2,'姓名':3,'复核':false,'审核':true}],
          tableHeader: ['部门','工资号','姓名','复核','审核'],
          flag: false,
          sign:'',
          ddl01:'',
          ddl02:'',
          ddl03:'',
          ddl04:'',
          ddl05:'',
          ddl06:'',
          loading01:'',
          loading02:'',
          loading03:'',
          UnrecheckFlag:''
        } 
    },
    computed: {
            ...mapGetters([
            'Users'
            ])
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
      async check(){
          this.loading01 = true
          this.flag = false
          this.sign = ''
          var dept = this.Users[0]['dept']
          const r = /^\+?[0-9][0-9]*$/
          switch(this.value){
            case '新版员工考勤':{
              if(1900<= this.year && 1<=this.month&&this.month<=12 && r.test(this.year) && r.test(this.month)){
                this.flag = true
                this.tableData = []
                this.tableHeader = []
                await GetData(this.year,this.month,dept).then(res => {this.tableData=res})
                    this.tableHeader=['部门','工资号','姓名','缺勤类型','缺勤年度','缺勤月份','缺勤开始日期','缺勤结束日期','缺勤天数含休息日','缺勤天数不含休息日','补考勤扣款'
                  ,'补工作餐积点扣款','备注','复核','审核','经办人','复核人','审核人']
                var person = '共计：'
                person = person + this.tableData.length+'人'
                var wday = 0
                var day = 0
                var amount = 0
                var point = 0
                this.tableData.forEach(i =>{
                  wday = wday + i['缺勤天数含休息日']
                  day = day + i['缺勤天数不含休息日']
                  amount = amount + i['补考勤扣款']
                  point = point + i['补工作餐积点扣款']
                })
                this.tableData.push({部门:'合计：',工资号:person,缺勤天数含休息日:wday,缺勤天数不含休息日:day,补考勤扣款:amount,补工作餐积点扣款:point})
              }
              else{
                this.flag = false
                this.$message({
                message: '查询失败！请填写合法且完整的信息！',
                type: 'error'
              })}   
              break
            }
            case '新版早夜班加班':{
              if(1900<= this.year && 1<=this.month&&this.month<=12 && r.test(this.year) && r.test(this.month)){
                this.flag = true
                this.tableData = []
                this.tableHeader = []
                await GetData02(this.year,this.month,dept).then(res => {this.tableData=res})
                    this.tableHeader=['部门','工资号','姓名','发生年份','发生月份','平均加班小时数','周末加班小时数','法定节日加班小时数','补加班工资','值班天数','补值班费'
                  ,'备注','早班次数','中班次数','夜班次数','昼夜次数','补早中夜餐补贴','备注2','复核','审核','经办人','复核人','审核人']
                var person = '共计：'
                person = person + this.tableData.length+'人'
                var sum1 = 0
                var sum2 = 0
                var sum3 = 0
                var sum4 = 0
                var sum5 = 0
                var sum6 = 0
                var sum7 = 0
                var sum8 = 0
                var sum9 = 0
                var sum10 = 0
                var sum11 = 0
                this.tableData.forEach(i =>{
                  sum1 = sum1 + i['平均加班小时数']
                  sum2 = sum2 + i['周末加班小时数']
                  sum3 = sum3 + i['法定节日加班小时数']
                  sum4 = sum4 + i['补加班工资']
                  sum5 = sum5 + i['值班天数']
                  sum6 = sum6 + i['补值班费']
                  sum7 = sum7 + i['早班次数']
                  sum8 = sum8 + i['中班次数']
                  sum9 = sum9 + i['夜班次数']
                  sum10 = sum10 + i['昼夜次数']
                  sum11 = sum11 + i['补早中夜餐补贴']
                })
                this.tableData.push({部门:'合计：',工资号:person,平均加班小时数:sum1,周末加班小时数:sum2,法定节日加班小时数:sum3,补加班工资:sum4,值班天数:sum5
                ,补值班费:sum6,早班次数:sum7,中班次数:sum8,夜班次数:sum9,昼夜次数:sum10,补早中夜餐补贴:sum11})
              }
              else{
                this.flag = false
                this.$message({
                message: '查询失败！请填写合法且完整的信息！',
                type: 'error'
              })}   
              break
            }
            case '劳务制(新版)员工考勤':{
              if(1900<= this.year && 1<=this.month&&this.month<=12 && r.test(this.year) && r.test(this.month)){
                this.flag = true
                this.tableData = []
                this.tableHeader = []
                await GetData03(this.year,this.month,dept).then(res => {this.tableData=res})
                    this.tableHeader=['部门','工资号','姓名','缺勤类型','缺勤年度','缺勤月份','缺勤开始日期','缺勤结束日期','缺勤天数含休息日','缺勤天数不含休息日','补考勤扣款'
                  ,'补工作餐积点扣款','备注','复核','审核','经办人','复核人','审核人']
                var person = '共计：'
                person = person + this.tableData.length+'人'
                var wday = 0
                var day = 0
                var amount = 0
                var point = 0
                this.tableData.forEach(i =>{
                  wday = wday + i['缺勤天数含休息日']
                  day = day + i['缺勤天数不含休息日']
                  amount = amount + i['补考勤扣款']
                  point = point + i['补工作餐积点扣款']
                })
                this.tableData.push({部门:'合计：',工资号:person,缺勤天数含休息日:wday,缺勤天数不含休息日:day,补考勤扣款:amount,补工作餐积点扣款:point})
              }
              else{
                this.flag = false
                this.$message({
                message: '查询失败！请填写合法且完整的信息！',
                type: 'error'
              })}   
              break
            }
            case '劳务制(新版)早夜班加班':{
              if(1900<= this.year && 1<=this.month&&this.month<=12 && r.test(this.year) && r.test(this.month)){
                this.flag = true
                this.tableData = []
                this.tableHeader = []
                await GetData04(this.year,this.month,dept).then(res => {this.tableData=res})
                    this.tableHeader=['部门','工资号','姓名','发生年份','发生月份','平均加班小时数','周末加班小时数','法定节日加班小时数','补加班工资','值班天数','补值班费'
                  ,'备注','早班次数','中班次数','夜班次数','昼夜次数','补早中夜餐补贴','备注2','复核','审核','经办人','复核人','审核人']
                  var person = '共计：'
                  person = person + this.tableData.length+'人'
                  var sum1 = 0
                  var sum2 = 0
                  var sum3 = 0
                  var sum4 = 0
                  var sum5 = 0
                  var sum6 = 0
                  var sum7 = 0
                  var sum8 = 0
                  var sum9 = 0
                  var sum10 = 0
                  var sum11 = 0
                  this.tableData.forEach(i =>{
                    sum1 = sum1 + i['平均加班小时数']
                    sum2 = sum2 + i['周末加班小时数']
                    sum3 = sum3 + i['法定节日加班小时数']
                    sum4 = sum4 + i['补加班工资']
                    sum5 = sum5 + i['值班天数']
                    sum6 = sum6 + i['补值班费']
                    sum7 = sum7 + i['早班次数']
                    sum8 = sum8 + i['中班次数']
                    sum9 = sum9 + i['夜班次数']
                    sum10 = sum10 + i['昼夜次数']
                    sum11 = sum11 + i['补早中夜餐补贴']
                  })
                  this.tableData.push({部门:'合计：',工资号:person,平均加班小时数:sum1,周末加班小时数:sum2,法定节日加班小时数:sum3,补加班工资:sum4,值班天数:sum5
                  ,补值班费:sum6,早班次数:sum7,中班次数:sum8,夜班次数:sum9,昼夜次数:sum10,补早中夜餐补贴:sum11})
              }
              else{
                this.flag = false
                this.$message({
                message: '查询失败！请填写合法且完整的信息！',
                type: 'error'
              })}   
              break
            }
          }
          this.loading01 = false 
        },
      async recheck(){
        this.loading02 = true
        var dept = this.Users[0]['dept']
        var pid = this.Users[0]['pid']
        switch(this.value){
            case '新版员工考勤':{
              if(this.year != this.ddl01 || this.month != this.ddl02)
              {
                this.$message({
                message: '只能复核当月的数据！',
                type: 'error'
              })
              }
              else{
                var item = {}
                item["Pid"] = pid
                item["Deptid"] = dept
                item["Year"] = this.year
                item["Month"] = this.month
                await Recheck(item).then(err=>{console.log(err);})
                this.$message({
                  message: '复核签字成功！',
                  type: 'success'
                })
                this.sign = '已复核'
                }
                break
            }
            case '新版早夜班加班':{
              if(this.year != this.ddl01 || this.month != this.ddl02)
              {
                this.$message({
                message: '只能复核当月的数据！',
                type: 'error'
              })
              }
              else{
                var item = {}
                item["Pid"] = pid
                item["Deptid"] = dept
                item["Year"] = this.year
                item["Month"] = this.month
                await Recheck02(item).then(err=>{console.log(err);})
              this.$message({
                message: '复核签字成功！',
                type: 'success'
              })
              this.sign = '已复核'
              }
              break
            }
            case '劳务制(新版)员工考勤':{
              if(this.year != this.ddl04 || this.month != this.ddl05)
              {
                this.$message({
                message: '只能复核当月的数据！',
                type: 'error'
              })
              }
              else{
                var item = {}
                item["Pid"] = pid
                item["Deptid"] = dept
                item["Year"] = this.year
                item["Month"] = this.month
                await Recheck03(item).then(err=>{console.log(err);})
              this.$message({
                message: '复核签字成功！',
                type: 'success'
              })
              this.sign = '已复核'
              }
              break
            }
            case '劳务制(新版)早夜班加班':{
              if(this.year != this.ddl04 || this.month != this.ddl05)
              {
                this.$message({
                message: '只能复核当月的数据！',
                type: 'error'
              })
              }
              else{
                var item = {}
                item["Pid"] = pid
                item["Deptid"] = dept
                item["Year"] = this.year
                item["Month"] = this.month
                await Recheck04(item).then(err=>{console.log(err);})
              this.$message({
                message: '复核签字成功！',
                type: 'success'
              })
              this.sign = '已复核'
              }
              break
            }
          }
          this.loading02 = false
          this.check()
      },
      async unrecheck(){
        this.loading03 = true
        var dept = this.Users[0]['dept']
        switch(this.value){
            case '新版员工考勤':{
              if(this.year != this.ddl01 || this.month != this.ddl02)
              {
                this.$message({
                message: '只能取消复核当月的数据！',
                type: 'error'
              })
              }
              else{
                var item = {}
                item["Deptid"] = dept
                item["Year"] = this.year
                item["Month"] = this.month
                await Unrecheck(item).then(res=>{this.UnrecheckFlag = res})
              if(this.UnrecheckFlag == -1){this.$message({
                message: '已审核的数据无法取消复核！',
                type: 'error'
              })}
              else{
              this.$message({
                message: '取消复核签字成功！',
                type: 'success'
              })
              this.sign = '未复核'
            }
              }
              break
            }
            case '新版早夜班加班':{
              if(this.year != this.ddl01 || this.month != this.ddl02)
              {
                this.$message({
                message: '只能取消复核当月的数据！',
                type: 'error'
              })
              }
              else{
                var item = {}
                item["Deptid"] = dept
                item["Year"] = this.year
                item["Month"] = this.month
                await Unrecheck02(item).then(res=>{this.UnrecheckFlag = res})
              if(this.UnrecheckFlag == -1){this.$message({
                message: '已审核的数据无法取消复核！',
                type: 'error'
              })}
              else{
              this.$message({
                message: '取消复核签字成功！',
                type: 'success'
              })
              this.sign = '未复核'
              }
            }
              break
            }
            case '劳务制(新版)员工考勤':{
              if(this.year != this.ddl04 || this.month != this.ddl05)
              {
                this.$message({
                message: '只能取消复核当月的数据！',
                type: 'error'
              })
              }
              else{
                var item = {}
                item["Deptid"] = dept
                item["Year"] = this.year
                item["Month"] = this.month
                await Unrecheck03(item).then(res=>{this.UnrecheckFlag = res})
              if(this.UnrecheckFlag == -1){this.$message({
                message: '已审核的数据无法取消复核！',
                type: 'error'
              })}
              else{
              this.$message({
                message: '取消复核签字成功！',
                type: 'success'
              })
              this.sign = '未复核'
              }
            }
              break
            }
            case '劳务制(新版)早夜班加班':{
              if(this.year != this.ddl04 || this.month != this.ddl05)
              {
                this.$message({
                message: '只能取消复核当月的数据！',
                type: 'error'
              })
              }
              else{
                var item = {}
                item["Deptid"] = dept
                item["Year"] = this.year
                item["Month"] = this.month
                await Unrecheck04(item).then(res=>{this.UnrecheckFlag = res})
              if(this.UnrecheckFlag == -1){this.$message({
                message: '已审核的数据无法取消复核！',
                type: 'error'
              })}
              else{
              this.$message({
                message: '取消复核签字成功！',
                type: 'success'
              })
              this.sign = '未复核'
              }
            }
              break
            }
          }
          this.loading03 = false
          this.check()
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