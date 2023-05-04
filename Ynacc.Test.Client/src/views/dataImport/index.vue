<template>
    <div>
      <el-select class="select1" v-model="value" placeholder="请选择导入数据类别">
        <el-option
          v-for="item in options"
          :key="item.value"
          :label="item.label"
          :value="item.value">
        </el-option>
      </el-select>
      <el-button v-loading="loading" class="button1" @click="save">保存</el-button>
      <span class="time"></span><span class="time"></span>
      <el-button class="button1" @click="close">关闭</el-button>
      <div class="app-container">
        <upload-excel-component :on-success="handleSuccess" :before-upload="beforeUpload" />
        <span class="form01">
          <a href="/员工考勤格式.xlsx">点击下载员工考勤模板</a>
          <span class="form02"></span>
          <a href="/早夜班加班格式.xlsx">点击下载早夜班加班模板</a>
        </span>
        <el-table v-if="flag1" :data="tableData" border highlight-current-row style="width: 100%;margin-top:20px;">
          <el-table-column v-for="item of tableHeader" :key="item" :prop="item" :label="item" />
        </el-table>
        <el-divider></el-divider>
        <span class="time">
          <span class="gap"></span>
          <el-select v-model="value2" placeholder="请选择数据查询类别">
            <el-option
              v-for="item in options2"
              :key="item.value"
              :label="item.label"
              :value="item.value">
            </el-option>
          </el-select>
          <span class="time"></span>
          上报年份：<input type="number" v-model="year">
          上报月份： <input type="number" v-model="month">
          <span class="gap"></span>
          <el-button @click="check" v-loading="loading02">查询</el-button>
          <span class="time"></span>
          <el-button @click="close2">关闭</el-button>
          <span class="time">
          </span>
        </span>
        <el-table v-if="flag2" :data="tableData2" border highlight-current-row style="width: 100%;margin-top:20px;">
          <el-table-column v-for="item of tableHeader2" :key="item" :prop="item" :label="item" :formatter="dataFormat"/>
        </el-table>
      </div>
    </div>
  </template>
  
  <script>
  import { mapGetters } from 'vuex'
  import UploadExcelComponent from '@/components/UploadExcel/index.vue'
  import {GetDdlTime} from '@/api/sysManage'
  import {GetDept} from '@/api/dashboard'
  import {ImpData01,ImpData02,ImpData03,ImpData04} from '@/api/dataImport'
  import {GetData,GetData02,GetData03,GetData04} from '@/api/recheck'
  export default {
    name: 'dataImport',
    components: { UploadExcelComponent },
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
        options2: [{
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
          value2:'',
          tableData: [],
          tableHeader: [],
          flag: '',
          legal:'',
          ddl01:'',
          ddl02:'',
          ddl03:'',
          ddl04:'',
          ddl05:'',
          ddl06:'',
          flag1: false,
          flag2: false,
          year:'2022',
          month:'11',
          tableData2: [],
          tableHeader2: [],
          latetype: ["病假","哺乳假","产假","产前假","父母护理假","工伤假","怀孕假","婚假","计划生育假","经期假","旷工","留用查看","年休假","陪产假","全勤","人工流产假","丧假",
          "事假","探亲假","停职检查","刑事强制措施","行政拘留","育儿假","育婴陪护假","重大疾病","自然流产假"],
          loading: false,
          loading02:false
        } 
  },
  computed: {
            ...mapGetters([
            'Users'
            ])
        },
    methods: {
      beforeUpload(file) {
        const isLt1M = file.size / 1024 / 1024 < 1

        if (isLt1M) {
          return true
        }

        this.$message({
          message: '文件大小不能超过1M.',
          type: 'warning'
        })
        return false
      },
      handleSuccess({ results, header }) {
        this.flag1= true
        this.tableData = results
        this.tableHeader = header
      },
      async save(){
        var pid = this.Users[0]['pid']
        var now = new Date();
        var nowmonth = now.getMonth() +1
        var nowtime = now.getFullYear() + '-' + nowmonth + '-' + now.getDate()
        var time001 = this.ddl01 + '-' + this.ddl02 + '-' + this.ddl03
        var time002 = this.ddl04 + '-' + this.ddl05 + '-' + this.ddl06
        switch(this.value){
          case '新版员工考勤':{
            this.legal = 1
            for(let item of this.tableData)
            {
              if(item["Year"] != this.ddl01 || item["Month"] != this.ddl02)
              {
                this.legal = 0
                this.$message({
                  message: '只能导入当前年份月份的数据！',
                  type: 'error'
                  })
                break
              }
              //是否超过截止日期 例如：3月只能导入三月的数据
              if((new Date(nowtime.replace(/\-/g, "\/"))) > (new Date(time001.replace(/\-/g, "\/")))){
                this.legal = 0
                this.$message({
                  message: '已超出截止日期！',
                  type: 'error'
                  })
                break
              }
              if(item["Deptid"] != this.Users[0]['dept']){
                this.legal = 0
                this.$message({
                  message: '只能导入本部门人员的数据！',
                  type: 'error'
                  })
                break
              }
              if(this.latetype.indexOf(item["Latetype"]) == -1){
                this.legal = 0
                this.$message({
                  message: '导入数据中休假类型有误！',
                  type: 'error'
                  })
                break
              }
            }
            if(this.legal == 1){
              this.loading = true
              for(let item of this.tableData)
              {
                item["Maker"] = pid
              }
              await ImpData01(this.tableData).then(res=>{
                    this.flag=res
                  })
              if(this.flag == "200")
                {
                  this.$message({
                            message: '导入数据成功！',
                            type: 'success'
                          })
                }
              else{
                this.$message({
                    message: this.flag,
                    type: 'error'
                    })
              } 
            }
            this.loading = false
            break
            }
          case '新版早夜班加班':{
            this.legal = 1
            for(let item of this.tableData)
            {
              if(item["Year"] != this.ddl01 || item["Month"] != this.ddl02)
              {
                this.legal = 0
                this.$message({
                  message: '只能导入当前年份月份的数据！',
                  type: 'error'
                  })
                break
              }
              //是否超过截止日期 
              if((new Date(nowtime.replace(/\-/g, "\/"))) > (new Date(time001.replace(/\-/g, "\/")))){
                this.legal = 0
                this.$message({
                  message: '已超出截止日期！',
                  type: 'error'
                  })
                break
              }
              if(item["Deptid"] != this.Users[0]['dept']){
                this.legal = 0
                this.$message({
                  message: '只能导入本部门人员的数据！',
                  type: 'error'
                  })
                break
              }
              if(this.latetype.indexOf(item["Latetype"]) == -1){
                this.legal = 0
                this.$message({
                  message: '导入数据中休假类型有误！',
                  type: 'error'
                  })
                break
              }
            }
            if(this.legal == 1){
              this.loading = true
              for(let item of this.tableData)
              {
                item["Maker"] = pid
              }
              await ImpData02(this.tableData).then(res=>{
                    this.flag=res
                  })
              if(this.flag == "200")
                {
                  this.$message({
                            message: '导入数据成功！',
                            type: 'success'
                          })
                }
              else{
                this.$message({
                    message: this.flag,
                    type: 'error'
                    })
              } 
            }
            this.loading = false
            break
            }
          case '劳务制(新版)员工考勤':{
            this.legal = 1
            for(let item of this.tableData)
            {
              if(item["Year"] != this.ddl04 || item["Month"] != this.ddl05)
              {
                this.legal = 0
                this.$message({
                  message: '只能导入当前年份月份的数据！',
                  type: 'error'
                  })
                break
              }
              //是否超过截止日期
              if((new Date(nowtime.replace(/\-/g, "\/"))) > (new Date(time002.replace(/\-/g, "\/")))){
                this.legal = 0
                this.$message({
                  message: '已超出截止日期！',
                  type: 'error'
                  })
                break
              }
              if(item["Deptid"] != this.Users[0]['dept']){
                this.legal = 0
                this.$message({
                  message: '只能导入本部门人员的数据！',
                  type: 'error'
                  })
                break
              }
              if(this.latetype.indexOf(item["Latetype"]) == -1){
                this.legal = 0
                this.$message({
                  message: '导入数据中休假类型有误！',
                  type: 'error'
                  })
                break
              }
            }
            if(this.legal == 1){
              this.loading = true
              for(let item of this.tableData)
              {
                item["Maker"] = pid
              }
              await ImpData03(this.tableData).then(res=>{
                    this.flag=res
                  })
              if(this.flag == "200")
                {
                  this.$message({
                            message: '导入数据成功！',
                            type: 'success'
                          })
                }
              else{
                this.$message({
                    message: this.flag,
                    type: 'error'
                    })
              } 
            }
            this.loading = false
            break
          }
          case '劳务制(新版)早夜班加班':{
            this.legal = 1
            for(let item of this.tableData)
            {

              if(item["Year"] != this.ddl04 || item["Month"] != this.ddl05)
              {
                this.legal = 0
                this.$message({
                  message: '只能导入当前年份月份的数据！',
                  type: 'error'
                  })
                break
              }
              //是否超过截止日期
              if((new Date(nowtime.replace(/\-/g, "\/"))) > (new Date(time002.replace(/\-/g, "\/")))){
                this.legal = 0
                this.$message({
                  message: '已超出截止日期！',
                  type: 'error'
                  })
                break
              }
              if(item["Deptid"] != this.Users[0]['dept']){
                this.legal = 0
                this.$message({
                  message: '只能导入本部门人员的数据！',
                  type: 'error'
                  })
                break
              }
              if(this.latetype.indexOf(item["Latetype"]) == -1){
                this.legal = 0
                this.$message({
                  message: '导入数据中休假类型有误！',
                  type: 'error'
                  })
                break
              }
            }
            if(this.legal == 1){
              this.loading = true
              for(let item of this.tableData)
              {
                item["Maker"] = pid
              }
              await ImpData04(this.tableData).then(res=>{
                    this.flag=res
                  })
              if(this.flag == "200")
                {
                  this.$message({
                            message: '导入数据成功！',
                            type: 'success'
                          })
                }
              else{
                this.$message({
                    message: this.flag,
                    type: 'error'
                    })
              } 
            }
            this.loading = false
            break
          }
        }
             
      },
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
          this.loading02 = true
          this.flag2 = false
          var dept = this.Users[0]['dept']
          const r = /^\+?[0-9][0-9]*$/
          switch(this.value2){
            case '新版员工考勤':{
              if(1900<= this.year && 1<=this.month&&this.month<=12 && r.test(this.year) && r.test(this.month) ){
                this.flag2 = true
                this.tableData2 = []
                this.tableHeader2 = []
                await GetData(this.year,this.month,dept).then(res => {this.tableData2=res})
                    this.tableHeader2=['部门','工资号','姓名','缺勤类型','缺勤年度','缺勤月份','缺勤开始日期','缺勤结束日期','缺勤天数含休息日','缺勤天数不含休息日','补考勤扣款'
                  ,'补工作餐积点扣款','备注','复核','审核','经办人','复核人','审核人']
                this.$message({
                  message: '查询成功！',
                  type: 'success'
                })
              }
              else{
                this.flag2 = false
                this.$message({
                message: '查询失败！请填写合法且完整的信息！',
                type: 'error'
              })} 
              break
            }
            case '新版早夜班加班':{
              if(1900<= this.year && 1<=this.month&&this.month<=12 && r.test(this.year) && r.test(this.month) ){
                this.flag2 = true
                this.tableData2 = []
                this.tableHeader2 = []
                await GetData02(this.year,this.month,dept).then(res => {this.tableData2=res})
                    this.tableHeader2=['部门','工资号','姓名','发生年份','发生月份','平均加班小时数','周末加班小时数','法定节日加班小时数','补加班工资','值班天数','补值班费'
                  ,'备注','早班次数','中班次数','夜班次数','昼夜次数','补早中夜餐补贴','备注2','复核','审核','经办人','复核人','审核人']
                this.$message({
                  message: '查询成功！',
                  type: 'success'
                })
              }
              else{
                this.flag2 = false
                this.$message({
                message: '查询失败！请填写合法且完整的信息！',
                type: 'error'
              })}   
              break
            }
            case '劳务制(新版)员工考勤':{
              if(1900<= this.year && 1<=this.month&&this.month<=12 && r.test(this.year) && r.test(this.month) ){
                this.flag2 = true
                this.tableData2 = []
                this.tableHeader2 = []
                await GetData03(this.year,this.month,dept).then(res => {this.tableData2=res})
                    this.tableHeader2=['部门','工资号','姓名','缺勤类型','缺勤年度','缺勤月份','缺勤开始日期','缺勤结束日期','缺勤天数含休息日','缺勤天数不含休息日','补考勤扣款'
                  ,'补工作餐积点扣款','备注','复核','审核','经办人','复核人','审核人']
                this.$message({
                  message: '查询成功！',
                  type: 'success'
                })
              }
              else{
                this.flag2 = false
                this.$message({
                message: '查询失败！请填写合法且完整的信息！',
                type: 'error'
              })} 
              break
            }
            case '劳务制(新版)早夜班加班':{
              if(1900<= this.year && 1<=this.month&&this.month<=12 && r.test(this.year) && r.test(this.month) ){
                this.flag2 = true
                this.tableData2 = []
                this.tableHeader2 = []
                await GetData04(this.year,this.month,dept).then(res => {this.tableData2=res})
                    this.tableHeader2=['部门','工资号','姓名','发生年份','发生月份','平均加班小时数','周末加班小时数','法定节日加班小时数','补加班工资','值班天数','补值班费'
                  ,'备注','早班次数','中班次数','夜班次数','昼夜次数','补早中夜餐补贴','备注2','复核','审核','经办人','复核人','审核人']
                this.$message({
                  message: '查询成功！',
                  type: 'success'
                })
              }
              else{
                this.flag2 = false
                this.$message({
                message: '查询失败！请填写合法且完整的信息！',
                type: 'error'
              })}   
              break
            }
            
          }
          this.loading02 = false
        },
        close(){
        this.flag1 = false
        this.tableData=[]
        this.tableHeader=[]
        },
        close2(){
        this.flag2 = false
        }

    }
  }
  </script>
  
  <style lang="css" scoped>
  .select1{
    margin-left: 20%;
    margin-top: 2%;
    position: fixed;
  }
  .button1{
    margin-left: 20%;
    margin-top: 5%;
    position: fixed;
  }
  a{
    text-decoration: underline;
  }
  a:link{
    color: blue;
  }
  a:visited{
    color:darkslateblue
  }
  .form01{
    margin-left: 70%;
  }
  .form02{
    margin-left: 3%;
  }
  .time{
    padding-left:2%;
  }
  .gap{
    padding-left:10%;
  }
  </style>