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
      <span v-if="this.roles[0] == 'Wage_Admin'">
        <el-select v-model="selectdept" placeholder="选择查询部门">
                <el-option
                v-for="item in depts"
                :key="item.value"
                :label="item.label"
                :value="item.value">
                </el-option>
        </el-select>
      </span>
      <span class="time">
        时间段： 从<input type="date" v-model="begin">
        到 <input type="date" v-model="end">
        <span class="time">
        <el-checkbox v-model="checked01">复核</el-checkbox>
        <el-checkbox v-model="checked02">审核</el-checkbox>
        </span>
      </span>
      <span class="gap"></span>
      <span class="time"><el-button @click="check" :loading = "loading">查询</el-button></span>
      <span class="time"><el-button :loading="downloadLoading" @click="handleDownload">导出EXCEL</el-button></span>
      <span class="time"><el-button @click="close">关闭</el-button></span>
      <el-table v-if="flag" :data="tableData" border highlight-current-row style="width: 100%;margin-top:20px;">
        <el-table-column v-for="item of tableHeader" :key="item" :prop="item" :label="item" :formatter="dataFormat"/>
      </el-table>
    </div>
  </template>
  
  <script>
    import { mapGetters } from 'vuex'
    import {GetDept} from '@/api/dashboard'
    import { parseTime } from '@/utils'
    import {GetNewAttend,GetOvertime,GetWorkNewAttend,GetWorkOvertime} from '@/api/dataSQL'
    export default {
      created:async function(){
        await GetDept().then(res => {this.$store.dispatch('user/getAchive', res)})
        var now = new Date();
        var nowmonth = now.getMonth() +1
        if(nowmonth < 10){nowmonth = '0' + nowmonth}
        var nowtime = now.getFullYear() + '-' + nowmonth + '-' + now.getDate()
        var nowtime1 = now.getFullYear() + '-' + nowmonth + '-01'
        this.begin = nowtime1
        this.end = nowtime
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
        depts: [ {
                  value: '00',
                  label: '所有部门:00'
                  },
                  {
                  value: '01',
                  label: '办公室:01'
                  }, {
                  value: '02',
                  label: '规划发展部:02'
                  }, {
                  value: '03',
                  label: '人力资源部:03'
                  }, {
                  value: '04',
                  label: '财务会计部:04'
                  }, {
                  value: '05',
                  label: '审计部:05'
                  }, {
                  value: '06',
                  label: '保卫部:06'
                  }, {
                  value: '07',
                  label: '运行管理部:07'
                  }, {
                  value: '08',
                  label: '服务与质量管理部:08'
                  }, {
                  value: '09',
                  label: '安全监察部:09'
                  }, {
                  value: '10',
                  label: '飞行技术管理部:10'
                  }, {
                  value: '11',
                  label: '综合管理部:11'
                  }, {
                  value: '12',
                  label: '运行控制部:12'
                  }, {
                  value: '13',
                  label: '客舱服务部:13'
                  }, {
                  value: '14',
                  label: '地面服务部:14'
                  }, {
                  value: '15',
                  label: '市场销售部:15'
                  }, {
                  value: '16',
                  label: '飞行部:16'
                  }, {
                  value: '17',
                  label: '货运部:17'
                  }, {
                  value: '18',
                  label: '昆明维修基地:18'
                  }, {
                  value: '19',
                  label: '党委工作部:19'
                  }, {
                  value: '20',
                  label: '离退休管理部:20'
                  }, {
                  value: '21',
                  label: '群团工作部:21'
                  }, {
                  value: '22',
                  label: '信息管理部:22'
                  }, {
                  value: '23',
                  label: '基本建设管理部:23'
                  }, {
                  value: '24',
                  label: '机场扩建协调指挥部:24'
                  }, {
                  value: '25',
                  label: '机关党委:25'
                  }, {
                  value: '26',
                  label: '纪委办公室:26'  
                  }, {
                  value: '27',
                  label: '团委:27'
                  }, {
                  value: '28',
                  label: '股份公司:28'
                  }, {
                  value: '29',
                  label: '北京分公司:29'
                  }, {
                  value: '30',
                  label: '东美公司:30'
                  }, {
                  value: '31',
                  label: '成都基地:31'
                  }, {
                  value: '32',
                  label: '丽江基地:32'
                  }, {
                  value: '33',
                  label: '局方飞行人员:33'
                  }, {
                  value: '34',
                  label: '驻国外:34'
                  }, {
                  value: '35',
                  label: '空警:35'
                  }, {
                  value: '36',
                  label: '内退:36'
                  }, {
                  value: '37',
                  label: '代垫工资:37'
                  }, {
                  value: '38',
                  label: '部门:38' 
                  }, {
                  value: '39',
                  label: '采购部:39'
                  }, {
                  value: '40',
                  label: '四川分公司:40'
                  }, {
                  value: '41',
                  label: '机务工程部:41'
                  }, {
                  value: '42',
                  label: '法律合规部:42'
                  }, {
                  value: '43',
                  label: '工会女工委:43'        
              }],
          selectdept: '',
          value: '',
          begin:'2022-10-01',
          end:'2022-11-17',
          tableData: [{'部门':1,'工资号':2,'姓名':3,'复核':4,'审核':5}],
          tableHeader: ['部门','工资号','姓名','复核','审核'],
          downloadLoading: false,
          autoWidth: true,
          bookType: 'xlsx',
          flag: false,
          checked01:false,
          checked02:false,
          loading:false
        } 
    },
    computed: {
            ...mapGetters([
            'Users','roles'
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
        this.loading = true
        if(this.value && this.begin && this.end && this.end >= this.begin){
          this.flag = true
          this.tableData = []
          this.tableHeader = []
          var dept = this.Users[0]['dept']
          //判断是调用哪张表
          switch(this.value){
            case '新版员工考勤':{
              //获取数据，条件：时间，复核，审核
              var item = {}
              item["checked01"] = this.checked01
              item["checked02"] = this.checked02
              item["begin"] = this.begin
              item["end"] = this.end
              item["dept"] = dept
              if(this.roles[0] == 'Wage_Admin'){
                item["dept"] = this.selectdept
              }
              await GetNewAttend(item).then(res => {this.tableData=res})
              this.tableHeader=['部门','工资号','姓名','缺勤类型','缺勤年度','缺勤月份','缺勤开始日期','缺勤结束日期','缺勤天数含休息日','缺勤天数不含休息日','补考勤扣款'
            ,'补工作餐积点扣款','备注','复核','审核']
            //this.tableData.push({部门:'合计：',工资号:'共计：7人',缺勤天数不含休息日:'321',补考勤扣款:'321',补工作餐积点扣款:'321'})
            var person = '共计：'
            person = person + this.tableData.length+'人'
            var day = 0
            var amount = 0
            var point = 0
            this.tableData.forEach(i =>{
              day = day + i['缺勤天数不含休息日']
              amount = amount + i['补考勤扣款']
              point = point + i['补工作餐积点扣款']
            })
            this.tableData.push({部门:'合计：',工资号:person,缺勤天数不含休息日:day,补考勤扣款:amount,补工作餐积点扣款:point})
            break
            }
            //repeat method
            case '新版早夜班加班':{
              //获取数据，条件：时间，复核，审核
              var item = {}
              item["checked01"] = this.checked01
              item["checked02"] = this.checked02
              item["begin"] = this.begin
              item["end"] = this.end
              item["dept"] = dept
              if(this.roles[0] == 'Wage_Admin'){
                item["dept"] = this.selectdept
              }
              await GetOvertime(item).then(res => {this.tableData=res})
              this.tableHeader=['部门','工资号','姓名','导入年份','导入月份','发生年份','发生月份','平均加班小时数','周末加班小时数','法定节日加班小时数','补加班工资','值班天数','补值班费'
                  ,'备注','早班次数','中班次数','夜班次数','昼夜次数','补早中夜餐补贴','备注2','复核','审核']
              //this.tableData.push({部门:'合计：',工资号:'共计：7人',缺勤天数不含休息日:'321',补考勤扣款:'321',补工作餐积点扣款:'321'})
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
              break
            }
            case '劳务制新版员工考勤':{
              //获取数据，条件：时间，复核，审核
              var item = {}
              item["checked01"] = this.checked01
              item["checked02"] = this.checked02
              item["begin"] = this.begin
              item["end"] = this.end
              item["dept"] = dept
              if(this.roles[0] == 'Wage_Admin'){
                item["dept"] = this.selectdept
              }
              await GetWorkNewAttend(item).then(res => {this.tableData=res})
              this.tableHeader=['部门','工资号','姓名','缺勤类型','缺勤年度','缺勤月份','缺勤开始日期','缺勤结束日期','缺勤天数含休息日','缺勤天数不含休息日','补考勤扣款'
            ,'补工作餐积点扣款','备注','复核','审核']
            //this.tableData.push({部门:'合计：',工资号:'共计：7人',缺勤天数不含休息日:'321',补考勤扣款:'321',补工作餐积点扣款:'321'})
            var person = '共计：'
            person = person + this.tableData.length+'人'
            var day = 0
            var amount = 0
            var point = 0
            this.tableData.forEach(i =>{
              day = day + i['缺勤天数不含休息日']
              amount = amount + i['补考勤扣款']
              point = point + i['补工作餐积点扣款']
            })
            this.tableData.push({部门:'合计：',工资号:person,缺勤天数不含休息日:day,补考勤扣款:amount,补工作餐积点扣款:point})
            break
            }
            case '劳务制新版早夜班加班':{
              //获取数据，条件：时间，复核，审核
              var item = {}
              item["checked01"] = this.checked01
              item["checked02"] = this.checked02
              item["begin"] = this.begin
              item["end"] = this.end
              item["dept"] = dept
              if(this.roles[0] == 'Wage_Admin'){
                item["dept"] = this.selectdept
              }
              await GetWorkOvertime(item).then(res => {this.tableData=res})
              this.tableHeader=['部门','工资号','姓名','导入年份','导入月份','发生年份','发生月份','平均加班小时数','周末加班小时数','法定节日加班小时数','补加班工资','值班天数','补值班费'
                  ,'备注','早班次数','中班次数','夜班次数','昼夜次数','补早中夜餐补贴','备注2','复核','审核']
              //this.tableData.push({部门:'合计：',工资号:'共计：7人',缺勤天数不含休息日:'321',补考勤扣款:'321',补工作餐积点扣款:'321'})
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
              break
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
        this.loading = false    
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
          excel.export_json_to_excel({
            header: this.tableHeader,
            data,
            filename: this.value,
            autoWidth: this.autoWidth,
            bookType: this.bookType
          })
          this.$message({
            message: '数据导出成功！',
            type: 'success'
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